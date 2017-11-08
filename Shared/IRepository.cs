using System;
using System.Collections.Generic;

namespace Shared
{
    public interface IRepository<T, ID>
    {
        void Insert(T item);
        void Update(T item);
        void Save(T item);


        T Get(ID id);
        IEnumerable<T> FindAll();
        bool Delete(T item);
    }
    public interface IRepository<T>: IRepository<T, int>{      
    }
}
