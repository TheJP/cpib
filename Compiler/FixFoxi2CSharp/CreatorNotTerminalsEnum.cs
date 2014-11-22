using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FixFoxi2CSharp
{
    public class CreatorNotTerminalsEnum : Creator
    {
        public void Create(StreamWriter writer, IDictionary<string, IDictionary<string, string[]>> parseTable)
        {
            writer.WriteLine("    public enum NotTerminals");
            writer.WriteLine("    {");
            foreach (string notTerminal in parseTable.Keys)
            {
                writer.WriteLine("        {0},", notTerminal);
            }
            writer.WriteLine("    }");
        }
    }
}
