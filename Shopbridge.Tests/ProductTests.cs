using Microsoft.EntityFrameworkCore;
using NUnit.Framework;
using Shopbridge.API.Models;
using Shopbridge.API.ProductData;
using System.Collections.Generic;

namespace Shopbridge.Tests
{
    [TestFixture]
    public class ProductTests
    {
        private static readonly DbContextOptions<ProductContext> dbContextOptions = new DbContextOptionsBuilder<ProductContext>()
            .UseInMemoryDatabase(databaseName: "ShopbridgeDb")
            .Options;

        ProductContext context;

        [OneTimeSetUp]
        public void Setup()
        {
            context = new ProductContext(dbContextOptions);
            context.Database.EnsureCreated();
            SeedData();
        }

        [Test]
        public void CanFetchAllProducts()
        {
            var dbProductData = new DbProductData(context);

            var result = dbProductData.GetProducts();

            Assert.IsInstanceOf<List<Product>>(result);
        }

        [Test]
        public void CanFetchSingleProduct_GivenProductId_ReturnsExistingProduct()
        {
            var dbProductData = new DbProductData(context);

            var result = dbProductData.GetProduct(new System.Guid("1F8A8F5B-1863-4F41-A4BE-4B4A5498FBA8"));

            Assert.IsTrue(result != null && result.ProductNumber == 2);
        }

        [Test]
        public void CanFetchSingleProduct_GivenProductId_ReturnsFalse()
        {
            var dbProductData = new DbProductData(context);

            var result = dbProductData.GetProduct(new System.Guid("c83f3ce2-658e-4e9b-bae7-17735ea42971"));

            Assert.IsTrue(result == null);
        }

        [Test]
        public void CanAddProduct_GivenProduct_ReturnsProductBack()
        {
            var dbProductData = new DbProductData(context);

            var product = new Product()
            {
                ProductId = new System.Guid("bf40c6cb-5722-4f17-c5f1-1d36f5dcfb53"),
                ProductNumber = 7,
                Name = "Product7",
                Description = "This is the seventh product",
                Price = 59.99,
                Quantity = 4
            };
            var result = dbProductData.AddProduct(product);

            Assert.IsTrue(result == product);
        }

        [Test]
        public void CanDeleteProduct_GivenProduct_ReturnsProductIdBack()
        {
            var dbProductData = new DbProductData(context);

            var id = new System.Guid("7A50242F-1777-4A70-9731-9C774966D95E");
            Product product = context.Products.Find(id);
            var result = dbProductData.DeleteProduct(product);

            Assert.IsTrue(result == id);
        }


        [Test]
        public void CanEditProduct_GivenProductId_ReturnsProductBack()
        {
            var dbProductData = new DbProductData(context);

            var id = new System.Guid("95571D2E-2C8D-4FD1-A753-6039DC466E4A");
            Product product = context.Products.Find(id);
            product.Name = "Edited Product1";
            product.ProductNumber = 5;
            product.Description = "This is the edited first product";
            product.Price = 69.99;
            product.Quantity = 100;
            var result = dbProductData.EditProduct(product);

            Assert.IsTrue(result == product);
        }

        [Test]
        public void CanEditProduct_GivenProductId_ReturnsErrorBack()
        {
            var dbProductData = new DbProductData(context);

            var id = new System.Guid("83243548-AAEE-47AD-9975-8AC11489F320");
            Product existingProduct = context.Products.Find(id);
            Product product = existingProduct;
            product.Name = "Edited Product4";
            product.ProductNumber = 6;
            product.Description = "This is the edited fourth product";
            product.Price = 69.99;
            product.Quantity = 100;
            context.Products.Remove(existingProduct);
            context.SaveChanges();
            var result = dbProductData.EditProduct(product);

            Assert.IsTrue(result != product);
        }

        [OneTimeTearDown]
        public void Clear()
        {
            context.Database.EnsureDeleted();
        }

        private void SeedData()
        {
            var productData = new List<Product>()
            {
                new Product()
                {
                    ProductId = new System.Guid("95571D2E-2C8D-4FD1-A753-6039DC466E4A"),
                    ProductNumber = 1,
                    Name = "Product1",
                    Description = "First product Desctiption",
                    Price = 85.50,
                    Quantity = 48
                },
                new Product()
                {
                    ProductId = new System.Guid("1F8A8F5B-1863-4F41-A4BE-4B4A5498FBA8"),
                    ProductNumber = 2,
                    Name = "Product2",
                    Description = "This is the second product",
                    Price = 66.00,
                    Quantity = 40
                },
                new Product()
                {
                    ProductId = new System.Guid("7A50242F-1777-4A70-9731-9C774966D95E"),
                    ProductNumber = 3,
                    Name = "Product3",
                    Description = "This is the third product",
                    Price = 145.00,
                    Quantity = 35
                },
                new Product()
                {
                    ProductId = new System.Guid("83243548-AAEE-47AD-9975-8AC11489F320"),
                    ProductNumber = 4,
                    Name = "Product4",
                    Description = "This is the fourth product",
                    Price = 88.05,
                    Quantity = 20
                }
            };
            context.Products.AddRange(productData);
            context.SaveChanges();
        }
    }
}
