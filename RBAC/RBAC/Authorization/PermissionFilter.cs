using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using RBAC.Data;

namespace RBAC.Authorization
{
    public class PermissionFilter : IAsyncAuthorizationFilter
    {
        private readonly string _permission;
        private readonly AppDbContext _context;

        public PermissionFilter(AppDbContext context, string permission)
        {
            _context = context;
            _permission = permission;
        }

        public async Task OnAuthorizationAsync(AuthorizationFilterContext context)
        {
            var permissions = context.HttpContext.Items["Permissions"] as List<string>;

            if (permissions == null)
            {
                context.Result = new ForbidResult();
                return;
            }

            if (!permissions.Contains(_permission))
            {
                context.Result = new ForbidResult();
                return;
            }

            await Task.CompletedTask;
        }
    }
}
