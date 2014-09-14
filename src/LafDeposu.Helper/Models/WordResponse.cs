using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LafDeposu.Helper.Models
{
    public class WordResponse
    {
        public WordResponseType Type { get; set; }
        public string Title { get; set; }
        public string Message { get; set; }
    }
}
