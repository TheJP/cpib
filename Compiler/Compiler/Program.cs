using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Compiler
{
    class ProgramMain
    {
        static void Main(string[] args)
        {
            try
            {
                Scanner scanner = new Scanner();
                var list = scanner.Scan(new StreamReader("test02.iml"));
                Console.WriteLine("[" + String.Join(", ", list) + "]");
                Parser parser = new Parser();
                var tree = parser.Parse(list);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Failed: " + ex.Message);
            }
        }
    }
}
