using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Compiler
{
    public class GTState : State
    {
        public override void Handle(Scanner scanner, char data)
        {
            scanner.CurrentState = new DefaultState();
            scanner.AddToken(new OperatorToken(Terminals.RELOPR, data == '=' ? Operators.GE : Operators.GT));
            if (data != '=') { scanner.CurrentState.Handle(scanner, data); }
        }
    }
}
