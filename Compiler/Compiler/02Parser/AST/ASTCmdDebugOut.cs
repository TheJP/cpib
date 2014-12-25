using Compiler._02Parser.AST;

namespace Compiler
{
    public class ASTCmdDebugOut : ASTCpsCmd
    {
        public IASTNode Expr { get; set; }
    }
}