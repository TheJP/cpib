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
        public virtual int GenerateLValue(int loc, IVirtualMachine vm, CheckerInformation info)
        {
            throw new IVirtualMachine.InternalError("Expression is no LValue");
        }
        public abstract int GenerateCode(int loc, IVirtualMachine vm, CheckerInformation info);
        public abstract Type GetExpressionType(CheckerInformation info);
        public abstract void GetUsedIdents(ScopeChecker.UsedIdents usedIdents);
    }
}