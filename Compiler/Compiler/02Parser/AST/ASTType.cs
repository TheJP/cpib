using Compiler._02Parser.AST;

namespace Compiler
{
    public class ASTType : IASTNode
    {
        public Type Type { get; set; }

        public IASTNode Expr { get; set; }
    }
}