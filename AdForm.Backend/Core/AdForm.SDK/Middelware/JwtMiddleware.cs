using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdForm.SDK
{
    /// <summary>
    /// Middleware for Jwt Token verification.
    /// </summary>
    public class JwtMiddleware
    {
        private readonly RequestDelegate _next;
       
        public JwtMiddleware(RequestDelegate next)
        {
            _next = next;
         }

        public async Task Invoke(HttpContext context, IJwtUtils jwtUtils)
        {
            var token = context.Request.Headers["Authorization"].FirstOrDefault()?.Split(" ").Last();
            var userId = jwtUtils.ValidateToken(token);
            if (userId != null)
            {
                // attach user to context on successful jwt validation
                context.Items["UserId"] = userId;
            }

            await _next(context);
        }
    }
}
