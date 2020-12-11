using ECommerce.Api.Products.DB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ECommerce.Api.Products.Interface
{
    public interface IProductsProvider
    {
        Task<(bool IsSuccess, IEnumerable<Models.Product> Products, String ErrorMessage)> GetProductsAsync();
        Task<(bool IsSuccess, Product Product, string ErrorMessage)> GetProductsAsync(int id);
    }
}
