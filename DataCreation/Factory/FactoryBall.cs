
using SportStore.Models;

namespace DataCreation.Factory
{
    public class FactoryBall : FactoryProduct
    {
        private const string _category = "Мячи";
        public override void CreateProduct()
        {
            Products = new List<Product>()
            {
                new Product{Name = "Футбольный Nike",Category = _category,Price = 4000M,Description="Кожаный мяч"},
                new Product{Name = "Футбольный Adidas",Category = _category,Price = 5000M,Description="Минифутбольный мяч"},
                new Product{Name = "Футбольный Adidas",Category = _category,Price = 3400M,Description="Кожаный мяч"},
                new Product{Name = "Футбольный Puma",Category = _category,Price = 3000M,Description="Кожаный мяч"},
                new Product{Name = "Футбольный Nike",Category = _category,Price = 5500M,Description="Минифутбольный мяч"},
                new Product{Name = "Футбольный Nike",Category = _category,Price = 5500M,Description="Минифутбольный мяч"},
            };
        }


    }
}
