using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Shared;
using Shared.Models;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace AspNetCoreBestPractice.Controllers
{
    [Route("api/[controller]")]
    public class CustomerController : Controller
    {
        public ICustomerRepository CustomerRepository { get; }

        public CustomerController(ICustomerRepository customerRepository){
            CustomerRepository = customerRepository;
        }


        // GET: api/values
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public Customer Get(int id)
        {
             return CustomerRepository.Get(id);
        }

        // POST api/values
        [HttpPost]
        public void Post([FromBody]Customer customer)
        {
            CustomerRepository.Insert(customer);
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody]Customer customer)
        {
            customer.Id = id; //just in case
            CustomerRepository.Update(customer);
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            
        }
    }
}
