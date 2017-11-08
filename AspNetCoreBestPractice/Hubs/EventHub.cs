using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR;
using Shared;
using Shared.Models;

namespace AspNetCoreBestPractice.Hubs
{
	public class EventHub : Hub
	{
		//readonly IEventRepository eventRepository;

		//public EventHub(IEventRepository eventRepository)
		//{
		//	this.eventRepository = eventRepository;
		//}

        public Task Connect(){
            var task = new Task(() =>
            {
				//do something

			});
            return task;
        }

		//public Task AddEvent(CalendarEvent calendarEvent)
		//{
		//	var task = new Task(() =>
		//	{
		//		this.eventRepository.Insert(calendarEvent);
		//	});
		//	task.Start();
		//	return task;
		//}

	}
}
