using System;
namespace Shared
{
    public interface IAuditable<T> : IIdentifiable<T> 
    {
        DateTime CreatedAt { get; set; }
        string CreatedBy { get; set; }
        string LastModifiedBy { get; set; }
        DateTime LastModifiedAt { get; set; }
    }
}
