using Common.Utilities;
using Microsoft.Extensions.DependencyInjection;
using Common.ServiceHelpers.Implementation.AutoMapper;

namespace Common.ServiceHelpers.Implementation.Mapper
{
    public static class MapperServiceDependencyInjection
    {
        private static IServiceCollection AddMapperService(IServiceCollection services, MapperOption option)
        {
            var assemblies = ReflectionUtilities.GetAssemblies(option.AssmblyNamesForLoadProfiles);
            services.AddAutoMapper(assemblies);
            services.AddSingleton<IMapperService, MapperService>();
            return services;
        }
        public static IServiceCollection AddMapperService(this IServiceCollection services, Action<MapperOption> setupAction)
        {
            var option = new MapperOption();
            setupAction.Invoke(option);
            return AddMapperService(services, option);
        }
    }
}
