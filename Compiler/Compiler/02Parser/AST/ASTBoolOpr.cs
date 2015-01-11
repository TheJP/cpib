namespace Compiler
{
    public class ASTBoolOpr : ASTExpression
    {
        public IASTNode Term { get; set; }
        public IASTNode RepTerm { get; set; }

        public Operators Operator { get; set; }

        public void SetLeftChild(IASTNode node)
        {
            if (Term is ASTEmpty)
            {
                Term = node;
            }
            else if(Term is ASTBoolOpr)
            {
                ((ASTBoolOpr)Term).SetLeftChild(node);
            }
        }

        public override string ToString()
        {
            return string.Format("({0} {1} {2})", Term, Operator, RepTerm);
        }

        public override void GenerateCode(uint block, ref uint loc, MachineCode mc, CheckerInformation info)
        {
            //1. generate the code of first term
            Term.GenerateCode(block, ref loc, mc, info);
            //1. a) generate second term if && or || (not conditional)
            if (Operator != Operators.CAND && Operator != Operators.COR)
            {
                RepTerm.GenerateCode(block, ref loc, mc, info);
                //2. a) (see above)
                mc[block, loc++] = new Command(Instructions.POP, (byte)MachineCode.Registers.D);
            }
            //2. get the results of the terms into registers
            mc[block, loc++] = new Command(Instructions.POP, (byte)MachineCode.Registers.C);

            //Check types
            var type = GetExpressionType(info);
            if (type != Type.BOOL)
            {
                throw new CodeGenerationException("There's an invalid operand type in ASTBoolOpr. type: " + type.ToString());
            }

            //3. calculate the result
            switch (Operator)
            {
                case Operators.AND:
                    mc[block, loc++] = new Command(Instructions.AND_R, (byte)MachineCode.Registers.C, (byte)MachineCode.Registers.D);
                    break;
                case Operators.OR:
                    mc[block, loc++] = new Command(Instructions.OR_R, (byte)MachineCode.Registers.C, (byte)MachineCode.Registers.D);
                    break;
                case Operators.CAND:
                    mc[block, loc++] = new Command(Instructions.CMP_R_C, (byte)MachineCode.Registers.C, 0);
                    //Jump over second term if term1==0
                    uint jumpPlaceholder = loc++;
                    //Generate second term
                    RepTerm.GenerateCode(block, ref loc, mc, info);
                    //Jump over push 0
                    mc[block, loc++] = new Command(Instructions.JMP, 2 * 4);
                    //Fill in placeholder
                    mc[block, jumpPlaceholder] = new Command(Instructions.JZ, (byte)((loc - jumpPlaceholder)*4));
                    break;
                case Operators.COR:
                    mc[block, loc++] = new Command(Instructions.CMP_R_C, (byte)MachineCode.Registers.C, 0);
                    //Jump over second term if term1==1
                    uint jumpPlaceholder2 = loc++;
                    //Generate second term
                    RepTerm.GenerateCode(block, ref loc, mc, info);
                    //Jump over push 0
                    mc[block, loc++] = new Command(Instructions.JMP, 2 * 4);
                    //Fill in placeholder
                    mc[block, jumpPlaceholder2] = new Command(Instructions.JNZ, (byte)((loc - jumpPlaceholder2) * 4));
                    break;
                default:
                    throw new CodeGenerationException("There's an invalid operator in ASTBoolOpr. Operator: " + Operator.ToString());
            }
            //4. write the result to the stack
            mc[block, loc++] = new Command(Instructions.PUSH, (byte)MachineCode.Registers.C);
        }

        public override Type GetExpressionType(CheckerInformation info)
        {
            var termType = ((ASTExpression)Term).GetExpressionType(info);
            var repTermType = ((ASTExpression)RepTerm).GetExpressionType(info);

            if (termType == Type.BOOL && repTermType == Type.BOOL)
            {
                return Type.BOOL;
            }

            throw new GrammarException(string.Format("Types {0}, {1} are not a valid combination for Bool Operation {2}", termType, repTermType, this.ToString()));
        }

        public override void GetUsedIdents(ScopeChecker.UsedIdents usedIdents)
        {
            Term.GetUsedIdents(usedIdents);
            RepTerm.GetUsedIdents(usedIdents);
        }
    }
}