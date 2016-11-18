using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace AbstractTool
{
    class AbstractTool
    {
        private string _path;

        private AbstractTool() { }
        public AbstractTool(string path) : this()
        {
            Path = path;
        }

        public void Inspection()
        {
            foreach (string filePath in Files)
                InspectFile(filePath);
        }

        private static void InspectFile(string path)
        {
            using (var fi = new Files.FileInspector(path))
            {
                Console.WriteLine(fi.File.Text);
                Dictionary<string, int> words = fi.Words;
                foreach (KeyValuePair<string, int> entry in words)
                    Console.WriteLine(entry.Key + ": " + entry.Value);
            }
        }

        public string Path
        {
            get { return _path; }
            set { _path = value; }
        }
        public string[] Files
        {
            get
            {
                Util.CheckDirectory(Path);
                string[] files = Util.GetFilesInFolder(Path, Variables.AllFilesPattern),
                    exclude = Util.GetFilesInFolder(Path, Variables.InfoFilesPattern);
                return files.Except(exclude).ToArray();
            }
        }
    }
    namespace Files
    { 
        class FileInspector : IDisposable
        {
            private string _path;

            private FileInspector() { }
            public FileInspector(string path) : this()
            {
                Path = path;
            }

            public string Path
            {
                get { return _path; }
                set { _path = value; }
            }
            public FileData File
            {
                get { return new FileData(Path); }
            }
            public Dictionary<string, int> Words
            {
                get
                {
                    return File.Inspection;
                }
            }

            public void Dispose()
            {
                Path = null;
                GC.SuppressFinalize(this);
            }
        }
        class FileData
        {
            private string _path;
            private DateTime _date;

            private FileData()
            {
                _date = DateTime.Now;
            }
            public FileData(string path) : this()
            {
                Path = path;
            }

            private static string[] TextToCleanArray(string source)
            {
                char[] delimiterChars = { '\'', ' ', ',', '.', ':', '\n',  };
                return source.Split(delimiterChars).Where(x => !(string.IsNullOrEmpty(x) || Restricted.Words.Contains(x))).ToArray();

            }

            public string Path
            {
                get { return _path; }
                set { _path = value; }
            }
            private string[] PathArray
            {
                get { return Path.Split(Variables.BackslashChar); }
            }
            public string DirectoryPath
            {
                get
                {
                    string[] pathArray = PathArray;
                    return string.Join(Variables.Backslash, pathArray.Take(pathArray.Length - 2));
                }
            }
            public string FileName
            {
                get { return FileToArray[0]; }
            }
            public string FileExtension
            {
                get { return FileToArray[1]; }
            }
            public string File
            {
                get { return PathArray.Last(); }
            }
            public string Text
            {
                get
                {
                    try
                    {
                        using (StreamReader sr = new StreamReader(Path))
                        {
                            return sr.ReadToEnd();
                        }
                    }
                    catch (IOException ex)
                    {
                        Console.WriteLine(Variables.FileCouldNotBeReadMessage);
                        Console.WriteLine(ex.Message);
                        return Variables.FileCouldNotBeReadError;
                    }
                }
            }
            private string[] FileToArray
            {
                get { return File.Split('.'); }
            }
            public DateTime Date
            {
                get { return _date; }
                set { _date = value; }
            }
            public Dictionary<string, int> Inspection
            {
                get
                {
                    Dictionary<string, int> wordInspection = new Dictionary<string, int>();
                    try
                    {
                        using (StreamReader sr = new StreamReader(Path))
                        {
                            string[] wordList = TextToCleanArray(sr.ReadToEnd().ToLower());
                            Util.CountOccurrences(wordList, ref wordInspection);
                            Util.SortDictionary(ref wordInspection);
                            return wordInspection;
                        }
                    }
                    catch (IOException ex)
                    {
                        Console.WriteLine(Variables.FileCouldNotBeReadMessage);
                        Console.WriteLine(ex.Message);
                        return wordInspection;
                    }
                }
            }
        }
    }
}
