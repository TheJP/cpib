using System.Collections.Generic;

namespace Compiler
{
    public class ASTOptInit : IASTNode
    {
        public string Ident { get; set; }

        public IASTNode NextInit { get; set; }

        public void GenerateCode(uint block, ref uint loc, MachineCode mc, CheckerInformation info)
        {
            throw new CheckerException("ASTOptInit.GenerateCode was called. This should never happen!");
        }

        public void GetUsedIdents(ScopeChecker.UsedIdents usedIdents) { }
    }
}