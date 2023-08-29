namespace API
{
    public static class AppSettings
    {
        public static IServiceCollection AddConfig(this IServiceCollection services, IConfiguration config)
        {
            services.Configure<ConnectionStrings>(config.GetSection(nameof(ConnectionStrings)));
            return services;
        }

        public class APIURLs
        {
            public required string Base { get; set; }
        }
        public class ConnectionStrings
        {
            public required string Default { get; set; } = String.Empty;
        }
    }
}
