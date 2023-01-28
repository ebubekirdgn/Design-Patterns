using BaseProject.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

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

using var scope = app.Services.CreateScope();

var identityDbContext = scope.ServiceProvider.GetRequiredService<AppIdentityDbContext>();

var userManager = scope.ServiceProvider.GetRequiredService<UserManager<AppUser>>();

identityDbContext.Database.Migrate();// migration yaptýktan sonra artýk update database dememize gerek yok eger hiç veritabaný yoksa hem veritabanýný oluþturur  þayet uygulanmayan migration varsada onu direk uygular.
if (!userManager.Users.Any())
{
    userManager.CreateAsync(new AppUser() { UserName = "user1", Email = "user1@outlook.com", PictureUrl = "/userpictures/primeuserpicture.png", Description = "Lorem Ipsum is simply dummy text of the printing and typesetting industry. Lorem Ipsum has been the industry's standard dummy text ever since the 1500s" }, "Password12*").Wait();

    userManager.CreateAsync(new AppUser() { UserName = "user2", Email = "user2@outlook.com", PictureUrl = "/userpictures/primeuserpicture.png", Description = "Lorem Ipsum is simply dummy text of the printing and typesetting industry. Lorem Ipsum has been the industry's standard dummy text ever since the 1500s" }, "Password12*").Wait();
    userManager.CreateAsync(new AppUser() { UserName = "user3", Email = "user3@outlook.com", PictureUrl = "/userpictures/primeuserpicture.png", Description = "Lorem Ipsum is simply dummy text of the printing and typesetting industry. Lorem Ipsum has been the industry's standard dummy text ever since the 1500s" }, "Password12*").Wait();
    userManager.CreateAsync(new AppUser() { UserName = "user4", Email = "user4@outlook.com", PictureUrl = "/userpictures/primeuserpicture.png", Description = "Lorem Ipsum is simply dummy text of the printing and typesetting industry. Lorem Ipsum has been the industry's standard dummy text ever since the 1500s" }, "Password12*").Wait();
    userManager.CreateAsync(new AppUser() { UserName = "user5", Email = "user5@outlook.com", PictureUrl = "/userpictures/primeuserpicture.png", Description = "Lorem Ipsum is simply dummy text of the printing and typesetting industry. Lorem Ipsum has been the industry's standard dummy text ever since the 1500s" }, "Password12*").Wait();
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