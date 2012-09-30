using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using RogersHouse.WebUI.Infrastructure;
using RogerHouse.Domain.Abstract;
using Telerik.Web.Mvc;

namespace RogersHouse.WebUI
{
    // Note: For instructions on enabling IIS6 or IIS7 classic mode, 
    // visit http://go.microsoft.com/?LinkId=9394801

    public class MvcApplication : HttpApplication
    {
        private static NinjectControllerFactory controllerFactory;
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
        }

        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                null, //RouteName
                "{path}", // URL with parameters
                new { controller = "Nav", action = "Page", path = UrlParameter.Optional },
                new { sitePage = new SitePageConstraint(controllerFactory.kernel.GetService(typeof(IPagesRepository)) as IPagesRepository) }
                );

            routes.MapRoute(
                null, // Route name
                "Admin/EditPage/{path}", // URL with parameters
                new { controller = "Admin", action = "EditPage", path = UrlParameter.Optional } // Parameter defaults
                );
            routes.MapRoute(
                null, // Route name
                "contract/Download", // URL with parameters
                new { controller = "Nav", action = "Download"} // Parameter defaults
                );

            routes.MapRoute(
                "Default", // Route name
                "{controller}/{action}/{id}", // URL with parameters
                new { controller = "Home", action = "Index", id = UrlParameter.Optional } // Parameter defaults
                );
        }

        protected void Application_Start()
        {
            controllerFactory = new NinjectControllerFactory();
            AreaRegistration.RegisterAllAreas();

            RegisterGlobalFilters(GlobalFilters.Filters);
            RegisterRoutes(RouteTable.Routes);
            WebAssetDefaultSettings.UseTelerikContentDeliveryNetwork = true;
            ControllerBuilder.Current.SetControllerFactory(controllerFactory);
        }
    }
}
