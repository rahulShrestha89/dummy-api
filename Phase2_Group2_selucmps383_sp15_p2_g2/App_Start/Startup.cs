using System;
using System.Threading.Tasks;
using Microsoft.Owin;
using Owin;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity;
using Microsoft.Owin.Security.OAuth;
using Phase2_Group2_selucmps383_sp15_p2_g2.Providers;
using Microsoft.Owin.Security.Cookies;
using Microsoft.Owin.Security;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Owin.Security.Cookies;


namespace Phase2_Group2_selucmps383_sp15_p2_g2.App_Start
{
    public partial class Startup
    {
        public static string PublicClientId { get; private set; }

        public static Func<UserManager<IdentityUser>> UserManagerFactory { get; set; }

        public static OAuthAuthorizationServerOptions OAuthOptions { get; private set; }
        static Startup()
        {
            
            PublicClientId = "self";

            UserManagerFactory = () => new UserManager<IdentityUser>(new UserStore<IdentityUser>());

            OAuthOptions = new OAuthAuthorizationServerOptions
            {
                TokenEndpointPath = new PathString("/Token"),
                Provider = new ApplicationOAuthProvider(PublicClientId, UserManagerFactory),
                AuthorizeEndpointPath = new PathString("/api/Account/ExternalLogin"),
                AllowInsecureHttp = true
            };

        }

        public void Configuration(IAppBuilder app)
        {

            app.UseCookieAuthentication(new CookieAuthenticationOptions());
            app.UseExternalSignInCookie(DefaultAuthenticationTypes.ExternalCookie);

            app.UseOAuthBearerTokens(OAuthOptions);
            Configuration(app);
        }
    }
}
