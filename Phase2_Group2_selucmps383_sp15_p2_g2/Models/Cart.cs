using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Phase2_Group2_selucmps383_sp15_p2_g2.Models
{
    public class Cart
    {
        public int CartId { get; set; }

        public string UserCartId { get; set; }  // store ID of the User associated with the cart as Session variable

        public int Quantity { get; set; }

        public System.DateTime CartDate { get; set; }

        public List<Game> Games { get; set; } 

    }
}