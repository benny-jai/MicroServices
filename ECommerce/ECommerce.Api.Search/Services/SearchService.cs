﻿using ECommerce.Api.Search.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ECommerce.Api.Search.Services
{
    public class SearchService : ISearchService
    {
        private readonly IOrdersService ordersService;
        private readonly IProductsService productsService;
        public SearchService(IOrdersService ordersService, IProductsService productsService)
        {
            this.ordersService = ordersService;
            this.productsService = productsService;
        }
        public async Task<(bool IsSuccess, dynamic SearchResults)> SearchAsync(int CustomerId)
        {
            var ordersResult = await ordersService.GetOrdersAsync(CustomerId);
            var productResult = await productsService.GetProductsAsync();
            foreach (var order in ordersResult.Orders)
            {
                foreach (var item in order.Items)
                {
                    item.ProductName = productResult.IsSuccess ? productResult.Products.FirstOrDefault(p => p.Id == item.ProductId)?.Name: "Product Information is not available.";
                }
            }
            if (ordersResult.IsSuccess)
            {
                var result = new
                {
                    Orders = ordersResult.Orders
                };
                return (true, result);
            }
            return (false, null);

        }
    }
}
