using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Phase2_Group2_selucmps383_sp15_p2_g2.Models
{
    public class Genre
    {
        public Genre()
        {
            Games = new List<Game>();
        }

        public int GenreId { get; set; }
        public string GenreName { get; set; }
        public ICollection<Game> Games { get; set; }
    }
}