using Microsoft.EntityFrameworkCore;
using SportStore.Models;

namespace SportStore.Data
{
    public class ApplicationDbContext :DbContext
    {
        public DbSet<Product> Products { get; set; }
        public DbSet<Order> Orders { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Server=localhost;Database=SportStore;Trusted_Connection=True;");
            
        }
    }
}
