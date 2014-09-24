using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Compiler
{
    /// <summary>
    /// Represents a basic Token, which is a single element of the tokenlist (tokenlist=output of the scanner)
    /// </summary>
    public class Token
    {
        public Terminals Terminal { get; set; }
        public Token(Terminals terminal)
        {
            this.Terminal = terminal;
        }
        public override string ToString()
        {
            return Terminal.ToString();
        }
    }
}
