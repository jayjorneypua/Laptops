using Laptops.Web.DTO;
using Laptops.Web.Models.Entities;

namespace Laptops.Web.Models
{
    public class LaptopUpdateViewModel
    {
        public LaptopUpdateRequestDTO LaptopUpdateRequestDTO { get; set; } = default!;
        public Laptop Laptop { get; set; } = default!;
    }
}
