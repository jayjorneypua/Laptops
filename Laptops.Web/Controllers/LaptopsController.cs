using Laptops.Web.Data;
using Laptops.Web.DTO;
using Laptops.Web.Models;
using Laptops.Web.Models.Entities;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.SqlServer.Query.Internal;
using System;
using System.Linq;

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
        public async Task<IActionResult> Add(LaptopAddRequestDTO obj)
        {
            var DTO = new Laptop
            {
                Name = obj.Name,
                Description = obj.Description,
                Brand = obj.Brand,
                Price = obj.Price,
                Available = obj.Available,
            };

            if (ModelState.IsValid)
            {
                string wwwRootPath = _hostingEnvironment.WebRootPath;
                if (obj.Photo != null)
                {
                    string fileName = Guid.NewGuid().ToString() + Path.GetExtension(obj.Photo.FileName);
                    string productPath = Path.Combine(wwwRootPath, @"images");

                    using (var fileStream = new FileStream(Path.Combine(productPath, fileName), FileMode.Create))
                    {
                        obj.Photo.CopyTo(fileStream);
                    }

                    DTO.ImageUrl = @"\images\" + fileName;
                }

                await dbContext.LaptopTable.AddAsync(DTO);
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

            return View(laptopFromDb);
        }

        [HttpPost]
        public IActionResult Update(LaptopAddRequestDTO obj) 
        {
            var DTO = new Laptop
            {
                Name = obj.Name,
                Description = obj.Description,
                Brand = obj.Brand,
                Price = obj.Price,
                Available = obj.Available,

            };

            if (ModelState.IsValid)
            {
                string wwwRootPath = _hostingEnvironment.WebRootPath;
                if (obj.Photo != null)
                {
                    string fileName = Guid.NewGuid().ToString() + Path.GetExtension(obj.Photo.FileName);
                    string productPath = Path.Combine(wwwRootPath, @"images");

                    using (var fileStream = new FileStream(Path.Combine(productPath, fileName), FileMode.Create))
                    {
                        obj.Photo.CopyTo(fileStream);
                    }

                    DTO.ImageUrl = @"\images\" + fileName;
                }

                dbContext.LaptopTable.Update(DTO);
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
            Laptop? obj = dbContext.LaptopTable.Find(id);
            if (obj == null)
            {
                return NotFound();
            }

            dbContext.LaptopTable.Remove(obj);
            dbContext.SaveChanges();
            TempData["success"] = "Item deleted successfully!";
            return RedirectToAction("List");
        }
        
    }  
    
}
