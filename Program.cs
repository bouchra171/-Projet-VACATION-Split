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

builder.Services.AddDistributedMemoryCache(); // Ajouter le cache en mémoire pour stocker les sessions

builder.Services.AddSession(options =>
{
    options.Cookie.Name = ".VacationSplit.Session";
    options.IdleTimeout = TimeSpan.FromMinutes(30); // Définir le délai d'expiration de la session
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true;
});

builder.Services.AddSession();

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

app.UseSession();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
