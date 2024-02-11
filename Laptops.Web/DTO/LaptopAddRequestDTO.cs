using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace Laptops.Web.DTO
{
    public class LaptopAddRequestDTO
    {
        
        public string? Name { get; set; }
        [Required]
        [DisplayName("Product Description")]
        public string? Description { get; set; }
        [Required]
        public string? Brand { get; set; }
        [Required]
        [Precision(8, 2)] //000000.00
        public decimal? Price { get; set; }
        [Required]
        [DisplayName("Available?")]
        public bool Available { get; set; }
        [ValidateNever]
        public IFormFile? Photo { get; set; }
        
    }
}
