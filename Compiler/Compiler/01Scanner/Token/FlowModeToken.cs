using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Compiler
{
    [Serializable]
    public class FlowModeToken : GenericParamToken<FlowMode>
    {
        public FlowModeToken(FlowMode value) : base(Terminals.FLOWMODE, value) { }
    }
}
