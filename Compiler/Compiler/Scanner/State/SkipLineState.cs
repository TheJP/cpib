using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Compiler
{
    public class SkipLineState : State
    {
        public override void Handle(Scanner scanner, char data)
        {
            if (data == '\n') { scanner.CurrentState = new DefaultState(); }
        }
    }
}
