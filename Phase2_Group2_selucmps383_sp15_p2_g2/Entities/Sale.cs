using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Phase2_Group2_selucmps383_sp15_p2_g2.Models
{
    public class Sale
    {
        public Sale()
        {
            CompanyUser = new User();
        }

        public int SaleId { get; set; }

        public decimal? TotalAmount { get; set; }

        public DateTime? SaleDate { get; set; }

        public int UserId { get; set; }
        public User CompanyUser { get; set; }
    }
}