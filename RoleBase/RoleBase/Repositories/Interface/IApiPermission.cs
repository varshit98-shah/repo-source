using RoleBase.Model;

namespace RoleBase.Repositories.Interface
{
    public interface IApiPermission
    {
        Task<List<string>> GetUserPermissionsAsync(int userId);

        Task<ApiPermission?> GetApiPermissionAsync(string methad , string path);
    }
}
