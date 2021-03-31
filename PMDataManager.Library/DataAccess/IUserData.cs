using PMDataManager.Library.Models;

namespace PMDataManager.Library.DataAccess
{
    public interface IUserData
    {
        UserModel GetUserById(string Id);
    }
}