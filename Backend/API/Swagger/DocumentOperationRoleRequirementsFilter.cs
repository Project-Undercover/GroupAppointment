using Azure;
using Microsoft.AspNetCore.Authorization;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;
using System.Data;
using System.Reflection;
using System.Web;
using static API.Middlewares.Authorization;

namespace API.Swagger
{
    public class DocumentOperationRoleRequirementsFilter : IOperationFilter
    {
        public void Apply(OpenApiOperation operation, OperationFilterContext context)
        {
            var auth = context.MethodInfo.GetCustomAttributes<AuthorizeUserAttribute>().FirstOrDefault();
            if (auth is null) return;


            if (auth.allowAll)
            {
                operation.Description = $@"**Roles Required:** {$"<code>{HttpUtility.HtmlEncode("Allow All")}</code>"}<br/>{operation.Description}";
                operation.Summary = $"Roles: Allow All {operation.Summary}";
                return;
            }


            var roles = auth.roles;
            if (roles.Count() > 0)
            {
                operation.Description = $@"**Roles Required:** {string.Join(", ", roles.Select(r => $"<code>{HttpUtility.HtmlEncode(r)}</code>"))}<br/>{operation.Description}";
                operation.Summary = $"Roles: {string.Join(", ", roles.Select(r => r))} {operation.Summary}";
            }
        }

    }
}
