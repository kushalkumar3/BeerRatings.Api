using BeerRatings.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;
namespace BeerRatings.Api.Filters
{
    public class UserNameValidationAttribute : ActionFilterAttribute
    {
        private readonly string userNamePattern;

        public UserNameValidationAttribute()
        {
            this.userNamePattern = @"^[\w-]+(\.[\w-]+)*@([\w-]+\.)+[a-zA-Z]{2,7}$";
        }

        public override void OnActionExecuting(HttpActionContext actionContext)
        {
            if (!IsValidUserName(actionContext.ActionArguments))
            {
                actionContext.Response = new HttpResponseMessage(System.Net.HttpStatusCode.BadRequest)
                {
                    Content = new StringContent("Invalid user name!"),
                    ReasonPhrase = "Bad Request"
                };
            }
        }

        private bool IsValidUserName(IDictionary<string, object> actionArguments)
        {
            if (actionArguments != null && actionArguments.ContainsKey("userRatings"))
            {
                UserRating userRating = actionArguments["userRatings"] as UserRating;

                if (!string.IsNullOrWhiteSpace(userRating.UserName))
                {
                    return Regex.IsMatch(userRating.UserName, userNamePattern);
                }
            }

            return false;
        }
    }
}