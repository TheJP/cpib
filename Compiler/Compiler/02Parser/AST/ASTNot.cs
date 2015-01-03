using System;

namespace Compiler
{
    public class ASTNot : ASTExpression
    {
        public ASTExpression Expr { get; set; }

        public override int GenerateCode(int loc, IVirtualMachine vm, CheckerInformation info)
        {
            loc = Expr.GenerateCode(loc, vm, info);

            var type = GetExpressionType(info);

            if (type == Type.BOOL || type == Type.INT32)
            {
                vm.IntInv(loc++);
            }
            else if (type == Type.DECIMAL)
            {
                vm.DecimalInv(loc++);
            }
            else
            {
                throw new IVirtualMachine.InternalError(
                            "Cannot negate Non-Bool value " + Expr.ToString());
            }

            return loc;
        }

        public override Type GetExpressionType(CheckerInformation info)
        {
            return Expr.GetExpressionType(info);
        }
    }  
}