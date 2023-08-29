using Microsoft.OpenApi.Any;
using Microsoft.OpenApi.Extensions;
using Microsoft.OpenApi.Models;
using Newtonsoft.Json.Schema;
using Swashbuckle.AspNetCore.SwaggerGen;
using System.ComponentModel;
using System.Text.Json.Serialization;

namespace API.Swagger
{
    public class AddAcceptLanguageHeaderParameter : IOperationFilter
    {
        [JsonConverter(typeof(JsonStringEnumConverter))]
        public enum SupportedLanguage
        {
            EN,
            HE,
            AR,
        }


        public void Apply(OpenApiOperation operation, OperationFilterContext context)
        {
            if (operation.Parameters == null)
            {
                operation.Parameters = new List<OpenApiParameter>();
            }


            var parameter = new OpenApiParameter
            {
                Name = "Language",
                In = ParameterLocation.Header,
                Required = false, // Depending on your use case
                Schema = new OpenApiSchema
                {
                    Type = "string",
                    Enum = Enum.GetNames(typeof(SupportedLanguage)).Select(lang => new OpenApiString(lang)).ToArray(),
                    Description = "Select language from the dropdown",
                }
            };


            operation.Parameters.Add(parameter);
        }
    }
}
