using System.ComponentModel.DataAnnotations;

namespace RoleBase.DTOs
{
    public class RoleDto
    {
        [Required]
        public string RoleName { get; set; } = string.Empty;
    }
}
    