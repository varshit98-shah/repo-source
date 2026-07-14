using System.ComponentModel.DataAnnotations;
namespace RoleBase.DTOs
{
    public class PermissionDto
    {
        [Required] 
        public string PermissionName { get; set; } 
    }
}
