using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace API.Models
{
    public static class DB
    {
        public static List<User> Users { get; set; }
        public static List<Game> Games { get; set; }
        static DB()
        {
            Users = new List<User>();
            Games = new List<Game>();
        }

    }
}