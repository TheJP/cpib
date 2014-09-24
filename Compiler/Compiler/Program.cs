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
            Scanner scanner = new Scanner();
            var list = scanner.Scan(new System.IO.StreamReader("test01.iml"));
            Console.WriteLine(list.ToString());
        }
    }
}
