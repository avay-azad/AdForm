using Microsoft.AspNetCore.Http;
using System.Linq;
using System.Threading.Tasks;

namespace AdForm.Core
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
