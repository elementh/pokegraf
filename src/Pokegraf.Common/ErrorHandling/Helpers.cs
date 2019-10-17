﻿using System.Net;

 namespace Pokegraf.Common.ErrorHandling
{
    public static class Helpers
    {
        public static Error NotFound(string message)
        {
            return new Error
            {
                Type = ErrorType.NotFound,
                Message = message
            };
        }

        public static Error HttpError(string message, HttpStatusCode statusCode)
        {
            return new Error
            {
                Type = ErrorType.HttpError,
                Message = message,
                Code = statusCode
            };
        }

        public static Error UnknownError(string message)
        {
            return new Error
            {
                Type = ErrorType.UnknownError,
                Message = message
            };
        }

        public static Error Timeout(string message)
        {
            return new Error
            {
                Type = ErrorType.Timeout,
                Message = message
            };
        }
    }
}