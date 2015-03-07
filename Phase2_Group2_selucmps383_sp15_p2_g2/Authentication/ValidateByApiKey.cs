using Phase2_Group2_selucmps383_sp15_p2_g2.Controllers;
using Phase2_Group2_selucmps383_sp15_p2_g2.DbContext;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Http;
using System.Net.Http;
using Phase2_Group2_selucmps383_sp15_p2_g2.Areas.API.Controllers;

namespace Phase2_Group2_selucmps383_sp15_p2_g2.Authentication
{
    public class ValidateByApiKey : AuthorizeAttribute 
    {
        private GameStoreContext db;

        public override void OnAuthorization(System.Web.Http.Controllers.HttpActionContext actionContext)
        {
            var controller = (BaseApiController)actionContext.ControllerContext.Controller;
            db = controller._db;

            if (actionContext.Request.Headers.Contains(Header.UserId) && actionContext.Request.Headers.Contains(Header.Key))
            {
                if (actionContext.Request.Headers.GetValues(Header.UserId) != null)
                {
                    // Get value from the header
                    var userIdString = Convert.ToString(actionContext.Request.Headers.GetValues(Header.UserId).FirstOrDefault());
                    int userId;

                    if (int.TryParse(userIdString, out userId))
                    {
                        var user = db.Users.FirstOrDefault(u => u.UserId == userId);

                        if (user != null && actionContext.Request.Headers.GetValues(Header.Key) != null)
                        {
                            var apiKey = actionContext.Request.Headers.GetValues(Header.Key).FirstOrDefault();

                            if (apiKey != user.ApiKey)
                            {
                                actionContext.Response = actionContext.Request.CreateResponse(HttpStatusCode.Unauthorized, "Invalid Credentials / Invalid Api Key ");
                                return;
                            }

                            controller.storeUser = user;
                            HttpContext.Current.Response.AddHeader("AuthenticationStatus", "Authorized");
                            return;
                        }
                    }
                }
                actionContext.Response = actionContext.Request.CreateResponse(HttpStatusCode.Unauthorized, "Key value" + Header.UserId + " is not a valid key.");
                return;
            }
            actionContext.Response = actionContext.Request.CreateResponse(HttpStatusCode.ExpectationFailed, "Key value " + Header.UserId + "and / or " + Header.Key + "were not included in your request");
        }


    }
}