using LafDeposu.Helper.Models;
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
        public override DataTable GetDataTable(string commandText)
        {
            DataTable returnValue = new DataTable();

            using (SqlConnection cnn = new SqlConnection(ConnectionString))
            {
                using (SqlDataAdapter da = new SqlDataAdapter(commandText, cnn))
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

            using (SqlCommand cmd = new SqlCommand(commandText))
            {
                cmd.Parameters.AddWithValue("word", word);
                cmd.Parameters.AddWithValue("meaning", meaning);
                cmd.Parameters.AddWithValue("length", word.Length);
                cmd.Parameters.AddWithValue("confirm", 1);

                return ExecuteCommand(cmd);
            }
        }

        private int ExecuteCommand(SqlCommand command)
        {
            int returnValue = -1;

            using (SqlConnection cnn = new SqlConnection(ConnectionString))
            {
                using (SqlCommand cmd = command)
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
