using Compiler._02Parser.AST;

namespace Compiler
{
    public class ASTCmdIDENT : ASTCpsCmd
    {
        public IASTNode LValue { get; set; }

        public IASTNode RValue { get; set; }
    }
}