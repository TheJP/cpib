using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Compiler
{
    public class Token
    {
        private Terminal Terminal { get; set; }
        public Token(Terminal terminal)
        {
            this.Terminal = terminal;
        }
    }
}
