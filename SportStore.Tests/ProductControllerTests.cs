using Moq;
using SportStore.Controllers;
using SportStore.Data.Repositories.Interfaces;
using SportStore.Models;
using SportStore.Models.ViewModels;

namespace SportStore.Tests
{
    public class ProductControllerTests
    {
        [Fact]
        public void Can_Paginate()
        {
            Mock<IProductRepository> mock = new Mock<IProductRepository>();
            mock.Setup(m => m.Products).Returns((new Product[]
            {
                new Product{ProductId = 1,Name="P1"},
                new Product{ProductId = 2,Name="P2"},
                new Product{ProductId = 3,Name="P3"},
                new Product{ProductId = 4,Name="P4"},
                new Product{ProductId = 5,Name="P5"},
            }).AsQueryable<Product>());
            ProductController controller = new ProductController(mock.Object);
            controller.PageSize = 3;
            //Действие
            ProductListViewModel result = controller.List(null,2).ViewData.Model as ProductListViewModel;
            //Утверждение
            Product[] producArray = result.Products.ToArray();
            Assert.True(producArray.Length == 2);
            Assert.Equal("P4", producArray[0].Name);
            Assert.Equal("P5", producArray[1].Name);
        }

        [Fact]
        public void Can_Send_Paination_View_Model()
        {
            Mock<IProductRepository> mock = new Mock<IProductRepository>();
            mock.Setup(m => m.Products).Returns((new Product[]
            {
                new Product{ProductId = 1,Name="P1"},
                new Product{ProductId = 2,Name="P2"},
                new Product{ProductId = 3,Name="P3"},
                new Product{ProductId = 4,Name="P4"},
                new Product{ProductId = 5,Name="P5"},
            }).AsQueryable<Product>());
            ProductController controller = new ProductController(mock.Object) { PageSize = 3 };
            //Действие
            ProductListViewModel result = controller.List(null,2).ViewData.Model as ProductListViewModel;
            //Утверждение
            PagingInfo pagingInfo = result.PagingInfo;
            Assert.Equal(2, pagingInfo.CurrentPage);
            Assert.Equal(3, pagingInfo.ItemsPerPage);
            Assert.Equal(5, pagingInfo.TotalItems);
            Assert.Equal(2, pagingInfo.TotalPages);
        }

        [Fact]
        public void Can_Filter_Products()
        {
            Mock<IProductRepository> mock = new Mock<IProductRepository>();
            mock.Setup(m => m.Products).Returns((new Product[]
            {
                new Product {ProductId = 1,Name="P1",Category="Cart1"},
                new Product {ProductId = 2,Name="P2",Category="Cart2"},
                new Product {ProductId = 3,Name="P3",Category="Cart1"},
                new Product {ProductId = 4,Name="P4",Category="Cart2"},
                new Product {ProductId = 5,Name="P5",Category="Cart3"},
            }).AsQueryable<Product>());
            ProductController controller = new ProductController(mock.Object);
            //Действие
            Product[] result = (controller.List("Cart2", 1).ViewData.Model as ProductListViewModel).Products.ToArray();
            //Утверждение
            Assert.Equal(2, result.Length);
            Assert.True(result[0].Name == "P2" && result[0].Category == "Cart2");
            Assert.True(result[1].Name == "P4" && result[1].Category == "Cart2");
        }
    }
}
