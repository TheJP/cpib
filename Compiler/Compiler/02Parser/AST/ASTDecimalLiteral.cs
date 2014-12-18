using Compiler._02Parser.AST;

namespace Compiler
{
    public class ASTDecimalLiteral : IASTNode
    {
        public ASTDecimalLiteral(decimal value)
        {
            this.Value = value;
        }

        public decimal Value { get; set; }
    }
}