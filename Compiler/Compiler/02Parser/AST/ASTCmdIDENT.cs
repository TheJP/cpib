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
            RValue.GenerateCode(block, ref loc, mc, info);
            LValue.GenerateLValue(block, ref loc, mc, info, false, true); //Writes address to register A
            mc[block, loc++] = new Command(Instructions.POP, (byte)MachineCode.Registers.C);
            mc[block, loc++] = new Command(Instructions.MOV_VM_R, (byte)MachineCode.Registers.A, (byte)MachineCode.Registers.C);
        }

        public override void GetUsedIdents(ScopeChecker.UsedIdents usedIdents)
        {
            LValue.GetUsedIdents(usedIdents);
            RValue.GetUsedIdents(usedIdents);
        }
    }
}