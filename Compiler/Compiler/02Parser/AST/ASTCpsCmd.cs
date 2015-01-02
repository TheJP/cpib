using Compiler._02Parser.AST;

namespace Compiler
{
    public abstract class ASTCpsCmd : IASTNode
    {
        public ASTCpsCmd()
        {
            NextCmd = new ASTEmpty();
        }
        public IASTNode NextCmd { get; set; }

        public abstract int GenerateCode(int loc, IVirtualMachine vm, CheckerInformation info);
    }
}