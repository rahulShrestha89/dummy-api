using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Phase2_Group2_selucmps383_sp15_p2_g2.Models
{
    public class SaleModel
    {
        public int SaleId { get; set; }
        public UserModel User { get; set; }
        public decimal? TotalAmount { get; set; }
        public DateTime? SaleDate { get; set; }
    }
}
