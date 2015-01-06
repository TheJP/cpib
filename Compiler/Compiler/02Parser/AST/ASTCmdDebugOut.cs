namespace Compiler
{
    public class ASTCmdDebugOut : ASTCpsCmd
    {
        public ASTExpression Expr { get; set; }

        public override int GenerateCode(int loc, IVirtualMachine vm, CheckerInformation info)
        {
            loc = Expr.GenerateCode(loc, vm, info);
            switch (Expr.GetExpressionType(info))
            {
                case Type.INT32:
                    vm.IntOutput(loc++, "DEBUGOUT");
                    break;
                case Type.BOOL:
                    vm.BoolOutput(loc++, "DEBUGOUT");
                    break;
                case Type.DECIMAL:
                    vm.DecimalOutput(loc++, "DEBUGOUT");
                    break;
            }
            return loc;
        }

        public override void GetUsedIdents(ScopeChecker.UsedIdents usedIdents)
        {
            Expr.GetUsedIdents(usedIdents);
        }
    }
}