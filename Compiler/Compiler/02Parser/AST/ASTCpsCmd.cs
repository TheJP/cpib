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

        public abstract void GenerateCode(uint block, ref uint loc, MachineCode mc, CheckerInformation info);
        public abstract void GetUsedIdents(ScopeChecker.UsedIdents usedIdents);
    }
}