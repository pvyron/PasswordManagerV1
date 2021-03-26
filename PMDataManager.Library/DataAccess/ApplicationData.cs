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

        public string GetApplicationOwner(int id)
        {
            SqlDataAccess sql = new SqlDataAccess();

            var p = new { Id = id };

            var output = sql.LoadData<string, dynamic>("dbo.spApplicationOwnerLookup_ById", p, "PMDatabase").FirstOrDefault();

            return output;
        }
    }
}
