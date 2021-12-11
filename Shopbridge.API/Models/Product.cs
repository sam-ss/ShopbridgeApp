using System;
using System.ComponentModel.DataAnnotations;

namespace Shopbridge.API.Models
{
    public class Product
    {
        [Key]
        public Guid ProductId { get; set; }

        [Required(ErrorMessage = "This field is required")]
        [Range(1, int.MaxValue, ErrorMessage = "Your input is invalid")]
        public int ProductNumber { get; set; }

        [Required(ErrorMessage = "This field is required")]
        [MaxLength(100, ErrorMessage = "Product Name cannot exceed 100 characters")]
        public string Name { get; set; }

        [Required(ErrorMessage = "This field is required")]
        [MaxLength(500, ErrorMessage = "Product Description cannot exceed 500 characters")]
        public string Description { get; set; }

        [Required(ErrorMessage = "This field is required")]
        [Range(0.01, double.MaxValue, ErrorMessage = "Your input is invalid")]
        public double Price { get; set; }

        [Required(ErrorMessage = "This field is required")]
        [Range(1, int.MaxValue, ErrorMessage = "Your input is invalid")]
        public int Quantity { get; set; }
    }
}
