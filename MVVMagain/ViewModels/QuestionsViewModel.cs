using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MVVMagain.Models;
using Microsoft.Win32;
using System.IO;
using System.Windows.Input;
using MVVMagain.Infrastructure;
using Microsoft.Office.Interop.Word;
using System.Windows;
using System.Threading;

namespace MVVMagain.ViewModels
{
    public class QuestionsViewModel : ViewModelBase
    {
        private readonly Questions questions;
        private ICommand openCommand;

        public QuestionsViewModel(Questions questions)
        {
            if (questions == null)
            {
                throw new NullReferenceException("questions");
            }
            this.questions = questions;
        }

        public string Text
        {
            get
            {
                return this.questions.Text;
            }
            set
            {
                this.questions.Text = value;
                OnPropertyChanged("Text");
            }
        }

        public ICommand OpenCommand
        {
            get
            {
                if (this.openCommand == null)
                {
                    this.openCommand = new RelayCommand(() => this.Open());
                }

                return this.openCommand;
            }
        }

        private void Open()
        {
            OpenFileDialog dialog = new OpenFileDialog();
            dialog.Filter = " Word documents(*.doc,*.docx)| *.doc;*.docx | text files (*.txt) | *.txt ";
            if (dialog.ShowDialog() == true)
            {
                if (dialog.FileName.EndsWith("txt"))
                {
                    OpenFile(dialog.FileName);
                }
                else
                {
                    OpenWordDocument(dialog.FileName);
                }
            }
        }

        private void OpenFile(string filepath)
        {
            Text = File.ReadAllText(filepath,Encoding.GetEncoding(1251));
        }

        private void OpenWordDocument(string filepath)
        {

            Object missing = Type.Missing;

            var wordApp = new Microsoft.Office.Interop.Word.Application();

            Object name = filepath;
            Object confirmConversions = false;

            _Document doc = wordApp.Documents.Open(
                ref name, ref confirmConversions,
                ref missing, ref missing, ref missing, ref missing,
                ref missing, ref missing, ref missing, ref missing,
                ref missing, ref missing, ref missing, ref missing,
                ref missing, ref missing
            );
            doc.ActiveWindow.Selection.WholeStory();
            doc.ActiveWindow.Selection.Copy();
            IDataObject data = Clipboard.GetDataObject();
            Text = data.GetData(DataFormats.Text).ToString();
            doc.Close();
           ((_Application)wordApp).Quit(ref missing, ref missing, ref missing);
        }
    }
}
