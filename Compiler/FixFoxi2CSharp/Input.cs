using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace FixFoxi2CSharp
{
    public class Input
    {
        public IDictionary<string, IDictionary<string, string[]>> ReadParseTable(string path)
        {
            IDictionary<string, IDictionary<string, string[]>> parseTable = new Dictionary<string, IDictionary<string, string[]>>();
            using (StreamReader reader = new StreamReader(path))
            {
                Regex notTerminalRegex = new Regex("<([\\w]+)>");
                string currentNotTerminal = null;
                string currentTerminal = null;
                bool reachedEnd = false;
                int lineNumber = 1;
                while (!reader.EndOfStream)
                {
                    string line = reader.ReadLine();
                    if (reachedEnd && !String.IsNullOrEmpty(line)) { Console.WriteLine("Warning: Found \"val it = () : unit\" not at last line. Please check to see if you created the input file correctly."); }
                    if (line[0] == '<')
                    {
                        //New row of the parsetable
                        currentNotTerminal = notTerminalRegex.Match(line).Captures[0].Value; //If this throws an exception: The input file is invalid
                        parseTable.Add(currentNotTerminal, new Dictionary<string, string[]>());
                    }
                    else if (line.StartsWith("  terminal"))
                    {
                        //New column of the parsetable
                        if (currentNotTerminal == null) { throw new Exception("Invalid ParseTable at " + lineNumber); }
                        currentTerminal = line.Substring("  terminal".Length);
                    }
                    else if (line.StartsWith("    ") || String.IsNullOrEmpty(line))
                    {
                        //Production, to be used with the current cell (row and column)
                        if (currentTerminal == null) { throw new Exception("Invalid ParseTable" + lineNumber); }
                        parseTable[currentNotTerminal].Add(currentTerminal, line.Trim().Split(' '));
                        currentTerminal = null;
                    }
                    else if (line.Equals("val it = () : unit"))
                    {
                        reachedEnd = true;
                    }
                    ++lineNumber;
                }
                if (!reachedEnd) { Console.WriteLine("Warning: Did not find expected \"val it = () : unit\" at the last line. Please check to see if you created the input file correctly. (Empty lines at the end of the output are important!)"); }
            }
            return parseTable;
        }
    }
}
