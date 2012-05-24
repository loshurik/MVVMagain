using System;
using MVVMagain.Models;
using MVVMagain.Commands;
using System.Windows.Input;

namespace MVVMagain.ViewModels
{
    public class PlayerViewModel:ViewModelBase
    {
        private readonly Player player;
        private ICommand rightAnswerCommand;

        public PlayerViewModel(Player player)
        {
            if (player == null)
            {
                throw new NullReferenceException("player");
            }
            this.player = player;
        }

        public ICommand RightAnswerCommand
        {
            get 
            {
                if (this.rightAnswerCommand == null)
                    this.rightAnswerCommand = new RelayCommand( ()=>this.RightAnswer());
                return this.rightAnswerCommand;
            }
            
        }

        private void RightAnswer()
        {
            this.Score += 10;
        }

        #region Properties
        public Player Player
        {
            get
            {
                return this.player;
            }
        }
        
        public string Name
        {
            get
            {
                return this.player.Name;
            }
            set
            {
                this.player.Name = value;
                OnPropertyChanged("Name");
            }
        }

        public int Score
        {
            get
            {
                return this.player.Score;
            }
            set
            {
                this.player.Score = value;
                OnPropertyChanged("Score");
            }
        }

        public bool? State
        {
            get
            {
                return this.player.State;
            }
            set
            {
                this.player.State = value;
                OnPropertyChanged("State");
            }
        }
        #endregion
    }
}
