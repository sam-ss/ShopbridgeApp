using Shopbridge.API.Interfaces.ProductData;
using Shopbridge.API.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Shopbridge.API.ProductData
{
    public class DbProductData : IProductData
    {
        private ProductContext _productContext;

        public DbProductData(ProductContext productContext)
        {
            _productContext = productContext;
        }
        public Product AddProduct(Product product)
        {
            var existingCustomer = _productContext.Products.Where(x => x.ProductNumber == product.ProductNumber).SingleOrDefault();
            if (existingCustomer == null)
            {
                product.ProductId = Guid.NewGuid();
                _productContext.Products.Add(product);
                _productContext.SaveChanges();
            }
            else
            {
                product = new Product();
            }
            return product;
        }

        public Guid DeleteProduct(Product product)
        {
            var id = product.ProductId;
            _productContext.Products.Remove(product);
            _productContext.SaveChanges();
            return id;
        }

        public Product EditProduct(Product product)
        {
            var existingProduct = _productContext.Products.Find(product.ProductId);

            if (existingProduct != null)
            {
                var existingCustomer = _productContext.Products.Where(x => x.ProductNumber == product.ProductNumber).SingleOrDefault();
                if (existingCustomer != null)
                {
                    existingProduct.Name = product.Name;
                    existingProduct.ProductNumber = product.ProductNumber;
                    existingProduct.Description = product.Description;
                    existingProduct.Price = product.Price;
                    existingProduct.Quantity = product.Quantity;
                    _productContext.Products.Update(existingProduct);
                    _productContext.SaveChanges();
                }
                else
                {
                    return (new Product());
                }
                return product;
            }
            else
            {
                return (new Product());
            }
        }

        public Product GetProduct(Guid id)
        {
            return _productContext.Products.FirstOrDefault(x => x.ProductId == id);
        }

        public List<Product> GetProducts()
        {
            return _productContext.Products.ToList();
        }
    }
}
