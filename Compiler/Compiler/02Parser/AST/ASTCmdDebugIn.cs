namespace Compiler
{
    public class ASTCmdDebugIn : ASTCpsCmd
    {
        public ASTExpression Expr { get; set; }

        public override int GenerateCode(int loc, IVirtualMachine vm, CheckerInformation info)
        {
            loc = Expr.GenerateLValue(loc, vm, info);
            switch (Expr.GetExpressionType(info))
            {
                case Type.INT32:
                    vm.IntInput(loc++, "DEBUGIN");
                    break;
                case Type.BOOL:
                    vm.BoolInput(loc++, "DEBUGIN");
                    break;
                case Type.DECIMAL:
                    vm.DecimalInput(loc++, "DEBUGIN");
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