using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Compiler
{
    public class TextState : State
    {
        private StringBuilder text;
        public TextState(string start) { text = new StringBuilder(start); }
        public override void Handle(Scanner scanner, char data)
        {
            if (Char.IsLetterOrDigit(data) && data <= 'z') { text.Append(data); }
            else
            {
                string token = text.ToString();
                if (scanner.Keywords.ContainsKey(token)) { scanner.AddToken(scanner.Keywords[token]); }
                else { scanner.AddToken(new IdentToken(token)); }
                scanner.CurrentState = new DefaultState();
                scanner.CurrentState.Handle(scanner, data);
            }
        }
    }
}
