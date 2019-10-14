﻿using System.Net;

 namespace Pokegraf.Common.ErrorHandling
{
    public static class Helpers
    {
        public static ResultError NotFound(string message)
        {
            return new ResultError
            {
                Type = ResultErrorType.NotFound,
                Message = message
            };
        }

        public static ResultError HttpError(string message, HttpStatusCode statusCode)
        {
            return new ResultError
            {
                Type = ResultErrorType.HttpError,
                Message = message,
                Code = statusCode
            };
        }

        public static ResultError UnknownError(string message)
        {
            return new ResultError
            {
                Type = ResultErrorType.UnknownError,
                Message = message
            };
        }
    }
}