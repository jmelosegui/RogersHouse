using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Routing;
using RogerHouse.Domain.Abstract;

namespace RogersHouse.WebUI.Infrastructure
{
    public class SitePageConstraint : IRouteConstraint
    {
        private IPagesRepository repository;

        public SitePageConstraint(IPagesRepository repository)
        {
            this.repository = repository;
        }

        public bool Match(HttpContextBase httpContext, Route route, string parameterName, RouteValueDictionary values, RouteDirection routeDirection)
        {
            var path = values["path"].ToString();

            if (String.IsNullOrEmpty(path.Trim()) || path == "System.Web.Mvc.UrlParameter")
                path = "Home";

            var result = repository.Pages.Any(p => "/" + path == p.Path && (p.Visible || httpContext.User.Identity.IsAuthenticated));
            return result;
        }
    }
}