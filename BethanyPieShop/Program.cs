using BethanyPieShop.Models;
using Microsoft.EntityFrameworkCore;
using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Identity;

var builder = WebApplication.CreateBuilder(args);
var connectionString = builder.Configuration.GetConnectionString("BethanyPieShopDbContextConnection") ??
            throw new InvalidOperationException("Connection string 'BethanyPieShopDbContextConnection' not found.");

//Service registration or add services to the container

builder.Services.AddScoped<ICategoryRepository, CategoryRepository>();
builder.Services.AddScoped<IPieRepository, PieRepository>();
builder.Services.AddScoped<IOrderRepository, OrderRepository>();
builder.Services.AddScoped<IShoppingCart,ShoppingCart>(sp=> ShoppingCart.GetCart(sp));

builder.Services.AddSession();
builder.Services.AddControllersWithViews()
      .AddJsonOptions(options =>
      {
          options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
      }); 
builder.Services.AddRazorPages();
builder.Services.AddServerSideBlazor();
builder.Services.AddHttpContextAccessor();
builder.Services.AddDbContext<BethanyPieShopDbContext>(options =>
    {
        options.UseSqlServer(builder.Configuration["ConnectionStrings:BethanyPieShopDbContextConnection"]);
    });

builder.Services.AddDefaultIdentity<IdentityUser>()
    .AddEntityFrameworkStores<BethanyPieShopDbContext>();

var app = builder.Build();

//Middleware request pipeline setup or Configure the HTTP request pipeline
if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
}

app.UseStaticFiles();
app.UseSession();
app.UseAuthentication();
app.UseAuthorization();
app.MapDefaultControllerRoute();
app.MapRazorPages();
app.MapBlazorHub();
app.MapFallbackToPage("/app/{*catchall}", "/App/Index");

DbInitializer.Seed(app);

app.Run();
