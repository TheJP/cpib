using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Compiler
{
    [Serializable]
    public class MechModeToken : GenericParamToken<MechMode>
    {
        public MechModeToken(MechMode value) : base(Terminals.MECHMODE, value) { }
    }
}
