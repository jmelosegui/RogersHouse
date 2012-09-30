using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using RogerHouse.Domain.Entities;

namespace RogersHouse.WebUI.Controllers
{
    public partial class AdminController
    {
        public int PageSize = 10;

        public ActionResult Pages([DefaultValue(1)] int page)
        {
            List<Page> pages = _repository.Pages
                .Skip((page - 1)*PageSize)
                .Take(PageSize)
                .ToList();
            return View(pages);
        }

        public ActionResult NewPage()
        {
            return View(new Page());
        }

        [HttpPost]
        public ActionResult NewPage(Page page)
        {
            if (ModelState.IsValid)
            {
                TratarPagina(page);
                _repository.SavePage(page);
                return RedirectToAction("Pages");
            }
            return View(page);
        }

        public ActionResult EditPage(string path, string language = "en")
        {
            Page page = _repository.Pages.Where(p => p.Path == "/" + path && p.Language == language).First();

            return View(page);
        }

        [HttpPost]
        public ActionResult EditPage(Page page)
        {
            if (ModelState.IsValid)
            {
                TratarPagina(page);
                _repository.SavePage(page);
                return RedirectToAction("Pages");
            }
            return View(page);
        }

        public ActionResult Delete(int id)
        {
            _repository.Delete(id);
            return RedirectToAction("Pages");
        }

        private static void TratarPagina(Page page)
        {
            if (!page.Path.StartsWith("/"))
            {
                page.Path = "/" + page.Path;
            }
            if (page.ShowInMenu)
                page.Visible = true;
            page.Path = page.Path.Trim().Replace(" ", "-");
            page.Body = HttpUtility.HtmlDecode(page.Body);
        }
    }
}