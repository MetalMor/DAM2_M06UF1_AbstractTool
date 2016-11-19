﻿using System;
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
            string fileName, input;
            AbstractTool at;
            Util.CheckDirectory(Variables.DirectoryPath);
            Console.WriteLine(Variables.FileNameInputMessage);
            do
            {
                input = Console.ReadLine();
                fileName = input;
                at = new AbstractTool(fileName);

                if (input == string.Empty) Console.WriteLine(Variables.ExitMessage);

                if (input.IndexOf(Variables.TextFileExtension) < 0)
                    if (input.IndexOf(Variables.Dot) < 0)
                    {
                        fileName += Variables.TextFileExtension;
                        if(File.Exists(fileName)) at = new AbstractTool(fileName);
                    }
                    else Console.WriteLine(Variables.FileNotAllowedMessage);
                else if (File.Exists(fileName)) at = new AbstractTool(fileName);
                else Console.WriteLine(Variables.FileNotFoundMessage);

                if (at != null) at.Inspect();
            }
            while (!input.Equals(string.Empty));
            Console.ReadKey();
        }

        
    }
}
