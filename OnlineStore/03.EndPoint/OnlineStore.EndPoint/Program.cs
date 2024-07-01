using Common.ServiceHelpers;
using Common.ServiceHelpers.Implementation.Mapper;
using ProductManagement.Infrastructure.Persistence;
using ProductManagement.Infrastructure.Persistence.Extensions;



var builder = WebApplication.CreateBuilder(args);
builder.Services.AddRazorPages();
var connectionString = builder.Configuration.GetConnectionString("OnlineStoreDB");

var app = builder.ConfigureServices();






// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapRazorPages();


app.Run();
