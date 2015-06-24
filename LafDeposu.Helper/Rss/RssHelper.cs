using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.ServiceModel.Syndication;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Xml;

namespace LafDeposu.Helper.Rss {
    public class RssHelper {
        public static List<KeyValuePair<string, string>> GetGoogleFeed() {
            List<KeyValuePair<string, string>> returnValue = new List<KeyValuePair<string, string>>();

            XmlTextReader reader = new XmlTextReader("https://www.google.com/trends/hottrends/atom/feed?pn=p24");
            SyndicationFeed feed = SyndicationFeed.Load(reader);

            foreach (SyndicationItem item in feed.Items) {
                if (item.Title == null || string.IsNullOrWhiteSpace(item.Title.Text)) {
                    continue;
                }

                string value = ToTitleCase(item.Title.Text);
                string key = RemoveDiacritics(value);
                key = CleanTextForUrl(key);

                KeyValuePair<string, string> newItem = new KeyValuePair<string, string>(key, value);
                returnValue.Add(newItem);
            }

            return returnValue;
        }

        private static string ToTitleCase(string text) {
            return CultureInfo.GetCultureInfo("tr-TR").TextInfo.ToTitleCase(text);
        }

        private static string RemoveDiacritics(string text) {
            Encoding srcEncoding = Encoding.UTF8;
            Encoding destEncoding = Encoding.GetEncoding(1252); // Latin alphabet

            text = destEncoding.GetString(Encoding.Convert(srcEncoding, destEncoding, srcEncoding.GetBytes(text)));

            string normalizedString = text.Normalize(NormalizationForm.FormD);
            StringBuilder result = new StringBuilder();

            for (int i = 0; i < normalizedString.Length; i++) {
                if (!CharUnicodeInfo.GetUnicodeCategory(normalizedString[i]).Equals(UnicodeCategory.NonSpacingMark)) {
                    result.Append(normalizedString[i]);
                }
            }

            return result.ToString();
        }

        private static string CleanTextForUrl(string text) {
            text = text.Trim();
            text = text.ToLowerInvariant();
            text = text.Replace('.', ' ');
            text = Regex.Replace(text, @"\s+", " ");
            text = text.Replace(' ', '-');
            return text;
        }
    }
}
