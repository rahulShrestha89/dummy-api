using Phase2_Group2_selucmps383_sp15_p2_g2.DbContext;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Net.Http;
using System.Net;
using Phase2_Group2_selucmps383_sp15_p2_g2.Controllers;
using Phase2_Group2_selucmps383_sp15_p2_g2.Areas.API.Controllers;

namespace Phase2_Group2_selucmps383_sp15_p2_g2.Authentication
{
    public class ValidateByHmac : AuthorizeAttribute
    {
        private const string UnauthMessage = "Access Denied | Unauthorized Request";
        private GameStoreContext db;
        private readonly IBuildMessageRepresentation _representationBuilder = new RepresentationBuilder();
        private readonly ICalculateSignature _signatureCalculator = new HmacSignatureCalculator();

        public override void OnAuthorization(System.Web.Http.Controllers.HttpActionContext context)
        {
            var controller = (BaseApiController)context.ControllerContext.Controller;
            db = controller._db;

            var request = context.Request;

            if (!(request.Headers.Contains(Header.UserId) && request.Headers.Contains(Header.HmacDigest)))
            {
                context.Response = request.CreateResponse(HttpStatusCode.ExpectationFailed, "Key values , { x_cmps383_authentication_id } and|or { x_cmps383_authentication_key } were not included in your request");
                return;
            }

            var userIdString = Convert.ToString(request.Headers.GetValues(Header.UserId).FirstOrDefault());
            int userId;

            if (!(int.TryParse(userIdString, out userId)))
            {
                context.Response = request.CreateResponse(HttpStatusCode.Unauthorized, "Key value { x_cmps383_authentication_id} is not valid");
            }

            var user = db.Users.FirstOrDefault(u => u.UserId == userId);

            if (user == null)
            {
                context.Response = request.CreateResponse(HttpStatusCode.Unauthorized, "Key value { x_cmps383_authentication_id} is not valid");
            }

            if (request.Headers.GetValues(Header.HmacDigest) == null)
            {
                context.Response = request.CreateResponse(HttpStatusCode.Unauthorized, "Key value { x_cmps383_authentication_id} is not valid");
            }

            var digest = request.Headers.GetValues(Header.HmacDigest).First();

            var representation = _representationBuilder.BuildRequestRepresentation(request);
            if (representation == null)
            {
                context.Response = request.CreateResponse(HttpStatusCode.Unauthorized, "Key value { x_cmps383_authentication_id} is not valid");
                return;
            }

            var signature = _signatureCalculator.Signature(user.ApiKey, representation);

            if (!digest.Equals(signature))
            {
                context.Response = request.CreateResponse(HttpStatusCode.Unauthorized, "Key value { x_cmps383_authentication_id} is not valid");
                return;
            }

            controller.storeUser = user;
            HttpContext.Current.Response.AddHeader("AuthenticationStatus", "Authorized");

        }
    }
}