using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Compiler
{
    public class BoolLiteralToken : GenericParamToken<bool>
    {
        public BoolLiteralToken(bool value) : base(Terminals.LITERAL, value) { }
    }
}
