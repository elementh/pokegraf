﻿using System.Net;

 namespace Pokegraf.Common.ErrorHandling
{
    public class ResultError
    {
        /// <summary>
        /// Error type.
        /// </summary>
        public ResultErrorType Type { get; set; }
        /// <summary>
        /// Message describing the error.
        /// </summary>
        public string Message { get; set; }
        /// <summary>
        /// Optional, available when error type is HttpError
        /// </summary>
        public HttpStatusCode Code { get; set; }
    }

    public enum ResultErrorType
    {
        NotFound,
        HttpError,
        UnknownError,
        Timeout
    }
}