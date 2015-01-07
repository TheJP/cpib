using System;

namespace Compiler
{
    public class ASTNot : ASTExpression
    {
        public ASTExpression Expr { get; set; }

        public override void GenerateCode(uint block, ref uint loc, MachineCode mc, CheckerInformation info)
        {
            //TODO
            /*
            loc = Expr.GenerateCode(loc, vm, info);

            var type = GetExpressionType(info);

            if (type == Type.BOOL)
            {
                //1 NE 1 == 0
                //0 NE 1 == 1
                vm.IntLoad(loc++, 1);
                vm.IntNE(loc++);
            }
            else
            {
                throw new IVirtualMachine.InternalError(
                            "Cannot negate Non-Bool value " + Expr.ToString());
            }
            */
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