using System;

namespace Compiler
{
    public class ASTBoolLiteral : ASTExpression
    {
        public ASTBoolLiteral(bool value)
        {
            this.Value = value;
        }

        public bool Value { get; set; }

        public override string ToString()
        {
            return Value.ToString();
        }

        public override void GenerateCode(uint block, ref uint loc, MachineCode mc, CheckerInformation info)
        {
            mc[block, loc++] = new Command(Instructions.MOV_R_C, (byte)MachineCode.Registers.C, (byte)(Value ? 1 : 0));
            mc[block, loc++] = new Command(Instructions.PUSH, (byte)MachineCode.Registers.C);
        }

        public override Type GetExpressionType(CheckerInformation info)
        {
            return Type.BOOL;
        }

        public override void GetUsedIdents(ScopeChecker.UsedIdents usedIdents) { }
    }
}