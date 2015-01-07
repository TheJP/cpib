using System.Collections.Generic;

namespace Compiler
{
    public abstract class ASTExpression : IASTNode
    {
        public ASTExpression()
        {
            NextExpression = new ASTEmpty();
        }

        public IASTNode NextExpression { get; set; }
        public virtual int GenerateLValue(int loc, MachineCode mc, CheckerInformation info, bool hasToBeLValue = true)
        {
            throw new CheckerException("Expression is no LValue");
        }
        public abstract int GenerateCode(int loc, MachineCode mc, CheckerInformation info);
        public abstract Type GetExpressionType(CheckerInformation info);
        public abstract void GetUsedIdents(ScopeChecker.UsedIdents usedIdents);
    }
}