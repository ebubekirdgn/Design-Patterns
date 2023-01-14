using BaseProject.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using WebApp.Strategy.Repositories;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddDbContext<AppIdentityDbContext>(options =>
        options.UseSqlServer(builder.Configuration.GetConnectionString("SqlServer")));

builder.Services.AddIdentity<AppUser, IdentityRole>(options =>
{
    options.User.RequireUniqueEmail = true;
}).AddEntityFrameworkStores<AppIdentityDbContext>();

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var identityDbContext = scope.ServiceProvider.GetRequiredService<AppIdentityDbContext>();
    var userManager = scope.ServiceProvider.GetRequiredService<UserManager<AppUser>>();

    identityDbContext.Database.Migrate(); // migration yaptýktan sonra artýk update database dememize gerek yok eger hiç veritabaný yoksa hem veritabanýný oluþturur  þayet uygulanmayan migration varsada onu direk uygular.
    if (!userManager.Users.Any())
    {
        userManager.CreateAsync(new AppUser() { UserName = "User1", Email = "efc@gmail.com" }, "Password123*").Wait();
        userManager.CreateAsync(new AppUser() { UserName = "User2", Email = "efc1@gmail.com" }, "Password123*").Wait();
        userManager.CreateAsync(new AppUser() { UserName = "User3", Email = "efc2@gmail.com" }, "Password123*").Wait();
        userManager.CreateAsync(new AppUser() { UserName = "User4", Email = "efc3@gmail.com" }, "Password123*").Wait();
        userManager.CreateAsync(new AppUser() { UserName = "User5", Email = "efc4@gmail.com" }, "Password123*").Wait();
    }
}

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
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

app.Run();