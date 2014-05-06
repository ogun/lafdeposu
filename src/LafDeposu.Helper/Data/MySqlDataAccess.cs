using MySql.Data.MySqlClient;
using System.Data;

namespace LafDeposu.Helper.Data
{
    class MySqlDataAccess : IDataAccess
    {
        public DataTable GetDataTable(string commandText, string connectionString)
        {
            DataTable returnValue = new DataTable();

            using (MySqlConnection cnn = new MySqlConnection(connectionString))
            {
                using (MySqlDataAdapter da = new MySqlDataAdapter(commandText, cnn))
                {
                    da.Fill(returnValue);
                }
            }

            return returnValue;
        }
    }
}
