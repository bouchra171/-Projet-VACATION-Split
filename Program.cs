using VacationSplit;
using VacationSplit.Models;
using VacationSplit.Data;
using Microsoft.EntityFrameworkCore;
using VacationSplit.Services;
using VacationSplit.IServices;

using Microsoft.Extensions.DependencyInjection;

//using (var context = new VacationSplitContext())
//{
//    context.Database.EnsureDeleted();
//    context.Database.EnsureCreated();
//    var user = new User
//    {
//        FirstName = "Mohamed",
//        LastName = "Bournane",
//        ProfileImage = "C:\\Users\\PC01\\Pictures\\modelisation.PNG",
//        Email = "mbournane@gmail.com",
//        Ville = "Paris",
//        Password = ""
//    };
//    var Category = new Category
//    {
//        Name = "Restaurant"
//    };
//var Expense = new Expense
//{
//    DateExpense  = DateTime.Now,
//    Amount = 75,
//    Category = 
//};
//    context.Add(user);
//    context.Add(Category);
//    context.SaveChanges();
//}

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddDbContext<VacationSplitContext>(options=>options.UseSqlServer(builder.Configuration.GetConnectionString("VacationSplitDB")));
builder.Services.AddScoped<ILoginService, LoginService>();
builder.Services.AddScoped<ISecurityService, SecurityService>();

builder.Services.AddDistributedMemoryCache();
builder.Services.AddSession(options =>

{
    options.IdleTimeout = TimeSpan.FromMinutes(20); // Vous pouvez ajuster le d�lai d'inactivit� selon vos besoins
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true;
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseSession();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
