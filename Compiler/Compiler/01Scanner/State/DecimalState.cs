using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Compiler
{
    public class DecimalState : State
    {
        private decimal value;
        private decimal factor = 0.1m;
        public DecimalState(long value)
        {
            this.value = value;
        }
        public override void Handle(Scanner scanner, char data)
        {
            if (Char.IsDigit(data))
            {
                value += (decimal)Char.GetNumericValue(data) * factor;
                factor /= 10;
            }
            else if (data == 'm')
            {
                scanner.AddToken(new DecimalLiteralToken((decimal)value));
                scanner.CurrentState = new DefaultState();
            }
            else if (data != '\'')
            {
                throw new LexicalException("Invalid Literal");
            }
        }
    }
}
