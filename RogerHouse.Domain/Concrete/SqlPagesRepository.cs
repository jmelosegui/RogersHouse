using System;
using System.Data.Linq;
using System.Linq;
using RogerHouse.Domain.Abstract;
using RogerHouse.Domain.Entities;

namespace RogerHouse.Domain.Concrete
{
    public class SqlPagesRepository : IPagesRepository
    {
        private readonly Table<Page> _pagesTable;
        private readonly Table<PagesLanguages> _pagesLanguageTable;
        private readonly Table<Language> _LanguageTable;

        public SqlPagesRepository(string connectionString)
        {
            var dc = new DataContext(connectionString);
            _pagesTable = (dc).GetTable<Page>();
            _pagesLanguageTable = dc.GetTable<PagesLanguages>();
            _LanguageTable = dc.GetTable<Language>();
        }

        #region IPagesRepository Members

        public IQueryable<Page> Pages
        {
            get
            {
                return from p in _pagesTable
                       join pl in
                           (from pl1 in _pagesLanguageTable select pl1)
                           on p.PageId equals pl.PageId into sr
                       from x in sr.DefaultIfEmpty()
                       join l in _LanguageTable on ((x != null) ? x.LanguageId : 1) equals l.LanguageId //|| l.LanguageId == 1 
                       select new SitePage
                       {
                           PageId = p.PageId,
                           Path = p.Path,
                           Title = (x.Title ?? p.Title),
                           Body = (x.Body ?? p.Body),
                           Visible = p.Visible,
                           ShowInMenu = p.ShowInMenu,
                           Language = l.Culture,
                           Order = p.Order
                       };
            }
        }

        public Page GetPage(string path, string language)
        {
            return Pages.Where(p => p.Path == path && p.Language == language).Single();
        }

        public void SavePage(Page page)
        {
            if(page.PageId == 0)
            {
                _pagesTable.InsertOnSubmit(page);
            }
            else
            {
                _pagesTable.Attach(page);
                _pagesTable.Context.Refresh(RefreshMode.KeepCurrentValues, page);
            }

            _pagesTable.Context.SubmitChanges();

        }

        public void Delete(int id)
        {
            var page = _pagesTable.SingleOrDefault(p=> p.PageId == id);
            _pagesTable.DeleteOnSubmit(page);
            _pagesTable.Context.SubmitChanges();
        }
        
        #endregion
    }
}