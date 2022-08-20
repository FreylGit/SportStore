using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Moq;
using SportStore.Controllers;
using SportStore.Data.Repositories.Interfaces;
using SportStore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SportStore.Tests
{
    public class AdminControllerTests
    {
        [Fact]
        public void Index_Contains_All_Products()
        {
            Mock<IProductRepository> mock = new Mock<IProductRepository>();
            mock.Setup(m => m.Products).Returns((new Product[] {
                new Product {ProductId = 1,Name = "P1"},
                new Product {ProductId = 2,Name = "P2"},
                new Product {ProductId = 3,Name = "P3"}
            }).AsQueryable<Product>());
            AdminController controller = new AdminController(mock.Object);
            Product[] result = GetViewModel<IEnumerable<Product>>(controller.Index())?.ToArray();
        }

        [Fact]
        public void Can_Edit_Product()
        {
            Mock<IProductRepository> mock = new Mock<IProductRepository>();
            mock.Setup(m => m.Products).Returns((new Product[] {
                new Product {ProductId = 1,Name = "P1"},
                new Product {ProductId = 2,Name = "P2"},
                new Product {ProductId = 3,Name = "P3"}
            }).AsQueryable<Product>());
            AdminController controller = new AdminController(mock.Object);

            Product p1 = GetViewModel<Product>(controller.Edit(1));
            Product p2 = GetViewModel<Product>(controller.Edit(2));
            Product p3 = GetViewModel<Product>(controller.Edit(3));

            Assert.Equal(1, p1.ProductId);
            Assert.Equal(2, p2.ProductId);
            Assert.Equal(3, p3.ProductId);
        }

        [Fact]
        public void Cannot_Edit_Nonexistent_Product()
        {
            Mock<IProductRepository> mock = new Mock<IProductRepository>();
            mock.Setup(m => m.Products).Returns((new Product[] {
                new Product {ProductId = 1,Name = "P1"},
                new Product {ProductId = 2,Name = "P2"},
                new Product {ProductId = 3,Name = "P3"}
            }).AsQueryable<Product>());
            AdminController controller = new AdminController(mock.Object);
            
            Product result = GetViewModel<Product>(controller.Edit(4));
            Assert.Null(result);
        }

        [Fact]
        public void Can_Save_Valid_Changes()
        {
            Mock<IProductRepository> mock = new Mock<IProductRepository>();
            Mock<ITempDataDictionary> tempData = new Mock<ITempDataDictionary>();
            AdminController controller = new AdminController(mock.Object)
            {
                TempData = tempData.Object
            };
            Product product = new Product { Name = "Test" };
            IActionResult result = controller.Edit(product).Result;
            mock.Verify(m => m.SaveProductAsync(product));
            Assert.IsType<RedirectToActionResult>(result);
            Assert.Equal("Index", (result as  RedirectToActionResult).ActionName);
        }

        [Fact]
        public void Cannot_Save_Invalid_Changes()
        {
            Mock<IProductRepository> mock = new Mock<IProductRepository>();
            AdminController controller = new AdminController(mock.Object);
            Product product = new Product { Name = "Test" };
            controller.ModelState.AddModelError("error", "error");
            IActionResult result = controller.Edit(product).Result;
            mock.Verify(m => m.SaveProductAsync(It.IsAny<Product>()), Times.Never());
            Assert.IsType<ViewResult>(result);
        }

        [Fact]
        public void Can_Delete_Valid_Products()
        {
            var product = new Product { ProductId = 2, Name = "Test" };
            Mock<IProductRepository> mock = new Mock<IProductRepository>();
            mock.Setup(m => m.Products).Returns((new Product[]
            {
                new Product {ProductId = 1,Name="P1"},
                new Product {ProductId = 3,Name="P3"}
            }).AsQueryable<Product>());
            AdminController controller = new AdminController(mock.Object);
            controller.Delete(product.ProductId);
            mock.Verify(m => m.DeleteProduct(product.ProductId));
        }
        private T GetViewModel<T>(IActionResult result) where T : class
        {
            return (result as ViewResult)?.ViewData.Model as T;
        }
    }
}
