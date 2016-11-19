
namespace AbstractTool
{
    static class Restricted
    {
        private static string[] _words;

        internal static string[] Words
        {
            get
            {
                if(_words == null)
                    AddWords(Xml.ReadXmlWordsFile(Variables.RestrictionsXmlFilePath));
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
