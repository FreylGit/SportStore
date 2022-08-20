using Microsoft.AspNetCore.Mvc;
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
    public class OrderControllerTests
    {
        [Fact]
        public void Cannot_Checkout_Empty_Cart()
        {
            Mock<IOrderRepository> mock = new Mock<IOrderRepository>();
            var cart = new Cart();
            Order order = new Order();
            OrderController controller = new OrderController(mock.Object, cart);
            ViewResult result = controller.Checkout(order) as ViewResult;
            //Утверждение - проверка, что  заказ был не сохранен
            mock.Verify(m => m.SaveOrder(It.IsAny<Order>()), Times.Never);
            Assert.True(string.IsNullOrEmpty(result.ViewName));
            Assert.False(result.ViewData.ModelState.IsValid);
        }

        [Fact]
        public void Cannot_Checkout_Invalid_ShippingDetails()
        {
            Mock<IOrderRepository> mock = new Mock<IOrderRepository>();
            var cart = new Cart();
            cart.AddItem(new Product(), 1);
            Order order = new Order();
            OrderController controller = new OrderController(mock.Object, cart);

            controller.ModelState.AddModelError("error", "error");
            ViewResult result = controller.Checkout(new Order()) as ViewResult;
            mock.Verify(m => m.SaveOrder(It.IsAny<Order>()), Times.Never);
            Assert.True(string.IsNullOrEmpty(result.ViewName));
            Assert.False(result.ViewData.ModelState.IsValid);
        }

        [Fact]
        public void Can_Checout_And_Submit_Order()
        {
            Mock<IOrderRepository> mock = new Mock<IOrderRepository>();
            var cart = new Cart();
            cart.AddItem(new Product(), 1);
            OrderController controller = new OrderController(mock.Object, cart);

            RedirectToActionResult result = controller.Checkout(new Order()) as RedirectToActionResult;

            mock.Verify(m => m.SaveOrder(It.IsAny<Order>()), Times.Once);
            Assert.Equal("Completed", result.ActionName);
        }
    }
}
