using System.ComponentModel.DataAnnotations;

namespace RBAC.DTOs
{
    public class CreateRoleDto
    {
        [Required]
        public string RoleName { get; set; } = string.Empty;
    }
}
