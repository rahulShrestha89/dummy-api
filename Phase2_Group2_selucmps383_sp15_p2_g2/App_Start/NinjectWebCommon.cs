using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using Ninject;
using Ninject.Web.Common;
using Phase2_Group2_selucmps383_sp15_p2_g2.DbContext;
using Phase2_Group2_selucmps383_sp15_p2_g2.Models;
using WebApiContrib.IoC.Ninject;

namespace Phase2_Group2_selucmps383_sp15_p2_g2.App_Start
{
   
 public static class NinjectWebCommon
    {
 
        private static IKernel CreateKernel()
        {
            var kernel = new StandardKernel();
            kernel.Bind<Func<IKernel>>().ToMethod(ctx => () => new Bootstrapper().Kernel);
            kernel.Bind<IHttpModule>().To<HttpApplicationInitializationHttpModule>();
 
            //Suport WebAPI Injection
            GlobalConfiguration.Configuration.DependencyResolver = new WebApiContrib.IoC.Ninject.NinjectResolver(kernel);

            GlobalConfiguration.Configuration.DependencyResolver = new NinjectResolver(kernel);

 
            RegisterServices(kernel);
            return kernel;
        }
 
     /// <summary>
     /// Load the modules or register services here
     /// </summary>
     /// <param name="kernel"></param>
        private static void RegisterServices(IKernel kernel)
        {
            kernel.Bind<GameStoreContext>().To<GameStoreContext>().InRequestScope();
            kernel.Bind<IGameStoreRepository>().To<IGameStoreRepository>().InRequestScope();
        }
    }
}