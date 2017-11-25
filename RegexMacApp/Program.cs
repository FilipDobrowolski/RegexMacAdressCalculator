using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RegexMacApp.App;

namespace RegexMacApp
{
    class Program
    {
        static void Main(string[] args)
        {
            //args = new string[] { "plik1.txt", "plik2.txt","\\" };
            RegexApplication.Start(args);
            Console.ReadKey();
        }
    }
}
