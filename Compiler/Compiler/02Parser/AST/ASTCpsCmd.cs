using System.Collections.Generic;

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
        public abstract void GetUsedIdents(ScopeChecker.UsedIdents usedIdents);
    }
}