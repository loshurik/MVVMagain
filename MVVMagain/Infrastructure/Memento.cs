using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MVVMagain.ViewModels;

namespace MVVMagain.Infrastructure
{
    public class Memento
    {
        private int question;
        private int category;
        private List<PlayerViewModel> players;
        private PlayerViewModel activePlayer;

        public Memento(int question, int category, List<PlayerViewModel> players, PlayerViewModel activePlayer)
        {
            this.question = question;
            this.category = category;
            this.players = players;
            this.activePlayer = activePlayer;
        }

        public int Question
        {
            get { return question; }
            set { question = value; }
        }

        public int Category
        {
            get { return category; }
            set { category = value; }
        }

        public List<PlayerViewModel> Players
        {
            get { return players; }
            set { players = value; }
        }

        public PlayerViewModel ActivePlayer
        {
            get { return activePlayer; }
            set { activePlayer = value; }
        }
    }
}
