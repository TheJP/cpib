using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FixFoxi2CSharp
{
    class Program
    {
        static void Main(string[] args)
        {
            IDictionary<string, IDictionary<string, string[]>> parseTable = new Input().ReadParseTable("grammar.txt");
        }
    }
}
