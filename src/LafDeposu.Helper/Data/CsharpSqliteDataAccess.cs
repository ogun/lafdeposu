using Community.CsharpSqlite.SQLiteClient;
using LafDeposu.Helper.Models;
using System.Data;

namespace LafDeposu.Helper.Data
{
    public class CsharpSqliteDataAccess : IDataAccess
    {
        public override DataTable GetDataTable(string commandText)
        {
            DataTable returnValue = new DataTable();

            using (SqliteConnection cnn = new SqliteConnection(ConnectionString))
            {
                using (SqliteDataAdapter da = new SqliteDataAdapter(commandText, cnn))
                {
                    da.Fill(returnValue);
                }
            }

            return returnValue;
        }


        public override int InsertWord(string word, string meaning)
        {
            throw new System.NotImplementedException();
        }
    }
}
