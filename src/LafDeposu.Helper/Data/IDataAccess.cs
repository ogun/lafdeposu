using System.Data;

namespace LafDeposu.Helper.Data
{
    public interface IDataAccess
    {
        DataTable GetDataTable(string commandText, string connectionString);
    }
}
