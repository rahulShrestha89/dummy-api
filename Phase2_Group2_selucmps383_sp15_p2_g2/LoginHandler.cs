using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Linq;
using System.Web;
using System.Threading.Tasks;
using System.Threading;
using Phase2_Group2_selucmps383_sp15_p2_g2.DbContext;
using System.Web.Helpers;

namespace Phase2_Group2_selucmps383_sp15_p2_g2
{
    public class Login : DelegatingHandler
    {
        GameStoreContext context = new GameStoreContext();
        readonly string method = "GET";
        
        protected override Task<System.Net.Http.HttpResponseMessage> SendAsync(System.Net.Http.HttpRequestMessage request, CancellationToken cancellationToken)
        {

            if (!login(request))
            {
                var response = new HttpResponseMessage();
                var tsc = new TaskCompletionSource<HttpResponseMessage>();
                tsc.SetResult(response);
                return tsc.Task;
            }
            return base.SendAsync(request, cancellationToken);
            
           
        }
        private bool login(HttpRequestMessage message)
        {
            var query = message.RequestUri.ParseQueryString();
            string username = query["username"];
            string password = query["password"];

            if(context.Users.Where(s=> s.EmailAddress==username && Crypto.VerifyHashedPassword(s.Password, password))!=null)
            {
                return true;
            }

            return false;
        }
    }
}