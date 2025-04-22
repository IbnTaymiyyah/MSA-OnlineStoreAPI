using Microsoft.EntityFrameworkCore;
using OnlineStoreAPI.Models;

namespace OnlineStoreAPI.Data
{
    public class AppDBContext : DbContext
    {
        public AppDBContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<Categorie> Categories { get; set; }   
        public DbSet<Product> Products { get; set; }
        public DbSet<Brand> Brands { get; set; }

        
    }
}
