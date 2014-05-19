using LafDeposu.Helper.Data;
using LafDeposu.Helper.Models;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Text;
using System.Text.RegularExpressions;

namespace LafDeposu.Helper
{
    public class FindWordHelper
    {
        public string ConnectionString { get; private set; }
        public DataAccessType DataAccessType { get; private set; }

        public FindWordHelper(string connectionString, DataAccessType dataAccessType)
        {
            this.ConnectionString = connectionString;
            this.DataAccessType = dataAccessType;
        }

        private bool ControlDbWord(string input, string dbWord)
        {
            bool returnValue = false;

            bool hasAsterix = input.IndexOf("*") > -1;
            int charPos = -1;

            for (int i = 0; i < dbWord.Length; i++)
            {
                charPos = input.IndexOf(dbWord[i]);

                if (charPos > -1)
                {
                    input = input.Remove(charPos, 1);
                    returnValue = true;
                }
                else
                {
                    if (hasAsterix)
                    {
                        charPos = input.IndexOf('*');

                        if (charPos > -1)
                        {
                            input = input.Remove(charPos, 1);
                            returnValue = true;
                        }
                        else
                        {
                            returnValue = false;
                            break;
                        }
                    }
                    else
                    {
                        returnValue = false;
                        break;
                    }
                }
            }

            return returnValue;
        }

        public bool ControlInput(ref string input)
        {
            bool returnValue = true;

            input = input.ToLower(CultureInfo.GetCultureInfo("tr-TR")).Trim();

            if (string.IsNullOrEmpty(input))
            {
                return false;
            }

            if (string.IsNullOrEmpty(input.Replace("*", string.Empty)))
            {
                return false;
            }

            Regex regexPattern = new Regex(@"[^A-Za-zışğüçöİĞÜŞÇÖ*]");
            if (regexPattern.IsMatch(input))
            {
                return false;
            }

            return returnValue;
        }

        private string CreateCommandText(string input, int? resultCharCount)
        {
            StringBuilder commandText = new StringBuilder();

            List<char> inputChars = new List<char>(input.ToCharArray());
            List<char> alphabet = new List<char>("abcçdefgğhıijklmnoöprsştuüvyz".ToCharArray());

            int inputLength = input.Length;
            bool hasAsterix = input.IndexOf("*") > -1;

            commandText.Append("SELECT word, meaning FROM Word WHERE ");

            if (!hasAsterix)
            {
                foreach (char letter in alphabet)
                {
                    if (!inputChars.Contains(letter))
                    {
                        commandText.AppendFormat("word NOT LIKE('%{0}%') AND ", letter);
                    }
                }
            }

            // Sadece iki harfli sonuçları getirebilir
            if (!resultCharCount.HasValue)
            {
                commandText.AppendFormat("length <= {0} AND length > 2 ORDER BY length DESC, word", inputLength);
            }
            else
            {
                commandText.AppendFormat("length = {0} ORDER BY length DESC, word", resultCharCount);
            }
            

            return commandText.ToString();
        }

        public WordList CreateResult(string input, string startsWith, string contains, string endsWith, int? resultCharCount)
        {
            if (string.IsNullOrEmpty(input))
            {
                return new WordList();
            }

            // Sadece küçük harflerle işlem yapıyoruz
            input = input.ToLower(CultureInfo.GetCultureInfo("tr-TR")).Trim();
            if (!string.IsNullOrEmpty(startsWith))
            {
                startsWith = startsWith.ToLower(CultureInfo.GetCultureInfo("tr-TR")).Trim();
            }

            if (!string.IsNullOrEmpty(contains))
            {
                contains = contains.ToLower(CultureInfo.GetCultureInfo("tr-TR")).Trim();
            }

            if (!string.IsNullOrEmpty(endsWith))
            {
                endsWith = endsWith.ToLower(CultureInfo.GetCultureInfo("tr-TR")).Trim();
            }

            
            WordList returnValue = new WordList();

            string commandText = CreateCommandText(input, resultCharCount);

            using (DataTable dataTable = GetDataTable(commandText, ConnectionString))
            {
                if (dataTable.Rows.Count > 0)
                {
                    int dbWordLength = -1;
                    WordGroup wg = new WordGroup();
                    wg.words = new List<Word>();

                    foreach (DataRow dbRow in dataTable.Rows)
                    {
                        string dbWord = dbRow["word"].ToString();
                        string dbMeaning = dbRow["meaning"].ToString();

                        bool isWordOK = ControlDbWord(input, dbWord);
                        bool isFilterOK = false;
                        if (isWordOK)
                        {
                            isFilterOK = ControlFilter(dbWord, startsWith, contains, endsWith);
                        }

                        if (isFilterOK)
                        {
                            if (!dbWordLength.Equals(dbWord.Length))
                            {
                                if (!dbWordLength.Equals(-1))
                                {
                                    returnValue.Add(wg);
                                    wg = new WordGroup();
                                    wg.words = new List<Word>();
                                }

                                wg.length = dbWord.Length;
                                dbWordLength = dbWord.Length;
                            }

                            Word w = new Word { w = dbWord.ToUpper(CultureInfo.GetCultureInfo("tr-TR")), m = dbMeaning };
                            wg.words.Add(w);
                        }
                    }

                    if (wg.words != null && wg.words.Count > 0)
                    {
                        returnValue.Add(wg);
                    }
                }
            }

            return returnValue;
        }

        private bool ControlFilter(string dbWord, string startsWith, string contains, string endsWith)
        {
            if (!string.IsNullOrEmpty(startsWith))
            {
                if (!dbWord.StartsWith(startsWith, ignoreCase: true, culture: CultureInfo.GetCultureInfo("tr-TR")))
                {
                    return false;
                }
            }

            if (!string.IsNullOrEmpty(contains))
            {
                if (!dbWord.Contains(contains))
                {
                    return false;
                }
            }

            if (!string.IsNullOrEmpty(endsWith))
            {
                if (!dbWord.EndsWith(endsWith, ignoreCase: true, culture: CultureInfo.GetCultureInfo("tr-TR")))
                {
                    return false;
                }
            }

            return true;
        }

        private DataTable GetDataTable(string commandText, string connectionString)
        {
            IDataAccess dataProccessor = DataAccessFactory.CreateDataAccess(DataAccessType);
            return dataProccessor.GetDataTable(commandText, connectionString);
        }
    }
}
