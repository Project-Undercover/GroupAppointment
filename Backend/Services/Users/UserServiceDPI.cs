using Core.IServices.Users;
using Microsoft.Extensions.DependencyInjection;

namespace Services.Users
{
    public static class UserServiceDPI
    {
        public static IServiceCollection AddUserService(this IServiceCollection services)
        {
            services.AddAutoMapper(typeof(UserServiceMapper));
            services.AddTransient<IUserService, UserService>();

            return services;
        }
    }
}
