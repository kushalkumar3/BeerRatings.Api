using System;
using System.Net;

namespace BeerRatings.Core.Common
{
    public class ApiException : Exception
    {
        public ApiException(HttpStatusCode statusCode, string reasonPhrase, string message)
            : base(message)
        {
            StatusCode = statusCode;
            ReasonPhrase = reasonPhrase;
        }

        public HttpStatusCode StatusCode { get; }

        public string ReasonPhrase { get; }
    }
}