using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Compiler
{
    public class Productions
    {
        public IDictionary<NotTerminals, IDictionary<Terminals, Symbol[]>> ParseTable { get; private set; }
        public Productions()
        {
            ParseTable = new Dictionary<NotTerminals, IDictionary<Terminals, Symbol[]>>();
            ParseTable[NotTerminals.expr] = new Dictionary<Terminals, Symbol[]>(); //for each NTS
            ParseTable[NotTerminals.expr][Terminals.ADDOPR] = new Symbol[]{ //For each Production
                new Symbol(Terminals.ADDOPR, t => {}),
                new Symbol(NotTerminals.expr, t => {})
            };
        }
    }
    public interface Expr : Treenode { } //For each NTS
    public class ExprAddopr : Expr { } //For each Production
}
