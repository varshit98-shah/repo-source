


namespace RoleBase.Attributes
{
    [AttributeUsage(AttributeTargets.Method)]
    public class RequirePermissionAttribute : Attribute
    {
        public string Permission { get; set; }

        public RequirePermissionAttribute(string permission) 
        {
          Permission = permission;
        }

    }
}
