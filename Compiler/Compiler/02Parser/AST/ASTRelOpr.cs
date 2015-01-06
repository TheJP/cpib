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

        public override int GenerateCode(int loc, IVirtualMachine vm, CheckerInformation info)
        {
            loc = Term.GenerateCode(loc, vm, info);
            loc = RepTerm.GenerateCode(loc, vm, info);

            var termType = ((ASTExpression)Term).GetExpressionType(info);
            var repTermType = ((ASTExpression)RepTerm).GetExpressionType(info);

            if (termType == Type.INT32 && repTermType == Type.INT32)
            {
                switch (Operator)
                {
                    case Operators.EQ:
                        vm.IntEQ(loc++);
                        break;
                    case Operators.NE:
                        vm.IntNE(loc++);
                        break;
                    case Operators.LT:
                        vm.IntLT(loc++);
                        break;
                    case Operators.LE:
                        vm.IntLE(loc++);
                        break;
                    case Operators.GT:
                        vm.IntGT(loc++);
                        break;
                    case Operators.GE:
                        vm.IntGE(loc++);
                        break;
                    default:
                        throw new IVirtualMachine.InternalError("There's an invalid operator in ASTRelOpr. Operator: " + Operator.ToString());
                }
            }
            else if ((termType == Type.INT32 && repTermType == Type.DECIMAL)
                      || (termType == Type.DECIMAL && repTermType == Type.INT32)
                      || (termType == Type.DECIMAL && repTermType == Type.DECIMAL))
            {
                switch (Operator)
                {
                    case Operators.EQ:
                        vm.DecimalEQ(loc++);
                        break;
                    case Operators.NE:
                        vm.DecimalNE(loc++);
                        break;
                    case Operators.LT:
                        vm.DecimalLT(loc++);
                        break;
                    case Operators.LE:
                        vm.DecimalLE(loc++);
                        break;
                    case Operators.GT:
                        vm.DecimalGT(loc++);
                        break;
                    case Operators.GE:
                        vm.DecimalGE(loc++);
                        break;
                    default:
                        throw new IVirtualMachine.InternalError("There's an invalid operator in ASTRelOpr. Operator: " + Operator.ToString());
                }
            }
            else
            {
                throw new IVirtualMachine.InternalError("There's an invalid operand in ASTRelOpr. Operand: " + termType.ToString() + ", " + repTermType.ToString());
            }


            return loc;
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