namespace RoleBase.Model
{
    public class ApiPermission
    {
        public int Id { get; set; }
        public string HttpMethod { get; set; }
        public int PermissionId { get; set; }
        public string ApiPath { get; set; }

        public Permission Permission { get; set; } = null;


    }
}
