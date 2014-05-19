using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LafDeposu.Helper.Models
{
    public class SearchShare
    {
        public string Keyword { get; set; }
        public string StartsWith { get; set; }
        public string Contains { get; set; }
        public string EndsWith { get; set; }
        public int? ResultCharCount { get; set; }
    }
}
