using System.Collections.Generic;

namespace Compiler
{
    public abstract class ASTCpsDecl : IASTNode, IASTDecl
    {
        public string Ident { get; set; }
        public int Address { get; set; }

        public ASTCpsDecl()
        {
            NextDecl = new ASTEmpty();
        }

        public IASTNode NextDecl { get; set; }

        public abstract void GenerateCode(uint block, ref uint loc, MachineCode mc, CheckerInformation info);
        public abstract void GetUsedIdents(ScopeChecker.UsedIdents usedIdents);
    }
}