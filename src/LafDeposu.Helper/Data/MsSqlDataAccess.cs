using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LafDeposu.Helper.Data
{
    public class MsSqlDataAccess : IDataAccess
    {
        public DataTable GetDataTable(string commandText, string connectionString)
        {
            DataTable returnValue = new DataTable();

            using (SqlConnection cnn = new SqlConnection(connectionString))
            {
                using (SqlDataAdapter da = new SqlDataAdapter(commandText, cnn))
                {
                    da.Fill(returnValue);
                }
            }

            return returnValue;
        }
    }
}
