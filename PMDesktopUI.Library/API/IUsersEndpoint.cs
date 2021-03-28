using PMDesktopUI.Library.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PMDesktopUI.Library.API
{
    public interface IUsersEndpoint
    {
        Task<List<UserModel>> GetAll();

        Task<Dictionary<string, string>> GetAllRoles();

        Task AddUserToRole(string userId, string roleName);

        Task DeleteUserFromRole(string userId, string roleName);
    }
}