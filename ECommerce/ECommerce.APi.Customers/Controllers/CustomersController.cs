using ECommerce.APi.Customers.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ECommerce.APi.Customers.Controllers
{ [ApiController]
    [Route("/api/Customers")]
    public class CustomersController : ControllerBase
    {
        private readonly ICustomerProvider customerProvider;

        public CustomersController(ICustomerProvider customersProvider)
        {
            this.customerProvider = customersProvider;
        }

        [HttpGet]
        public async Task<IActionResult>GetCustomerAsync(){

            var result = await customerProvider.GetCustomerAsync();
            if (result.IsSuccess) {

                return Ok(result.Customers);
            
            }
            return NotFound();

}
    }
}
