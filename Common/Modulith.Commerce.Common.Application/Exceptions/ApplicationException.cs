using Modulith.Commerce.Common.Domain.Abstractions;

namespace Modulith.Commerce.Common.Application.Exceptions
{
    public class ApplicationException : Exception
    {
        public string RequestName { get; set; }
        public Error? Error { get; set; }
        public ApplicationException(string requestName, Error? error, Exception? innerException) : base("Application Exception", innerException)
        {
            RequestName = requestName;
            Error = error;
        }
    }
}
