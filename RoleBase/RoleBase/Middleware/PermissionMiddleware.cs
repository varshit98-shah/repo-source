using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Routing;
using RoleBase.Repositories.Interface;
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
            HttpContext context,
            IApiPermission apiPermission)
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

            // Get route template
            var routeEndpoint = endpoint as RouteEndpoint;

            var path = routeEndpoint?
                .RoutePattern
                .RawText?
                .ToLower();

            var method =
                context.Request.Method.ToUpper();

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

            // Find API configuration
            var apiPermissionData =
                await apiPermission
                    .GetApiPermissionAsync(
                        method,
                        "/" + path);

            // API not configured
            if (apiPermissionData == null)
            {
                context.Response.StatusCode =
                    StatusCodes.Status403Forbidden;

                await context.Response.WriteAsync(
                    $"API permission not configured for {method} {path}");

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

            int userId =
                int.Parse(userIdClaim.Value);

            // Load user permissions
            var userPermissions =
                await apiPermission
                    .GetUserPermissionsAsync(
                        userId);

            // Check permission
            bool hasPermission =
                userPermissions.Contains(
                    apiPermissionData
                        .Permission
                        .PermissionName);

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