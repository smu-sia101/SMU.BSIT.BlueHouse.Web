using System.ComponentModel.DataAnnotations;

namespace SMU.BSIT.BlueHouse.Web.Models.Admin
{
    public class ProductViewModel
    {
        public Guid Id { get; set; }

        [Required(ErrorMessage = "Product name is required.")]
        [StringLength(255, MinimumLength = 2, ErrorMessage = "Product name must be between 2 and 255 characters.")]
        public string Name { get; set; }

        public string Description { get; set; }

        [Range(0, short.MaxValue, ErrorMessage = "Stock must be a positive number.")]
        public short Stock { get; set; }

        [Required(ErrorMessage = "Price is required.")]
        [Range(0, 999999999, ErrorMessage = "Price must be a positive value.")]
        public double Price { get; set; }

        [Required(ErrorMessage = "Category is required.")]
        public string Category { get; set; }

        [Required(ErrorMessage = "Manufacturer is required.")]
        public string Manufacturer { get; set; }

        public IFormFile? CoverImage { get; set; }

        public string? CoverImageUrl { get; set; }
    }

}
