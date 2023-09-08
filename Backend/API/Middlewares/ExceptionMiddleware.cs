using API.Utils;
using Core.IUtils;
using Infrastructure.DTOs;
using Infrastructure.Exceptions;

namespace API.Middlewares
{
    public class ExceptionMiddleware
    {
        private const string JsonContentType = "application/json";
        private readonly RequestDelegate _next;
        private readonly ILogger<ExceptionMiddleware> _logger;
        private readonly ITranslationService _translationService;
        private readonly IHostEnvironment _env;

        /// <summary>
        /// Initializes a new instance of the <see cref="ExceptionMiddleware"/> class.
        /// </summary>
        /// <param name="next">The next.</param>
        public ExceptionMiddleware(RequestDelegate next, ILogger<ExceptionMiddleware> logger, ITranslationService translationService, IHostEnvironment env)
        {
            _next = next;
            _logger = logger;
            _translationService = translationService;
            _env = env;
        }

        /// <summary>
        /// Invokes the specified context.
        /// </summary>
        /// <param name="context">The context.</param>
        /// <returns></returns>
        public Task Invoke(HttpContext context) => this.InvokeAsync(context);

        async Task InvokeAsync(HttpContext context)
        {
            string langKey = Headers.GetLanguage(context.Request.Headers);

            try
            {
                 await _next(context);
            }
            catch (NotFoundException e)
            {
                _logger.LogError(e.Message);
                await Response(context, StatusCodes.Status404NotFound, langKey, e);
            }
            catch (ValidationException e)
            {
                _logger.LogError(e.Message);
                await Response(context, StatusCodes.Status400BadRequest, langKey, e);
            }
            catch (ForbiddenException e)
            {
                _logger.LogError(e.Message);
                await Response(context, StatusCodes.Status403Forbidden, langKey, e);
            }
            catch (UnAuthorizedException e)
            {
                _logger.LogError(e.Message);
                await Response(context, StatusCodes.Status401Unauthorized, langKey, e);
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message);

                if (_env.InDevelopment() || true)
                {
                    await ResponseDev(context, StatusCodes.Status400BadRequest, e.Message); // send the real error
                    return;
                }
                await Response(context, StatusCodes.Status400BadRequest, langKey);
            }
        }


        async Task Response(HttpContext context, int code, string langKey, BaseException? baseException = null)
        {

            string message = baseException is not null ?
                _translationService.GetByKey(baseException.Key, langKey, baseException.Args)
                : _translationService.GetByKey(TranslationKeys.RequestFailed, langKey);



            // set http status code and content type
            context.Response.StatusCode = code;
            context.Response.ContentType = JsonContentType;

            // writes / returns error model to the response
            await context.Response.WriteAsJsonAsync(MessageResponseFactory.Create(message));
        }



        async Task ResponseDev(HttpContext context, int code, string message)
        {
            // set http status code and content type
            context.Response.StatusCode = code;
            context.Response.ContentType = JsonContentType;

            // writes / returns error model to the response
            await context.Response.WriteAsJsonAsync(MessageResponseFactory.Create(message));
        }
    }
}
