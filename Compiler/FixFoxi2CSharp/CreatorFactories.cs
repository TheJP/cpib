using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FixFoxi2CSharp
{
    public class CreatorFactories : Creator
    {
        public void Create(StreamWriter writer, IDictionary<string, IDictionary<string, string[]>> parseTable)
        {
            writer.WriteLine("//* Factories *//");
            writer.WriteLine();
            writer.WriteLine("Factories = new Dictionary<NotTerminals, IDictionary<Terminals, Func<Treenode>>>();");
            writer.WriteLine();
            foreach (KeyValuePair<string, IDictionary<string, string[]>> entry in parseTable)
            {
                writer.WriteLine("Factories[NotTerminals.{0}] = new Dictionary<Terminals, Func<Treenode>>();", entry.Key);
                foreach (KeyValuePair<string, string[]> terminal in entry.Value)
                {
                    writer.WriteLine("Factories[NotTerminals.{0}][Terminals.{1}] = () => {{ return new {2}{1}(); }};", entry.Key, terminal.Key, entry.Key.FirstUpper());
                }
                writer.WriteLine();
            }
        }
    }
}
