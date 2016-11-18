using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AbstractTool
{
    class Util
    {
        public static string[] GetFilesInFolder(string path, string fileNamePattern)
        {
            return Directory.GetFiles(
                path, 
                fileNamePattern + Variables.TextFileExtension, 
                SearchOption.AllDirectories
            );
        }
    }
}
