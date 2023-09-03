using Shopping_List.Data;
using Shopping_List.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

//Product Connection
var productconnectionString = builder.Configuration.GetConnectionString("ProductConnection") ?? throw new InvalidOperationException("Connection string 'ProductConnection' not found.");

builder.Services.AddDbContext<ProductDbContext>(options => options.UseSqlServer(productconnectionString));

//Identity Connection
var identityconnectionString = builder.Configuration.GetConnectionString("IdentityConnection") ?? throw new InvalidOperationException("Connection string 'IdentityConnection' not found.");

builder.Services.AddDbContext<IdentityDbContext>(options =>
    options.UseSqlServer(identityconnectionString));
    
builder.Services.AddDatabaseDeveloperPageExceptionFilter();

//builder.Services.AddDefaultIdentity<CustomUser>(options => options.SignIn.RequireConfirmedAccount = true)
builder.Services.AddDefaultIdentity<CustomUser>(options => options.Password.RequireNonAlphanumeric = false)
    .AddRoles<IdentityRole>()
    .AddEntityFrameworkStores<IdentityDbContext>();
builder.Services.AddControllersWithViews();

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    var dbContext = services.GetRequiredService<IdentityDbContext>(); 
    var userManager = services.GetRequiredService<UserManager<CustomUser>>();
    var roleManager = services.GetRequiredService<RoleManager<IdentityRole>>();

    //admin@gmail.com kullanýcý adýnda adminin varlýðýný kontrol eder
    // Admin kullanýcýsý yoksa oluþturur
    var adminUser = userManager.FindByNameAsync("admin@gmail.com").Result;
    if (adminUser == null)
    {
        adminUser = new CustomUser
        {
            Email = "admin@gmail.com",
            EmailConfirmed = true,
            Name = "Admin",
            UserName = "admin@gmail.com",
            NormalizedUserName = "ADMIN@GMAIL.COM",
            Role = "Admin"
        };

        var result = userManager.CreateAsync(adminUser, "Admin123*").Result;

        if (result.Succeeded)
        {
            userManager.AddToRoleAsync(adminUser, "Admin").Wait();
        }
    }
}

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseMigrationsEndPoint();
}
else
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}


app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");
app.MapRazorPages();

app.Run();
