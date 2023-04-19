using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Hosting;

using Yld.GamingApi.WebApi.Extensions;

namespace Yld.GamingApi.WebApi.Attributes
{
    public class AppExceptionFilterAttribute : IExceptionFilter
    {
        private readonly IHostEnvironment _hostEnvironment;

        public AppExceptionFilterAttribute(IHostEnvironment hostEnvironment) => _hostEnvironment = hostEnvironment;

        public void OnException(ExceptionContext context)
        {
            context.Result = context.Exception.ToActionResult(_hostEnvironment.IsDevelopment());
        }
    }
}
