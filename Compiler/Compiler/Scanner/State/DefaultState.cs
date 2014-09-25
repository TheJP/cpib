using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Compiler
{
    public class DefaultState : State
    {
        public override void Handle(Scanner scanner, char data)
        {
            if (Char.IsLetter(data) && data <= 'z') { scanner.CurrentState = new TextState(data.ToString()); } //[a-zA-Z]
            else if (Char.IsDigit(data)) { scanner.CurrentState = new NumberState(data); } //[0-9]
            else if (!Char.IsWhiteSpace(data))
            {
                switch (data)
                {
                    case '(': Add(scanner, Terminals.LPAREN); break;
                    case ')': Add(scanner, Terminals.RPAREN); break;
                    case ',': Add(scanner, Terminals.COMMA); break;
                    case ';': Add(scanner, Terminals.SEMICOLON); break;
                    case '+': scanner.AddToken(new OperatorToken(Terminals.ADDOPR, Operators.PLUS)); break;
                    case '-': scanner.AddToken(new OperatorToken(Terminals.ADDOPR, Operators.MINUS)); break;
                    case '*': scanner.AddToken(new OperatorToken(Terminals.MULTOPR, Operators.TIMES)); break;
                    case '=': scanner.AddToken(new OperatorToken(Terminals.RELOPR, Operators.EQ)); break;
                    case '<': scanner.CurrentState = new LTState(); break;
                    case '>': scanner.CurrentState = new GTState(); break;
                    case '/': scanner.CurrentState = new SlashState(); break;
                    case '&': scanner.CurrentState = new AndState(); break;
                    case '|': scanner.CurrentState = new OrState(); break;
                    case ':': scanner.CurrentState = new ColonState(); break;
                    default:
                        throw new LexicalException(String.Format("Unexpected charachter: '{0}'", data));
                }
            }
        }
    }
}
