namespace Compiler
{
    public class ASTAddOpr : ASTExpression
    {
        public ASTAddOpr()
        {
            Term = new ASTEmpty();
            RepTerm = new ASTEmpty();
        }

        public Operators Operator { get; set; }

        public IASTNode Term { get; set; }

        public IASTNode RepTerm { get; set; }

        public void SetLeftChild(IASTNode child)
        {
            if (Term is ASTEmpty)
            {
                Term = child;
            }
            else
            {
                var mult = (ASTAddOpr)Term;
                mult.SetLeftChild(child);
            }
        }

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
                throw new CodeGenerationException("There's an invalid operand type in ASTAddOpr. type: " + type.ToString());
            }

            //3. calculate the result
            switch (Operator)
            {
                case Operators.PLUS:
                    mc[block, loc++] = new Command(Instructions.ADD_R, (byte)MachineCode.Registers.C, (byte)MachineCode.Registers.D);
                    break;
                case Operators.MINUS:
                    mc[block, loc++] = new Command(Instructions.SUB_R, (byte)MachineCode.Registers.C, (byte)MachineCode.Registers.D);
                    break;
                default:
                    throw new CodeGenerationException("There's an invalid operator in ASTAddOpr. Operator: " + Operator.ToString());
            }
            //4. write the result to the stack
            mc[block, loc++] = new Command(Instructions.PUSH, (byte)MachineCode.Registers.C);
        }

        public override Type GetExpressionType(CheckerInformation info)
        {
            var termType = ((ASTExpression)Term).GetExpressionType(info);
            var repTermType = ((ASTExpression)RepTerm).GetExpressionType(info);

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
            Term.GetUsedIdents(usedIdents);
            RepTerm.GetUsedIdents(usedIdents);
        }
    }
}