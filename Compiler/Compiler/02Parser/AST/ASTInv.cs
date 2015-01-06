using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Compiler
{
    public class ASTInv : ASTExpression
    {
        public ASTExpression Expr { get; set; }

        public override int GenerateCode(int loc, IVirtualMachine vm, CheckerInformation info)
        {
            loc = Expr.GenerateCode(loc, vm, info);

            var type = GetExpressionType(info);

            if (type == Type.INT32)
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
                            "Cannot inverse " + type.ToString() + " value " + Expr.ToString());
            }

            return loc;
        }

        public override Type GetExpressionType(CheckerInformation info)
        {
            return Expr.GetExpressionType(info);
        }
    }
}
