using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Compiler
{
    public abstract class State
    {
        protected void Add(Scanner scanner, Terminals terminal) { scanner.AddToken(new Token(terminal)); }
        public abstract void Handle(Scanner scanner, char data);
    }
}
