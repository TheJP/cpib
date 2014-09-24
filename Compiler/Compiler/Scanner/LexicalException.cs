using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Compiler
{
    public class LexicalException : Exception
    {
        public LexicalException(string message) : base(message) { }
    }
}
