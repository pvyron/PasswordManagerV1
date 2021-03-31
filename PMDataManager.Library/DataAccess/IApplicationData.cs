using PMDataManager.Library.Models;
using System.Collections.Generic;

namespace PMDataManager.Library.DataAccess
{
    public interface IApplicationData
    {
        string GetApplicationOwner(int id);
        List<ApplicationModel> GetApplicationsByUserId(string Id);
    }
}