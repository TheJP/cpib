using Compiler._02Parser.AST;

namespace Compiler
{
    public class ASTAddOpr : IASTNode
    {
        public Operators Operator { get; set; }

        public IASTNode Term { get; set; }

        public IASTNode RepTerm { get; set; }
    }
}