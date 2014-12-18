using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace FixFoxi2CSharp
{
    public class CreatorAST : Creator
    {
        public void Create(StreamWriter writer, IDictionary<string, IDictionary<string, string[]>> parseTable)
        {
            writer.WriteLine("public partial class Tokennode : Treenode {");
            writer.WriteLine("public virtual IASTNode ToAbstractSyntax()");
            writer.WriteLine("{");
            writer.WriteLine("    throw new NotImplementedException();");
            writer.WriteLine("} }");
            foreach (KeyValuePair<string, IDictionary<string, string[]>> entry in parseTable)
            {
                foreach (KeyValuePair<string, string[]> terminal in entry.Value)
                {
                    writer.WriteLine("public partial class {0}{1} : {0}", entry.Key.FirstUpper(), terminal.Key);
                    writer.WriteLine("{");
                    writer.WriteLine("    public virtual IASTNode ToAbstractSyntax()");
                    writer.WriteLine("    {");
                    writer.WriteLine("        throw new NotImplementedException();");
                    writer.WriteLine("    }");
                    writer.WriteLine("}");
                }
            }
        }
    }
}
