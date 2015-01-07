using System.Collections.Generic;

namespace Compiler
{
    public class ASTGlobalParam : IASTNode
    {
        public ASTGlobalParam()
        {
            NextParam = new ASTEmpty();
        }

        public IASTNode NextParam { get; set; }

        public string Ident { get; set; }

        public ChangeMode? OptChangemode { get; set; }

        public FlowMode? FlowMode { get; set; }

        public override string ToString()
        {
            return string.Format("{0} {1} {2}", FlowMode, OptChangemode, Ident);
        }

        public void GenerateCode(uint block, ref uint loc, MachineCode mc, CheckerInformation info)
        {
            throw new CheckerException("ASTGlobalParam.GenerateCode was called. This should never happen!");
        }
        public void GetUsedIdents(ScopeChecker.UsedIdents usedIdents)
        {
            string tmp = usedIdents.CurrentNamespace;
            usedIdents.CurrentNamespace = null;
            usedIdents.AddStoIdent(Ident, false); //TODO: init!
            usedIdents.CurrentNamespace = tmp;
        }
    }
}