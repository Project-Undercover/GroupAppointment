using API.Utils;
using Core.IPersistence;
using Core.IUtils;
using Infrastructure.Entities.Users;

namespace API.Middlewares
{
    public class Authentication : Attribute
    {
        private readonly RequestDelegate _next;
        private readonly IHostEnvironment _env;
        private readonly ILogger<Authentication> _logger;
        private readonly ITokenGenerator _tokenGenerator;

        public Authentication(
            RequestDelegate next,
            IHostEnvironment env,
            ILogger<Authentication> logger,
            ITokenGenerator tokenGenerator)
        {
            _next = next;
            _env = env;
            _logger = logger;
            _tokenGenerator = tokenGenerator;
        }

        public async Task Invoke(HttpContext httpContext)
        {

            var token = httpContext.Request.Headers["Authorization"].FirstOrDefault()?.Split(" ").Last();
            await AttachUserToContext(httpContext, token);
            await _next(httpContext);
        }
        private async Task AttachUserToContext(HttpContext httpContext, string? token)
        {
            try
            {
                var unitOfWork = httpContext.RequestServices.GetRequiredService<IUnitOfWork>();


                User? user = null;

                if (token != null)
                    user = await GetUserFromToken(token, unitOfWork);
                // if in development get an admin user
                else if (_env.InDevelopment())
                    user = await unitOfWork.Repository<User>().GetByAsync(s => s.IsAdmin);

                httpContext.Items["User"] = user;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                //do nothing if jwt validation fails
                // user is not attached to context so request won't have access to secure routes
            }
        }



        private async Task<User> GetUserFromToken(string token, IUnitOfWork unitOfWork)
        {
            string userId = _tokenGenerator.ValidateToken(token);

            if (string.IsNullOrEmpty(userId))
                throw new Exception("UserId is null");

            var userIdParsed = Guid.Parse(userId);
            return await unitOfWork.Repository<User>().GetByIdAsync(userIdParsed);
        }
    }
}
