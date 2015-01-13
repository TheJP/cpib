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

        public override int GenerateCode(int loc, IVirtualMachine vm, CheckerInformation info)
        {
            if (LValue.GetExpressionType(info) != RValue.GetExpressionType(info))
            {
                throw new CheckerException("An assignment has to have the same types for LValue and RValue");
            }
            loc = RValue.GenerateCode(loc, vm, info);
            loc = LValue.GenerateLValue(loc, vm, info);
            vm.Store(loc++);
            return loc;
        }

        public override void GetUsedIdents(ScopeChecker.UsedIdents usedIdents)
        {
            LValue.GetUsedIdents(usedIdents);
            RValue.GetUsedIdents(usedIdents);
        }
    }
}