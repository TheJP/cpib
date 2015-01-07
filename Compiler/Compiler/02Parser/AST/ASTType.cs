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
            //TODO
            /*
            if(Type != Type.DECIMAL && Type != Type.INT32){ throw new IVirtualMachine.InternalError("Use of invalid (not existing) casting type " + Type.ToString()); }
            Type exprType = Expr.GetExpressionType(info);
            if(exprType != Type.DECIMAL && exprType != Type.INT32){ throw new IVirtualMachine.InternalError("Cannot cast from type " + exprType.ToString()); }
            loc = Expr.GenerateCode(loc, vm, info);
            if (Type != exprType)
            {
                if (Type == Type.DECIMAL && exprType == Type.INT32)
                {
                    vm.IntToDecimal(loc++);
                }
                else if (Type == Type.INT32 && exprType == Type.DECIMAL)
                {
                    vm.DecimalToInt(loc++);
                }
                else
                {
                    throw new IVirtualMachine.InternalError("Invalid casting operation");
                }
            }
            */
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
