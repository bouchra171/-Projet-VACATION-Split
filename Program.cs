using VacationSplit.Models;
using VacationSplit.Repositories;

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
//        Password = ""

//    };

//    context.Add(user);
//    context.SaveChanges();

//}

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddDbContext<VacationSplitContext>();

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

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
