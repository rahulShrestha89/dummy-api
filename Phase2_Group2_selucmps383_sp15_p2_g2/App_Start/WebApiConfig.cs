using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Formatting;
using System.Web.Http;
using Newtonsoft.Json.Serialization;
using System.Web.Http.Cors;

namespace Phase2_Group2_selucmps383_sp15_p2_g2
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            //Allows for any origin to make a COR.
            var cors = new EnableCorsAttribute("*", "*", "*");
            config.EnableCors(cors);

            config.Routes.MapHttpRoute(
                name: "ApiKey",
                routeTemplate: "api/ApiKey",
                defaults: new { id = RouteParameter.Optional },
                constraints: null,
                handler: new Login()
                );

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );

            var jsonFormatter = config.Formatters.OfType<JsonMediaTypeFormatter>().First();
            jsonFormatter.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();

            // Uncomment the following line of code to enable query support for actions with an IQueryable or IQueryable<T> return type.
            // To avoid processing unexpected or malicious queries, use the validation settings on QueryableAttribute to validate incoming queries.
            // For more information, visit http://go.microsoft.com/fwlink/?LinkId=279712.
            //config.EnableQuerySupport();

            // To disable tracing in your application, please comment out or remove the following line of code
            // For more information, refer to: http://www.asp.net/web-api
            config.EnableSystemDiagnosticsTracing();
        }
    }
}
