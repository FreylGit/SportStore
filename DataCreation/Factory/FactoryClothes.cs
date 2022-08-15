
using SportStore.Models;

namespace DataCreation.Factory
{
    public class FactoryClothes : FactoryProduct
    {
        private const string _category = "Одежда";
        public override void CreateProduct()
        {
            Products = new List<Product>()
            {
                new Product{Name = "Спортивные шорты Nike",Category = _category,Price = 2000M,Description="Синий цвет"},
                new Product{Name = "Спортивные шорты Adidas",Category = _category,Price = 1400M,Description="Цвет черный"},
                new Product{Name = "Спортианая футболка Adidas",Category = _category,Price = 2400M,Description="Черный цвет"},
                new Product{Name = "Спортианая футболка Puma",Category = _category,Price = 3000M,Description="Цвет оранжевый"},
                new Product{Name = "Спортианая футболка Nike",Category = _category,Price = 5500M,Description="Цвет синий"},
                new Product{Name = "Спортианая футболкай Nike",Category = _category,Price = 5500M,Description="Цвет белый"},
            };
        }
    }
}
