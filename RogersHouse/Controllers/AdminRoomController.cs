using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using RogerHouse.Domain.Entities;
using RogersHouse.WebUI.Infrastructure;

namespace RogersHouse.WebUI.Controllers
{
    public partial class AdminController
    {
        public ActionResult Rooms()
        {
            return View(_roomsRepository.Rooms);
        }

        public ActionResult NewRoom()
        {
            return View(new Room());
        }

        [HttpPost]
        public ActionResult NewRoom(string languageSelector, Room room)
        {
            if (languageSelector != null)
            {
                ModelState.Clear();
                return View(room);
            }
            if (ModelState.IsValid)
            {
                _roomsRepository.SaveRoom(room);
                return RedirectToAction("Rooms");
            }
            return View(room);
        }

        public ActionResult EditRoom(int id)
        {
            return View(_roomsRepository.Rooms.Single(r => r.RoomId == id));
        }

        [HttpPost]
        public ActionResult EditRoom(string languageSelector, Room room)
        {
            if (languageSelector != null)
            {
                ModelState.Clear();
                return View(room);
            }
            if (ModelState.IsValid)
            {
                room.Description = HttpUtility.HtmlDecode(room.Description);
                _roomsRepository.SaveRoom(room);
                return RedirectToAction("Rooms");
            }
            return View(room);
        }

        public ActionResult DeleteRoom(int id)
        {
            return View(_roomsRepository.Rooms.Single(r => r.RoomId == id));
        }

        [HttpPost]
        public ActionResult DeleteRoom(Room room)
        {
            var files = _roomsRepository.RoomPhotos.Where(p => p.RoomId == room.RoomId).Select(p => p.FileName).ToList();

            _roomsRepository.DeleteRoom(room.RoomId);

            try
            {
                foreach (var file in files)
                {
                    var physicalPath = Path.Combine(Server.MapPath("~/RoomsPhotos/"), file);
                    if(System.IO.File.Exists(physicalPath))
                    {
                        System.IO.File.Delete(physicalPath);
                    }
                }
            }
            catch(Exception x)
            {
                Logger.Error(x);
            }
            
            return RedirectToAction("Rooms");
        }

        public ActionResult SavePhotos(IEnumerable<HttpPostedFileBase> photos, int id)
        {
            try
            {
                Logger.Info("pase");
                var lista = _roomsRepository.RoomPhotos.Where(rp => rp.RoomId == id).Select(rp => rp.FileName).ToList();
                int startId = (lista.Any()) ? lista.Select(s => int.Parse(Path.GetFileNameWithoutExtension(s).Replace(String.Format("Room{0}_", id), ""))).Max() + 1 : 1;
                var manager = new ImageManager();
                foreach (var file in photos)
                {
                    if (file != null && file.ContentLength>0)
                    {
                        startId++;
                        // Some browsers send file names with full path. This needs to be stripped.
                        var ext = Path.GetExtension(file.FileName);
                        var fileName = String.Format("Room{0}_{1}{2}", id, startId, ext);
                        var physicalPath = Path.Combine(Server.MapPath("~/RoomsPhotos/{0}/"), fileName);
                        _roomsRepository.SaveRoomPhoto(new RoomPhoto { RoomId = id, FileName = fileName });
                        manager.ResizeImage(file.InputStream).Save(String.Format(physicalPath, "images"));
                        manager.CreateThumbs(file.InputStream).Save(String.Format(physicalPath, "thumbs"));
                    }
                }
                
                // Return an empty string to signify success
                return RedirectToAction("RoomPhotos", new { id });
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw;
            }
        }

        public ActionResult RemovePhotos(int id)
        {
            // The parameter of the Remove action must be called "fileNames"
            var photo = _roomsRepository.RoomPhotos.Where(p => p.PhotoId == id).Single();
            var physicalPath = Path.Combine(Server.MapPath("~/RoomsPhotos/"), photo.FileName);

            if (System.IO.File.Exists(physicalPath))
            {
                // The files are not actually removed in this demo
                System.IO.File.Delete(physicalPath);
            }
            _roomsRepository.DeleteRoomPhoto(id);
            // Return an empty string to signify success
            return RedirectToAction("RoomPhotos", new { id = photo.RoomId });
        }

        public ViewResult RoomPhotos(int id)
        {
            var photos = _roomsRepository.RoomPhotos.Where(rp => rp.RoomId == id);
            ViewBag.RoomId = id;
            return View(photos);
        }

        public ViewResult NewInputFileControl()
        {
            return View("_PhotoInputFileControl");
        }
    }
}