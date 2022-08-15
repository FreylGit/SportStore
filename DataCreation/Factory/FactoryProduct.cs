
using SportStore.Models;

namespace DataCreation.Factory
{
    public abstract class FactoryProduct
    {
        public List<Product> Products { get; set; }
        public abstract void CreateProduct();
    }
}
