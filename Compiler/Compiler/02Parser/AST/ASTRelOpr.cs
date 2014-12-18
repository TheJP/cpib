using Compiler._02Parser.AST;

namespace Compiler
{
    public class ASTRelOpr : IASTNode
    {
        public Operators Operation { get; set; }

        public IASTNode Term { get; set; }

        public IASTNode RepTerm { get; set; }
    }
}