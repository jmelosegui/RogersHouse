using System.Web.Mvc;
using RogerHouse.Domain.Abstract;

namespace RogersHouse.WebUI.Controllers
{
    public class RoomsController : Controller
    {
        private readonly IRoomsRepository _roomsRepository;

        public RoomsController(IRoomsRepository roomRepository)
        {
            _roomsRepository = roomRepository;
        }

        public ActionResult Index()
        {
            return View(_roomsRepository.RoomWithPhotos());
        }
    }
}