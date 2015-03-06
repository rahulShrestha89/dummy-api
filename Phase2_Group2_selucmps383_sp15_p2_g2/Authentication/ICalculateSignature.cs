using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Phase2_Group2_selucmps383_sp15_p2_g2.Authentication
{
    public interface ICalculateSignature
    {
        string Signature(string secret, string value);
    }
}
