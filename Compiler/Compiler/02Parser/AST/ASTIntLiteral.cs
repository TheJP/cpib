using Compiler._02Parser.AST;

namespace Compiler
{
    public class ASTIntLiteral : IASTNode
    {
        public ASTIntLiteral(int value)
        {
            this.Value = value;
        }

        public int Value { get; set; }

        public override string ToString()
        {
            return Value.ToString();
        }
    }
}