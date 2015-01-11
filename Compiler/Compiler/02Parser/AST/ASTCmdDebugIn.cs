using System;

namespace Compiler
{
    public class ASTCmdDebugIn : ASTCpsCmd
    {
        public ASTExpression Expr { get; set; }

        public override void GenerateCode(uint block, ref uint loc, MachineCode mc, CheckerInformation info)
        {
            throw new NotSupportedException("DebugIn not implemented: Would consume to much code");
        }

        public override void GetUsedIdents(ScopeChecker.UsedIdents usedIdents)
        {
            Expr.GetUsedIdents(usedIdents);
        }
    }
}