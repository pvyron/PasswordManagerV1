﻿using Microsoft.Extensions.Configuration;
using PMDataManager.Library.Internal.DataAccess;
using PMDataManager.Library.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PMDataManager.Library.DataAccess
{
    public class PasswordData : IPasswordData
    {
        private readonly ISqlDataAccess _sql;
        private readonly IApplicationData _applicationData;

        public PasswordData(ISqlDataAccess sql, IApplicationData applicationData)
        {
            _sql = sql;
            _applicationData = applicationData;
        }
        public List<PasswordModel> GetPasswordsByUserId(string id)
        {
            var p = new { userId = id };

            var output = _sql.LoadData<PasswordModel, dynamic>("dbo.spPasswordLookup_ForUser", p, "PMDatabase");

            return output;
        }

        public void UpdatePasswordForUser(int id, string userId, PasswordUpdateModel passwordUpdateModel)
        {
            string passwordOwner = GetPasswordOwner(id);

            if (passwordOwner != userId)
            {
                throw new Exception("Password was not found or user is unauthorized.");
            }

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

            _sql.UpdateData("dbo.spPasswordUpdate_ByUserById", p, "PMDatabase");
        }

        public int CreatePassword(string userId, PasswordCreateModel passwordCreateModel)
        {
            string applicationOwner = _applicationData.GetApplicationOwner(passwordCreateModel.ApplicationId);

            if (applicationOwner != userId)
            {
                throw new Exception("Application was not found or user is unauthorized.");
            }

            var p = new
            {
                UserId = userId,
                ApplicationId = passwordCreateModel.ApplicationId,
                PasswordAlias = passwordCreateModel.PasswordAlias,
                Username = passwordCreateModel.Username,
                Password = passwordCreateModel.Password,
                Encrypted = passwordCreateModel.Encrypted
            };

            var output = _sql.SaveData<int, dynamic>("dbo.spPasswordAdd", p, "PMDatabase");

            return output;
        }

        public void DeletePasswordForUser(int id, string userId)
        {
            string passwordOwner = GetPasswordOwner(id);

            if (passwordOwner != userId)
            {
                throw new Exception("Password was not found or user is unauthorized.");
            }

            var p = new { Id = id };

            _sql.UpdateData("dbo.spPasswordDelete_ById", p, "PMDatabase");
        }

        public string GetPasswordOwner(int id)
        {

            var p = new { Id = id };

            var output = _sql.LoadData<string, dynamic>("dbo.spPasswordOwnerLookup_ById", p, "PMDatabase").FirstOrDefault();

            return output;
        }
    }
}
