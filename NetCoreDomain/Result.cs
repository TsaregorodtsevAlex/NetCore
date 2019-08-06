using System;

namespace NetCoreDomain
{
    public class Result
    {
        public bool IsSuccess { get; }
        public string Error { get; }
        public bool IsFailure => IsSuccess == false;

        public ResultErrorObject ErrorObject { get; }

        public Result(bool isSuccess, string error, object errorObject)
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
            ErrorObject = new ResultErrorObject
            {
                Message = error,
                Object = errorObject
            };
        }

        public static Result Ok()
        {
            return new Result(true, string.Empty, null);
        }

        public static Result Fail(string failMessage)
        {
            return new Result(false, failMessage, null);
        }

        public void ThrowExceptionIfFailure()
        {
            if (IsFailure)
            {
                throw new Exception(Error);
            }
        }
    }

    public class Result<T> : Result
    {
        public T Value { get; }

        public bool HasValue => Value != null;

        public Result(T value, bool isSuccess, string error, object errorObject) : base(isSuccess, error, errorObject)
        {
            Value = value;
        }

        public static Result<T> Ok(T resultValue)
        {
            return new Result<T>(resultValue, true, string.Empty, null);
        }

        public static Result<T> Fail(T resultValue, string errorMessage)
        {
            return new Result<T>(resultValue, false, errorMessage, null);
        }

        public static Result<T> Fail(T resultValue, string errorMessage, object errorObject)
        {
            return new Result<T>(resultValue, false, errorMessage, errorObject);
        }

        public Result<TResult> ReturnFail<TResult>() where TResult : class
        {
            return Result<TResult>.Fail(null, Error);
        }
    }

    public class ResultErrorObject
    {
        public string Message { get; set; }
        public object Object { get; set; }
    }
}
