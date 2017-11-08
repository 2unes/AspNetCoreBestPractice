using System;
using System.Collections.Generic;
using System.Data;
using Dapper;
using Shared;
using Shared.Models;

namespace Repositories.Dapper
{
    public class CustomerRepository : Repository<Customer>, ICustomerRepository
    {
        private readonly IDbConnection _conn;
        public CustomerRepository(IDbConnection conn)
        {
            _conn = conn;
        }

        protected override IDbConnection Conn => _conn;

        public override bool Delete(int id)
        {
            try{    
                 Conn.Execute("delete from Customer where id = @id", new {id});
            }catch(Exception){
                return false;
            }
            return true;

        }
        public override bool Delete(Customer item)
        {
            return base.Delete(item);
        }

        public IEnumerable<Customer> FindByName(string name)
        {
            return Conn.Query<Customer>("select * from customer where name = @name order by name", new { name });
        }

        public IEnumerable<Customer> FindFuzzy(string contains){
            return Conn.Query<Customer>("select * from customer where name like @compare", new { compare = "%" + contains + "%" });
        }
    }
}
