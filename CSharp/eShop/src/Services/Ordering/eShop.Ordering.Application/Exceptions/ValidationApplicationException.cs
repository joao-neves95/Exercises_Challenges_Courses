using FluentValidation.Results;

namespace eShop.Ordering.Application.Exceptions
{
    public class ValidationApplicationException : ApplicationException
    {
        private readonly IDictionary<string, string[]> Errors;

        public ValidationApplicationException()
            : base("One or more validation errors occurred.")
        {
        }

        public ValidationApplicationException(IEnumerable<ValidationFailure> failures)
            : this()
        {
            Errors = failures
                .GroupBy(fail => fail.PropertyName, fail => fail.ErrorMessage)
                .ToDictionary(failGroup => failGroup.Key, failGroup => failGroup.ToArray());
        }
    }
}
