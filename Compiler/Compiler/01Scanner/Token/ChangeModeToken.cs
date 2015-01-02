using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Compiler
{
    [Serializable]
    public class ChangeModeToken : GenericParamToken<ChangeMode>
    {
        public ChangeModeToken(ChangeMode value) : base(Terminals.CHANGEMODE, value) { }
    }
}
