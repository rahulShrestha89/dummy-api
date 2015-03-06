using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;

namespace Phase2_Group2_selucmps383_sp15_p2_g2.Authentication
{
    class HmacSignatureCalculator:ICalculateSignature
    {
        public string Signature(string secret, string value)
        {
            var secretBytes = Encoding.UTF8.GetBytes(secret);
            var valueBytes = Encoding.UTF8.GetBytes(value);
            string signature;

            using (var hmac = new HMACSHA1(secretBytes))
            {
                var hash = hmac.ComputeHash(valueBytes);
                signature = Convert.ToString(hash);
            }
            return signature;
        }
    }
}
