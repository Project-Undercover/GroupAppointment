using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.OpenApi.Models;
using System.Reflection;

namespace API.Swagger
{
    public static class SwaggerConfig
    {
        public const string Version = "v1.0.0";
        public const bool TokenSecurityEnabled = true;


        public static IServiceCollection AddSwaggerWithConfig(this IServiceCollection services, string env)
        {
          
            services.AddSwaggerGen(config =>
            {
                config.EnableAnnotations();
                config.SwaggerDoc("v1", ApiInfo(env));
                config.CustomSchemaIds(type => type.FullName?.Replace("+", "_"));
                config.OperationFilter<AddAcceptLanguageHeaderParameter>();
        

                if (TokenSecurityEnabled)
                {
                    var jwtSecurityScheme = TokenSecurity();
                    config.AddSecurityDefinition(jwtSecurityScheme.Reference.Id, jwtSecurityScheme);
                    config.AddSecurityRequirement(new OpenApiSecurityRequirement { { jwtSecurityScheme, Array.Empty<string>() } });
                }


                // for adding summary text, Add this "<GenerateDocumentationFile>true</GenerateDocumentationFile>" in PropertyGroup tag of .csproj file
                var xmlFilename = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                config.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, xmlFilename));
            });

            return services;
        }


        /// <summary>
        /// Open API Information
        /// </summary>
        /// <param name="env">Current Enviroment</param>
        /// <returns></returns>
        private static OpenApiInfo ApiInfo(string env) => new OpenApiInfo
        {
            Title = "Documentation",
            Version = $"{Version} - {env}",
            License = new OpenApiLicense() { Name = "Tarik & Wissam" }
        };


        /// <summary>
        /// Token Security Scheme
        /// </summary>
        /// <returns></returns>
        private static OpenApiSecurityScheme TokenSecurity() => new OpenApiSecurityScheme
        {
            Scheme = "bearer",
            BearerFormat = "JWT",
            Name = "JWT Authentication",
            In = ParameterLocation.Header,
            Type = SecuritySchemeType.Http,
            Description = "Put **_ONLY_** your JWT Bearer token in the text box below!",
            Reference = new OpenApiReference
            {
                Id = JwtBearerDefaults.AuthenticationScheme,
                Type = ReferenceType.SecurityScheme,
            }
        };
    }
}
