using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Phase2_Group2_selucmps383_sp15_p2_g2.Models
{
    public class User
    {
        public int UserId { get; set; }

        [Display(Name = "User Name")]
        [DataType(DataType.EmailAddress)]
        [Required(ErrorMessage = "Enter a Valid email-address...")]
        public string EmailAddress { get; set; }

        [Display(Name = "Password")]
        [DataType(DataType.Password)]
        [Required(ErrorMessage = "Password is required...")]
        public string Password { get; set; }

        public string ApiKey { get; set; }

        [Required]
        public int RoleId { get; set; }
        public Role Role { get; set; }
        public List<Game> Games { get; set; }
        public List<Sale> Sales { get; set; } 

    }
}