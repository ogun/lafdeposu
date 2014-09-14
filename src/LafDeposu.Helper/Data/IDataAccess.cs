using LafDeposu.Helper.Models;
using System.Data;

namespace LafDeposu.Helper.Data
{
    public abstract class IDataAccess
    {
        public string ConnectionString { get; set; }

        public abstract DataTable GetDataTable(string commandText);

        public abstract int InsertWord(string word, string meaning);
    }
}
