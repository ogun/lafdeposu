using System;
using System.Collections.Generic;

namespace LafDeposu.Helper.Models
{
    [Serializable]
    public class WordGroup
    {
        public int length { get; set; }
        public List<Word> words { get; set; }
    }
}