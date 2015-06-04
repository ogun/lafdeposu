using System;

namespace LafDeposu.Helper.Data
{
    public class DataAccessFactory
    {
        public static IDataAccess CreateDataAccess(DataAccessType access, string connectionString)
        {
            IDataAccess returnValue = null;

            switch (access)
            {
                case DataAccessType.CsharpSqlite:
                    returnValue = new CsharpSqliteDataAccess();
                    break;
                case DataAccessType.MsSql:
                    returnValue = new MsSqlDataAccess();
                    break;
                case DataAccessType.MySql:
                    returnValue = new MySqlDataAccess();
                    break;
                default:
                    throw new NotImplementedException();
            }

            returnValue.ConnectionString = connectionString;

            return returnValue;
        }
    }
}
