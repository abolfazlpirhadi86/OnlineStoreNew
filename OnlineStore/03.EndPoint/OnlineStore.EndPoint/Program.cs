using Common.ServiceHelpers;
using Common.ServiceHelpers.Implementation.Mapper;
using ProductManagement.Infrastructure.Persistence;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();
var connectionString = builder.Configuration.GetConnectionString("OnlineStoreDB");

ProductManagementBootstrapper.Configure(builder.Services, connectionString);
//builder.Services.AddSingleton<IMapperService, MapperService>();
//MapperConfiguration.assambly = domainAssembly;
//builder.Services.AddAutoMapper(AutoMapperConfiguration.InitializeAutoMapper);

var app = builder.Build();
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
