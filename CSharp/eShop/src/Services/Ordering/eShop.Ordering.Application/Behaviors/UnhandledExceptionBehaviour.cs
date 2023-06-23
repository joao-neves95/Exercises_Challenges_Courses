using MediatR;

using Microsoft.Extensions.Logging;

namespace eShop.Ordering.Application.Behaviors
{
    public sealed class UnhandledExceptionBehaviour<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
    {
        private readonly ILogger<TRequest> _logger;

        public UnhandledExceptionBehaviour(ILogger<TRequest> logger)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
        {
            try
            {
                return await next();
            }
            catch (Exception ex)
            {
                _logger.LogError(
                    ex,
                    "Application Request Exception: Unhanded Exception on the Request {Name} {@Request}",
                    typeof(TRequest).Name,
                    request);

                throw;
            }
        }
    }
}
