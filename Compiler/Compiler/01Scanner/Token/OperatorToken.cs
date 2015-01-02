using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Compiler
{
    [Serializable]
    public class OperatorToken : GenericParamToken<Operators>
    {
        public OperatorToken(Terminals terminal, Operators op) : base(terminal, op) { }
    }
}
