using Core.IServices.Auth;
using Microsoft.Extensions.DependencyInjection;

namespace Services.Auth
{
    public static class AuthServiceDPI
    {
        public static IServiceCollection AddAuthService(this IServiceCollection services)
        {
            services.AddTransient<IAuthService, AuthService>();

            return services;
        }
    }
}
