using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Compiler
{
    public class NumberState : State
    {
        private long value;
        public NumberState(char start)
        {
            value = (long)Char.GetNumericValue(start);
        }
        public override void Handle(Scanner scanner, char data)
        {
            if (Char.IsDigit(data)) { value = (value * 10) + (long)Char.GetNumericValue(data); }
            else if (data != '\'') //Ignore ' (Allows to write int32 literals as 1''000'000'000)
            {
                scanner.AddToken(new IntLiteralToken((int)value));
                scanner.CurrentState = new DefaultState();
                scanner.CurrentState.Handle(scanner, data);
            }
            if (value > Int32.MaxValue) { throw new LexicalException("Int32 Literal '" + value + "' is to large"); }
        }
    }
}
