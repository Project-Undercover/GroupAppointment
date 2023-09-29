using Microsoft.OpenApi.Models;
using System.Reflection;

namespace API.Swagger
{
    public static class SwaggerConfig
    {
        public const string Version = "v1.0.0";


        public static IServiceCollection AddSwaggerWithConfig(this IServiceCollection services, string env)
        {
            //services.AddApiVersioning(options =>
            //{
            //    options.AssumeDefaultVersionWhenUnspecified = true;
            //    options.DefaultApiVersion = new Microsoft.AspNetCore.Mvc.ApiVersion(1, 0);
            //});

            //services.AddApiVersioning(opt =>
            //{
            //    opt.DefaultApiVersion = new Microsoft.AspNetCore.Mvc.ApiVersion(1, 0);
            //    opt.AssumeDefaultVersionWhenUnspecified = true;
            //    opt.ReportApiVersions = true;
            //    opt.ApiVersionReader = ApiVersionReader.Combine(new UrlSegmentApiVersionReader(),
            //                                                    new HeaderApiVersionReader("x-api-version"),
            //                                                    new MediaTypeApiVersionReader("x-api-version"));
            //});

            services.AddSwaggerGen(config =>
            {
                config.EnableAnnotations();
                config.SwaggerDoc("v1", ApiInfo(env));
                config.CustomSchemaIds(type => type.FullName?.Replace("+", "_"));
                config.OperationFilter<AddAcceptLanguageHeaderParameter>();
                config.OperationFilter<AuthResponsesOperationFilter>();
                config.OperationFilter<DocumentOperationRoleRequirementsFilter>();

                config.AddSecurityDefinition("bearerAuth", TokenSecurity());
                config.AddSecurityDefinition("apiKeyAuth", ApiKeySecurity());

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
            Title = "Watten Documentation",
            Description = "",
            Version = $"{Version} - {env}",
            //TermsOfService = new Uri("https://blog.georgekosmidis.net/privacy-policy/"),
            Contact = new OpenApiContact
            {
                Name = "Tarik Husin",
                Email = "tarik.id.10@hotmail.com",
                Url = new Uri("https://github.com/6arek212")
            },
            License = new OpenApiLicense
            {
                Name = "MIT License",
                Url = new Uri("https://opensource.org/licenses/MIT")
            }
        };


        /// <summary>
        /// Token Security Scheme
        /// </summary>
        /// <returns></returns>
        public static OpenApiSecurityScheme TokenSecurity() => new OpenApiSecurityScheme
        {
            Scheme = "bearer",
            BearerFormat = "JWT",
            Name = "JWT Authentication",
            In = ParameterLocation.Header,
            Type = SecuritySchemeType.Http,
            Description = "Put **_ONLY_** your JWT Bearer token in the text box below!"
        };



        public static OpenApiSecurityScheme ApiKeySecurity() => new OpenApiSecurityScheme
        {
            In = ParameterLocation.Header,
            Type = SecuritySchemeType.ApiKey,
            Name = "ApiKey",
            Scheme = "apikey",
            Description = "Input apikey to access this API"
        };
    }
}
