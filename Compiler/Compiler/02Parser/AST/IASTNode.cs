using System.Collections.Generic;

namespace Compiler
{
    public interface IASTNode
    {
        int GenerateCode(int loc, IVirtualMachine vm, CheckerInformation info);
        void GetUsedIdents(ScopeChecker.UsedIdents usedIdents);
    }
}