using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AbstractTool
{
    static class Restricted
    {
        private static string[] _words;

        public static string[] Words
        {
            get
            {
                if (_words != null && _words.Length > 0)
                    return _words;
                AddWords("d", "l", "i", "ni", "o bé", "o", 
                    "ni", "no", "va",
                    "ara", "a més", "ja", "un", "altre",
                    "dir", "això", "és", "o", "sigui",
                    "doncs", "per tant", "conseqüència",
                    "encara", "endemés", "més", "tot", 
                    "després", "mentre", "tal que", "si", 
                    "sempre", "que", "malgrat", "encara", 
                    "perquè", "el", "els", "en", "es", "ets", 
                    "la", "les", "lo", "los", "a", "un", 
                    "una", "unes", "uns", "amb", "arran", 
                    "contra", "dalt", "damunt", "davall", 
                    "avall", "de", "deçà", "dellà", "des",
                    "devers", "devora", "dintre", "durant",
                    "entre", "envers", "excepte", "fins", 
                    "llevat", "mitjançant", "per", "pro", 
                    "salvant", "salvat", "segons", "sens", 
                    "sense", "sobre", "sota", "sots", "tret", 
                    "ultra", "via", "al", "als", "del", "pel", 
                    "dels", "pels", "as", "des", "dets", "pes",
                    "can", "cal", "cals", "cas", "son", "çon");
                return _words;
            }
        }
        private static void AddWord(string word)
        {
            _words[_words.Length] = word;
        }
        private static void AddWords(params string[] words)
        {
            _words = words;
        }
    }
}
