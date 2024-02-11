using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Laptops.Web.Models.Entities
{
    //An entity is basically a class with unique ID
    public class Laptop
    {
        //Properties that will represent this identity
        [Key]
        public int Id { get; set; }
        [Required]
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
        [DisplayName("Upload Image")]
        public string? ImageUrl { get; set; }

    } // we will use this entities in the ApplicationDbContext
}
