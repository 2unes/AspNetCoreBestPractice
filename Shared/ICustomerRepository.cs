using System.Collections.Generic;
using Shared.Models;

namespace Shared
{
    public interface ICustomerRepository : IRepository<Customer>
    {
        IEnumerable<Customer> FindByName(string name);
        IEnumerable<Customer> FindFuzzy(string contains);
    }
}
