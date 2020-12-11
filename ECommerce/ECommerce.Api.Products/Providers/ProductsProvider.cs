using AutoMapper;
using ECommerce.Api.Products.DB;
using ECommerce.Api.Products.Interface;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ECommerce.Api.Products.Providers
{
    public class ProductsProvider : IProductsProvider
    {
        private readonly ProductsDbContext dbContext;
        private readonly ILogger<ProductsProvider> logger;
            private readonly IMapper mapper;

        public ProductsProvider(ProductsDbContext dbContext, ILogger<ProductsProvider> logger, IMapper mapper)
        {
            this.dbContext = dbContext;
            this.logger = logger;
            this.mapper = mapper;
            SeedData();

        }

        private void SeedData()
        {
            if (!dbContext.Products.Any()) {

                dbContext.Products.Add(new DB.Product() {Id=1, Name="Keyboard", Price=20, Inventory=100 });
                dbContext.Products.Add(new DB.Product() { Id = 2, Name = "Mouse", Price = 20, Inventory = 100 });
                dbContext.Products.Add(new DB.Product() { Id = 3, Name = "Monitor", Price = 20, Inventory = 100 });
                dbContext.Products.Add(new DB.Product() { Id = 4, Name = "CPU", Price = 20, Inventory = 100 });
                dbContext.SaveChanges();
            }
        }

        public async Task<(bool IsSuccess, Product Product, string ErrorMessage)> GetProductsAsync(int id)
        {
            try
            {
                logger?.LogInformation($"Querying products with id: {id}");
                var product = await dbContext.Products.FirstOrDefaultAsync(p => p.Id == id);
                if (product != null)
                {
                    logger?.LogInformation("Product found");
                    var result = mapper.Map<Product>(product);
                    return (true, result, null);
                }
                return (false, null, "Not found");
            }
            catch (Exception ex)
            {
                logger?.LogError(ex.ToString());
                return (false, null, ex.Message);
            }
        }
        public async Task<(bool IsSuccess, IEnumerable<Models.Product> Products, string ErrorMessage)> GetProductsAsync()
        {
            try
            {
                var  products = await dbContext.Products.ToListAsync();

                if (products != null && products.Any()) { 

                        var result=mapper.Map<IEnumerable<DB.Product>, IEnumerable<Models.Product>>(products);

                        return (true, result, null);
                    }
       
             return (false, null, "Not found");
        }
            catch (Exception ex )
            {
                logger.LogError(ex.ToString());

                return (false, null, ex.Message);
            }

           
        }
    }
}
