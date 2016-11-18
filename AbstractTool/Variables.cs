using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AbstractTool
{
    static class Variables
    {
        public static string DirectoryPath = @"C:\Users\" + Environment.UserName + @"\Desktop\AbstractTool";

        public static string FolderMessagePost = " folder.";
        public static string CreatedFolderMessagePre = "Created";
        public static string FoundFolderMessagePre = "Found";
        public static string CreatedFolderMessage = CreatedFolderMessagePre + FolderMessagePost;
        public static string FoundFolderMessage = FoundFolderMessagePre + FolderMessagePost;

        public static string TextFileExtension = ".txt";
        public static string AllFilesPattern = "*";
        public static string InfoFilesPattern = "*_info";

        public static string FileCouldNotBeReadMessage = "File could not be read: ";
        public static string FileCouldNotBeReadError = "ERROR: could not read file";

        public static string VoidSpace = " ";
        public static string RestrictedApostropheL = "l'";
        public static string RestrictedApostropheD = "d'";
        public static string Backslash = @"\";
        public static string PluralEndOfString = "s";

        public static char BackslashChar { get { return GetSingleChar(Backslash); } }
        public static char VoidSpaceChar { get { return GetSingleChar(VoidSpace); } }

        private static char GetSingleChar(string str)
        {
            return str.ToCharArray()[0];
        }
    }
}
