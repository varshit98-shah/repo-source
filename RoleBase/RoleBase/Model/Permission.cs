using System.ComponentModel.DataAnnotations;

namespace RoleBase.Model
{
    public class Permission
    {
        public int Id { get; set; }
        [Required]
        public string PermissionName { get; set; } 

        public ICollection<RolePermission> RolePermission { get; set; } = new List<RolePermission>();
    }
}
