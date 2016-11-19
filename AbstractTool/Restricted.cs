using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace AbstractTool
{
    static class Restricted
    {
        private static string[] _words;

        internal static string[] Words
        {
            get
            {
                if(_words == null) ReadRestrictionsXmlFile();
                return _words;
            }
        }
        private static void ReadRestrictionsXmlFile()
        {
            XmlDocument doc = new XmlDocument();
            doc.Load(Variables.RestrictionsXmlFilePath);
            XmlElement root = doc.DocumentElement;
            XmlNodeList nodes = root.SelectNodes(Variables.WordElementTagName);
            List<string> words = new List<String>();
            foreach (XmlNode node in nodes) words.Add(node.InnerText);
            AddWords(words.ToArray());
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
