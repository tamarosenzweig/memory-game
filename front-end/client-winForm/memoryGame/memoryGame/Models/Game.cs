using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace memoryGame.Models
{
   public class Game
    {
        public User Player1 { get; set; }
        public User Player2 { get; set; }
        public string CurrentTurn { get; set; }
        public Dictionary<string, string> CardArray = new Dictionary<string, string>();

        public Game()
        {
            for (int i = 0; i < 9; i++)
            {
                CardArray.Add(i.ToString(), null);
            }
        }
    }
}
