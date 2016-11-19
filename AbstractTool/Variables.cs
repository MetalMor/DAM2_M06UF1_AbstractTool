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
        internal const int NumberTopWords = 5;

        internal const string ProgramFolderName = @"\AbstractTool\";
        internal const string InspectingFileMessage = "File inspected: ";
        internal const string FileNotFoundMessage = "File not found, try again.";
        internal const string ExitMessage = "Leaving. Press any key.";
        internal const string FileNotAllowedMessage = "Only TXT files are allowed.";

        internal const string WordListElementTagName = "WordList";
        internal const string WordElementTagName = "Word";
        internal const string CountAttributeName = "Count";
        internal const string OccurrencesAttributeName = "Occurrences";

        internal const string FileCouldNotBeReadMessage = "File could not be read: ";
        internal const string FileCouldNotBeWrittenMessage = "File could not be written: ";
        internal const string FileCouldNotBeReadError = "ERROR: could not read file";

        internal const string TextFileExtension = ".txt";
        internal const string XmlFileExtension = ".xml";
        internal const string AllFilesPattern = "*";
        internal const string InfoFilesPattern = "*_info";

        internal const string VoidSpace = " ";
        internal const string RestrictedApostropheL = "l'";
        internal const string RestrictedApostropheD = "d'";
        internal const string Backslash = @"\";
        internal const string PluralEndOfString = "s";
        internal const string FileNameLabel = "File name: ";
        internal const string ExtensionLabel = "Extension: ";
        internal const string DateLabel = "Date: ";
        internal const string WordCountLabel = "Word count: ";
        internal const string AboutLabel = "About: ";
        internal const string DateFormatPattern = "yyyy-MM-dd";
        internal const string InfoFilesSuffix = "_info";
        internal const string Dot = ".";
        internal const string Colon = ":";
        internal const string CommaSeparator = ", ";

        private const string RestrictionsFileName = "restrictions";
        internal const string DirectoryCreatedMessage = "Created folder";

        internal static string RestrictionsTxtFileName { get { return RestrictionsFileName + TextFileExtension; } }
        internal static string RestrictionsXmlFileName { get { return RestrictionsFileName + XmlFileExtension; } }
        internal static string DirectoryPath { get { return Environment.GetFolderPath(Environment.SpecialFolder.Desktop) + ProgramFolderName; } }
        internal static string RestrictionsFilePath { get { return DirectoryPath + RestrictionsTxtFileName; } }
        internal static string CreatedFolderMessage { get { return DirectoryCreatedMessage + PutFilesInFolder; } }
        internal static string FileNameInputMessage { get { return "Enter the name of a file in " + DirectoryPath + " (leave blank to exit)"; } }

        private static string PutFilesInFolder { get { return CommaSeparator + "put txt files inside " + DirectoryPath + ProgramFolderName + Dot; } }
        
        internal static char BackslashChar { get { return GetSingleChar(Backslash); } }
        internal static char VoidSpaceChar { get { return GetSingleChar(VoidSpace); } }
        internal static char DotChar { get { return GetSingleChar(Dot); } }

        internal static char[] DelimiterChars
        {
            get
            {
                char[] dc = { '\'', ' ', ',', '.', ':', '\r', '\n', };
                return dc;
            }
        }

        public static string RestrictionsXmlFilePath { get { return DirectoryPath + RestrictionsXmlFileName; } }

        private static char GetSingleChar(string str)
        {
            return str.ToCharArray()[0];
        }
    }
}
