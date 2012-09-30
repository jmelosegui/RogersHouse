using System.Web.Mvc;
using Ninject;
using RogersHouse.WebUI.Infrastructure.Logging;

namespace RogersHouse.WebUI.Controllers
{
    public class ControllerBase : Controller
    {
        [Inject]
        public ILogger Logger { get; set; }
    }
}