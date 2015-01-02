using Compiler._02Parser.AST;

namespace Compiler
{
    public class ASTComma : IASTNode
    {
        public IASTNode Expr { get; set; }

        public IASTNode RepExpr { get; set; }

        public int GenerateCode(int loc, IVirtualMachine vm, CheckerInformation info)
        {
            return loc;
        }
    }
}