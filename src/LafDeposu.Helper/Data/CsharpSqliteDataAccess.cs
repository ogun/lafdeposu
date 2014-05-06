using Community.CsharpSqlite.SQLiteClient;
using System.Data;

namespace LafDeposu.Helper.Data
{
    public class CsharpSqliteDataAccess : IDataAccess
    {
        public DataTable GetDataTable(string commandText, string connectionString)
        {
            DataTable returnValue = new DataTable();

            using (SqliteConnection cnn = new SqliteConnection(connectionString))
            {
                using (SqliteDataAdapter da = new SqliteDataAdapter(commandText, cnn))
                {
                    da.Fill(returnValue);
                }
            }

            return returnValue;
        }
    }
}
