using Microsoft.OpenApi.Any;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace API.Swagger
{
    public class AddAcceptLanguageHeaderParameter : IOperationFilter
    {
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
                    Default = new OpenApiString(SupportedLanguage.HE.ToString())
                },
                Description = "The language of the API response",
            };


            operation.Parameters.Add(parameter);
        }
    }
}
