using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Phase2_Group2_selucmps383_sp15_p2_g2.Models
{
    public class UserModel : UserBaseModel
    {

        public string Password { get; set; }
        public Cart CustomerCart { get; set; }
        public IEnumerable<SaleModel> Sales { get; set; } 

        
    }
}