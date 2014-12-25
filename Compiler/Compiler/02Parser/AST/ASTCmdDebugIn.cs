using Compiler._02Parser.AST;

namespace Compiler
{
    public class ASTCmdDebugIn : ASTCpsCmd
    {
        public IASTNode Expr { get; set; }
    }
}