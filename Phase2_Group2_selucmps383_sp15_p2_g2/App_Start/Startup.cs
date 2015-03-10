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
using WebApiContrib.IoC.Ninject;
using System.Web.Http;
using Phase2_Group2_selucmps383_sp15_p2_g2.App_Start;


namespace Phase2_Group2_selucmps383_sp15_p2_g2
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

        public void ConfigureAuth(IAppBuilder app)
        {
            var config = new HttpConfiguration();
            config.DependencyResolver = new NinjectResolver(NinjectWebCommon.CreateKernel());

            config.Routes.MapHttpRoute("default", "api/{controller}/{id}", new { id = RouteParameter.Optional });

            app.UseWebApi(config);





            app.UseCookieAuthentication(new CookieAuthenticationOptions());
            app.UseExternalSignInCookie(DefaultAuthenticationTypes.ExternalCookie);

            app.UseOAuthBearerTokens(OAuthOptions);
           // ConfigureAuth(app);
        }
    }
}
