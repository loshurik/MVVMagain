using System;
using MVVMagain.Models;
using MVVMagain.Infrastructure;
using System.Windows.Input;
using MVVMagain.Interfaces;

namespace MVVMagain.ViewModels
{
    public class PlayerViewModel:ViewModelBase
    {
        private IGame iGame;
        private readonly Player player;
        private ICommand rightAnswerCommand;
        private ICommand wrongAnswerCommand;

        public PlayerViewModel(Player player, IGame game)
        {
            if (player == null)
            {
                throw new NullReferenceException("player");
            }
            this.player = player;
            iGame = game;
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
            if (this.State!=false)
                iGame.IncreaseScore(this);
        }
        public ICommand WrongAnswerCommand
        {
            get
            {
                if (this.wrongAnswerCommand == null)
                    this.wrongAnswerCommand = new RelayCommand(() => this.WrongAnswer());
                return this.wrongAnswerCommand;
            }
        }

        private void WrongAnswer()
        {
            if (this.State != false)
            {
                iGame.DecreaseScore(this);
                this.State = false;
            }
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

        public IGame CurrentGame
        {
            get 
            {
                return this.iGame;
            }
            set 
            {
                this.iGame = value;
                OnPropertyChanged("CurrentGame");
            }
        }
        #endregion
    }
}
