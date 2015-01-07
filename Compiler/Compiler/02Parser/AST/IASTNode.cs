using System.Collections.Generic;

namespace Compiler
{
    public interface IASTNode
    {
        void GenerateCode(uint block, ref uint loc, MachineCode mc, CheckerInformation info);
        void GetUsedIdents(ScopeChecker.UsedIdents usedIdents);
    }
}