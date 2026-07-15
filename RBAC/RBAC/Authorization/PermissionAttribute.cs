using Microsoft.AspNetCore.Mvc;

namespace RBAC.Authorization
{
    public class PermissionAttribute : TypeFilterAttribute
    {
        public PermissionAttribute(string permission)
            : base(typeof(PermissionFilter))
        {
            Arguments = new object[] { permission };
        }
    }
}
