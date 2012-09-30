using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data.Linq;
using System.Linq;
using System.Text;
using System.Data.Linq.Mapping;

namespace RogerHouse.Domain.Entities
{
    [Table(Name = "RoomPhotos")]
    public class RoomPhoto
    {

        [Column(IsPrimaryKey = true, IsDbGenerated = true, AutoSync = AutoSync.OnInsert)]
        public int PhotoId { get; set; }

        [Column]
        public int RoomId { get; set; }

        [Column, Required, StringLength(12)]
        public string FileName { get; set; }

        [Column, StringLength(25)]
        public string Description { get; set; }

    }
}
