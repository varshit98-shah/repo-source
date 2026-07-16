namespace RoleBase.Model
{
    public class User
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Email { get; set; }

        public string Password { get; set; }

        public bool IsDelete  { get; set; } = false;



        public ICollection<UserRole> UserRoles { get; set; } = new List<UserRole>();

    }
}
