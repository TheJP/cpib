namespace Compiler
{
    public class ASTRelOpr : ASTExpression
    {
        public ASTRelOpr()
        {
            Term = new ASTEmpty();
            RepTerm = new ASTEmpty();
        }
        public Operators Operator { get; set; }

        public IASTNode Term { get; set; }

        public IASTNode RepTerm { get; set; }

        public override string ToString()
        {
            return string.Format("({0} {1} {2})", Term, Operator, RepTerm);
        }

        public override void GenerateCode(uint block, ref uint loc, MachineCode mc, CheckerInformation info)
        {
            //1. generate the code of both terms
            Term.GenerateCode(block, ref loc, mc, info);
            RepTerm.GenerateCode(block, ref loc, mc, info);
            //2. get the results of the terms into registers
            mc[block, loc++] = new Command(Instructions.POP, (byte)MachineCode.Registers.D);
            mc[block, loc++] = new Command(Instructions.POP, (byte)MachineCode.Registers.C);

            //Check types
            var type = GetExpressionType(info);
            if (type != Type.INT32)
            {
                throw new CodeGenerationException("There's an invalid operand type in ASTRelOpr. type: " + type.ToString());
            }

            //3. calculate the result
            if (Operator != Operators.LE && Operator != Operators.GT)
            {
                mc[block, loc++] = new Command(Instructions.CMP_R_R, (byte)MachineCode.Registers.C, (byte)MachineCode.Registers.D);
            }
            else
            {
                mc[block, loc++] = new Command(Instructions.INC, (byte)MachineCode.Registers.D);
                mc[block, loc++] = new Command(Instructions.CMP_R_R, (byte)MachineCode.Registers.C, (byte)MachineCode.Registers.D);
            }
            switch (Operator)
            {
                case Operators.EQ:
                    mc[block, loc++] = new Command(Instructions.JZ, 3 * 4);
                    break;
                case Operators.NE:
                    mc[block, loc++] = new Command(Instructions.JNZ, 3 * 4);
                    break;
                case Operators.LT:
                case Operators.LE: //The difference is handled above
                    mc[block, loc++] = new Command(Instructions.JS, 3 * 4);
                    break;
                case Operators.GT: //The difference is handled above
                case Operators.GE:
                    mc[block, loc++] = new Command(Instructions.JNS, 3 * 4);
                    break;
                default:
                    throw new CodeGenerationException("There's an invalid operator in ASTRelOpr. Operator: " + Operator.ToString());
            }
            //4. write the result to the stack
            mc[block, loc++] = new Command(Instructions.PUSH, (byte)MachineCode.Registers.B); //Write 0
            mc[block, loc++] = new Command(Instructions.JMP, 3 * 4); //Jump over write 0
            mc[block, loc++] = new Command(Instructions.MOV_R_C, (byte)MachineCode.Registers.C, 1); //Write 1
            mc[block, loc++] = new Command(Instructions.PUSH, (byte)MachineCode.Registers.C);
        }

        public override Type GetExpressionType(CheckerInformation info)
        {
            return Type.BOOL;
        }

        public override void GetUsedIdents(ScopeChecker.UsedIdents usedIdents)
        {
            Term.GetUsedIdents(usedIdents);
            RepTerm.GetUsedIdents(usedIdents);
        }
    }
}