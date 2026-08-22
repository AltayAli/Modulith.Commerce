using Modulith.Commerce.Common.Application.Abstractions;

namespace Modulith.Commerce.Common.Application.Exceptions
{
    public class ValidationException : Exception
    {
        public IEnumerable<ValidationError> Errors { get; }
        public ValidationException(IEnumerable<ValidationError> errors)
        {
            Errors = errors;
        }
    }
}
