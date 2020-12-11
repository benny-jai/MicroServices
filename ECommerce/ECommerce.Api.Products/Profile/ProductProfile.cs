using ECommerce.Api.Products.DB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace ECommerce.Api.Products.Profile
{
    public class ProductProfile : AutoMapper.Profile
    {
        public ProductProfile()
        {
            //CreateMap<>();
          CreateMap<Product, Models.Product>();
        }
    }
}
