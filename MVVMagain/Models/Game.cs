using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MVVMagain.Models
{
    public class Game
    {
        #region Constants
        public const int MaximumPlayersCount = 6;
        public const int MinimumPlayersCount = 2;
        public const int NominalPoints = 10;
        public const int QuestionsInCategory = 5;
        #endregion

        private readonly List<Player> players;
        private string[] teams = { "Страпелька", "Одушевленные Аэросани", "Хронически разумные United", "МИД-2", "Енотики-7", "Боливария" };
        public int CurrentCategory { get; set; }
        public int CurrentQuestion { get; set; }


        public List<Player> Players { get { return players; } }

        public Game()
        {
            players = new List<Player>();
            this.CurrentQuestion = 1;
            this.CurrentCategory = 1;
            for (int i = 0; i < MaximumPlayersCount; i++)
            {
                players.Add(new Player() { Name = teams[i] });
                
            }
        }
        public void RemovePlayer(Player p)
        {
            players.Remove(p);
        }
    }
}
