using LafDeposu.Helper.Models;
using System.Collections.Generic;
using System.Data;

namespace LafDeposu.Helper.Data
{
    public abstract class IDataAccess
    {
        public string ConnectionString { get; set; }
        public abstract DataTable GetDataTable(string commandText);
        public abstract int InsertWord(string word, string meaning);
        public abstract int ExecuteCommand(string commandText);
        public abstract int ExecuteCommand(string commandText, Dictionary<string, string> parameters);
    }
}
