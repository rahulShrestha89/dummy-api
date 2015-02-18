using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Phase2_Group2_selucmps383_sp15_p2_g2.Models
{
    public class Tag
    {
        public int TagId { get; set; }

        [Display(Name = "Tag Name")]
        public string TagName { get; set; }

        public List<Game> Games { get; set; } 
    }
}