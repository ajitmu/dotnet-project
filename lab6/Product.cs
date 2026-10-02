using System.ComponentModel.DataAnnotations;

namespace product.Models
{
    public class Product
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Product Name is required")]
        [StringLength(100, ErrorMessage = "Name cannot exceed 100 characters")]
        public string Name { get; set; } = string.Empty;

        [Required(ErrorMessage = "Category is required")]
        public string Category { get; set; } = string.Empty;

        [Required(ErrorMessage = "Price is required")]
        [Range(0.01, 10000.00, ErrorMessage = "Price must be between 0.01 and 10000")]
        public decimal Price { get; set; }

        public string Description { get; set; } = string.Empty;

        public bool IsInStock { get; set; } = true;

        public string ImageUrl { get; set; } = "https://via.placeholder.com/300x200?text=Product+Image";
    }
}