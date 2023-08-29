using Infrastructure.DTOs;
using Infrastructure.Entities.Users;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc;
using API.Utils;
using Infrastructure.Exceptions;

namespace API.Middlewares
{
    public class Authorization
    {
        public class AuthorizeUserAttribute : Attribute, IAuthorizationFilter
        {
            public void OnAuthorization(AuthorizationFilterContext context)
            {
                try
                {
                    User user = context.HttpContext.GetUser<User>();
                    if (!user.IsActive)
                        throw new ForbiddenException(TranslationKeys.InActiveUser);
                }
                catch (Exception e)
                {
                    var logger = context.HttpContext.RequestServices.GetRequiredService<ILogger<User>>();
                    logger.LogError(e.Message);
                    throw new UnAuthorizedException(TranslationKeys.UnAuthorized);
                }
            }
        }
    }
}
