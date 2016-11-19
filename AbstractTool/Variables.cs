using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AbstractTool
{
    static class Variables
    {
        internal static string DirectoryPath
        { 
            get
            {
                return Environment.GetFolderPath(Environment.SpecialFolder.Desktop) + ProgramFolderName;
            }
        }
        internal static string ProgramFolderName = @"\AbstractTool\";
        internal static string FolderMessagePost = " folder.";
        internal static string CreatedFolderMessagePre = "Created";
        internal static string FoundFolderMessagePre = "Found";
        private static string PutFilesInFolder = CommaSeparator + "put txt files inside " + DirectoryPath + ProgramFolderName + Dot;
        internal static string CreatedFolderMessage = CreatedFolderMessagePre + FolderMessagePost + PutFilesInFolder;
        internal static string FoundFolderMessage = FoundFolderMessagePre + FolderMessagePost;
        internal static string InspectingFileMessage = "File inspected: ";

        internal static string TextFileExtension = ".txt";
        internal static string AllFilesPattern = "*";
        internal static string InfoFilesPattern = "*_info";

        internal static string FileCouldNotBeReadMessage = "File could not be read: ";
        internal static string FileCouldNotBeWrittenMessage = "File could not be written: ";
        internal static string FileCouldNotBeReadError = "ERROR: could not read file";

        internal static string VoidSpace = " ";
        internal static string RestrictedApostropheL = "l'";
        internal static string RestrictedApostropheD = "d'";
        internal static string Backslash = @"\";
        internal static string PluralEndOfString = "s";
        internal static string FileNameLabel = "File name: ";
        internal static string ExtensionLabel = "Extension: ";
        internal static string DateLabel = "Date: ";
        internal static string WordCountLabel = "Word count: ";
        internal static string AboutLabel = "About: ";
        internal static string DateFormatPattern = "yyyy-MM-dd";
        internal static string InfoFilesSuffix = "_info";
        internal static string Dot = ".";
        internal static string CommaSeparator = ", ";

        internal static char BackslashChar { get { return GetSingleChar(Backslash); } }
        internal static char VoidSpaceChar { get { return GetSingleChar(VoidSpace); } }


        internal static char[] DelimiterChars = { '\'', ' ', ',', '.', ':', '\r', '\n', };

        private static char GetSingleChar(string str)
        {
            return str.ToCharArray()[0];
        }
    }
}
