namespace Compiler
{
    public class ASTCmdDebugIn : ASTCpsCmd
    {
        public ASTExpression Expr { get; set; }

        public override void GenerateCode(uint block, ref uint loc, MachineCode mc, CheckerInformation info)
        {
            //TODO
            /*
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
            */
        }

        public override void GetUsedIdents(ScopeChecker.UsedIdents usedIdents)
        {
            Expr.GetUsedIdents(usedIdents);
        }
    }
}