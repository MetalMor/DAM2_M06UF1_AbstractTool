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
    class Program
    {
        static void Main(string[] args)
        {
            Assistant.Run();
        }
    }
    static class Assistant
    {
        internal static void Run()
        {
            string path, input;
            AbstractTool at;
            Util.CheckDirectory(Variables.DirectoryPath);
            Console.WriteLine(Variables.FileNameInputMessage);
            do
            {
                input = Console.ReadLine();
                path = Variables.DirectoryPath + input;
                at = new AbstractTool(path);

                if (input == string.Empty) Console.WriteLine(Variables.ExitMessage);

                if (input.IndexOf(Variables.TextFileExtension) < 0)
                    if (input.IndexOf(Variables.Dot) < 0)
                    {
                        path += Variables.TextFileExtension;
                        if(File.Exists(path)) at = new AbstractTool(path);
                    }
                    else Console.WriteLine(Variables.FileNotAllowedMessage);
                else if (File.Exists(path)) at = new AbstractTool(path);
                else Console.WriteLine(Variables.FileNotFoundMessage);

                if (at != null) at.Inspect();
            }
            while (!input.Equals(string.Empty));
            Console.ReadKey();
        }

        
    }
}
