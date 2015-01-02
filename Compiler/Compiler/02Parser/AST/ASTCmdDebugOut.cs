namespace Compiler
{
    public class ASTCmdDebugOut : ASTCpsCmd
    {
        public IASTNode Expr { get; set; }

        public override int GenerateCode(int loc, IVirtualMachine vm, CheckerInformation info)
        {
            loc = Expr.GenerateCode(loc, vm, info);
            var type = ((ASTExpression)Expr).GetExpressionType(info);
            if (type == Type.INT32)
            {
                vm.IntOutput(loc++, "DEBUGOUT");
            }else if (type == Type.DECIMAL)
            {
                vm.DecimalOutput(loc++, "DEBUGOUT");
            }else if (type == Type.BOOL)
            {
                vm.BoolOutput(loc++, "DEBUGOUT");
            }
            return loc;
        }
    }
}