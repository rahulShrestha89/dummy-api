using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Phase2_Group2_selucmps383_sp15_p2_g2.Models
{
    public class GameModel
    {
        public int GameId { get; set; }

        public string GameName { get; set; }

        public DateTime ReleaseDate { get; set; }

        public decimal GamePrice { get; set; }
        public int InventoryCount { get; set; }

        public ICollection<Genre> Genres { get; set; }
        public ICollection<Tag> Tags { get; set; }

    }
}