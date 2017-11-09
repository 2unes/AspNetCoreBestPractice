using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Shared.Models
{
    [Table("customer")]
    public class Customer : AuditableRecord<int>
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Phone { get; set; }
    }
}
