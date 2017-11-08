using System;
namespace Shared.Models
{
    public class Customer : AuditableRecord<int>
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Phone { get; set; }
    }
}
