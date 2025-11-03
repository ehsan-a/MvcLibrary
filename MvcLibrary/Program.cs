using MvcLibrary.Data;
using MvcLibrary.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddSingleton<IRepository<Book>, BookRepository>();
builder.Services.AddSingleton<IRepository<User>, UserRepository>();
builder.Services.AddSingleton<IRepository<Borrow>, BorrowRepository>();

builder.Services.AddDistributedMemoryCache();

builder.Services.AddSession();
builder.Services.AddHttpContextAccessor();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;

    var userRepo = services.GetRequiredService<IRepository<User>>();
    var bookRepo = services.GetRequiredService<IRepository<Book>>();
    var borrowRepo = services.GetRequiredService<IRepository<Borrow>>();
    SeedData.InitializeUser(userRepo);
    SeedData.InitializeBook(bookRepo);
    SeedData.InitializeBorrow(borrowRepo);
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.UseSession();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
