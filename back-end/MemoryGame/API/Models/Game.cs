using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace API.Models
{
    public class Game
    {
        public User Player1 { get; set; }
        public User Player2 { get; set; }
        public string CurrentTurn { get; set; }
        public Dictionary<string, string> CardArray { get; set; }
        private List<char> chars = "$%#@!*abcdefghijklmnopqrstuvwxyz1234567890".ToList();

        public  char GetLetter()
        {
            Random rand = new Random();
            int index = rand.Next(0, chars.Count - 1);
            char letter = chars[index];
            chars.RemoveAt(index);
            return letter;
        }
        public Game()
        {
            CardArray = new Dictionary<string, string>();
            for (int i = 0; i < 9; i++)
            {
                CardArray.Add(GetLetter().ToString(), null);
            }
        }
    }
}