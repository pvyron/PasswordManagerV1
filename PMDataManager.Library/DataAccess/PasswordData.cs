using Microsoft.Extensions.Configuration;
using PMDataManager.Library.Internal.DataAccess;
using PMDataManager.Library.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PMDataManager.Library.DataAccess
{
    public class PasswordData
    {
        private readonly IConfiguration _config;

        public PasswordData(IConfiguration config)
        {
            _config = config;
        }
        public List<PasswordModel> GetPasswordsByUserId(string id)
        {
            SqlDataAccess sql = new SqlDataAccess(_config);

            var p = new { userId = id };

            var output = sql.LoadData<PasswordModel, dynamic>("dbo.spPasswordLookup_ForUser", p, "PMDatabase");

            return output;
        }

        public void UpdatePasswordForUser(int id, string userId, PasswordUpdateModel passwordUpdateModel)
        {
            string passwordOwner = GetPasswordOwner(id);

            if (passwordOwner != userId)
            {
                throw new Exception("Password was not found or user is unauthorized.");
            }

            SqlDataAccess sql = new SqlDataAccess(_config);

            var p = new
            {
                Id = id,
                UserId = userId,
                ApplicationId = passwordUpdateModel.ApplicationId,
                PasswordAlias = passwordUpdateModel.PasswordAlias,
                Username = passwordUpdateModel.Username,
                Password = passwordUpdateModel.Password,
                Encrypted = passwordUpdateModel.Encrypted
            };

            sql.UpdateData("dbo.spPasswordUpdate_ByUserById", p, "PMDatabase");
        }

        public int CreatePassword(string userId, PasswordCreateModel passwordCreateModel)
        {
            ApplicationData applicationData = new ApplicationData(_config);

            string applicationOwner = applicationData.GetApplicationOwner(passwordCreateModel.ApplicationId);

            if (applicationOwner != userId)
            {
                throw new Exception("Application was not found or user is unauthorized.");
            }

            SqlDataAccess sql = new SqlDataAccess(_config);

            var p = new
            {
                UserId = userId,
                ApplicationId = passwordCreateModel.ApplicationId,
                PasswordAlias = passwordCreateModel.PasswordAlias,
                Username = passwordCreateModel.Username,
                Password = passwordCreateModel.Password,
                Encrypted = passwordCreateModel.Encrypted
            };

            var output = sql.SaveData<int, dynamic>("dbo.spPasswordAdd", p, "PMDatabase");

            return output;
        }

        public void DeletePasswordForUser(int id, string userId)
        {
            string passwordOwner = GetPasswordOwner(id);

            if (passwordOwner != userId)
            {
                throw new Exception("Password was not found or user is unauthorized.");
            }

            SqlDataAccess sql = new SqlDataAccess(_config);

            var p = new { Id = id };

            sql.UpdateData("dbo.spPasswordDelete_ById", p, "PMDatabase");
        }

        public string GetPasswordOwner(int id)
        {
            SqlDataAccess sql = new SqlDataAccess(_config);

            var p = new { Id = id };

            var output = sql.LoadData<string, dynamic>("dbo.spPasswordOwnerLookup_ById", p, "PMDatabase").FirstOrDefault();

            return output;
        }
    }
}
