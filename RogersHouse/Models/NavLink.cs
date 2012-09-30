using System.Web.Routing;

namespace RogersHouse.WebUI.Models
{
    public class NavLink
    {
        public string Text { get; set; }
        public RouteValueDictionary RouteValues { get; set; }
        public bool IsSelected { get; set; }
    }
}