using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data.Linq;
using System.Data.Linq.Mapping;
using System.Linq;

namespace RogerHouse.Domain.Entities
{
    [Table(Name = "Rooms")]
    public class Room
    {
        [Column(IsPrimaryKey = true, IsDbGenerated = true, AutoSync = AutoSync.OnInsert)]
        public int RoomId { get; set; }

        [Column, Required, StringLength(17)]
        public string Name { get; set; }

        [Column]
        public decimal Price { get; set; }

        [Column, Display(Name = "Price Description", Description = "(Ex:. €/month,  for two persons, etc...)"), StringLength(17)]
        public string PriceDescription { get; set; }

        [Column, Required]
        public int Size { get; set; }

        [Column, Required, StringLength(35)]
        public string Location { get; set; }

        [Column, Required]
        public string Description { get; set; }

        private List<RoomPhoto> photos;
        public List<RoomPhoto> Photos
        {
            get
            {
                if (photos == null)
                    photos = new List<RoomPhoto>();
                return photos;
            }
            set { photos = value; }
        }
    }
}