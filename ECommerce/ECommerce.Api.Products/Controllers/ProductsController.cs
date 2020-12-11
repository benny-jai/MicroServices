using ECommerce.Api.Products.Interface;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ECommerce.Api.Products.Controllers
{
    [ApiController]
    [Route("api/Products")]

    public class ProductsController : ControllerBase
    {
        private readonly IProductsProvider productsProvider;
        public ProductsController(IProductsProvider productsProvider)
        {
            this.productsProvider = productsProvider;
        }
        [HttpGet]
        public async Task<IActionResult> GetProductAsync()
        {
            var result = await productsProvider.GetProductsAsync();
            if (result.IsSuccess)
            {
                return Ok(result.Products);

            }
            return NotFound();
        }
    }
    
}
