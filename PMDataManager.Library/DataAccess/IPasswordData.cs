using PMDataManager.Library.Models;
using System.Collections.Generic;

namespace PMDataManager.Library.DataAccess
{
    public interface IPasswordData
    {
        int CreatePassword(string userId, PasswordCreateModel passwordCreateModel);
        void DeletePasswordForUser(int id, string userId);
        string GetPasswordOwner(int id);
        List<PasswordModel> GetPasswordsByUserId(string id);
        void UpdatePasswordForUser(int id, string userId, PasswordUpdateModel passwordUpdateModel);
    }
}