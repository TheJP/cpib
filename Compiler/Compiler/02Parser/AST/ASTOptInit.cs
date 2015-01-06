using System.Collections.Generic;

namespace Compiler
{
    public class ASTOptInit : IASTNode
    {
        public string Ident { get; set; }

        public IASTNode NextInit { get; set; }

        public int GenerateCode(int loc, IVirtualMachine vm, CheckerInformation info)
        {
            throw new IVirtualMachine.InternalError("ASTOptInit.GenerateCode was called. This should never happen!");
        }

        public void GetUsedIdents(ScopeChecker.UsedIdents usedIdents) { }
    }
}