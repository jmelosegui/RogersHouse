using System.Collections.Generic;
using System.Linq;
using RogerHouse.Domain.Entities;

namespace RogerHouse.Domain.Abstract
{
    public interface IRoomsRepository
    {
        IQueryable<Room> Rooms { get; }
        IQueryable<RoomPhoto> RoomPhotos { get; }
        IEnumerable<Room> RoomWithPhotos();
        Room GetRoom(int id, string language);
        void SaveRoom(Room room);
        void SaveRoomPhoto(RoomPhoto roomphoto);
        void DeleteRoom(int id);
        void DeleteRoomPhoto(int id);
    }
}