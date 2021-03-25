using PMDataManager.Library.Internal.DataAccess;
using PMDataManager.Library.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PMDataManager.Library.DataAccess
{
    public class ApplicationData
    {
        public List<ApplicationModel> GetApplicationsByUserId(string Id)
        {
            SqlDataAccess sql = new SqlDataAccess();

            var p = new { userId = Id };

            var output = sql.LoadData<ApplicationModel, dynamic>("dbo.spApplicationLookup_ForUser", p, "PMDatabase");

            return output;
        }
    }
}
