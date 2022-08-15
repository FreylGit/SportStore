using Microsoft.EntityFrameworkCore.ChangeTracking;
using SportStore.Data.Repositories.Interfaces;
using SportStore.Models;

namespace SportStore.Data.Repositories
{
    public class EFProductRepository : IProductRepository
    {
        private readonly ApplicationDbContext _context;
        public IQueryable<Product> Products => _context.Products;
        public EFProductRepository(ApplicationDbContext context)
        {
            _context = context;
        }

      

        public void SaveProduct(Product product)
        {
            _context.Products.Add(product);
            _context.SaveChanges();
        }

        public async Task SaveProductAsync(Product product)
        {
            _context.Products.Add(product);
            await _context.SaveChangesAsync();
        }
    }
}
