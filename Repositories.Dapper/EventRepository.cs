using System;
using System.Data;
using Shared;
using Shared.Models;

namespace Repositories.Dapper
{
    public class EventRepository : Repository<CalendarEvent>, IEventRepository
    {
        private readonly IDbConnection _conn;
        protected override IDbConnection Conn => _conn;
		

        public EventRepository(IDbConnection conn){
            _conn = conn;
        }

        public override bool Delete(int id)
        {
            throw new NotImplementedException();
        }
    }
}
