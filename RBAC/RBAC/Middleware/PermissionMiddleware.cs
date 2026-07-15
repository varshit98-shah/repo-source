using System.Security.Claims;
using RBAC.Repositories.Implementations;
using RBAC.Repositories.Interfaces;

namespace RBAC.Middleware
{
    public class PermissionMiddleware
    {
        private readonly RequestDelegate _next;

        public PermissionMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context, IUserRepository repository)
        {
            if (!context.User.Identity!.IsAuthenticated)
            {
                await _next(context);
                return;
            }

            var userIdClaim = context.User.FindFirst(ClaimTypes.NameIdentifier);

            if(userIdClaim == null)
            {
                context.Response.StatusCode = 401;
                await context.Response.WriteAsync("UserId Not Found");
                return;
            }

            int userId = int.Parse(userIdClaim.Value);

            var permissions = await repository.GetUserPermissionAsync(userId);

            context.Items["Permissions"] = permissions;

            await _next(context);
        }
    }
}
