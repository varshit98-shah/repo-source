using System.ComponentModel.DataAnnotations;

namespace RBAC.DTOs
{
    public class UpdateUserDto
    {
        public string Name { get; set; } = string.Empty;

        [Required(ErrorMessage = "Email is Required.")]
        [EmailAddress(ErrorMessage = "Please Enter a valid email address")]
        public string Email { get; set; } = string.Empty;
        public string Password { get; set; }
    }
}
