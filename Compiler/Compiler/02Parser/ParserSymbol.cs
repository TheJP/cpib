using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Compiler
{
    /// <summary>
    /// Symbol, which is used by the parser
    /// </summary>
    public class ParserSymbol
    {
        public Symbol S { get; private set; }
        public Treenode Node { get; private set; }
        public ParserSymbol(Symbol symbol, Treenode node)
        {
            this.S = symbol;
            this.Node = node;
        }
        public void ExecuteCallback(Treenode contains)
        {
            S.Callback(Node, contains);
        }
        public bool IsTerminal { get { return S.Terminal != null; } }
        public bool IsNotTerminal { get { return S.NotTerminal != null; } }
    }
}
