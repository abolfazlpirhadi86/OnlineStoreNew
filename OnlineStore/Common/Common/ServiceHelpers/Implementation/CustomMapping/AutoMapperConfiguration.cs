using AutoMapper;
using System.Reflection;

namespace Common.ServiceHelpers.Implementation.CustomMapping
{
    public static class AutoMapperConfiguration
    {
        public static Assembly assambly;
        public static void InitializeAutoMapper(this IMapperConfigurationExpression config)
        {
            var _mappConfig = new MapperConfiguration(cfg =>
            {
                config.AddCustomMappingProfile(assambly);
            });

            _mappConfig.CompileMappings();
        }
        public static void AddCustomMappingProfile(this IMapperConfigurationExpression config, params Assembly[] assemblies)
        {
            var allTypes = assemblies.SelectMany(a => a.ExportedTypes);// every class can out of project can be access it

            var list = allTypes.Where(type => type.IsClass && !type.IsAbstract &&
                type.GetInterfaces().Contains(typeof(ICutomMapping)))
                .Select(type => (ICutomMapping)Activator.CreateInstance(type));

            var profile = new CustomMappingProfile(list);

            config.AddProfile(profile);
        }
    }
}
