using System.Collections.Generic;

namespace Compiler
{
    public class ASTEmpty : IASTNode
    {
        public int GenerateCode(int loc, MachineCode mc, CheckerInformation info) { return loc; }
        public void GetUsedIdents(ScopeChecker.UsedIdents usedIdents) { }
    }
}