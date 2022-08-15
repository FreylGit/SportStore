using SportStore.Models;

namespace SportStore.Tests
{
    public class CartTests
    {
        [Fact]
        public void Can_Add_New_Lines()
        {
            var p1 = new Product { ProductId = 1, Name = "P1" };
            var p2 = new Product { ProductId = 2, Name = "P2" };

            Cart cart = new Cart();
            cart.AddItem(p1, 1);
            cart.AddItem(p2, 1);
            CartLine[] result = cart.Lines.ToArray();

            Assert.Equal(2, result.Length);
            Assert.Equal(p1, result[0].Product);
            Assert.Equal(p2, result[1].Product);
        }
        [Fact]
        public void Can_And_Quantity_For_Exiting_Lines()
        {
            var p1 = new Product { ProductId = 1, Name = "P1" };
            var p2 = new Product { ProductId = 2, Name = "P2" };
            Cart cart = new Cart();
            cart.AddItem(p1, 1);
            cart.AddItem(p2, 1);
            cart.AddItem(p1, 10);
            CartLine[] result = cart.Lines.ToArray();

            Assert.Equal(2, result.Length);
            Assert.Equal(11, result[0].Quantity);
            Assert.Equal(1, result[1].Quantity);
        }

        [Fact]
        public void Can_Remove_Line()
        {
            var p1 = new Product { ProductId = 1, Name = "P1" };
            var p2 = new Product { ProductId = 2, Name = "P2" };
            var p3 = new Product { ProductId = 3, Name = "P3" };
            Cart cart = new Cart();
            cart.AddItem(p1, 1);
            cart.AddItem(p2, 3);
            cart.AddItem(p3, 5);
            cart.AddItem(p1, 1);

            cart.RemoveLine(p2);
            Assert.Equal(0, cart.Lines.Where(c => c.Product == p2).Count());
            Assert.Equal(2, cart.Lines.Count());
        }

        [Fact]
        public void Calculate_Cart_Total()
        {
            var p1 = new Product { ProductId = 1, Name = "P1", Price = 100M };
            var p2 = new Product { ProductId = 2, Name = "P2", Price = 50M };
            Cart cart = new Cart();
            cart.AddItem(p1, 1);
            cart.AddItem(p2, 1);
            cart.AddItem(p1, 3);
            decimal  result = cart.ComputeTotalValue();

            Assert.Equal(450M, result);
        }
        public void Can_Clear_Contents()
        {
            var p1 = new Product { ProductId = 1, Name = "P1", Price = 100M };
            var p2 = new Product { ProductId = 2, Name = "P2", Price = 50M };
            Cart cart = new Cart();
            cart.AddItem(p1, 1);
            cart.AddItem(p2, 1);

            cart.Clear();
            Assert.Equal(0, cart.Lines.Count());
        }
    }
}



