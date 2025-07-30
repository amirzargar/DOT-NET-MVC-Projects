using Microsoft.EntityFrameworkCore;
using MyFirstMvcApp.Models;

namespace MyFirstMvcApp.Data
{
    public class AppDbContext: DbContext
    {
       public AppDbContext(DbContextOptions<AppDbContext> options) :base(options)  // Constructor that accepts DbContextOptions and passes it to the base class
        {

        }

        public DbSet<Product> Products { get; set; }  // Represents the Products table in the database

    }
    
}
