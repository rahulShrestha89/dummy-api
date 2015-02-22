using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

using Phase2_Group2_selucmps383_sp15_p2_g2.DbContext;

namespace Phase2_Group2_selucmps383_sp15_p2_g2.Models
{
    
    public class User
    {

        public User()
        {
            CustomerCart = new Cart();
            Games = new List<Game>();
            Sales = new List<Sale>();
        }

        public int UserId { get; set; }

        [Display(Name = "First Name")]
        public string FirstName { get; set; }

        [Display(Name = "Last Name")]
        public string LastName { get; set; }

        [Display(Name = "User Name")]
        [DataType(DataType.EmailAddress)]
        [Required(ErrorMessage = "Enter a Valid email-address...")]
        public string EmailAddress { get; set; }

        [Display(Name = "Password")]
        [DataType(DataType.Password)]
        [Required(ErrorMessage = "Password is required...")]
        public string Password { get; set; }

        public string ApiKey { get; set; }

        public Cart CustomerCart { get; set; }
        public Enums.Role Role { get; set; }
        public ICollection<Game> Games { get; set; }
        public ICollection<Sale> Sales { get; set; } 

    }


    
   
}