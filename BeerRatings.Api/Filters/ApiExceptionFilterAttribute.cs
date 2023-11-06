using BeerRatings.Core.Common;
using System.Net;
using System.Net.Http;
using System.Web.Http.Filters;

namespace BeerRatings.Api.Filters
{
    public class ApiExceptionFilterAttribute : ExceptionFilterAttribute
    {
        public override void OnException(HttpActionExecutedContext context)
        {
            if (context.Exception is ApiException)
            {
                var apiException = context.Exception as ApiException;
                var response = new HttpResponseMessage(apiException.StatusCode)
                {
                    Content = new StringContent(apiException.Message),
                    ReasonPhrase = apiException.ReasonPhrase,
                };

                context.Response = response;
            }
            else
            {

                // Handle other exceptions if needed
                var response = new HttpResponseMessage(HttpStatusCode.InternalServerError)
                {
                    Content = new StringContent("An unexpected error occurred."),
                    ReasonPhrase = "Internal Server Error",
                };

                context.Response = response;
            }
        }
    }
}