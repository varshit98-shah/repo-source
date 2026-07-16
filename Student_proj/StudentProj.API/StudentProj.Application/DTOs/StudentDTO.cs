using System.ComponentModel.DataAnnotations;

namespace StudentProj.Application.DTOs
{
    public class StudentDTO
    {
        public int Id { get; set; }

        [Required]
        [StringLength(50, MinimumLength = 3)]
        public string Name { get; set; }

        [Required]
        [EmailAddress]
        [StringLength(50)]

        public string Email { get; set; }

        [Required]
        [StringLength(200)]
        public string Address { get; set; }

        [Required]
        [StringLength(13, MinimumLength = 13)]
        [RegularExpression(@"^\+91[0-9]{10}$", ErrorMessage = "Phone number must start with +91 followed by 10 digits.")]
        public string Phone { get; set; }


    }
}
