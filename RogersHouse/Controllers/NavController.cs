using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using System.Web.Routing;
using RogerHouse.Domain.Abstract;
using RogerHouse.Domain.Entities;
using RogersHouse.WebUI.Models;

namespace RogersHouse.WebUI.Controllers
{
    public class NavController : Controller
    {
        private readonly IPagesRepository _repository;

        public NavController(IPagesRepository repository)
        {
            _repository = repository;
        }

        public ViewResult Menu(string path)
        {
            // Just so we don't have to write this code twice
            Func<Page, NavLink> makeLink = page => new NavLink
                                                       {
                                                           Text = page.Title,
                                                           RouteValues = new RouteValueDictionary(new
                                                                                                      {
                                                                                                          path =
                                                                                                      page.Path.
                                                                                                      TrimStart('/')
                                                                                                      }),
                                                           IsSelected = (page.Path.TrimStart('/') == path)
                                                       };

            // Put a Home link at the top
            List<NavLink> navLinks = _repository.Pages
                .Where(nl => nl.ShowInMenu && nl.Visible)
                .OrderBy(x => x.Order)
                .Select(p => makeLink(p))
                .ToList();

            // Add a link for each distinct category

            return View(navLinks);
        }

        public ViewResult Page(string path, string language = "en")
        {
            Page sitePage = _repository.GetPage("/" + (path ?? "Home"), language);

            //if (sitePage == null)
            //{
            //    sitePage = ((SiteEFSession)session).GetPage("index", CultureManager.GetCurrentCulture());
            //    ViewData["IsHomePage"] = true;
            //    TempData["error"] = string.Format("No se ha encontrado la pagina <b>{0}</b> en la base de datos.", page);
            //}
            return View("Index", sitePage);
        }

        public FileContentResult Download(string language = "en")
        {
            Page sitePage = _repository.GetPage("/Contract", language);

            var content = sitePage.Body;

            byte[] word = Encoding.UTF8.GetBytes(sitePage.Body);
            return File(word, "application/ms-word", "contract.doc");
        }
    }
}