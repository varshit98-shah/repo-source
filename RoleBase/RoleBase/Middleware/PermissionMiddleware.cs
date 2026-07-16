using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Routing;
using System.Security.Claims;

namespace RoleBase.Middleware
{
    public class PermissionMiddleware
    {
        private readonly RequestDelegate _next;

        public PermissionMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(
            HttpContext context)
        {
            
            // Get endpoint information
            var endpoint = context.GetEndpoint();

            // Skip authorization for [AllowAnonymous]
            if (endpoint?.Metadata
                .GetMetadata<AllowAnonymousAttribute>() != null)
            {
                await _next(context);
                return;
            }
            // Check JWT authentication
            if (context.User.Identity == null ||
                !context.User.Identity.IsAuthenticated)
            {
                context.Response.StatusCode =
                    StatusCodes.Status401Unauthorized;

                await context.Response.WriteAsync(
                    "Unauthorized");

                return;
            }
            // Read user id from JWT
            var userIdClaim =
                context.User.FindFirst(
                    ClaimTypes.NameIdentifier);

            if (userIdClaim == null)
            {
                context.Response.StatusCode =
                    StatusCodes.Status401Unauthorized;

                await context.Response.WriteAsync(
                    "Invalid Token");

                return;
            }
           

            var routeValues = context.Request.RouteValues;

            var controller = routeValues["controller"]?.ToString();

            string method = context.Request.Method;
            string action = method.ToUpper() switch
            {
                "GET" => "READ",
                "POST" => "Create",
                "PUT" => "Update",
                "DELETE" => "Delete",
                _ => "Unknown"
            };
            var requiredPermission = action;

            var userPermissions = context.User.Claims
                 .Where(c => c.Type == "Permission")
                 .Select(c => c.Value)
                 .ToList();


            bool hasPermission = userPermissions
                        .Any(p => p.Equals( requiredPermission,
                                         StringComparison.OrdinalIgnoreCase));

            if (!hasPermission)
            {
                context.Response.StatusCode =
                    StatusCodes.Status403Forbidden;

                await context.Response.WriteAsync(
                    "Forbidden");

                return;
            }

            // Continue request pipeline
            await _next(context);
        }
    }
}