using System.ComponentModel.DataAnnotations;

namespace RBAC.DTOs
{
    public class RegisterDto
    {
        [Required(ErrorMessage ="Name is Required.")]
        public string Name { get; set; } = string.Empty;

        [Required(ErrorMessage ="Email is Required.")]
        [EmailAddress (ErrorMessage ="Please Enter a valid email address")]
        public string Email { get; set; } = string.Empty;

        [Required]
        public string Password { get; set; } = string.Empty;

        [Required]
        [Phone]
        [RegularExpression(@"^[6-9]\d{9}$",
            ErrorMessage = "Phone number must be 10 digits and start with 6-9.")]
        public string PhoneNumber { get; set; } = string.Empty;

        [Required]
        [StringLength(100)]
        public string City { get; set; } = string.Empty;

        [Required]
        [StringLength(100)]
        public string State { get; set; } = string.Empty;

        [Required]
        [StringLength(100)]
        public string Country { get; set; } = string.Empty;
    }
}
