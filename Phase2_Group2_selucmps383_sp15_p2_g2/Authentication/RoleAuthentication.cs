using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;
using System.Web.Http.Controllers;
using Phase2_Group2_selucmps383_sp15_p2_g2.Controllers;
using Phase2_Group2_selucmps383_sp15_p2_g2.Models;
using Phase2_Group2_selucmps383_sp15_p2_g2.Enums;

namespace Phase2_Group2_selucmps383_sp15_p2_g2.Authentication
{
    public class RoleAuthentication : AuthorizeAttribute
    {
        private List<string> authorizedRoles;
        private BaseApiController Controller;
        private User user;

        public RoleAuthentication(string roles)
        {
            authorizedRoles = new List<string>();
            if (roles != null)
            {
                roles.Trim(' ');
                authorizedRoles.AddRange(roles.Split(','));
            }
            if (Roles != null)
            {
                Roles.Trim(' ');
                authorizedRoles.AddRange(Roles.Split(','));
            }
        }

        public override void OnAuthorization(HttpActionContext actionContext)
        {
            Controller = (BaseApiController)actionContext.ControllerContext.Controller;
            user = Controller.storeUser;

            var userRoles = Enum.GetName(typeof(Role), user.Role);

            if (userRoles.Contains("StoreAdmin"))
            {
                return;
            }

            int userId = user.UserId;

            foreach (var role in authorizedRoles)
            {
                if (userRoles.Contains(role))
                {
                    return;
                }
            }

            actionContext.Response = actionContext.Request.CreateResponse(HttpStatusCode.Unauthorized,
                "Higher Authorization Level Required");
            return;
        }
    }
}