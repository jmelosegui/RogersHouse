using System;
using System.Data.Linq.Mapping;
using System.ComponentModel.DataAnnotations;
namespace RogerHouse.Domain.Entities
{
    [Table(Name = "Pages")]
    public class Page
    {
        [Column(IsPrimaryKey = true, IsDbGenerated = true, AutoSync = AutoSync.OnInsert)]
        public int PageId { get; set; }
        [Column, Required]
        public string Title { get; set; }
        [Column, Required]
        public string Body { get; set; }
        [Column, Required]
        public string Path { get; set; }
        [Column]
        public bool Visible { get; set; }
        [Column, Display(Name = "Show in menu", Description = "Check this option if you want the page title appears on the site menu.")]
        public bool ShowInMenu { get; set; }
        [Column, Required]
        public int Order { get; set; }
        public string Language { get; set; }

    }

    [Table(Name = "PagesLanguages")]
    public class PagesLanguages
    {
        [Column]
        public int PageId { get; set; }
        [Column, Required]
        public string Title { get; set; }
        [Column, Required]
        public string Body { get; set; }
        [Column]
        public int LanguageId { get; set; }
    }

    public class SitePage : Page{}
 }