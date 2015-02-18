using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Phase2_Group2_selucmps383_sp15_p2_g2.Models
{
    public class Genre
    {
        public int GenreId { get; set; }
        public List<Game> Games { get; set; }
    }
}