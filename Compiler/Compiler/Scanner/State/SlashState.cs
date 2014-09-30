using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Compiler
{
    public class SlashState : State
    {
        public override void Handle(Scanner scanner, char data)
        {
            if (data == '/') { scanner.CurrentState = new SkipLineState(); }
            else
            {
                scanner.CurrentState = new DefaultState();
                scanner.AddToken(data == '=' ?
                    new OperatorToken(Terminals.RELOPR, Operators.NE) : // /=
                    new OperatorToken(Terminals.MULTOPR, Operators.DIV)); // /
                if (data != '=') { scanner.CurrentState.Handle(scanner, data); }
            }
        }
    }
}
