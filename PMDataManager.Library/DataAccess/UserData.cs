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
    public class UserData : IUserData
    {
        private readonly ISqlDataAccess _sql;

        public UserData(ISqlDataAccess sql)
        {
            _sql = sql;
        }

        public UserModel GetUserById(string Id)
        {

            var p = new { Id = Id };

            var output = _sql.LoadData<UserModel, dynamic>("dbo.spUserLookup", p, "PMDatabase").FirstOrDefault();

            if (output is null)
            {
                return new UserModel()
                {
                    Id = Id
                };
            }

            return output;
        }
    }
}
