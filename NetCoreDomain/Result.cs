using System;

namespace NetCoreDomain
{
    public class Result
    {
        public bool IsSuccess { get; }
        public string Error { get; private set; }
        public bool IsFailure => IsSuccess == false;

        public Result(bool isSuccess, string error)
        {
            if (isSuccess && string.IsNullOrEmpty(error) == false)
            {
                throw new InvalidOperationException();
            }

            if (isSuccess == false && string.IsNullOrEmpty(error))
            {
                throw new InvalidOperationException();
            }

            IsSuccess = isSuccess;
            Error = error;
        }

        public static Result Ok()
        {
            return new Result(true, string.Empty);
        }

        public static Result Fail(string failMessage)
        {
            return new Result(false, failMessage);
        }
    }

    public class Result<T> : Result
    {
        public T Value { get; }

        public Result(T value, bool isSuccess, string error) : base(isSuccess, error)
        {
            Value = value;
        }

        public static Result<T> Ok(T resultValue)
        {
            return new Result<T>(resultValue, true, string.Empty);
        }

        public static Result<T> Fail(T resultValue, string errorMessage)
        {
            return new Result<T>(resultValue, false, errorMessage);
        }
    }
}
