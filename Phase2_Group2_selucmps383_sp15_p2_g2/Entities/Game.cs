using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Phase2_Group2_selucmps383_sp15_p2_g2.Models
{
    public class Game
    {

        public Game()
        {
            Users = new List<User>();
            Genres = new List<Genre>();
            Tags = new List<Tag>();
        }

        public int GameId { get; set; }

        [Display(Name = "Game Name")]
        public string GameName { get; set; }

        [Display(Name = "Release Date")]
        public DateTime ReleaseDate { get; set; }

        [Display(Name = "Price")]
        public decimal GamePrice { get; set; }

        [Display(Name = "Quantity Left")]
        public int InventoryCount { get; set; }

        public ICollection<User> Users { get; set; } 
        public ICollection<Genre> Genres { get; set; }
        public ICollection<Tag> Tags { get; set; } 

    }
}