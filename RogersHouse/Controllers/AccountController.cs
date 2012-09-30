using System.Web.Mvc;
using System.Web.Security;
using RogersHouse.WebUI.Models;

namespace RogersHouse.WebUI.Controllers
{
    public class AccountController : Controller
    {

        //
        // GET: /Account/LogOn

        public ActionResult LogOn()
        {
            return View();
        }

        //
        // POST: /Account/LogOn

        [HttpPost]
        public ActionResult LogOn(LogOnViewModel model, string returnUrl)
        {
            if (ModelState.IsValid)
                if (!FormsAuthentication.Authenticate(model.UserName, model.Password))
                    ModelState.AddModelError("", "Incorrect username or password");

            if (ModelState.IsValid)
            {
                FormsAuthentication.SetAuthCookie(model.UserName, false);
                return Redirect(returnUrl ?? Url.Action("Index", "Admin"));
            }

            return View(model);

        }

        //
        // GET: /Account/LogOff

        public ActionResult LogOff()
        {
            FormsAuthentication.SignOut();

            return RedirectToAction("Index", "Home");
        }
    }
}
