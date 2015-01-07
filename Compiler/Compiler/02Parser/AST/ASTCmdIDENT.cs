namespace Compiler
{
    public class ASTCmdIdent : ASTCpsCmd
    {
        public ASTExpression LValue { get; set; }

        public ASTExpression RValue { get; set; }

        public override string ToString()
        {
            return string.Format("{0} := {1}", LValue, RValue);
        }

        public override void GenerateCode(uint block, ref uint loc, MachineCode mc, CheckerInformation info)
        {
            //TODO
            /*
            loc = RValue.GenerateCode(loc, vm, info);
            loc = LValue.GenerateLValue(loc, vm, info);
            vm.Store(loc++);
            */
        }

        public override void GetUsedIdents(ScopeChecker.UsedIdents usedIdents)
        {
            LValue.GetUsedIdents(usedIdents);
            RValue.GetUsedIdents(usedIdents);
        }
    }
}