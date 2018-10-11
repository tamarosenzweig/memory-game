using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace memoryGame.Models
{
   public class User
    {
        public string UserName { get; set; }
        public int Age { get; set; }
        public string PartnerUserName { get; set; }
        public int Score { get; set; }

        public User()
        {
            Score = 0;
        }
    }
}
