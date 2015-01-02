using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Compiler
{
    public class CheckerException : Exception
    {
        public CheckerException(string message) : base(message) { }
    }
}
