using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Compiler
{
    public class ColonState : State
    {
        public override void Handle(Scanner scanner, char data)
        {
            scanner.CurrentState = new DefaultState();
            scanner.AddToken(new Token(data == '=' ? Terminals.BECOMES : Terminals.COLON));
            if (data != '=') { scanner.CurrentState.Handle(scanner, data); }
        }
    }
}
