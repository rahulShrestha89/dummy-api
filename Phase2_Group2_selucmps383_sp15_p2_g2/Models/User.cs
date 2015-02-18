using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using FluentValidation;
using Phase2_Group2_selucmps383_sp15_p2_g2.DbContext;

namespace Phase2_Group2_selucmps383_sp15_p2_g2.Models
{
    [FluentValidation.Attributes.Validator(typeof(PlaceValidator))]
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


    /**
     * Fluent Validation to make sure that the Email-Addresses are unique
     */
    public class PlaceValidator : AbstractValidator<User>
    {
        public PlaceValidator()
        {
            RuleFor(x => x.EmailAddress).Must(BeUnique).WithMessage("UserName Already Exists! Try Again!!!");
        }

        private bool BeUnique(string username)
        {
            var _db = new GameStoreContext();
            if (_db.Users.SingleOrDefault(x => x.EmailAddress == username) == null) return true;
            return false;
        }
    }
}