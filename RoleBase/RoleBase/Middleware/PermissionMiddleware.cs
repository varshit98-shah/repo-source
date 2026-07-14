using RoleBase.Attributes;
using RoleBase.Repositories.Interface;
using RoleBase.Repositories.RepoImplementations;
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
        public async Task InvokeAsync(HttpContext context, IPermissionRepository permissionRepository) 
        {
          var endpoint = context.GetEndpoint();

            var permissionAttribute = endpoint?.Metadata
                       .GetMetadata<RequirePermissionAttribute>();

            if (permissionAttribute == null)
            {
                await _next(context);
                return;
            }

            var userIdClaim = context.User.FindFirst(ClaimTypes.NameIdentifier);

            if (userIdClaim == null) 
            {
                context.Response.StatusCode = 401;

                await context.Response.WriteAsync(
                    "You are Unauthorized");

                return;
            }

            int userId = int.Parse(userIdClaim.Value);


            var permissions = await permissionRepository
                            .GetPermissionsByUserIdAsync(userId);

            bool hasPermission = permissions.Contains(
                        permissionAttribute.Permission);

            if (!hasPermission)
            {
                context.Response.StatusCode = 403;

                await context.Response.WriteAsync(
                    "Forbidden");

                return;
            }
            await _next(context);
        }
    }
}
