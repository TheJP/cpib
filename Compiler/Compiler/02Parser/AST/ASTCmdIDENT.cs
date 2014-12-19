using Compiler._02Parser.AST;

namespace Compiler
{
    public class ASTCmdIdent : ASTCpsCmd
    {
        public IASTNode LValue { get; set; }

        public IASTNode RValue { get; set; }
    }
}