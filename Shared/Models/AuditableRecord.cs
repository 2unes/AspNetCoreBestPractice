using System;
namespace Shared.Models
{
    public class AuditableRecord<T> : IAuditable<T>
    {
        public T Id { get; set; }
        public DateTime CreatedAt { get; set; }
        public string CreatedBy { get; set; }
        public string LastModifiedBy { get; set; }
        public DateTime LastModifiedAt { get; set; }
    }
}
