using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace FixFoxi2CSharp
{
    public class CreatorClasses : Creator
    {
        public void Create(StreamWriter writer, IDictionary<string, IDictionary<string, string[]>> parseTable)
        {
            Regex notTerminalRegex = new Regex("<([\\w]+)>");
            writer.WriteLine("public class Tokennode : Treenode { public Token Token { get; private set; } public Tokennode(Token token) { this.Token = token; } }");
            foreach (KeyValuePair<string, IDictionary<string, string[]>> entry in parseTable)
            {
                writer.WriteLine("public interface {0} : Treenode {{ }}", entry.Key.FirstUpper());
                foreach (KeyValuePair<string, string[]> terminal in entry.Value)
                {
                    writer.WriteLine("public class {0}{1} : {0}", entry.Key.FirstUpper(), terminal.Key);
                    writer.WriteLine("{");
                    foreach (string symbol in terminal.Value)
                    {
                        if (symbol.StartsWith("<"))
                        {
                            string nts = notTerminalRegex.Match(symbol).Groups[1].Value;
                            writer.WriteLine("    public {0} {0} {{ get; set; }}", nts.FirstUpper());
                        }
                        else
                        {
                            writer.WriteLine("    public Tokennode {0} {{ get; set; }}", symbol);
                        }

                    }
                    writer.WriteLine("}");
                }
            }
        }
    }
}
