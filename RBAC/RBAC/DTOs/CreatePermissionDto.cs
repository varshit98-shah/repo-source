using System.ComponentModel.DataAnnotations;

namespace RBAC.DTOs
{
    public class CreatePermissionDto
    {
        [Required]
        public string PermissionName { get; set; } = string.Empty;
    }
}
