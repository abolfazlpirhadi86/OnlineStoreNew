using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Serilog;
using Serilog.Enrichers.Span;
using mainSerilog = Serilog;

namespace Common.Logs.Serilog
{
    public static class SerilogDependencyInjection
    {
        public static WebApplicationBuilder AddSerilog(this WebApplicationBuilder builder,
            Action<SerilogApplicationEnricherOptions> setupAction)
        {
            builder.Services.Configure(setupAction);
            return AddServices(builder);
        }

        private static WebApplicationBuilder AddServices(WebApplicationBuilder builder)
        {
            builder.Services.AddSingleton<UserEnricher>();
            builder.Services.AddSingleton<ApplicaitonEnricher>();

            builder.Host.UseSerilog((ctx, services, lc) =>
            {
                lc
                .Enrich.FromLogContext()
                .Enrich.With(services.GetRequiredService<UserEnricher>())
                .Enrich.With(services.GetRequiredService<ApplicaitonEnricher>())
                .Enrich.With<ActivityEnricher>()
                .Enrich.WithSpan()
                .ReadFrom.Configuration(ctx.Configuration);
            });

            mainSerilog.Debugging.SelfLog.Enable(msg => Console.WriteLine(msg));
            return builder;
        }
    }
}
