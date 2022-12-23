using BethanyPieShop.Models;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

//Service registration or add services to the container
builder.Services.AddControllersWithViews();
builder.Services.AddScoped<ICategoryRepository, CategoryRepository>();
builder.Services.AddScoped<IPieRepository, PieRepository>();

builder.Services.AddScoped<IShoppingCart,ShoppingCart>(sp=> ShoppingCart.GetCart(sp));
builder.Services.AddSession();
builder.Services.AddHttpContextAccessor();

builder.Services.AddDbContext<BethanyPieShopDbContext>(options =>
    {
        options.UseSqlServer(builder.Configuration["ConnectionStrings:BethanyPieShopDbContextConnection"]);
    });

var app = builder.Build();

//Middleware request pipeline setup or Configure the HTTP request pipeline
if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
}

app.UseStaticFiles();
app.UseSession();

app.MapDefaultControllerRoute();

DbInitializer.Seed(app);

app.Run();
