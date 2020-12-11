using AutoMapper;
using ECommerce.APi.Customers.Db;
using ECommerce.APi.Customers.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ECommerce.APi.Customers.Provider
{
    public class CustomerProvider : ICustomerProvider
    { 
        private readonly CustomersDbContext dbContext;
    private readonly ILogger<CustomerProvider> logger;
    private readonly IMapper mapper;


        public CustomerProvider(CustomersDbContext dbContext, ILogger<CustomerProvider> logger, IMapper mapper)
        {
            this.dbContext = dbContext;
            this.logger = logger;
            this.mapper = mapper;
            SeedData();

        }


        private void SeedData()
        {
            if (!dbContext.Customers.Any())
            {

                dbContext.Customers.Add(new Db.Customer() { Id = 1, Name = "Benny", Address= "Westmead" });
                dbContext.Customers.Add(new Db.Customer() { Id = 2, Name = "Rathinm", Address = "Westmead" });
                dbContext.Customers.Add(new Db.Customer() { Id = 3, Name = "Ramu", Address = "Westmead" });
                
                dbContext.SaveChanges();
            }
        }
        
        public async Task<(bool IsSuccess, IEnumerable<Models.Customer> Customers, String ErrorMMessage)> GetCustomerAsync()
        {
            try
            {
                var customers = await dbContext.Customers.ToListAsync();

                if (customers != null && customers.Any())
                {

                    var result = mapper.Map<IEnumerable<Db.Customer>, IEnumerable<Models.Customer>>(customers);

                    return (true, result, null);
                }

                return (false, null, "Not found");
            }
            catch (Exception ex)
            {
                logger.LogError(ex.ToString());

                return (false, null, ex.Message);
            }


        }

        
    }
}
