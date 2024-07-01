using Common.ServiceHelpers;
using Common.ServiceHelpers.Implementation.CustomMapping;
using Common.ServiceHelpers.Implementation.Mapper;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Builder;
using ProductManagement.Domain;
using ProductManagement.Application;
using Common.ServiceHelpers.Implementation.DependencyInjection;
using Castle.DynamicProxy;
using FluentValidation;

namespace ProductManagement.Infrastructure.Persistence.Extensions
{
    public static class ProductManagementBootstrapper
    {
        public static WebApplication ConfigureServices(this WebApplicationBuilder builder)
        {
            var services = builder.Services;
            var configuration = builder.Configuration;

            var domainAssembly = typeof(DomainAssembly).Assembly;
            var applicationAssembly = typeof(ApplicationAssembly).Assembly;
            var infrastructureAssembly = typeof(PersistenceAssembly).Assembly;

            //services.AddSingleton(new ProxyGenerator());

            #region Services 
            //services.AddCustomServices(e => e.AssmblyNamesForLoad = applicationAssembly.GetName().Name);

            services.AddValidatorsFromAssemblies(new List<System.Reflection.Assembly> { domainAssembly }, lifetime: ServiceLifetime.Singleton);

            #endregion

            #region AutoMapper
            services.AddSingleton<IMapperService, MapperService>();
            AutoMapperConfiguration.assambly = domainAssembly;
            builder.Services.AddAutoMapper(AutoMapperConfiguration.InitializeAutoMapper);
            #endregion

            var webApplication = builder.Build();

            #region Static Classes DI Configuring
            //DependencyInjectionUtilities.ConfigureDIForServices(webApplication.Services, configuration);
            #endregion

            #region Other
            //services.AddHttpContextAccessor();
            //services.AddHttpClient();
            #endregion

            return webApplication;
        }

        public static WebApplication ConfigurePipeLine(this WebApplication app)
        {
            return app;
        }
        //public static void Configure(IServiceCollection services, string connectionString)
        //{
        //    services.AddSingleton<IMapperService, MapperService>();
        //    AutoMapperConfiguration.assambly = domainAssembly;
        //    services.Services.AddAutoMapper(AutoMapperConfiguration.InitializeAutoMapper);

        //    services.AddTransient<IProductCategoryRepository, ProductCategoryRepository>();
        //    services.AddTransient<IProductCategoryService, ProductCategoryService>();

        //    services.AddDbContext<ProductManagementContext>(x => x.UseSqlServer(connectionString));
        //}
    }
}
