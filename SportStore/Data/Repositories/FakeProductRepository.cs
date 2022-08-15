using SportStore.Data.Repositories.Interfaces;
using SportStore.Models;

namespace SportStore.Data.Repositories
{
    public class FakeProductRepository : IProductRepository
    {
        public IQueryable<Product> Products => new List<Product> {
            new Product{Name = "name1",Category="category",Price=123}
        }.AsQueryable<Product>();

        public void SaveProduct(Product product)
        {
            throw new NotImplementedException();
        }

        public Task SaveProductAsync(Product product)
        {
            throw new NotImplementedException();
        }
    }
}
