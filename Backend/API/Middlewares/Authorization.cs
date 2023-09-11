using API.Utils;
using Infrastructure.Entities.Users;
using Infrastructure.Exceptions;
using Microsoft.AspNetCore.Mvc.Filters;
using static Infrastructure.Enums.Enums;

namespace API.Middlewares
{
    public class Authorization
    {
        public class AuthorizeUserAttribute : Attribute, IAuthorizationFilter
        {

            public List<UserRole> roles;
            public bool allowAll = false;

            public AuthorizeUserAttribute(params UserRole[] role)
            {
                roles = role.Distinct().ToList();
            }

            public AuthorizeUserAttribute()
            {
                allowAll = true;
            }


            public void OnAuthorization(AuthorizationFilterContext context)
            {
                try
                {
                    User user = context.HttpContext.GetUser<User>();

                    if (!allowAll && !user.IsAdmin && !roles.Contains(user.Role))
                        throw new UnAuthorizedException(TranslationKeys.UnAuthorized);

                    if (!user.IsActive)
                        throw new ForbiddenException(TranslationKeys.InActiveUser);
                }
                catch (Exception e) when (e is not BaseException)
                {
                    var logger = context.HttpContext.RequestServices.GetRequiredService<ILogger<User>>();
                    logger.LogError(e.Message);
                    throw new UnAuthorizedException(TranslationKeys.UnAuthorized);
                }
            }
        }
    }
}
