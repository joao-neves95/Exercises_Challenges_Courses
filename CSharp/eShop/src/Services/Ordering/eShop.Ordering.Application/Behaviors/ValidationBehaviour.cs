using eShop.Ordering.Application.Exceptions;

using FluentValidation;

using MediatR;

namespace eShop.Ordering.Application.Behaviors
{
    public sealed class ValidationBehaviour<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
    {
        private readonly IEnumerable<IValidator<TRequest>> _validators;

        public ValidationBehaviour(IEnumerable<IValidator<TRequest>> validators)
        {
            _validators = validators ?? throw new ArgumentNullException(nameof(validators));
        }

        public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
        {
            if (_validators.Any())
            {
                var validationContext = new ValidationContext<TRequest>(request);
                var validationResults = await Task.WhenAll(
                    _validators.Select(validator => validator.ValidateAsync(validationContext, cancellationToken)));

                var failures = validationResults.SelectMany(result => result.Errors).Where(fail => fail != null);

                if (validationResults.Any())
                {
                    throw new ValidationApplicationException(failures.ToArray());
                }
            }

            return await next();
        }
    }
}
