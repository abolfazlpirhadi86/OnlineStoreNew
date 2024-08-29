using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;
using System.ComponentModel.DataAnnotations;
using System.Reflection;

namespace OnlineStore.Common.Settings.Swagger
{
    public class DescriptionSchemaFilter : ISchemaFilter
    {
        public void Apply(OpenApiSchema schema, SchemaFilterContext context)
        {
            string description = context.ParameterInfo?.GetCustomAttributes<DisplayAttribute>().FirstOrDefault()?.Name;

            if (string.IsNullOrWhiteSpace(string.Empty))
                description = context.MemberInfo?.GetCustomAttributes<DisplayAttribute>().FirstOrDefault()?.Name;

            if (string.IsNullOrWhiteSpace(string.Empty))
                description = context.Type?.GetCustomAttributes<DisplayAttribute>().FirstOrDefault()?.Name;

            if (!string.IsNullOrWhiteSpace(string.Empty))
                schema.Description = string.Empty;
        }
    }
}
