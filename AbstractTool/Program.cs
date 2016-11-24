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
            bool ok;
            AbstractTool at;
            string action;
            do
            {
                Console.WriteLine(Variables.ActionToPerform);
                action = Console.ReadLine().ToLower();
                ok = action.Equals(Variables.ActionInspect) || action.Equals(Variables.ActionCensor);
                Console.WriteLine(ok ? Variables.ActionOk : Variables.ActionKo );
            } while (!ok);
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
                    {
                        at = new AbstractTool(fileName);
                        switch(action)
                        {
                            case Variables.ActionInspect:
                                at.Inspect();
                                break;
                            case Variables.ActionCensor:
                                at.Censore();
                                break;
                        }
                    }
                    else Console.WriteLine(Variables.FileNotFoundMessage);
                }
            }
            while (!input.Equals(string.Empty));
            Console.WriteLine(Variables.ExitMessage);
            Console.ReadKey();
        }
    }
}
