using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace Phase2_Group2_selucmps383_sp15_p2_g2.Models
{
    public class Genre
    {
        [Key]
        public string GenreName { get; set; }
        public List<Game> Games { get; set; }
    }
}