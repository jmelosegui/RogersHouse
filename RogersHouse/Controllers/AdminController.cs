using System.Web.Mvc;
using RogerHouse.Domain.Abstract;

namespace RogersHouse.WebUI.Controllers
{
    [Authorize]
    public partial class AdminController : ControllerBase
    {
        private readonly IPagesRepository _repository;
        private readonly IRoomsRepository _roomsRepository;

        public AdminController(IPagesRepository pageRepository, IRoomsRepository roomRepository)
        {
            _roomsRepository = roomRepository;
            _repository = pageRepository;
        }

        public ActionResult Index()
        {
            return View();
        }
    }
}