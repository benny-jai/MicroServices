using ECommerce.APi.Customers.Db;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ECommerce.APi.Customers.Interfaces
{
    public interface ICustomerProvider
    {
        Task<(bool IsSuccess, IEnumerable<Models.Customer> Customers, String ErrorMMessage)> GetCustomerAsync();
    }
}
