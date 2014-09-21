using Community.CsharpSqlite.SQLiteClient;
using System;
using System.Collections.Generic;
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
            string commandText = @"IF NOT EXISTS (SELECT word FROM Word WHERE word = @word)
                                    BEGIN                           
                                        INSERT INTO Word (word, meaning, length, confirm)
                                        VALUES (@word, @meaning, @length, @confirm)
                                    END";

            using (SqliteCommand cmd = new SqliteCommand(commandText))
            {
                cmd.Parameters.Add("word", word);
                cmd.Parameters.Add("meaning", meaning);
                cmd.Parameters.Add("length", word.Length);
                cmd.Parameters.Add("confirm", 1);

                return ExecuteCommand(cmd);
            }
        }

        private int ExecuteCommand(SqliteCommand command)
        {
            int returnValue = -1;

            using (SqliteConnection cnn = new SqliteConnection(ConnectionString))
            {
                using (SqliteCommand cmd = command)
                {
                    cmd.Connection = cnn;

                    try
                    {
                        cnn.Open();
                        returnValue = cmd.ExecuteNonQuery();
                    }
                    catch (Exception)
                    {
                        throw;
                    }
                }
            }

            return returnValue;
        }

        public override int ExecuteCommand(string commandText)
        {
            using (SqliteCommand cmd = new SqliteCommand(commandText))
            {
                return ExecuteCommand(cmd);
            }
        }

        public override int ExecuteCommand(string commandText, Dictionary<string, string> parameters)
        {
            using (SqliteCommand cmd = new SqliteCommand(commandText))
            {
                foreach (var item in parameters)
                {
                    cmd.Parameters.Add(item.Key, item.Value);
                }
                return ExecuteCommand(cmd);
            }
        }
    }
}
