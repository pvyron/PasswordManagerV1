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
        public List<PasswordModel> GetPasswordsByUserId(string Id)
        {
            SqlDataAccess sql = new SqlDataAccess();

            var p = new { userId = Id };

            var output = sql.LoadData<PasswordModel, dynamic>("dbo.spPasswordLookup_ForUser", p, "PMDatabase");

            return output;
        }
    }
}
