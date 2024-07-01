using Common.Extensions;
using Common.ServiceHelpers.Abstraction.DependencyInjection;
using Common.Utilities;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Server.Kestrel.Core;
using Microsoft.Extensions.DependencyInjection;

namespace Common.ServiceHelpers.Implementation.DependencyInjection
{
    public static class DependencyInjectionServiceCollectionExtensions
    {
        private static IServiceCollection ConfigWebHosting(this IServiceCollection services)
        {
            // If using Kestrel:
            services.Configure<KestrelServerOptions>(options =>
            {
                options.AddServerHeader = false;

            })
              // If using IIS:
              .Configure<IISServerOptions>(options =>
              {

              });
            return services;
        }
        private static IServiceCollection AddWithScopedLifetime(this IServiceCollection services, string assmblyNames)
            => services.Scan(s => s.FromAssemblies(ReflectionUtilities.GetAssemblies(assmblyNames))
            .AddClasses(c => c.AssignableToAny(typeof(IScopedLifeTime)))
            .AsImplementedInterfaces()
            .WithScopedLifetime());
        private static IServiceCollection AddWithTransientLifetime(this IServiceCollection services, string assmblyNames)
            => services.Scan(s => s.FromAssemblies(ReflectionUtilities.GetAssemblies(assmblyNames))
            .AddClasses(c => c.AssignableToAny(typeof(ITransientLifeTime)))
            .AsImplementedInterfaces()
            .WithTransientLifetime());
        private static IServiceCollection AddWithSingletonLifetime(this IServiceCollection services, string assmblyNames)
            => services.Scan(s => s.FromAssemblies(ReflectionUtilities.GetAssemblies(assmblyNames))
            .AddClasses(c => c.AssignableToAny(typeof(ISingletonLifeTime)))
            .AsImplementedInterfaces()
            .WithSingletonLifetime());
        private static IServiceCollection AddWithInterceptor(IServiceCollection services, Type interceptorType, string assmblyNames)
        {
            var assemblieList = ReflectionUtilities.GetAssemblies(assmblyNames);
            foreach (var assembly in assemblieList)
            {
                var interfaceList = assembly.GetTypes().Where(e => e.IsAssignableTo(interceptorType) && e.IsGenericType == false && e.IsInterface == true).ToList();
                foreach (var @interface in interfaceList)
                {
                    var implementation = assembly.GetTypes().Where(e => e.IsAssignableTo(@interface)).FirstOrDefault();
                    if (implementation != null)
                    {
                        if (interceptorType == typeof(ITransientLifeTimeWithInterceptor))
                        {
                            services.AddProxiedTransient(@interface, implementation);
                        }
                        else if (interceptorType == typeof(IScopedLifeTimeWithInterceptor))
                        {
                            services.AddProxiedScoped(@interface, implementation);
                        }
                        else if (interceptorType == typeof(ISingletonLifeTimeWithInterceptor))
                        {
                            services.AddProxiedSingleton(@interface, implementation);
                        }
                    }
                }
            }
            return services;
        }
        private static IServiceCollection AddWithScopedLifeTimeWithInterceptor(this IServiceCollection services, string assmblyNames)
        {
            return AddWithInterceptor(services, typeof(IScopedLifeTimeWithInterceptor), assmblyNames);
        }
        private static IServiceCollection AddWithTransientLifeTimeWithInterceptor(this IServiceCollection services, string assmblyNames)
        {
            return AddWithInterceptor(services, typeof(ITransientLifeTimeWithInterceptor), assmblyNames);
        }
        private static IServiceCollection AddWithSingletonLifeTimeWithInterceptor(this IServiceCollection services, string assmblyNames)
        {
            return AddWithInterceptor(services, typeof(ISingletonLifeTimeWithInterceptor), assmblyNames);
        }
        public static IServiceCollection AddCustomServices(this IServiceCollection services, Action<DependencyInjectionOption> setupAction)
        {
            var option = new DependencyInjectionOption();
            setupAction.Invoke(option);

            services.ConfigWebHosting()
                .AddWithTransientLifetime(option.AssmblyNamesForLoad)
                .AddWithScopedLifetime(option.AssmblyNamesForLoad)
                .AddWithSingletonLifetime(option.AssmblyNamesForLoad)
                .AddWithTransientLifeTimeWithInterceptor(option.AssmblyNamesForLoad)
                .AddWithScopedLifeTimeWithInterceptor(option.AssmblyNamesForLoad)
                .AddWithSingletonLifeTimeWithInterceptor(option.AssmblyNamesForLoad)
                .Configure(setupAction);

            return services;
        }
    }
}
