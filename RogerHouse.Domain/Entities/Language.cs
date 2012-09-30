using System.ComponentModel.DataAnnotations;
using System.Data.Linq.Mapping;

namespace RogerHouse.Domain.Entities
{
    [Table(Name = "Languages")]
    public class Language
    {
        [Column(IsPrimaryKey = true, IsDbGenerated = true, AutoSync = AutoSync.OnInsert)]
        public int LanguageId { get; set; }

        [Column, Required, StringLength(5)]
        public string Culture { get; set; }

        [Column, Required, StringLength(50)]
        public string Name { get; set; }
    }
}