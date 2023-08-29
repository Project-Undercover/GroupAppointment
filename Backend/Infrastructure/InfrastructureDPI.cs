using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure
{
    public static class InfrastructureDPI
    {
        public static IServiceCollection AddInfrastructureLayer(this IServiceCollection services)
        {
            return services;
        }
    }
}