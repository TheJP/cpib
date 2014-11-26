using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Compiler
{
    public class DecimalLiteralToken : GenericParamToken<decimal>
    {
        public DecimalLiteralToken(decimal value) : base(Terminals.LITERAL, value) { }
    }
}
