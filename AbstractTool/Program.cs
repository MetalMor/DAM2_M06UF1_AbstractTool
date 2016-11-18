using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AbstractTool
{
    class Program
    {
        static void Main(string[] args)
        {
            AbstractTool at = new AbstractTool(Variables.DirectoryPath);
            string[] files = at.Files;
            foreach (string file in files) AbstractTool.InspectFile(file);
            Console.ReadKey();
        }
    }
}
