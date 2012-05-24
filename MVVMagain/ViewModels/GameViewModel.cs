using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MVVMagain.Models;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows.Data;
using System.Windows.Input;
using MVVMagain.Commands;
using System.Windows;

namespace MVVMagain.ViewModels
{
    public class GameViewModel : ViewModelBase
    {
        private readonly Game currentGame;
        private readonly ObservableCollection<PlayerViewModel> players;
        private readonly ICollectionView collectionView;
        private ICommand removeCommand;

        public GameViewModel(Game game)
        {
            this.currentGame = game;
            this.players = new ObservableCollection<PlayerViewModel>();

            foreach (Player p in this.currentGame.Players)
            {
                this.players.Add(new PlayerViewModel(p));
            }

            this.collectionView = CollectionViewSource.GetDefaultView(this.players);
            if (this.collectionView == null)
                throw new NullReferenceException("collectionView");

            this.collectionView.CurrentChanged += new EventHandler(this.OnCollectionViewCurrentChanged);
        }

        #region Properties
        public ObservableCollection<PlayerViewModel> Players
        {
            get { return this.players; }
        }

        public PlayerViewModel SelectedPlayer
        {
            get { return this.collectionView.CurrentItem as PlayerViewModel; }
        }

        public int CurrentQuestion
        {
            get
            {
                return this.currentGame.CurrentQuestion;
            }
            set
            {
                this.currentGame.CurrentQuestion = value;
                OnPropertyChanged("CurrentQuestion");
            }
        }

        public int CurrentCategory
        {
            get
            {
                return this.currentGame.CurrentCategory;
            }
            set
            {
                this.currentGame.CurrentCategory = value;
                OnPropertyChanged("CurrentCategory");
            }
        }
        #endregion

        #region RemoveCommand
        public ICommand RemoveCommand
        {
            get
            {
                if (this.removeCommand == null)
                    this.removeCommand = new RelayCommand(() => this.RemovePlayer(), () => this.CanRemovePlayer());

                return this.removeCommand;
            }
        }

        private bool CanRemovePlayer()
        {
            return this.SelectedPlayer != null;
        }

        private void RemovePlayer()
        {
            this.currentGame.RemovePlayer(this.SelectedPlayer.Player);
            this.players.Remove(this.SelectedPlayer);
        }
        #endregion

        private void OnCollectionViewCurrentChanged(object sender, EventArgs e)
        {
            OnPropertyChanged("SelectedPlayer");
        }
    }
}
