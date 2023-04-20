using System.Net;

using GamingApi.WebApi.Core.Exceptions;

using Microsoft.AspNetCore.Mvc;

namespace Yld.GamingApi.WebApi.Extensions
{
    public static class ExceptionExtensions
    {
        public static IActionResult ToActionResult(this Exception exception, bool isDevEnv)
        {
            if (exception.Is<InvalidInputException>())
            {
                return new BadRequestObjectResult(exception.Message);
            }

            if (exception.Is<NotFoundException>())
            {
                return new NotFoundObjectResult(exception.Message);
            }

            return new ObjectResult(isDevEnv ? exception.ToString() : nameof(HttpStatusCode.InternalServerError))
            {
                StatusCode = (int)HttpStatusCode.InternalServerError,
            };
        }

        public static bool Is<T>(this Exception exception)
        {
            return exception.GetType().IsAssignableFrom(typeof(T));
        }
    }
}
