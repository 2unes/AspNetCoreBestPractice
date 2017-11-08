using Microsoft.AspNetCore.Mvc;
using Shared;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace AspNetCoreBestPractice.Controllers
{
    public class EventController : Controller
    {
        readonly IEventRepository _eventRepository;
       
        public EventController(IEventRepository eventRepository)
        {
            _eventRepository = eventRepository;
        }

        // GET: /<controller>/
        public IActionResult Index()
        {
            return Json(new { success = true });
        }

        public IActionResult List(){
            var events = _eventRepository.FindAll();
            return Json(events);
        }
    }
}
