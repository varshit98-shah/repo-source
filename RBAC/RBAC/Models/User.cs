    namespace RBAC.Models
    {
        public class User
        {
            public int Id { get; set; }
            public string Name { get; set; } = string.Empty;
            public string Email { get; set; } = string.Empty;
            public string Password { get; set; } = string.Empty;
            public string PhoneNumber { get; set; } = string.Empty;
            public string City { get; set; } = string.Empty;
            public string State { get; set; } = string.Empty;
            public string Country { get; set; } = string.Empty;
            public string? CreatedBy { get; set; }
            public DateTime CreateDate { get; set; }
            public string? ModifiedBy { get; set; }
            public DateTime ModifiedDate { get; set; }
            public bool IsDeleted { get; set; }

            public ICollection<UserRole> UserRoles { get; set; } = new List<UserRole>();
        }
    }
