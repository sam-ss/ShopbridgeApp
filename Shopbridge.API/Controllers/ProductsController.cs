using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Shopbridge.API.Interfaces.ProductData;
using Shopbridge.API.Models;
using System;
using System.Linq;

namespace Shopbridge.API.Controllers
{
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private IProductData _productData;

        public ProductsController(IProductData productData)
        {
            _productData = productData;
        }

        [HttpGet]
        [Route("api/[controller]")]
        public IActionResult GetProducts()
        {
            return Ok(_productData.GetProducts());
        }

        [HttpGet]
        [Route("api/[controller]/{id}")]
        public IActionResult GetProduct(Guid id)
        {
            var product = _productData.GetProduct(id);
            if (product != null)
            {
                return Ok(_productData.GetProduct(id));
            }
            else
            {

            }
            return NotFound($"Product with Id: {id} was not found!");
        }

        [HttpPost]
        [Route("api/[controller]")]
        public IActionResult AddProduct(Product product)
        {
            var existingProduct = _productData.GetProducts().Where(x => x.ProductNumber == product.ProductNumber).SingleOrDefault();
            if (existingProduct == null)
            {
                _productData.AddProduct(product);
                return Created(HttpContext.Request.Scheme + "://" + HttpContext.Request.Host + HttpContext.Request.Path + product.ProductId, product);
            }
            else
            {
                return StatusCode(405, "This Product Number already exists");
            }
        }

        [HttpDelete]
        [Route("api/[controller]/{id}")]
        public IActionResult DeleteProduct(Guid id)
        {
            var product = _productData.GetProduct(id);

            if (product != null)
            {
                _productData.DeleteProduct(product);
                return Ok($"Product Number: {product.ProductNumber} was deleted successfully");
            }
            else
            {
                return NotFound($"Product with Id: {id} was not found!");
            }
        }

        [HttpPatch]
        [Route("api/[controller]/{id}")]
        public IActionResult EditProduct(Guid id, Product product)
        {
            var existingProduct = _productData.GetProduct(id);

            if (existingProduct != null)
            {
                var duplicateProductNumber = _productData.GetProducts().Where(x => x.ProductNumber == product.ProductNumber && x.ProductId != product.ProductId).SingleOrDefault();
                if (duplicateProductNumber == null)
                {
                    product.ProductId = existingProduct.ProductId;
                    _productData.EditProduct(product);
                    return Ok("Product " + product.Name + " edited Successfully");
                }
                else
                {
                    return StatusCode(405, "This Product Number already exists");
                }
            }
            else
            {
                return NotFound($"Product with Id: {id} was not found!");
            }
        }
    }
}
