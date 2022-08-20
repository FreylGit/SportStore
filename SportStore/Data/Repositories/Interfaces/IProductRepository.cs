using SportStore.Models;

namespace SportStore.Data.Repositories.Interfaces
{
    public interface IProductRepository
    {
        IQueryable<Product> Products { get;  }
        public void SaveProduct(Product product);
        public Task SaveProductAsync(Product product);
        public Product DeleteProduct(int productId);   
    }
}
