using System.ComponentModel.DataAnnotations;

namespace RoleBase.DTOs
{
    public class UpdateDto
    {
        [Required]
        public string Name { get; set; } = string.Empty;
        [Required]
        [EmailAddress]
        public string Email { get; set; } = string.Empty;

    }
}
