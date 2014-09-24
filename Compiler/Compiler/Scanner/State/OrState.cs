using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Compiler
{
    public class OrState : State
    {
        public override void Handle(Scanner scanner, char data)
        {
            scanner.CurrentState = new DefaultState();
            switch (data)
            {
                case '|': scanner.AddToken(new OperatorToken(Terminals.BOOLOPR, Operators.OR)); break;
                case '?': scanner.AddToken(new OperatorToken(Terminals.BOOLOPR, Operators.COR)); break;
                default: throw new LexicalException("Invalid character '" + data + "' after '|'");
            }
        }
    }
}
