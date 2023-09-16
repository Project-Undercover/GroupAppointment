using API.Utils;
using Infrastructure.Entities.Shared;
using Infrastructure.Entities.Users;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Linq.Expressions;

namespace API.Middlewares
{
    public class FieldAuthorization<T> : Attribute, IAuthorizationFilter where T : Entity
    {

        //public Expression<Func<T, bool>> _authExpression;

        //public FieldAuthorization(Expression<Func<T, bool>> authExpression)
        //{
        //    _authExpression = authExpression;
        //}

        public void OnAuthorization(AuthorizationFilterContext context)
        {

        //    User user = context.HttpContext.GetUser<User>();


        //    _authExpression.Compile().Invoke();



        }
    }
}
