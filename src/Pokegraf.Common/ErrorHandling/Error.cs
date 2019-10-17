﻿using System.Net;

 namespace Pokegraf.Common.ErrorHandling
{
    public class Error
    {
        /// <summary>
        /// Error type.
        /// </summary>
        public ErrorType Type { get; set; }
        /// <summary>
        /// Message describing the error.
        /// </summary>
        public string Message { get; set; }
        /// <summary>
        /// Optional, available when error type is HttpError
        /// </summary>
        public HttpStatusCode Code { get; set; }
    }

    public enum ErrorType
    {
        NotFound,
        HttpError,
        UnknownError,
        Timeout
    }
}