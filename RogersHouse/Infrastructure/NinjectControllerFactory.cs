using System;
using System.Configuration;
using System.Web.Mvc;
using System.Web.Routing;
using Ninject;
using Ninject.Modules;
using RogerHouse.Domain.Abstract;
using RogerHouse.Domain.Concrete;
using RogersHouse.WebUI.Infrastructure.Logging;

namespace RogersHouse.WebUI.Infrastructure
{
    public class NinjectControllerFactory : DefaultControllerFactory
    {
        // A Ninject "kernel" is the thing that can supply object instances
        internal IKernel kernel = new StandardKernel(new RogerHouseServices());

        // ASP.NET MVC calls this to get the controller for each request
        protected override IController GetControllerInstance(RequestContext context, Type controllerType)
        {
            if (controllerType == null)
                return null;
            return (IController)kernel.Get(controllerType);
        }

        // Configures how abstract service types are mapped to concrete implementations
        private class RogerHouseServices : NinjectModule
        {
            public override void Load()
            {
                Bind<IPagesRepository>()
                    .To<SqlPagesRepository>()
                    .WithConstructorArgument("connectionString",
                                             ConfigurationManager.ConnectionStrings["ApplicationServices"].ConnectionString
                    );
                Bind<IRoomsRepository>()
                    .To<SqlRoomsRepository>()
                    .WithConstructorArgument("connectionString",
                                             ConfigurationManager.ConnectionStrings["ApplicationServices"].ConnectionString
                    );

                Bind<ILogger>().To<NLogLogger>();
            }
        }
    }
}