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
                throw  new InvalidOperationException();
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
        private readonly T _value;

        public T Value
        {
            get
            {
                if (IsFailure)
                {
                    throw new InvalidOperationException();
                }

                return _value;
            }
        }

        public Result(T value, bool isSuccess, string error) : base(isSuccess, error)
        {
            _value = value;
        }
    }
}
