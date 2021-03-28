using PMDataManager.Library.Internal.DataAccess;
using PMDataManager.Library.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PMDataManager.Library.DataAccess
{
    public class UserData
    {
        public UserModel GetUserById(string Id)
        {
            SqlDataAccess sql = new SqlDataAccess();

            var p = new { Id = Id };

            var output = sql.LoadData<UserModel, dynamic>("dbo.spUserLookup", p, "PMDatabase").FirstOrDefault();

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
