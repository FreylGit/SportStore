using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Infrastructure;
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
            if(product.ProductId == 0)
            {
                _context.Products.Add(product);
            }
            else
            {
                var productEntity = _context.Products.FirstOrDefault(p => p.ProductId == product.ProductId);
                if (productEntity != null)
                {
                    productEntity.Name = product.Name;
                    productEntity.Description = product.Description;
                    productEntity.Price = product.Price;
                    productEntity.Category = product.Category;
                }
            }
            
            _context.SaveChanges();
        }

        public async Task SaveProductAsync(Product product)
        {

            if (product.ProductId == 0)
            {
                await _context.Products.AddAsync(product);
            }
            else
            {
                var productEntity = await _context.Products.FirstOrDefaultAsync(p => p.ProductId == product.ProductId);
                if (productEntity != null)
                {
                    productEntity.Name = product.Name;
                    productEntity.Description = product.Description;
                    productEntity.Price = product.Price;
                    productEntity.Category = product.Category;
                }
            }

            await _context.SaveChangesAsync();
        }

        public Product DeleteProduct(int productId)
        {
            var product = _context.Products.FirstOrDefault(p => p.ProductId == productId);
            if (product != null)
            {
                _context.Products.Remove(product);
                _context.SaveChanges();
            }
            return product;
            
        }
    }
}
