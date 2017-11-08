using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Shared.Models
{
    [Table("calendarevent")]
    public class CalendarEvent : IIdentifiable<int>
    {
        public int Id { get; set; }
        public DateTime Date { get; set; }
        public string Name { get; set; }
    }
}
