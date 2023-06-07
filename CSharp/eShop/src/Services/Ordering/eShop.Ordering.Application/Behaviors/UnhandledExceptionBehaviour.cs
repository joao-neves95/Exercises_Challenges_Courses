using MediatR;

using Microsoft.Extensions.Logging;

namespace eShop.Ordering.Application.Behaviors
{
    public sealed class UnhandledExceptionBehaviour<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
    {
        private readonly ILogger<TRequest> _logger;

        public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
        {
            try
            {
                return await next();
            }
            catch (Exception ex)
            {
                var requestName = typeof(TRequest).Name;
                _logger.LogError(
                    ex,
                    "Application Request Exception: Unhanded Exception on the Request {Name} {@Request}",
                    requestName, request);

                throw;
            }
        }
    }
}
