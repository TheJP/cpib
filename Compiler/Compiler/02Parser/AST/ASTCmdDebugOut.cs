namespace Compiler
{
    public class ASTCmdDebugOut : ASTCpsCmd
    {
        public ASTExpression Expr { get; set; }

        public override void GenerateCode(uint block, ref uint loc, MachineCode mc, CheckerInformation info)
        {
            //TODO
            /*
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
            */
        }

        public override void GetUsedIdents(ScopeChecker.UsedIdents usedIdents)
        {
            Expr.GetUsedIdents(usedIdents);
        }
    }
}