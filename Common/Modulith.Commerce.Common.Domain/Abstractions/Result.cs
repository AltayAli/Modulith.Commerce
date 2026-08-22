using Newtonsoft.Json;

namespace Modulith.Commerce.Common.Domain.Abstractions
{
    public class Result
    {
        protected internal Result(bool isSuccess, Error error)
        {
            if (!isSuccess && (error is null || error == Error.None))
            {
                throw new InvalidOperationException();
            }

            if (isSuccess && !(error is null || error == Error.None))
            {
                throw new InvalidOperationException();
            }

            IsSuccess = isSuccess;
            Error = error;
        }
        public bool IsSuccess { get; }
        public bool IsFailure => !IsSuccess;
        [JsonProperty("error", NullValueHandling = NullValueHandling.Ignore)]
        public Error? Error { get; }
        public static Result Success() => new Result(true, Error.None);
        public static Result Failure(Error error) => new Result(false, error);
        public static Result<T> Success<T>(T value) => new Result<T>(value, true, Error.None);
        public static Result<T> Failure<T>(T value, Error error) => new Result<T>(value, false, error);
        public static Result<T> Create<T>(T value)
                => value is null ? Failure<T>(value, Error.NullValue) : Success<T>(value);
    }

    public class Result<T> : Result
    {
        public Result(T value, bool isSuccess, Error error) : base(isSuccess, error)
        {
            Value = value;
        }
        public T Value { get; private set; }

        public static implicit operator T(Result<T> value) => Create<T>(value);
    }
}
