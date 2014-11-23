using System;
using System.Collections.Generic;
using System.IO;
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
            using (StreamWriter writer = new StreamWriter("NotTerminals.cs"))
            {
                new CreatorNotTerminalsEnum().Create(writer, parseTable);
            }
            using (StreamWriter writer = new StreamWriter("Productions.cs"))
            {
                writer.WriteLine(@"public class Productions
{
    public IDictionary<NotTerminals, IDictionary<Terminals, Symbol[]>> ParseTable { get; private set; }
    public IDictionary<NotTerminals, IDictionary<Terminals, Func<Treenode>>> Factories { get; private set; }
    public Productions()
    {");
                new CreatorProductions().Create(writer, parseTable);
                new CreatorFactories().Create(writer, parseTable);
                writer.WriteLine("}");
                writer.WriteLine("}");
                writer.WriteLine();
                new CreatorClasses().Create(writer, parseTable);
            }
        }
    }
}
