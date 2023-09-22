using VacationSplit.Data;
using Microsoft.EntityFrameworkCore;
using VacationSplit.Services;
using VacationSplit.IServices;

using Microsoft.Extensions.DependencyInjection;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddDbContext<VacationSplitContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("VacationSplitDB")));
builder.Services.AddScoped<ILoginService, LoginService>();
builder.Services.AddScoped<ISecurityService, SecurityService>();
builder.Services.AddScoped<ICategoryService, CategoryService>();
builder.Services.AddScoped<IExpenseService, ExpenseService>();

builder.Services.AddDistributedMemoryCache(); 

builder.Services.AddSession(options =>
{
    options.Cookie.Name = ".VacationSplit.Session";
    options.IdleTimeout = TimeSpan.FromMinutes(30); // Définir le délai d'expiration de la session
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
    name: "expense",
    pattern: "{controller=Expense}/{action=Index}/{id?}"); 

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}"); 
app.Run();
