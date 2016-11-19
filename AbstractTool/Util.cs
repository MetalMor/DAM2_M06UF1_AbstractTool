using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AbstractTool
{
    class Util
    {
        internal static string[] GetFilesInFolder(string path, string fileNamePattern)
        {
            return Directory.GetFiles(
                path, 
                fileNamePattern + Variables.TextFileExtension, 
                SearchOption.AllDirectories
            );
        }

        internal static void CountOccurrences(string[] list, ref Dictionary<string, int> inspection)
        {
            string toSet;
            foreach (string element in list)
                if (inspection.ContainsKey(toSet = element) ||
                    inspection.ContainsKey(toSet = element.Remove(element.Length - 1)) ||
                    inspection.ContainsKey(toSet = element.Remove(element.Length - 2)))
                    inspection[toSet]++;
                else inspection.Add(element, 1);
        }

        internal static void SortDictionary(ref Dictionary<string, int> dictionary)
        {
            var list = from pair in dictionary orderby pair.Value descending select pair;
            dictionary = new Dictionary<string, int>();
            foreach (KeyValuePair<string, int> pair in list)
                dictionary.Add(pair.Key, pair.Value);
        }

        internal static void CheckDirectory(string path)
        {
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
                Console.WriteLine(Variables.CreatedFolderMessage);
            }
        }

        internal static bool ReadFile(string path, out string output)
        {
            try
            {
                using (StreamReader sr = new StreamReader(path))
                    output = sr.ReadToEnd();
            } catch(IOException ioEx)
            {
                Console.WriteLine(Variables.FileCouldNotBeReadMessage);
                Console.WriteLine(ioEx.Message);
                output = null;
            }
            return output != null;
        }

        internal static bool WriteFile(string path, string input)
        {
            bool ok = false;
            try
            {
                string output;
                using (StreamWriter sw = new StreamWriter(path))
                    sw.Write(input);
                ok = ReadFile(path, out output) && output.Equals(input);
            } catch(IOException ioEx)
            {
                Console.WriteLine(Variables.FileCouldNotBeWrittenMessage);
                Console.WriteLine(ioEx.Message);
            }
            return ok;
        }

        internal static string[] TextToArray(string source)
        {
            return source.Split(Variables.DelimiterChars).Where(x => !(string.IsNullOrEmpty(x))).ToArray();
        }

        internal static string[] TextToCleanArray(string source)
        {
            string[] list = source.Split(Variables.DelimiterChars).Where(x => !string.IsNullOrEmpty(x) && !Restricted.Words.Contains(x)).ToArray();
            return list;
        }
    }
}
