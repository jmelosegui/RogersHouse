using System;
using System.Collections.Generic;
using System.Linq;
using RogerHouse.Domain.Abstract;
using RogerHouse.Domain.Entities;

namespace RogerHouse.Domain.Concrete
{
    public class FakePagesRepository : IPagesRepository
    {
        public IQueryable<Page> fakePages = new List<Page>
                                                {
                                                    new Page{PageId = 1, Path = "/Home", ShowInMenu = true, Visible = true},
                                                    new Page{PageId = 2, Path = "/About", ShowInMenu = true, Visible = true},
                                                    new Page{PageId = 2, Path = "/Contact", ShowInMenu = true, Visible = true}
                                                }.AsQueryable();
        public IQueryable<Page> Pages
        {
            get { return fakePages; }
        }

        public Page GetPage(string path, string language)
        {
            throw new NotImplementedException();
        }

        public void SavePage(Page page)
        {
            var mockRepository = new Mock<IPagesRepository>()
        }
    }
}
