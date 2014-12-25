using Compiler._02Parser.AST;

namespace Compiler
{
    public class ASTCmdIdent : ASTCpsCmd
    {
        public IASTNode LValue { get; set; }

        public IASTNode RValue { get; set; }

        public override string ToString()
        {
            return string.Format("{0} := {1}", LValue, RValue);
        }
    }
}