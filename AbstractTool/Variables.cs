using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AbstractTool
{
    static class Variables
    {
        internal static string DirectoryPath = @"C:\Users\" + Environment.UserName + @"\Desktop\AbstractTool";

        internal static string FolderMessagePost = " folder.";
        internal static string CreatedFolderMessagePre = "Created";
        internal static string FoundFolderMessagePre = "Found";
        internal static string CreatedFolderMessage = CreatedFolderMessagePre + FolderMessagePost;
        internal static string FoundFolderMessage = FoundFolderMessagePre + FolderMessagePost;

        internal static string TextFileExtension = ".txt";
        internal static string AllFilesPattern = "*";
        internal static string InfoFilesPattern = "*_info";

        internal static string FileCouldNotBeReadMessage = "File could not be read: ";
        internal static string FileCouldNotBeReadError = "ERROR: could not read file";

        internal static string VoidSpace = " ";
        internal static string RestrictedApostropheL = "l'";
        internal static string RestrictedApostropheD = "d'";
        internal static string Backslash = @"\";
        internal static string PluralEndOfString = "s";

        internal static char BackslashChar { get { return GetSingleChar(Backslash); } }
        internal static char VoidSpaceChar { get { return GetSingleChar(VoidSpace); } }

        private static char GetSingleChar(string str)
        {
            return str.ToCharArray()[0];
        }
    }
}
