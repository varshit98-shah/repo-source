namespace RoleBase.Model
{
    public class RolePermission
    {
        public int RoleId { get; set; }
        public int PermissionId { get; set; }
        public string PermissionName { get; set; }
        public string RoleName { get; set; }

        public Permission Permission { get; set; }
        public Role Role { get; set; }
    }
}
