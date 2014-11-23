using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace FixFoxi2CSharp
{
    public class CreatorProductions : Creator
    {
        public void Create(StreamWriter writer, IDictionary<string, IDictionary<string, string[]>> parseTable)
        {
            Regex notTerminalRegex = new Regex("<([\\w]+)>");
            writer.WriteLine("//* Productions *//");
            writer.WriteLine();
            writer.WriteLine("ParseTable = new Dictionary<NotTerminals, IDictionary<Terminals, Symbol[]>>();");
            writer.WriteLine();
            foreach (KeyValuePair<string, IDictionary<string, string[]>> entry in parseTable)
            {
                writer.WriteLine("ParseTable[NotTerminals.{0}] = new Dictionary<Terminals, Symbol[]>();", entry.Key);
                foreach (KeyValuePair<string, string[]> terminal in entry.Value)
                {
                    writer.WriteLine("ParseTable[NotTerminals.{0}][Terminals.{1}] = new Symbol[]{{", entry.Key, terminal.Key);
                    foreach (string symbol in terminal.Value)
                    {
                        writer.Write("    ");
                        if (symbol.StartsWith("<"))
                        {
                            //NotTerminalSymbol (NTS)
                            string nts = notTerminalRegex.Match(symbol).Groups[1].Value;
                            writer.WriteLine("new Symbol(NotTerminals.{0}, (p,s) => {{ (({1}{2})p).{3} = ({3})s; }}),", nts, entry.Key.FirstUpper(), terminal.Key, nts.FirstUpper());
                        }
                        else
                        {
                            //TerminalSymbol
                            writer.WriteLine("new Symbol(Terminals.{0}, (p,s) => {{ (({1}{2})p).{0} = (Tokennode)s; }}),", symbol, entry.Key.FirstUpper(), terminal.Key);
                        }
                    }
                    writer.WriteLine("};");
                    writer.WriteLine();
                }
            }
        }
    }
}
