using DataCreation.Factory;
using SportStore.Data;
using SportStore.Data.Repositories;

FactoryProduct ball = new FactoryBall();
ball.CreateProduct();
FactoryProduct clothes = new FactoryClothes();
clothes.CreateProduct();

using (var context = new ApplicationDbContext())
{
    var repository = new EFProductRepository(context);
    foreach (var product in ball.Products)
    {
        await repository.SaveProductAsync(product);
    }
    foreach (var product in clothes.Products)
    {
        await repository.SaveProductAsync(product);
    }
}

Console.WriteLine("Save database");