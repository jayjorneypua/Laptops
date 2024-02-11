using Laptops.Web.Data;
using Laptops.Web.DTO;
using Laptops.Web.Models;
using Laptops.Web.Models.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Laptops.Web.Controllers
{
    public class LaptopsController : Controller
    {
        private readonly ApplicationDbContext dbContext;
        private readonly IWebHostEnvironment _hostingEnvironment;

        //inject the dbcontext class in this LaptopsController to update database using properties
        public LaptopsController(ApplicationDbContext dbContext, IWebHostEnvironment environment)
        {
            this.dbContext = dbContext;
            _hostingEnvironment = environment;
        }

        [HttpGet]
        public async Task<IActionResult> List()
        {
            List<Laptop> laptops = await dbContext.LaptopTable.ToListAsync();
            return View(laptops);
        }

        [HttpGet]
        public IActionResult Add()
        {
            return View(); // return View() will return the page back to the view folder
        }

        [HttpPost] // Once the submit is clicked in the view of Add, it will be send here at the httpPost
        public async Task<IActionResult> Add(LaptopAddRequestDTO laptopAddRequestDTO)
        {
            var laptop = new Laptop
            {
                Name = laptopAddRequestDTO.Name,
                Description = laptopAddRequestDTO.Description,
                Brand = laptopAddRequestDTO.Brand,
                Price = laptopAddRequestDTO.Price,
                Available = laptopAddRequestDTO.Available,
            };

            if (ModelState.IsValid)
            {
                string wwwRootPath = _hostingEnvironment.WebRootPath;
                if (laptopAddRequestDTO.Photo != null)
                {
                    string fileName = Guid.NewGuid().ToString() + Path.GetExtension(laptopAddRequestDTO.Photo.FileName);
                    string productPath = Path.Combine(wwwRootPath, @"images");

                    using (var fileStream = new FileStream(Path.Combine(productPath, fileName), FileMode.Create))
                    {
                        laptopAddRequestDTO.Photo.CopyTo(fileStream);
                    }

                    laptop.ImageUrl = @"\images\" + fileName;
                }

                await dbContext.LaptopTable.AddAsync(laptop);
                await dbContext.SaveChangesAsync();
                TempData["success"] = "Item added successfully!";
                return RedirectToAction("List");
            }

            return View();
        }

        [HttpGet]
        public IActionResult Update(int? id)
        {
            if (id == 0 || id == null)
            {
                return NotFound();
            }

            Laptop? laptopFromDb = dbContext.LaptopTable.Find(id);

            if (laptopFromDb == null)
            {
                return NotFound();
            }

            TempData["ImageUrl"] = laptopFromDb.ImageUrl;

            var laptopUpdateRequestDTO = new LaptopUpdateRequestDTO()
            {
                Id = laptopFromDb.Id,
                Name = laptopFromDb.Name,
                Available = laptopFromDb.Available,
                Brand = laptopFromDb.Brand,
                Description = laptopFromDb.Description,
                Price = laptopFromDb.Price,
            };

            return View(laptopUpdateRequestDTO);
        }

        [HttpPost]
        public IActionResult Update(LaptopUpdateRequestDTO laptopUpdateRequestDTO)
        {
            var laptop = dbContext.LaptopTable.Find(laptopUpdateRequestDTO.Id);
            if (laptop == null) return NotFound();

            if (ModelState.IsValid)
            {
                string wwwRootPath = _hostingEnvironment.WebRootPath;
                if (laptopUpdateRequestDTO.Photo != null)
                {
                    string fileName = Guid.NewGuid().ToString() + Path.GetExtension(laptopUpdateRequestDTO.Photo.FileName);
                    string productPath = Path.Combine(wwwRootPath, @"images");

                    using (var fileStream = new FileStream(Path.Combine(productPath, fileName), FileMode.Create))
                    {
                        laptopUpdateRequestDTO.Photo.CopyTo(fileStream);
                    }

                    laptop.ImageUrl = @"\images\" + fileName;
                }

                dbContext.LaptopTable.Update(laptop);
                dbContext.SaveChanges();
                TempData["success"] = "Item details was changed successfully!";
                return RedirectToAction("List");
            }
            return View();
        }

        [HttpGet]
        public IActionResult Delete(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }

            Laptop? laptopFromDb = dbContext.LaptopTable.Find(id);
            if (laptopFromDb is null)
            {
                return NotFound();
            }
            return View(laptopFromDb);
        }

        [HttpPost, ActionName("Delete")]
        public IActionResult DeletePost(int? id)
        {
            var laptop = dbContext.LaptopTable.Find(id);
            if (laptop == null)
            {
                return NotFound();
            }

            dbContext.LaptopTable.Remove(laptop);
            dbContext.SaveChanges();
            TempData["success"] = "Item deleted successfully!";
            return RedirectToAction("List");
        }

    }

}
