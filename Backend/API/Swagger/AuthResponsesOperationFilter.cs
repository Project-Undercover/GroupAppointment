using API.Middlewares;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;
using System.Reflection;
using System.Web;
using static API.Middlewares.Authorization;

namespace API.Swagger
{
    public class AuthResponsesOperationFilter : IOperationFilter
    {
        public void Apply(OpenApiOperation operation, OperationFilterContext context)
        {
            var authAttributes = context.MethodInfo.DeclaringType.GetCustomAttributes(true)
                .Union(context.MethodInfo.GetCustomAttributes(true))
                .OfType<IAuthorizationFilter>();

            if (!authAttributes.Any())
                return;


            if (!operation.Responses.ContainsKey("401"))
                operation.Responses.Add("401", new OpenApiResponse { Description = "Unauthorized - Authentication" });

            foreach (var auth in authAttributes)
            {
                //if (auth is AuthorizeApiKey)
                //    operation.Security.Add(new OpenApiSecurityRequirement { { APIKeyScheme(), Array.Empty<string>() } });

                if (auth is AuthorizeUserAttribute)
                {
                    operation.Security.Add(new OpenApiSecurityRequirement { { JWTScheme(), Array.Empty<string>() } });
                }
            }


        }


        public OpenApiSecurityScheme JWTScheme()
        {
            return new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference { Type = ReferenceType.SecurityScheme, Id = "bearerAuth" }
            };
        }


        public OpenApiSecurityScheme APIKeyScheme()
        {
            return new OpenApiSecurityScheme()
            {
                Reference = new OpenApiReference { Type = ReferenceType.SecurityScheme, Id = "apiKeyAuth" }
            };
        }
    }
}
