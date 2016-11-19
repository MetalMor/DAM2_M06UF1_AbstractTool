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
        private string _path = default(string);

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
                Dictionary<string, int> words = fi.Words;
                fi.SaveInfo();
                Console.WriteLine(Variables.InspectingFileMessage + path.Split(Variables.BackslashChar).Last());
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
            private string _path = default(string);
            private FileData _file = default(FileData);

            private FileInspector() { }
            public FileInspector(string path) : this()
            {
                Path = path;
            }

            private void GetProperties(out string output)
            {
                string o = string.Empty;
                o += Variables.FileNameLabel + File.Name + Environment.NewLine;
                o += Variables.ExtensionLabel + File.Extension + Environment.NewLine;
                o += Variables.DateLabel + File.Date.ToString(Variables.DateFormatPattern) + Environment.NewLine;
                o += Variables.WordCountLabel + File.WordCount + Environment.NewLine;
                o += Variables.AboutLabel + string.Join(Variables.CommaSeparator, File.Inspection.Keys.ToList().GetRange(0, 5));
                output = o;
            }

            public string Path
            {
                get { return _path; }
                set { _path = value; }
            }
            private string InspectionPath
            {
                get
                {
                    return File.DirectoryPath + File.Name + Variables.InfoFilesSuffix + Variables.Dot + File.Extension;
                }
            }
            public FileData File
            {
                get {
                    if (_file == null) _file = new FileData(Path);
                    return _file;
                }
                private set
                {
                    _file = value;
                }
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
                File = null;
                GC.SuppressFinalize(this);
            }

            internal void SaveInfo()
            {
                try
                {
                    string output;
                    using (StreamWriter sw = new StreamWriter(InspectionPath))
                    {
                        GetProperties(out output);
                        sw.Write(output);
                    }
                } catch(IOException ioEx)
                {
                    Console.WriteLine(Variables.FileCouldNotBeWrittenMessage);
                    Console.WriteLine(ioEx.Message);
                }
            }
        }
        class FileData
        {
            private string _path = default(string);
            private DateTime _date = DateTime.Now;
            private string _text = default(string);
            private Dictionary<string, int> _inspection = new Dictionary<string, int>();

            private FileData() { }
            public FileData(string path) : this()
            {
                Path = path;
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
                    return string.Join(Variables.Backslash, pathArray.Take(pathArray.Length - 1)) + Variables.Backslash;
                }
            }
            public string Name
            {
                get { return FileToArray[0]; }
            }
            public string Extension
            {
                get { return FileToArray[1]; }
            }
            public string FullName
            {
                get { return PathArray.Last(); }
            }
            public string Text
            {
                get
                {
                    if (_text == default(string))
                    {
                        try
                        {
                            _text = File.ReadAllText(Path);
                        }
                        catch (IOException ex)
                        {
                            Console.WriteLine(Variables.FileCouldNotBeReadMessage);
                            Console.WriteLine(ex.Message);
                            _text = Variables.FileCouldNotBeReadError;
                        }
                    }
                    return _text;
                }
            }
            private string[] FileToArray
            {
                get { return FullName.Split('.'); }
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
                    if (_inspection.Count == 0)
                    {
                        Dictionary<string, int> wordInspection = new Dictionary<string, int>();
                        try
                        {
                            string[] wordList = Util.TextToCleanArray(Text.ToLower());
                            Util.CountOccurrences(wordList, ref wordInspection);
                            Util.SortDictionary(ref wordInspection);
                            _inspection =  wordInspection;
                        }
                        catch (IOException ex)
                        {
                            Console.WriteLine(Variables.FileCouldNotBeReadMessage);
                            Console.WriteLine(ex.Message);
                            _inspection = wordInspection;
                        }
                    }
                    return _inspection;
                }
            }
            public int WordCount
            {
                get
                {
                    return Util.TextToArray(Text).Length;
                }
            }
        }
    }
}
