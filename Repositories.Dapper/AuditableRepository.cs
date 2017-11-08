using System;
using System.Data;
using Shared;

namespace Repositories.Dapper
{
    public abstract class AuditableRepository<T> : Repository<T>
        where T : class, IAuditable<int>
    {
        
    }
    public abstract class AuditableRepository<T, K> : Repository<T, K>
        where T : class, IAuditable<K>
    {
		

        public override void Insert(T item)
        {
			item.CreatedAt = DateTime.UtcNow;
            base.Insert(item);

        }
     
        public override void Update(T item){
            item.LastModifiedAt = DateTime.UtcNow;
            base.Update(item);
        }

        public virtual void Insert(T item, string createdBy)
        {
            item.CreatedAt = DateTime.UtcNow;
            item.CreatedBy = createdBy;
            item.LastModifiedAt = DateTime.UtcNow;
            base.Insert(item);
        }
        public virtual void Update(T item, string updatedBy){
            item.LastModifiedAt = DateTime.UtcNow;
            item.LastModifiedBy = updatedBy;
            base.Update(item);
        }
    }
}
