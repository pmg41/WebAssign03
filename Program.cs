using Assign03.Data;
using Microsoft.EntityFrameworkCore;
using static System.Runtime.InteropServices.JavaScript.JSType;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddDbContext<WebAssign3Context>(options =>
    options.UseSqlServer("Data Source=WebAssign3.db"));

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

/*app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");*/

app.UseEndpoints(endpoints =>
{
    endpoints.MapControllers();
});

app.Run();

/*Scaffold - DbContext "Server=JUPIN\PRAGNESHSQL19;Database=WebAssign3;Integrated Security=True;" +
    "Connect Timeout=30;Packet Size=4096;MultipleActiveResultSets=True;Encrypt=False;" 
    Microsoft.EntityFrameworkCore.SqlServer - OutputDir Data*/

/*dotnet tool install --global dotnet-aspnet-codegenerator
dotnet tool install --global dotnet-ef --version 8.*
dotnet ef migrations add InitialCreate
dotnet ef database update*/