using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using RogerHouse.Domain.Entities;

namespace RogerHouse.Domain.Abstract
{
    public interface IPagesRepository
    {
        IQueryable<Page> Pages { get;}
        Page GetPage(string path, string language);
        void SavePage(Page page);
        void Delete(int id);
    }
}
