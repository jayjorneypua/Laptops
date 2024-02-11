using System.ComponentModel;

namespace Laptops.Web.Models
{
    public class AddLaptopViewModel
    {
        public string? Name { get; set; }
        public string? Description { get; set; }
        public string? Brand { get; set; }
        public string? Price { get; set; }
        [DisplayName("Available?")]
        public bool Available { get; set; }
    }
}
