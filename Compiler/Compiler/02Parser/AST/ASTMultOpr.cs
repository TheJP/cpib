namespace Compiler
{
    public class ASTMultOpr : ASTExpression
    {
        public ASTMultOpr()
        {
            Factor = new ASTEmpty();
            RepFactor = new ASTEmpty();
        }
        public Operators Operator { get; set; }

        public IASTNode Factor { get; set; }

        public IASTNode RepFactor { get; set; }

        public void SetLeftChild(IASTNode child)
        {
            if (Factor is ASTEmpty)
            {
                Factor = child;
            }
            else
            {
                var mult = (ASTMultOpr)Factor;
                mult.SetLeftChild(child);
            }
        }

        public override string ToString()
        {
            return string.Format("({0} {1} {2})", Factor, Operator, RepFactor);
        }

        public override void GenerateCode(uint block, ref uint loc, MachineCode mc, CheckerInformation info)
        {
            //1. generate the code of both terms
            Factor.GenerateCode(block, ref loc, mc, info);
            RepFactor.GenerateCode(block, ref loc, mc, info);
            //2. get the results of the terms into registers
            mc[block, loc++] = new Command(Instructions.POP, (byte)MachineCode.Registers.D);
            mc[block, loc++] = new Command(Instructions.POP, (byte)MachineCode.Registers.C);

            //Check types
            var type = GetExpressionType(info);
            if (type != Type.INT32)
            {
                throw new CodeGenerationException("There's an invalid operand type in ASTMultOpr. type: " + type.ToString());
            }

            //3. calculate the result
            switch (Operator)
            {
                case Operators.TIMES:
                    mc[block, loc++] = new Command(Instructions.MUL_R, (byte)MachineCode.Registers.C, (byte)MachineCode.Registers.D);
                    break;
                case Operators.DIV:
                    mc[block, loc++] = new Command(Instructions.DIV_R, (byte)MachineCode.Registers.C, (byte)MachineCode.Registers.D);
                    break;
                case Operators.MOD:
                    mc[block, loc++] = new Command(Instructions.MOD_R, (byte)MachineCode.Registers.C, (byte)MachineCode.Registers.D);
                    break;
                default:
                    throw new CodeGenerationException("There's an invalid operator in ASTMultOpr. Operator: " + Operator.ToString());
            }
            //4. write the result to the stack
            mc[block, loc++] = new Command(Instructions.PUSH, (byte)MachineCode.Registers.C);
        }

        public override Type GetExpressionType(CheckerInformation info)
        {
            var termType = ((ASTExpression)Factor).GetExpressionType(info);
            var repTermType = ((ASTExpression)RepFactor).GetExpressionType(info);

            if (termType == Type.INT32 && repTermType == Type.INT32)
            {
                return Type.INT32;
            }

            if ((termType == Type.INT32 && repTermType == Type.DECIMAL)
                      || (termType == Type.DECIMAL && repTermType == Type.INT32)
                      || (termType == Type.DECIMAL && repTermType == Type.DECIMAL))
            {
                return Type.DECIMAL;
            }

            throw new GrammarException(string.Format("Types {0}, {1} are not a valid combination for AddOperation {2}", termType, repTermType, this.ToString()));
        }

        public override void GetUsedIdents(ScopeChecker.UsedIdents usedIdents)
        {
            Factor.GetUsedIdents(usedIdents);
            RepFactor.GetUsedIdents(usedIdents);
        }
    }
}