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
    public class ApplicationData : IApplicationData
    {
        private readonly ISqlDataAccess _sql;

        public ApplicationData(ISqlDataAccess sql)
        {
            _sql = sql;
        }

        public List<ApplicationModel> GetApplicationsByUserId(string Id)
        {

            var p = new { userId = Id };

            var output = _sql.LoadData<ApplicationModel, dynamic>("dbo.spApplicationLookup_ForUser", p, "PMDatabase");

            return output;
        }

        public string GetApplicationOwner(int id)
        {

            var p = new { Id = id };

            var output = _sql.LoadData<string, dynamic>("dbo.spApplicationOwnerLookup_ById", p, "PMDatabase").FirstOrDefault();

            return output;
        }
    }
}
