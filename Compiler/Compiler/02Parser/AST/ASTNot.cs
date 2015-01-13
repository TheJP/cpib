using System;

namespace Compiler
{
    public class ASTNot : ASTExpression
    {
        public ASTExpression Expr { get; set; }

        public override void GenerateCode(uint block, ref uint loc, MachineCode mc, CheckerInformation info)
        {
            Expr.GenerateCode(block, ref loc, mc, info);

            var type = GetExpressionType(info);

            if (type == Type.BOOL)
            {
                mc[block, loc++] = new Command(Instructions.POP, (byte)MachineCode.Registers.C);
                mc[block, loc++] = new Command(Instructions.XOR_C, (byte)MachineCode.Registers.C, 1); //XOR with 0x01 negates the least significant bit
                mc[block, loc++] = new Command(Instructions.PUSH, (byte)MachineCode.Registers.C);
            }
            else
            {
                throw new CheckerException(
                            "Cannot negate Non-Bool value " + Expr.ToString());
            }
        }

        public override Type GetExpressionType(CheckerInformation info)
        {
            return Expr.GetExpressionType(info);
        }

        public override void GetUsedIdents(ScopeChecker.UsedIdents usedIdents)
        {
            Expr.GetUsedIdents(usedIdents);
        }
    }  
}