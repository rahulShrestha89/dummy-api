using Phase2_Group2_selucmps383_sp15_p2_g2.DbContext;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Http;
using System.Net.Http;


namespace Phase2_Group2_selucmps383_sp15_p2_g2.Authentication
{
    public class ValidateByLogIn : AuthorizeAttribute
    {
        public GameStoreContext db = new GameStoreContext();

        public override void OnAuthorization(System.Web.Http.Controllers.HttpActionContext actionContext)
        {
            if (actionContext.Request.Headers.Contains(Header.Email) && actionContext.Request.Headers.Contains(Header.Password))
            {
                if (actionContext.Request.Headers.GetValues(Header.Email).FirstOrDefault() != null)
                {
                    var email = Convert.ToString(actionContext.Request.Headers.GetValues(Header.Email).First());

                    var user = db.Users.FirstOrDefault(u => u.EmailAddress.Equals(email));

                    if (user != null)
                    {
                        if (actionContext.Request.Headers.GetValues(Header.Password).FirstOrDefault() != null)
                        {
                            var password = actionContext.Request.Headers.GetValues(Header.Password).First();

                            if (!Crypto.VerifyHashedPassword(user.Password, password))
                            {
                                actionContext.Response = actionContext.Request.CreateResponse(HttpStatusCode.Unauthorized, "Acces Denied based on Credentials");
                                return;
                            }

                            user.ApiKey = GetApiKey();

                            db.SaveChanges();

                            HttpContext.Current.Response.Headers.Add("AuthenticationStatus", "User Authorized.");
                            HttpContext.Current.Response.Headers.Add("ApiKey", user.ApiKey);
                            return;
                            
                        }
                    }

                    actionContext.Response = actionContext.Request.CreateResponse(HttpStatusCode.Unauthorized, "Invalid Credentials!");
                    return;
                }
            }

            actionContext.Response = actionContext.Request.CreateResponse(HttpStatusCode.ExpectationFailed, "Key Values :::" + Header.Email + " and | or " + Header.Password + " were not included in your request");
            return;
        }
    }
}