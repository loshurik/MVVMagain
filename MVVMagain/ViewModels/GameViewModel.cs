using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Windows.Data;
using System.Windows.Input;
using MVVMagain.Infrastructure;
using MVVMagain.Interfaces;
using MVVMagain.Models;

namespace MVVMagain.ViewModels
{
    public class GameViewModel : ViewModelBase, IGame, IMemento
    {
        private Game currentGame;
        private ObservableCollection<PlayerViewModel> players;
        private ICollectionView collectionView;

        private ICommand nextCommand;

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

        public Game CurrentGame
        {
            get { return currentGame; }
            set
            {
                currentGame = value;
                //this.players = new ObservableCollection<PlayerViewModel>();

                //foreach (Player p in this.currentGame.Players)
                //{
                //    this.players.Add(new PlayerViewModel(p, this));
                //}

                //this.collectionView = CollectionViewSource.GetDefaultView(this.players);
                //if (this.collectionView == null)
                //    throw new NullReferenceException("collectionView");

                //this.collectionView.CurrentChanged += new EventHandler(this.OnCollectionViewCurrentChanged);
            }
        }

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

        #region NextCommand
        public ICommand NextCommand
        {
            get
            {
                if (this.nextCommand == null)
                    this.nextCommand = new RelayCommand(() => this.Next());
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
            if (CurrentQuestion % (Game.NominalPoints * Game.QuestionsInCategory) == 0)
            {
                CurrentQuestion = Game.NominalPoints;
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
            if (this.players.All(p => p.State == false))
                Next();
        }

        #endregion

        #region IMemento
        public Memento SaveMemento()
        {
            return new Memento(CurrentQuestion, CurrentCategory, players.ToList(), SelectedPlayer);
        }

        public void RestoreMemento(Memento memento)
        {
            //
        }
        #endregion
    }
}
