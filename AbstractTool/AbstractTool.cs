using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Xml;

namespace AbstractTool
{
    class AbstractTool
    {
        private string _path = default(string);

        private AbstractTool() { }
        public AbstractTool(string fileName) : this()
        {
            Path = Variables.DirectoryPath + fileName;
        }

        public bool Inspect()
        {
            if (File.Exists(Path))
                using (var fi = new Files.FileInspector(Path))
                    return fi.SaveInfo();
            return false;
        }

        public string Path
        {
            get { return _path; }
            set { _path = value; }
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
                List<string> keyList = FileData.Inspection.Keys.ToList();
                int numberTop = keyList.Count >= Variables.NumberTopWords ? 
                    Variables.NumberTopWords : keyList.Count;
                o += Variables.FileNameLabel + FileData.Name + Environment.NewLine;
                o += Variables.ExtensionLabel + FileData.Extension + Environment.NewLine;
                o += Variables.DateLabel + FileData.Date.ToString(Variables.DateFormatPattern) + Environment.NewLine;
                o += Variables.WordCountLabel + FileData.WordCount + Environment.NewLine;
                o += Variables.AboutLabel + string.Join(Variables.CommaSeparator, keyList.GetRange(0, numberTop));
                output = o;
            }

            public string Path
            {
                get { return _path; }
                set { _path = value; }
            }
            private string DestinationFolder
            {
                get
                {
                    return FileData.DirectoryPath + FileData.Name + Variables.Backslash;
                }
            }
            private string InfoFileName
            {
                get
                {
                    return FileData.Name + Variables.InfoFilesSuffix + Variables.Dot + FileData.Extension;
                }
            }
            private string InspectionPath
            {
                get
                {
                    return DestinationFolder + InfoFileName;
                }
            }
            public FileData FileData
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
                    return FileData.Inspection;
                }
            }

            public void Dispose()
            {
                Path = null;
                FileData = null;
                GC.SuppressFinalize(this);
            }

            internal bool SaveInfo()
            {
                string output;
                GetProperties(out output);
                if(Util.WriteFile(InspectionPath, output))
                {
                    string destinationPath = DestinationFolder + FileData.FullName;
                    if(!File.Exists(destinationPath)) File.Move(Path, DestinationFolder + FileData.FullName);
                    Console.WriteLine(Variables.InspectingFileMessage + Path.Split(Variables.BackslashChar).Last());
                    return true;
                }
                return false;
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
                    if (_text == default(string)) Util.ReadFile(Path, out _text);
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
                        string[] wordList = Util.TextToCleanArray(Text.ToLower());
                        Util.CountOccurrences(wordList, ref wordInspection);
                        Util.SortDictionary(ref wordInspection);
                        Xml.WriteXmlWordsFile(Xml.GetFilePath(Name), wordInspection);
                        _inspection =  wordInspection;
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
