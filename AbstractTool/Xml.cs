using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace AbstractTool
{
    static class Xml
    {
        internal static string GetFilePath(string name)
        {
            return Variables.DirectoryPath + name + Variables.Backslash + name + Variables.XmlFileExtension;
        }
        internal static string[] ReadXmlWordsFile(string path)
        {
            XmlDocument doc = new XmlDocument();
            doc.Load(path);
            XmlElement root = doc.DocumentElement;
            XmlNodeList nodes = root.SelectNodes(Variables.WordElementTagName);
            List<string> words = new List<String>();
            foreach (XmlNode node in nodes) words.Add(node.InnerText);
            return words.ToArray();
        }

        internal static void WriteXmlWordsFile(string path, Dictionary<string, int> dict)
        {
            string[] pathArray = path.Split(Variables.BackslashChar);
            string directoryPath = string.Join(Variables.Backslash, pathArray.Take(pathArray.Count() - 1).ToArray());
            Directory.CreateDirectory(directoryPath);
            XmlWriter writer = XmlWriter.Create(path);
            writer.WriteStartDocument();

            writer.WriteStartElement(Variables.WordListElementTagName);
            writer.WriteAttributeString(Variables.CountAttributeName, dict.Count.ToString());

            foreach (KeyValuePair<string, int> entry in dict)
            {
                writer.WriteStartElement(Variables.WordElementTagName);
                writer.WriteAttributeString(Variables.OccurrencesAttributeName, entry.Value.ToString());
                writer.WriteString(entry.Key);
                writer.WriteEndElement();
            }

            writer.WriteEndElement();

            writer.WriteEndDocument();
            writer.Close();
        }
    }
}
