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

        public bool Censore()
        {
            if (File.Exists(Path))
                using (var fc = new Files.FileCensorer(Path))
                    return fc.Censor();
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
        abstract class FileProcessor : IDisposable
        {
            private string _path = default(string);

            protected FileProcessor() { }
            protected FileProcessor(string path)
            {
                Path = path;
            }

            public void Dispose()
            {
                Path = null;
                GC.SuppressFinalize(this);
            }

            public string Path
            {
                get { return _path; }
                set { _path = value; }
            }
        }
        class FileCensorer : FileProcessor, IDisposable
        {
            public FileCensorer(): base() { }
            public FileCensorer(string path): base(path) { }

            private string[] GetCensoredWords()
            {
                string data;
                bool ok = Util.ReadFile(Variables.CensoredWordsTxtFileName, out data);
                if (ok) return Regex.Split(data, Environment.NewLine);
                return new string[0];
            }

            public bool Censor()
            {
                string[] censoredWords = GetCensoredWords();
                string text;
                bool ok = Util.ReadFile(Path, out text);
                if (ok) 
                    foreach (string word in censoredWords)
                        text = text.Replace(word, Variables.CensoredWord);
                if (Util.WriteFile(CensoredFilePath, text))
                {
                    int lastBackslashIndex = Path.LastIndexOf(Variables.Backslash);
                    string fileName = Path.Substring(lastBackslashIndex, Path.LastIndexOf(Variables.Dot) - lastBackslashIndex);
                    string destinationFolder = Path.Substring(0, lastBackslashIndex);
                    string destinationPath = destinationFolder + fileName
                        + Variables.CensoredFileNameSuffix + Variables.TextFileExtension;
                    if (!File.Exists(destinationPath)) File.Move(Path, destinationFolder + fileName
                        + Variables.CensoredFileNameSuffix + Variables.TextFileExtension);
                    Console.WriteLine(Variables.CensoringFileMessage + Path.Split(Variables.BackslashChar).Last());
                    return true;
                }
                return ok;
            } 

            public new void Dispose()
            {
                base.Dispose();
            }

            private string CensoredFilePath
            {
                get
                {

                    return Path.Substring(0, Path.LastIndexOf(Variables.DotChar)) + Variables.CensoredFileNameSuffix + Variables.TextFileExtension;
                }
            }
        }
        class FileInspector : FileProcessor, IDisposable
        {
            private FileData _file = default(FileData);

            private FileInspector(): base() { }
            public FileInspector(string path) : base(path) { }

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

            public new void Dispose()
            {
                base.Dispose();
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
