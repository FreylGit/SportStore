using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.ViewComponents;
using Microsoft.AspNetCore.Routing;
using Moq;
using SportStore.Components;
using SportStore.Data.Repositories.Interfaces;
using SportStore.Models;

namespace SportStore.Tests
{
    public class NavigationMenuViewComponentTest
    {
        [Fact]
        public void Can_Select_Categories()
        {
            Mock<IProductRepository> mock = new Mock<IProductRepository>();
            mock.Setup(m => m.Products).Returns((new Product[]
            {
                new Product {ProductId = 1 ,Name = "P1",Category="Apples"},
                new Product {ProductId = 2 ,Name = "P2",Category="Apples"},
                new Product {ProductId = 3 ,Name = "P3",Category="Plums"},
                new Product {ProductId = 4 ,Name = "P4",Category="Oranges"},
            }).AsQueryable<Product>());

            NavigationMenuViewComponent target = new NavigationMenuViewComponent(mock.Object);
            string[] result = ((IEnumerable<string>)(target.Invoke() as ViewViewComponentResult).ViewData.Model).ToArray();
            Assert.True(Enumerable.SequenceEqual(new string[] { "Apples", "Oranges", "Plums" }, result));
        }

        [Fact]
        public void Indicates_Selected_Category()
        {
            string categoryToSelect = "Apples";
            Mock<IProductRepository> mock = new Mock<IProductRepository>();
            mock.Setup(m => m.Products).Returns((new Product[]
            {
                new Product{ProductId = 1,Name = "P1",Category="Apples"},
                new Product{ProductId = 2,Name = "P2",Category="Oranges"},
            }).AsQueryable<Product>());

            NavigationMenuViewComponent target =
                new NavigationMenuViewComponent(mock.Object);
            target.ViewComponentContext = new ViewComponentContext
            {
                ViewContext = new ViewContext
                {
                    RouteData = new RouteData()
                }
            };
            target.RouteData.Values["category"] = categoryToSelect;
            //Действие
            string result = (string)(target.Invoke() as ViewViewComponentResult).ViewData["SelectedCategory"];
            //Утверждение
            Assert.Equal(categoryToSelect,result);
        }
    }
}
