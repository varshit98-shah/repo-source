using System.ComponentModel.DataAnnotations;

namespace RBAC.DTOs
{
    public class LoginDto
    {
        [Required(ErrorMessage = "Email is Required.")]
        [EmailAddress(ErrorMessage = "Please Enter a valid email address")]
        public string Email { get; set; } = string.Empty;

        [Required]
        public string Password { get; set; } = string.Empty;
    }
}
