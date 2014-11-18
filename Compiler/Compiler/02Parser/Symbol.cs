using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Compiler
{
    /// <summary>
    /// A symbol is either a Terminal and a callback or a NotTerminal and a callback
    /// </summary>
    public class Symbol
    {
        public Terminals? Terminal { get; private set; }
        public NotTerminals? NotTerminal { get; private set; }
        public Action<Treenode> Callback { get; set; }

        public Symbol(Terminals terminal, Action<Treenode> callback)
        {
            this.Terminal = terminal;
            this.NotTerminal = null;
            this.Callback = callback;
        }
        public Symbol(NotTerminals notTerminal, Action<Treenode> callback)
        {
            this.Terminal = null;
            this.NotTerminal = notTerminal;
            this.Callback = callback;
        }
    }
}
