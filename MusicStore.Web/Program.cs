using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using MusicStore.Infrastructure;
using MusicStore.Application.Interfaces;
using MusicStore.Application.Services;
 using MusicStore.Infrastructure.Repositories; 

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

// Configure EF Core with SQL Server
builder.Services.AddDbContext<MusicStoreDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// Add session support for cart/user state
builder.Services.AddSession();

// Configure Identity (replaces old Membership). This creates AspNetUsers/AspNetRoles tables.
builder.Services.AddIdentity<IdentityUser, IdentityRole>(options =>
{
    // keep default password rules or adjust as needed
    options.Password.RequireDigit = false;
    options.Password.RequireNonAlphanumeric = false;
    options.Password.RequireUppercase = false;
    options.Password.RequiredLength = 6;
})
    .AddEntityFrameworkStores<MusicStoreDbContext>()
    .AddDefaultTokenProviders();

// Configure authentication cookie paths to match existing controllers
builder.Services.ConfigureApplicationCookie(options =>
{
    options.LoginPath = "/Account/LogOn";
    options.LogoutPath = "/Account/LogOff";
    options.AccessDeniedPath = "/Account/AccessDenied";
});

// Dependency Injection registrations
builder.Services.AddScoped<IAlbumService, AlbumService>();
builder.Services.AddScoped<IGenreService, GenreService>();
builder.Services.AddScoped<IArtistService, ArtistService>();
builder.Services.AddScoped<ICartService, CartService>();
builder.Services.AddScoped<IOrderService, OrderService>();
builder.Services.AddScoped<IUserService, UserService>();

builder.Services.AddScoped<IAlbumRepository, AlbumRepository>();
builder.Services.AddScoped<IGenreRepository, GenreRepository>();
builder.Services.AddScoped<IArtistRepository, ArtistRepository>();
builder.Services.AddScoped<ICartRepository, CartRepository>();
builder.Services.AddScoped<IOrderRepository, OrderRepository>();
builder.Services.AddScoped<IOrderDetailRepository, OrderDetailRepository>();
// UserRepository removed: user management is handled by UserService in Application layer for now.

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseSession();
app.UseAuthentication();
app.UseAuthorization();

// Routing configuration
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
