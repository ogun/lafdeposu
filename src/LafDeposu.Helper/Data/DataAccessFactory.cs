using System;

namespace LafDeposu.Helper.Data
{
    public class DataAccessFactory
    {
        public static IDataAccess CreateDataAccess(DataAccessType access)
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

            return returnValue;
        }
    }
}
