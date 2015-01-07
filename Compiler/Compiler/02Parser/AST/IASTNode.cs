using System.Collections.Generic;

namespace Compiler
{
    public interface IASTNode
    {
        int GenerateCode(int loc, MachineCode mc, CheckerInformation info);
        void GetUsedIdents(ScopeChecker.UsedIdents usedIdents);
    }
}