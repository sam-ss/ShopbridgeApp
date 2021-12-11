using Shopbridge.API.Models;
using System;
using System.Collections.Generic;

namespace Shopbridge.API.Interfaces.ProductData
{
    public interface IProductData
    {
        List<Product> GetProducts();

        Product GetProduct(Guid id);

        Product AddProduct(Product product);

        Guid DeleteProduct(Product product);

        Product EditProduct(Product product);

    }
}
