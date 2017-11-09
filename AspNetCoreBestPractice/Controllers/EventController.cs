using Microsoft.AspNetCore.Mvc;
using Shared;
using Shared.Models;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace AspNetCoreBestPractice.Controllers
{
    public class EventController : BaseController<IEventRepository, CalendarEvent>
    {
        public EventController(IEventRepository repo) : base(repo)
        {
        }
     
    }
}
