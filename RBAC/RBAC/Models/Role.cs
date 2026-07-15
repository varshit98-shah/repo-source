namespace RBAC.Models
{
    public class Role
    {
        public int Id { get; set; }
        public string RoleName { get; set; } = string.Empty;

        public ICollection<UserRole> UserRoles = new List<UserRole>();

        public ICollection<RolePermission> RolePermissions = new List<RolePermission>();
    }
}
