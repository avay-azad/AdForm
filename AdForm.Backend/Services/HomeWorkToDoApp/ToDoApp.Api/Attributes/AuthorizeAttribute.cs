using AdForm.Core;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Net;

namespace ToDoApp.Api
{
    /// <summary>
    /// Authorization attribute.
    /// </summary>
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
    public class AuthorizeAttribute : Attribute, IAuthorizationFilter
    {
        /// <summary>
        /// Handle onauthorization.
        /// </summary>
        /// <param name="context">Authorization filter context.</param>
        public void OnAuthorization(AuthorizationFilterContext context)
        {
            var userId = context.HttpContext.Items["UserId"];
            if (userId == null)
            {
                throw new AuthenticationException(ApiErrorMessage.Global_Authentication_Validation, HttpStatusCode.Unauthorized, AuthenticationExceptionType.TokenInvalid);
            }
        }
    }
}
