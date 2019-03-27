using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SmartDealer.Models.Models.Customer;
using SmartDealer.Service.Services;

namespace SmartDealer.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerController : ControllerBase
    {
        private readonly ICustomerService custService;
        public CustomerController(ICustomerService service)
        {
            custService = service;
        }

        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<Customer>), 200)]
        public async Task<IActionResult> GetAllCustomers()
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var CustomerList =  await custService.GetCustomerListAsync();
            if (CustomerList != null)
            {
                return Ok(CustomerList);
            }
            else
            {
                return NotFound();
            }
        }

        [HttpPost]
        public async Task<IActionResult> CreateCustomers([FromBody]Customer customer)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
             await custService.CreateCustomerAsync(customer);

            return Ok();
            
        }
    }
}