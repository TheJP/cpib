using System;
using System.Collections.Generic;

namespace Compiler
{
    public class ASTType : ASTExpression
    {
        public Type Type { get; set; }

        public ASTExpression Expr { get; set; }

        public override string ToString()
        {
            return string.Format("{0}({1})", Type, Expr);
        }

        public override void GenerateCode(uint block, ref uint loc, MachineCode mc, CheckerInformation info)
        {
            throw new NotSupportedException("Decimals are not implemented, so casting would not make sense");
        }

        public override Type GetExpressionType(CheckerInformation info)
        {
            return this.Type;
        }

        public override void GetUsedIdents(ScopeChecker.UsedIdents usedIdents)
        {
            Expr.GetUsedIdents(usedIdents);
        }
    }
}
