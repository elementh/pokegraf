using System.Collections.Generic;

namespace Pokegraf.Common.Result
{
    public class Result : IResult
    {
        public IDictionary<string, string[]> Errors { get; protected set; }
        public bool Succeeded { get; protected set; }

        public Result()
        {
            Errors = new Dictionary<string, string[]>();
        }

        public void AddError(string errorKey, List<string> errorDescription)
        {
            if (Errors.Keys.Contains(errorKey))
            {
                errorDescription.AddRange(Errors[errorKey]);

                Errors.Remove(errorKey);
            }
            
            Errors.Add(errorKey, errorDescription.ToArray());
        }
        
        public static Result Fail(string errorKey, List<string> errorDescription)
        {
            var result = new Result()
            {
                Succeeded = false
            };
            
            result.AddError(errorKey, errorDescription);

            return result;
        }
        
        public static Result NotFound(List<string> errorDescription)
        {
            return Fail("not_found", errorDescription);
        }
        
        public static Result UnknownError(List<string> errorDescription)
        {
            return Fail("unknown_error", errorDescription);
        }
        
        public static Result ValidationFailure(List<string> errorDescription)
        {
            return Fail("validation_failures", errorDescription);
        }

        public static Result Success()
        {
            var result = new Result()
            {
                Succeeded = true
            };

            return result; 
        }
    }
    
    public class Result<T> : Result, IResult<T>
    {
        public T Value { get; protected set; }

        public new static Result<T> Fail(string errorKey, List<string> errorDescription)
        {
            return FromResult(Result.Fail(errorKey, errorDescription));
        }

        public static Result<T> Success(T value)
        {
            var result = new Result<T>()
            {
                Succeeded = true,
                Value = value
            };

            return result;
        }
        
        public new static Result<T> NotFound(List<string> errorDescription)
        {
            return FromResult(Result.NotFound(errorDescription));
        }
        
        public new static Result<T> UnknownError(List<string> errorDescription)
        {
            return FromResult(Result.UnknownError(errorDescription));
        }
        
        public new static Result<T> ValidationFailure(List<string> errorDescription)
        {
            return FromResult(Result.ValidationFailure(errorDescription));
        }

        public static Result<T> FromResult(IResult result)
        {
            var newResult = new Result<T>()
            {
                Errors = result.Errors,
                Succeeded = result.Succeeded,
                Value = default(T)
            };

            return newResult;
        }
    }
}