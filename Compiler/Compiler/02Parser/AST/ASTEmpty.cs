using System.Collections.Generic;

namespace Compiler
{
    public class ASTEmpty : IASTNode
    {
        public void GenerateCode(uint block, ref uint loc, MachineCode mc, CheckerInformation info) { }
        public void GetUsedIdents(ScopeChecker.UsedIdents usedIdents) { }
    }
}