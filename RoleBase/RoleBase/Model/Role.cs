using System.ComponentModel.DataAnnotations;

namespace RoleBase.Model
{
    public class Role
    {

        public int Id { get; set; }
        [Required]
        public string RoleName { get; set; } = string.Empty;    

        public ICollection<UserRole> UserRoles { get; set; }= new List<UserRole>();
        public ICollection<RolePermission> RolePermission { get; set; }= new List<RolePermission>();
    }
}
