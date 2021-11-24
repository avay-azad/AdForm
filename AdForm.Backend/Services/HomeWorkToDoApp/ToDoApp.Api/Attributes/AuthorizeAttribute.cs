﻿using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System;

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
                context.Result = new JsonResult(new { message = "Unauthorized" }) { StatusCode = StatusCodes.Status401Unauthorized };
            }
        }
    }
}
