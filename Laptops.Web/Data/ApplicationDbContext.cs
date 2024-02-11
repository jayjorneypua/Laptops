using Laptops.Web.Models.Entities;
using Microsoft.EntityFrameworkCore;

namespace Laptops.Web.Data
{
    public class ApplicationDbContext: DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options): base(options) 
        {
            
        }
        public DbSet<Laptop> LaptopTable { get; set; }
       

        //See Notes Below:







        //public class ApplicationDbContext: DbContext //after the : DbContext was inherited by ApplicationDbContext. ApplicationDbContext is the bridge between the app and sqlserver

        //DbContextOptions<> allows you to configure the behavior of the DbContext by specifying options such as the database provider, connection string, and other related settings.

        //public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) //it passes to the base class
        //{
        //    //This is a constructor
        //}

        // a DbSet is a collection of type. DbSet<Laptop> is basically a collection of Laptops and the LaptopTable is the tableName.

        //we will create a property for the entity of laptops we just created
        //public DbSet<Laptop> LaptopTable { get; set;
        //We can use this LaptopTable to basically access the laptop table in database

    }
}
