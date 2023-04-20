using Microsoft.AspNetCore.Mvc.Filters;

using Yld.GamingApi.WebApi.Extensions;

namespace Yld.GamingApi.WebApi.ActionFilters
{
    public class ExceptionHttpResponseFilter : IActionFilter, IExceptionFilter
    {
        private readonly IHostEnvironment _hostEnvironment;

        public ExceptionHttpResponseFilter(IHostEnvironment hostEnvironment) => _hostEnvironment = hostEnvironment;

        public void OnActionExecuting(ActionExecutingContext context)
        {
            // Method intentionally left empty.
        }

        public void OnActionExecuted(ActionExecutedContext context)
        {
            if (context?.Exception is null)
            {
                return;
            }

            context.Result = context.Exception.ToActionResult(_hostEnvironment.IsDevelopment());
            context.ExceptionHandled = true;
        }

        public void OnException(ExceptionContext context)
        {
            if (context?.Exception is null)
            {
                return;
            }

            context.Result = context.Exception.ToActionResult(_hostEnvironment.IsDevelopment());
            context.ExceptionHandled = true;
        }
    }
}
