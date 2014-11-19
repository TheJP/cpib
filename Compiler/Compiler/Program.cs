using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Compiler
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                Scanner scanner = new Scanner();
                var list = scanner.Scan(new System.IO.StreamReader("parsertest01.iml"));
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
