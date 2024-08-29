using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using OnlineStore.Application;
using OnlineStore.Common.Services;
using OnlineStore.Common.Services.Mapper.AutoMapper;
using OnlineStore.Common.Services.Mapper.Configurations;
using OnlineStore.Common.Settings.Swagger;
using OnlineStore.Infrastructure.DataBase;
using OnlineStores.Domain;

namespace OnlineStore.Infrastructure.Extensions
{
    public static class HostingExtensions
    {
        public static WebApplication ConfigureServices(this WebApplicationBuilder builder)
        {
            var currentSystem = "OnlineStore";
            var services = builder.Services;
            var configuration = builder.Configuration;

            var domainAssembly = typeof(DomainAssembly).Assembly;
            var applicationAssembly = typeof(ApplicationAssembly).Assembly;
            var infrastructureAssembly = typeof(InfrastructureAssembly).Assembly;

            #region Database            
            services.AddDbContext<ApplicationDbContext>(options =>
            {
                options.UseSqlServer(configuration.GetConnectionString("ApplicationDbContextConnectionString"));
            });
            #endregion

            #region Swagger            
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo
                {
                    Version = "v1",
                    Title = currentSystem.ToString(),
                    Description = $"{currentSystem.ToString()} Project.",
                    Contact = new OpenApiContact
                    {
                        Name = "OnlineStore",
                        Email = "OnlineStore@gmail.com",
                        Url = new Uri("https://Tiddev.com/")
                    }
                });
                c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    Name = "Authorization",
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.ApiKey,
                    Scheme = "Bearer",
                    BearerFormat = "JWT",
                    Description = "Input your Bearer token in this format - Bearer {your token here} to access this API"
                });
                c.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Type = ReferenceType.SecurityScheme,
                                Id = "Bearer"
                            },
                            Scheme = "Bearer",
                            Name = "Bearer",
                            In = ParameterLocation.Header
                        },
                        new List<string>()
                    }
                });

                c.SchemaFilter<DescriptionSchemaFilter>();
            });
            #endregion

            #region AutoMapper
            services.AddSingleton<IMapperService, MapperService>();
            AutoMapperConfiguration.assambly = domainAssembly;
            builder.Services.AddAutoMapper(AutoMapperConfiguration.InitializeAutoMapper);
            #endregion

            var webApplication = builder.Build();
            return webApplication;
        }

        public static WebApplication ConfigurePipeLine(this WebApplication app)
        {
            #region Auth
            app.UseAuthentication();
            app.UseAuthorization();
            #endregion

            #region Middleware
            app.UseMiddleware<ExceptionHandlerMiddleware>();
            #endregion

            #region Swagger            
            app.UseSwagger();
            app.UseSwaggerUI();
            #endregion

            return app;
        }
    }
}
