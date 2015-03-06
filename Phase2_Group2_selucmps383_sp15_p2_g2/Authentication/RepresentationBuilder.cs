using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;

namespace Phase2_Group2_selucmps383_sp15_p2_g2.Authentication
{
    public class RepresentationBuilder:IBuildMessageRepresentation
    {
        public string BuildRequestRepresentation(HttpRequestMessage requestMessage)
        {
            string httpMethod = requestMessage.Method.Method;
            string url = requestMessage.RequestUri.AbsolutePath;
            string representation = httpMethod + "\n" + url + "\n";
            return representation;
        }
    }
}