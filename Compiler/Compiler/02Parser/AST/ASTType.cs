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

        public override int GenerateCode(int loc, IVirtualMachine vm, CheckerInformation info)
        {
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
            return loc;
        }

        public override Type GetExpressionType(CheckerInformation info)
        {
            return this.Type;
        }
    }
}