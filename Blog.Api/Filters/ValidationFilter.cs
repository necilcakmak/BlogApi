using Blog.Core.Results;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Blog.APi.Filters
{
    public class ValidationFilter : ActionFilterAttribute
    {
        //hata geldiğinde çalıştır
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            if (!context.ModelState.IsValid)
            {
                Result result = new(false, "Validation Errors");

                Dictionary<string, string> validationErrors = new();

                validationErrors = context.ModelState
                .Where(x => x.Value.Errors.Count > 0)
                .ToDictionary(
                    kvp => kvp.Key,
                    kvp => kvp.Value.Errors.Select(e => e.ErrorMessage).First()
                );
                result.ValidationErrors = validationErrors;
                context.Result = new BadRequestObjectResult(result);
            }
        }
    }
}
