using SportStore.Data;
using SportStore.Data.Repositories;
using SportStore.Data.Repositories.Interfaces;
using SportStore.Models;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddMvc();
builder.Services.AddMemoryCache();//����������� ��������� ������ � ������
builder.Services.AddSession();
builder.Services.AddControllersWithViews();
builder.Services.AddDbContext<ApplicationDbContext>();
builder.Services.AddTransient<IProductRepository, EFProductRepository>();
builder.Services.AddTransient<IOrderRepository, EFOrderRepository>();
builder.Services.AddScoped<Cart>(sp => SessionCart.GetCart(sp));
builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseDeveloperExceptionPage();
app.UseStatusCodePages();
app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseSession();
app.UseRouting();


app.UseEndpoints(endpoints =>
{
    endpoints.MapControllerRoute(
       name: null,
       pattern: "{category}/Page{productPage:int}",
       defaults: new { controller = "Product", action = "List" });
    endpoints.MapControllerRoute(
       name: null,
                    pattern: "Page{productPage:int}",
                    defaults: new { controller = "Product", action = "List", productPage = 1 });

    endpoints.MapControllerRoute(
       name: null,
                    pattern: "{category}",
                    defaults: new { controller = "Product", action = "List", productPage = 1 });

    endpoints.MapControllerRoute(
       name: null,
                    pattern: "",
                    defaults: new { controller = "Product", action = "List", productPage = 1 });

    endpoints.MapControllerRoute(
        name: null, pattern: "{controller}/{action}/{id?}");
});
app.Run();
