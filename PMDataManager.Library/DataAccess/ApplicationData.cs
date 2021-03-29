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
    public class ApplicationData
    {
        private readonly IConfiguration _config;

        public ApplicationData(IConfiguration config)
        {
            _config = config;
        }

        public List<ApplicationModel> GetApplicationsByUserId(string Id)
        {
            SqlDataAccess sql = new SqlDataAccess(_config);

            var p = new { userId = Id };

            var output = sql.LoadData<ApplicationModel, dynamic>("dbo.spApplicationLookup_ForUser", p, "PMDatabase");

            return output;
        }

        public string GetApplicationOwner(int id)
        {
            SqlDataAccess sql = new SqlDataAccess(_config);

            var p = new { Id = id };

            var output = sql.LoadData<string, dynamic>("dbo.spApplicationOwnerLookup_ById", p, "PMDatabase").FirstOrDefault();

            return output;
        }
    }
}
