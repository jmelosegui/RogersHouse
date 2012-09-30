using System;
using System.Collections.Generic;
using System.Data.Linq;
using System.IO;
using System.Linq;
using System.Text;
using System.Transactions;
using RogerHouse.Domain.Abstract;
using RogerHouse.Domain.Entities;

namespace RogerHouse.Domain.Concrete
{
    public class SqlRoomsRepository : IRoomsRepository
    {
        private readonly Table<Room> roomsTable;
        private readonly Table<RoomPhoto> roomPhotoTable;

        public SqlRoomsRepository(string connectionString)
        {
            var dc = new DataContext(connectionString);
            roomsTable = dc.GetTable<Room>();
            roomPhotoTable = dc.GetTable<RoomPhoto>();
        }

        public IQueryable<Room> Rooms
        {
            get { return roomsTable; }
        }

        public IEnumerable<Room> RoomWithPhotos()
        {
            var result = new List<Room>();
            foreach (var room in roomsTable)
            {
                room.Photos = roomPhotoTable.Where(ph => ph.RoomId == room.RoomId).ToList();
                result.Add(room);
            }
            return result.AsReadOnly();
        }

        public IQueryable<RoomPhoto> RoomPhotos
        {
            get { return roomPhotoTable; }
        }

        public Room GetRoom(int id, string language)
        {
            throw new NotImplementedException();
        }

        public void SaveRoom(Room room)
        {
            if (room.RoomId == 0)
            {
                roomsTable.InsertOnSubmit(room);
            }
            else
            {
                roomsTable.Attach(room);
                roomsTable.Context.Refresh(RefreshMode.KeepCurrentValues, room);
            }

            roomsTable.Context.SubmitChanges();
        }

        public void SaveRoomPhoto(RoomPhoto roomphoto)
        {
            if (roomphoto.PhotoId == 0)
            {
                roomPhotoTable.InsertOnSubmit(roomphoto);
            }
            else
            {
                roomPhotoTable.Attach(roomphoto);
                roomPhotoTable.Context.Refresh(RefreshMode.KeepCurrentValues, roomphoto);
            }

            roomPhotoTable.Context.SubmitChanges();
        }

        public void DeleteRoom(int id)
        {
            using (var tx = new TransactionScope())
            {
                roomPhotoTable.DeleteAllOnSubmit(roomPhotoTable.Where(p => p.RoomId == id));
                roomPhotoTable.Context.SubmitChanges();
                roomsTable.DeleteAllOnSubmit(roomsTable.Where(r => r.RoomId == id));
                roomsTable.Context.SubmitChanges();

                tx.Complete();
            }
        }

        public void DeleteRoomPhoto(int id)
        {
            var photo = roomPhotoTable.Where(p => p.PhotoId == id).SingleOrDefault();
            if (photo != null)
            {
                roomPhotoTable.DeleteOnSubmit(photo);
                roomPhotoTable.Context.SubmitChanges();
            }
        }

    }
}
