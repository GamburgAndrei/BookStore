using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using BookStore.Models;
using System.Data.Entity;
using System.Web.Mvc;

namespace BookStore.Models
{
    public class Team
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string Coach { get; set; }
        public ICollection<Player> Players { get; set; }
        public Team()
        {
            Players = new List<Player>();
        }
    }
}