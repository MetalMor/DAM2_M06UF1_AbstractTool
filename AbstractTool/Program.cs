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
            new AbstractTool(Variables.DirectoryPath).Inspection();
            Console.ReadKey();
        }
    }
}
