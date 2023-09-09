using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc;
using Infrastructure.DTOs;
using Azure.Core;
using Core.IUtils;

namespace API.Utils
{
    public class CustomValidationErrorFilter : IResultFilter
    {
        public void OnResultExecuted(ResultExecutedContext context)
        {
            // No need to implement this method
        }

        public void OnResultExecuting(ResultExecutingContext context)
        {
            if (context.Result is ObjectResult objectResult && objectResult.StatusCode == 400)
            {
                string langKey = Headers.GetLanguage(context.HttpContext.Request.Headers);
                ITranslationService? translationService = context.HttpContext.RequestServices.GetService<ITranslationService>();


                var validationProblem = objectResult.Value as ValidationProblemDetails;

                if (validationProblem != null)
                {
                    string fieldName = validationProblem.Errors.Keys.FirstOrDefault() ?? "Field";
                    fieldName = char.ToUpper(fieldName[0]) + fieldName.Substring(1);


                    var errorMessage = validationProblem.Errors.Values.FirstOrDefault()?.FirstOrDefault() ?? "";
                    var message = translationService != null ? translationService.GetByKey(TranslationKeys.Invalid, langKey, fieldName) : errorMessage;

                    var errorResponse = MessageResponseFactory.Create(message);

                    context.Result = new ObjectResult(errorResponse)
                    {
                        StatusCode = 400
                    };
                }
            }
        }
    }
}
