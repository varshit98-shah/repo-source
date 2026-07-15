namespace RoleBase.Model
{
    public class RolePermission
    {
        public int RoleId { get; set; }
        public int PermissionId { get; set; }
        public string? PermissionName { get; set; } = string.Empty;
        public string? RoleName { get; set; } = string.Empty;

        public Permission Permission { get; set; }
        public Role Role { get; set; }
    }
}
