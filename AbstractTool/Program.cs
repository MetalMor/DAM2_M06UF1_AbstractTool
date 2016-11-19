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
            string fileName, input, path;
            Util.CheckDirectory(Variables.DirectoryPath);
            Console.WriteLine(Variables.FileNameInputMessage);
            do
            {
                input = Console.ReadLine();
                fileName = input;
                if (input != string.Empty)
                {
                    if (input.IndexOf(Variables.TextFileExtension) < 0)
                        if (input.IndexOf(Variables.Dot) < 0) fileName += Variables.TextFileExtension;
                        else Console.WriteLine(Variables.FileNotAllowedMessage);

                    if (File.Exists(Util.GetProgramFilePath(fileName)))
                        new AbstractTool(fileName).Inspect();
                    else Console.WriteLine(Variables.FileNotFoundMessage);
                }
            }
            while (!input.Equals(string.Empty));
            Console.WriteLine(Variables.ExitMessage);
            Console.ReadKey();
        }
    }
}
