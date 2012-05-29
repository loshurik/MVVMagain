using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Windows;
using System.Windows.Data;
using System.Windows.Input;
using MVVMagain.Infrastructure;
using MVVMagain.Interfaces;
using MVVMagain.Models;
using MVVMagain.Views;

namespace MVVMagain.ViewModels
{
    public class StartViewModel:ViewModelBase
    {
        private Game currentGame;
        private ObservableCollection<PlayerViewModel> players;
        private ICollectionView collectionView;

        private ICommand startCommand;
        private ICommand validatePlayersCommand;

        private string errorMessage;

        public Game CurrentGame
        {
            get { return currentGame; }
            set { currentGame = value; OnPropertyChanged("CurrentGame"); }
        }

         public StartViewModel(Game game)
        {
            this.currentGame = game;
            this.players = new ObservableCollection<PlayerViewModel>();
            
            foreach (Player p in this.currentGame.Players)
            {
                this.players.Add(new PlayerViewModel(p, null));
            }

            //this.collectionView = CollectionViewSource.GetDefaultView(this.players);
            //if (this.collectionView == null)
            //    throw new NullReferenceException("collectionView");

        }

         public ObservableCollection<PlayerViewModel> Players
         {
             get { return this.players; }
         }

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
            StartWindow startWindow = new StartWindow(currentGame);
            startWindow.ShowDialog();

        }

        private void ResetGame()
        {
            this.currentGame = new Game();
        }

        #endregion

        #region ValidatePlayersCommand
        public ICommand ValidatePlayersCommand
        {
            get
            {
                if (validatePlayersCommand == null)
                    validatePlayersCommand = new RelayCommand(() => this.ValidatePlayers());
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
            for (int i = currentGame.Players.Count - 1; i >= 0; i--)
            {
                if (currentGame.Players[i].Name == null)
                {
                    this.currentGame.Players.RemoveAt(i);
                    this.players.RemoveAt(i);
                }
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
        #endregion

    }
}
