using LafDeposu.Helper.Data;
using LafDeposu.Helper.Models;
using System;
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
        public IDataAccess DataProccessor { get; private set; }

        public FindWordHelper(string connectionString, DataAccessType dataAccessType)
        {
            this.ConnectionString = connectionString;
            DataProccessor = DataAccessFactory.CreateDataAccess(dataAccessType, connectionString); ;
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

            using (DataTable dataTable = GetDataTable(commandText))
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

                            Word w = new Word { w = dbWord.ToUpper(CultureInfo.GetCultureInfo("tr-TR")), m = dbMeaning, j = ListJokerChars(input, dbWord).ToUpper(CultureInfo.GetCultureInfo("tr-TR")) };
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

        private string ListJokerChars(string input, string dbWord) {
            string returnValue = string.Empty;

            foreach (char oldChar in input) {
                var regex = new Regex(Regex.Escape(oldChar.ToString()));
                dbWord = regex.Replace(dbWord, string.Empty, 1);
            }
            returnValue = dbWord;

            return returnValue;
        }

        private bool ControlFilter(string dbWord, string startsWith, string contains, string endsWith)
        {
            if (!string.IsNullOrEmpty(startsWith))
            {
                bool returnValue = false;
                string[] startsWithList = startsWith.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
                foreach (string word in startsWithList) {
                    if (dbWord.StartsWith(word, ignoreCase: true, culture: CultureInfo.GetCultureInfo("tr-TR"))) {
                        returnValue = true;
                    }
                }
                if (!returnValue) {
                    return false;
                }
            }

            if (!string.IsNullOrEmpty(contains))
            {
                bool returnValue = false;
                string[] containsList = contains.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
                foreach (string word in containsList) {
                    if (dbWord.Contains(word)) {
                        returnValue = true;
                    }
                }
                if (!returnValue) {
                    return false;
                }
            }

            if (!string.IsNullOrEmpty(endsWith))
            {
                bool returnValue = false;
                string[] endsWithList = endsWith.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
                foreach (string word in endsWithList) {
                    if (dbWord.EndsWith(word, ignoreCase: true, culture: CultureInfo.GetCultureInfo("tr-TR"))) {
                        returnValue = true;
                    }
                }
                if (!returnValue) {
                    return false;
                }
            }

            return true;
        }

        private DataTable GetDataTable(string commandText)
        {
            return DataProccessor.GetDataTable(commandText);
        }

        private int ExecuteCommand(string commandText)
        {
            return DataProccessor.ExecuteCommand(commandText);
        }

        public WordResponse AddWord(string word, string meaning)
        {
            WordResponse returnValue = new WordResponse();

            word = word.Trim();
            word = word.ToLower(CultureInfo.GetCultureInfo("tr-TR"));

            meaning = meaning.Trim();
            meaning = meaning.Trim('.');

            int effectedRowCount = DataProccessor.InsertWord(word, meaning);

            if (effectedRowCount.Equals(1))
            {
                returnValue.Type = WordResponseType.Notice;
                returnValue.Title = "Kayıt Eklendi";
                returnValue.Message = string.Format("{0} veritabanına eklendi.", word.ToUpper(CultureInfo.GetCultureInfo("tr-TR")));
            }
            else
            {
                returnValue.Type = WordResponseType.Warning;
                returnValue.Title = "Varolan Kayıt";
                returnValue.Message = string.Format("{0} veritabanında mevcut.", word.ToUpper(CultureInfo.GetCultureInfo("tr-TR")));
            }

            return returnValue;
        }

        public List<InsertedWord> GetInsertedList()
        {
            List<InsertedWord> returnValue = new List<InsertedWord>();

            string commandText = "SELECT id, word, meaning FROM Word WHERE confirm = 1 ORDER BY word";

            DataTable dt = GetDataTable(commandText);
            if (dt != null && dt.Rows.Count > 0)
            {
                foreach (DataRow dr in dt.Rows)
                {
                    InsertedWord iw = new InsertedWord();
                    iw.ID = (int)dr["id"];
                    iw.Word = dr["word"].ToString();
                    iw.Meaning = dr["meaning"].ToString();

                    returnValue.Add(iw);
                }
            }

            return returnValue;
        }

        public WordResponse UpdateWord(string id, string word, string meaning)
        {
            WordResponse returnValue = new WordResponse();

            word = word.Trim();
            word = word.ToLower(CultureInfo.GetCultureInfo("tr-TR"));

            meaning = meaning.Trim();
            meaning = meaning.Trim('.');

            string commandText = "UPDATE Word SET word = @word, meaning = @meaning, length = @length WHERE confirm = 1 AND id = @id";
            Dictionary<string, string> parameters = new Dictionary<string, string>();
            parameters.Add("id", id);
            parameters.Add("word", word);
            parameters.Add("meaning", meaning);
            parameters.Add("length", word.Length.ToString());
            int effectedRowCount = DataProccessor.ExecuteCommand(commandText, parameters);

            if (effectedRowCount.Equals(1))
            {
                returnValue.Type = WordResponseType.Notice;
                returnValue.Title = "Kayıt Güncellendi";
                returnValue.Message = string.Format("{0} kelimesi güncellendi.", word.ToUpper(CultureInfo.GetCultureInfo("tr-TR")));
            }
            else
            {
                returnValue.Type = WordResponseType.Warning;
                returnValue.Title = "Varolmayan Kayıt";
                returnValue.Message = string.Format("{0} veritabanında mevcut değil.", word.ToUpper(CultureInfo.GetCultureInfo("tr-TR")));
            }

            return returnValue;
        }

        public WordResponse ApproveWord(string id, string word)
        {
            WordResponse returnValue = new WordResponse();

            word = word.Trim();
            word = word.ToLower(CultureInfo.GetCultureInfo("tr-TR"));

            string commandText = "UPDATE Word SET confirm = 2 WHERE confirm = 1 AND id = @id AND word = @word";
            Dictionary<string, string> parameters = new Dictionary<string, string>();
            parameters.Add("id", id);
            parameters.Add("word", word);
            int effectedRowCount = DataProccessor.ExecuteCommand(commandText, parameters);

            if (effectedRowCount.Equals(1))
            {
                returnValue.Type = WordResponseType.Notice;
                returnValue.Title = "Kayıt Onaylandı";
                returnValue.Message = string.Format("{0} kelimesi onaylandı.", word.ToUpper(CultureInfo.GetCultureInfo("tr-TR")));
            }
            else
            {
                returnValue.Type = WordResponseType.Warning;
                returnValue.Title = "Varolmayan Kayıt";
                returnValue.Message = string.Format("{0} veritabanında mevcut değil.", word.ToUpper(CultureInfo.GetCultureInfo("tr-TR")));
            }

            return returnValue;
        }

        public WordResponse RemoveWord(string id, string word)
        {
            WordResponse returnValue = new WordResponse();

            word = word.Trim();
            word = word.ToLower(CultureInfo.GetCultureInfo("tr-TR"));

            string commandText = "DELETE FROM Word WHERE confirm = 1 AND id = @id AND word = @word";
            Dictionary<string, string> parameters = new Dictionary<string, string>();
            parameters.Add("id", id);
            parameters.Add("word", word);
            int effectedRowCount = DataProccessor.ExecuteCommand(commandText, parameters);

            if (effectedRowCount.Equals(1))
            {
                returnValue.Type = WordResponseType.Notice;
                returnValue.Title = "Silme Onaylandı";
                returnValue.Message = string.Format("{0} veritabanından silindi.", word.ToUpper(CultureInfo.GetCultureInfo("tr-TR")));
            }
            else
            {
                returnValue.Type = WordResponseType.Warning;
                returnValue.Title = "Varolmayan Kayıt";
                returnValue.Message = string.Format("{0} veritabanında mevcut değil.", word.ToUpper(CultureInfo.GetCultureInfo("tr-TR")));
            }

            return returnValue;
        }
    }
}
