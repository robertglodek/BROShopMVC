using FluentValidation.Results;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BRO.Domain.Utilities
{
    public class Result
    {
        private Result(bool isSuccess, string message, IEnumerable<Error> errors)
        {
            IsSuccess = isSuccess;
            Message = message;
            Errors = errors;
        }
        public string Message { get; }
        public bool IsSuccess { get; }
        public IEnumerable<Error> Errors { get; }
        public static Result Fail()
       => new Result(false, "", Enumerable.Empty<Error>());
        public static Result Fail(string message)
            => new Result(false, message, Enumerable.Empty<Error>());
        public static Result Fail(ValidationResult validationResult)
            => new Result(
                false,
                string.Join(", ", validationResult.Errors.Select(x => x.ErrorMessage)),
                validationResult.Errors.Select(x => new Error(x.PropertyName, x.ErrorMessage))
            );
        public static Result Ok()
            => new Result(true, "", Enumerable.Empty<Error>());
        public static Result Ok(string message)
            => new Result(true, message, Enumerable.Empty<Error>());
      
        public class Error
        {
            public Error(string propertyName, string message)
            {
                PropertyName = propertyName;
                Message = message;
            }
            public string PropertyName { get; set; }
            public string Message { get; set; }
        }
    }
}
