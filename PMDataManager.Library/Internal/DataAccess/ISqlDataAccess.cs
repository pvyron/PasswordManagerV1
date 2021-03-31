using System.Collections.Generic;

namespace PMDataManager.Library.Internal.DataAccess
{
    public interface ISqlDataAccess
    {
        string GetConnectionString(string name);
        List<T> LoadData<T, U>(string storedProcedure, U parameters, string connectionStringName);
        T SaveData<T, U>(string storedProcedure, U parameters, string connectionStringName);
        void UpdateData<T>(string storedProcedure, T parameters, string connectionStringName);
    }
}