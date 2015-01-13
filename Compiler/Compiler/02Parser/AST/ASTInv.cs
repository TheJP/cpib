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

        public override void GenerateCode(uint block, ref uint loc, MachineCode mc, CheckerInformation info)
        {
            //TODO
            /*
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
            */
            throw new NotImplementedException("Not implemented ASTInv");
        }

        public override Type GetExpressionType(CheckerInformation info)
        {
            return Expr.GetExpressionType(info);
        }

        public override void GetUsedIdents(ScopeChecker.UsedIdents usedIdents)
        {
            Expr.GetUsedIdents(usedIdents);
        }
    }
}
