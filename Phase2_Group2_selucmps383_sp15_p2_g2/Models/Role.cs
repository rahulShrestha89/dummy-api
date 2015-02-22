using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Phase2_Group2_selucmps383_sp15_p2_g2.Models
{
    public class Role
    {
        public int RoleId { get; set; }

        [Display(Name = "Role")]
        public string RoleName { get; set; }

        public List<User> Users { get; set; } 
    }
}