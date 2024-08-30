using OnlineStore.Infrastructure.Extensions;

try
{
    var builder = WebApplication.CreateBuilder(args);
    var app = builder.ConfigureServices().ConfigurePipeLine();

    app.Run();
}
catch (Exception ex)
{
    //Log.Fatal(ex, "Host terminated unexpectedly");
}
finally 
{

}






