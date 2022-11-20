using System;

namespace Sys.Common.Models
{
    public class RestApiResponse
    {
        public bool IsSuccess { get; set; }
        public string ExceptionMessage { get; set; }
        public Exception Exception { get; set; }
        public bool IsCustomException { get; set; }
        public CustomExceptionResponse CustomException { get; set; }
    }

    public class RestApiResponse<T> : RestApiResponse
    {
        public T Result { get; set; }
    }

    public class CustomExceptionResponse
    {
        public string Code { get; set; }
        public string Message { get; set; }
    }
}