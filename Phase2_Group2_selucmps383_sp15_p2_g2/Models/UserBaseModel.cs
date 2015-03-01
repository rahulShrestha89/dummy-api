using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Phase2_Group2_selucmps383_sp15_p2_g2.Models
{
    // models for user view
    public class UserBaseModel
    {
        public int UserId { get; set; }
        public string Url { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public Enums.Role Role { get; set; }
        

    }
}