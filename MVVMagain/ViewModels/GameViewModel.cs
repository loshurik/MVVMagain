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
using MVVMagain.Interfaces;
using MVVMagain.Views;

namespace MVVMagain.ViewModels
{
    public class GameViewModel : ViewModelBase, IGame
    {
        private readonly Game currentGame;
        private readonly ObservableCollection<PlayerViewModel> players;
        private readonly ICollectionView collectionView;

        private ICommand removeCommand;
        private ICommand nextCommand;
        private ICommand startCommand;
        private ICommand validatePlayersCommand;

        private string errorMessage;

        public GameViewModel(Game game)
        {
            this.currentGame = game;
            this.players = new ObservableCollection<PlayerViewModel>();

            foreach (Player p in this.currentGame.Players)
            {
                this.players.Add(new PlayerViewModel(p, this));
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

        public string ErrorMessage
        {
            get
            {
                return this.errorMessage;
            }
            set
            {
                this.errorMessage = value;
                OnPropertyChanged("ErrorMessage");
            }
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

        #region ValidatePlayersCommand
        public ICommand ValidatePlayersCommand
        {
            get
            {
                if (validatePlayersCommand==null)
                    validatePlayersCommand = new RelayCommand(()=>this.ValidatePlayers());
                return validatePlayersCommand;
            }
        }

        private bool ValidatePlayers()
        {
            DeleteUnusedPlayers();
            return ValidateCount();
        }

        private void DeleteUnusedPlayers()
        {
            for (int i = players.Count - 1; i >= 0; i--)
            {
                if (players[i].Name == "")
                    this.players.RemoveAt(i);
            }
        }

        private bool ValidateCount()
        {
            if (players.Count < 2)
            {
                ErrorMessage = "Мало игроков";
                return false;
            }
            return true;
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

        #region NextCommand
        public ICommand NextCommand
        {
            get
            {
                if (this.nextCommand==null)
                    this.nextCommand=new RelayCommand(()=>this.Next());
                return this.nextCommand;
            }
        }

        private void Next()
        {
            ResetPlayersState();
            MoveToNextQuestion();
        }

        private void MoveToNextQuestion()
        {
            if (CurrentQuestion % 50 == 0)
            {
                CurrentQuestion = 10;
                CurrentCategory++;
            }
            else
            {
                CurrentQuestion += Game.NominalPoints;
            }
        }

        private void ResetPlayersState()
        {
            foreach (PlayerViewModel pvm in this.Players)
            {
                pvm.State = null;
            }
        }

        #endregion

        #region StartCommand
        public ICommand StartCommand
        {
            get 
            {
                if (startCommand == null)
                    startCommand = new RelayCommand(() => this.Start());
                return startCommand;
            }
        }

        private void Start()
        {
            ResetGame();
            StartWindow startWindow = new StartWindow(this);
            startWindow.ShowDialog();
        }

        private void ResetGame()
        {
            ResetPlayers();
            ResetCategory();
            
        }

        private void ResetCategory()
        {
            CurrentCategory = 1;
            CurrentQuestion = Game.NominalPoints;
        }

        private void ResetPlayers()
        {
            //???
        }
        #endregion

        private void OnCollectionViewCurrentChanged(object sender, EventArgs e)
        {
            OnPropertyChanged("SelectedPlayer");
        }

        #region IGame
        public void IncreaseScore(PlayerViewModel player)
        {
            player.Score += CurrentQuestion;
            Next();
        }

        public void DecreaseScore(PlayerViewModel player)
        {
            player.Score -= CurrentQuestion;
        }

        #endregion
    }
}
