using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Compiler
{
    /// <summary>
    /// Identifier Token (e.g. Variable names)
    /// </summary>
    [Serializable]
    public class IdentToken : GenericParamToken<string>
    {
        public IdentToken(string ident) : base(Terminals.IDENT, ident) { }
    }
}
