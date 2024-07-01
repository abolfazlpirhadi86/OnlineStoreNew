using Castle.DynamicProxy;
using Microsoft.Extensions.DependencyInjection;

namespace Common.Extensions
{
    public static class HostingExtensions
    {
        public static void AddProxiedScoped<TInterface, TImplementation>
            (this IServiceCollection services) where TInterface : class where TImplementation : class, TInterface
        {
            services.AddScoped<TImplementation>();
            services.AddScoped(typeof(TInterface), serviceProvider =>
            {
                var proxyGenerator = serviceProvider.GetRequiredService<ProxyGenerator>();
                var actual = serviceProvider.GetRequiredService<TImplementation>();
                var interceptors = serviceProvider.GetServices<IAsyncInterceptor>().ToArray();
                return proxyGenerator.CreateInterfaceProxyWithTarget(typeof(TInterface), actual, interceptors);
            });
        }
        public static void AddProxiedTransient<TInterface, TImplementation>
            (this IServiceCollection services) where TInterface : class where TImplementation : class, TInterface
        {
            services.AddTransient<TImplementation>();
            services.AddTransient(typeof(TInterface), serviceProvider =>
            {
                var proxyGenerator = serviceProvider.GetRequiredService<ProxyGenerator>();
                var actual = serviceProvider.GetRequiredService<TImplementation>();
                var interceptors = serviceProvider.GetServices<IAsyncInterceptor>().ToArray();
                return proxyGenerator.CreateInterfaceProxyWithTarget(typeof(TInterface), actual, interceptors);
            });
        }
        public static void AddProxiedSingleton<TInterface, TImplementation>
            (this IServiceCollection services) where TInterface : class where TImplementation : class, TInterface
        {
            services.AddSingleton<TImplementation>();
            services.AddSingleton(typeof(TInterface), serviceProvider =>
            {
                var proxyGenerator = serviceProvider.GetRequiredService<ProxyGenerator>();
                var actual = serviceProvider.GetRequiredService<TImplementation>();
                var interceptors = serviceProvider.GetServices<IAsyncInterceptor>().ToArray();
                return proxyGenerator.CreateInterfaceProxyWithTarget(typeof(TInterface), actual, interceptors);
            });
        }
        public static void AddProxiedScoped(this IServiceCollection services, Type interfaceType, Type implementationType)
        {
            services.AddScoped(implementationType);
            services.AddScoped(interfaceType, serviceProvider =>
            {
                var proxyGenerator = serviceProvider.GetRequiredService<ProxyGenerator>();
                var actual = serviceProvider.GetRequiredService(implementationType);
                var interceptors = serviceProvider.GetServices<IAsyncInterceptor>().ToArray();
                return proxyGenerator.CreateInterfaceProxyWithTarget(interfaceType, actual, interceptors);
            });
        }
        public static void AddProxiedTransient(this IServiceCollection services, Type interfaceType, Type implementationType)
        {
            services.AddTransient(implementationType);
            services.AddTransient(interfaceType, serviceProvider =>
            {
                var proxyGenerator = serviceProvider.GetRequiredService<ProxyGenerator>();
                var actual = serviceProvider.GetRequiredService(implementationType);
                var interceptors = serviceProvider.GetServices<IAsyncInterceptor>().ToArray();
                return proxyGenerator.CreateInterfaceProxyWithTarget(interfaceType, actual, interceptors);
            });
        }
        public static void AddProxiedSingleton(this IServiceCollection services, Type interfaceType, Type implementationType)
        {
            services.AddSingleton(implementationType);
            services.AddSingleton(interfaceType, serviceProvider =>
            {
                var proxyGenerator = serviceProvider.GetRequiredService<ProxyGenerator>();
                var actual = serviceProvider.GetRequiredService(implementationType);
                var interceptors = serviceProvider.GetServices<IAsyncInterceptor>().ToArray();
                return proxyGenerator.CreateInterfaceProxyWithTarget(interfaceType, actual, interceptors);
            });
        }
    }
}
