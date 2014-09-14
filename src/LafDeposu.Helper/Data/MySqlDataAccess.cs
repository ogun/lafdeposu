using LafDeposu.Helper.Models;
using MySql.Data.MySqlClient;
using System.Data;
using System;

namespace LafDeposu.Helper.Data
{
    class MySqlDataAccess : IDataAccess
    {
        public override DataTable GetDataTable(string commandText)
        {
            DataTable returnValue = new DataTable();

            using (MySqlConnection cnn = new MySqlConnection(ConnectionString))
            {
                using (MySqlDataAdapter da = new MySqlDataAdapter(commandText, cnn))
                {
                    da.Fill(returnValue);
                }
            }

            return returnValue;
        }


        public override int InsertWord(string word, string meaning)
        {
            string commandText = @"INSERT INTO Word (word, meaning, length, confirm)
                                   SELECT * FROM (SELECT @word AS word, @meaning AS meaning, @length AS length, @confirm AS confirm) AS tmp
                                   WHERE NOT EXISTS (SELECT word FROM Word WHERE word = @word) LIMIT 1;";

            using (MySqlCommand cmd = new MySqlCommand(commandText))
            {
                cmd.Parameters.AddWithValue("word", word);
                cmd.Parameters.AddWithValue("meaning", meaning);
                cmd.Parameters.AddWithValue("length", word.Length);
                cmd.Parameters.AddWithValue("confirm", 1);

                return ExecuteCommand(cmd);
            }
        }

        private int ExecuteCommand(MySqlCommand command)
        {
            int returnValue = -1;

            using (MySqlConnection cnn = new MySqlConnection(ConnectionString))
            {
                using (MySqlCommand cmd = command)
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
    }
}
