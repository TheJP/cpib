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

        public override int GenerateCode(int loc, IVirtualMachine vm, CheckerInformation info)
        {
            loc = Term.GenerateCode(loc, vm, info);
            loc = RepTerm.GenerateCode(loc, vm, info);

            var type = GetExpressionType(info);

            if (type == Type.INT32)
            {
                switch (Operator)
                {
                    case Operators.PLUS:
                        vm.IntAdd(loc++);
                        break;
                    case Operators.MINUS:
                        vm.IntSub(loc++);
                        break;
                    default:
                        throw new IVirtualMachine.InternalError(
                            "There's an invalid operator in ASTAddOpr. Operator: " + Operator.ToString());
                }
            }else if (type == Type.DECIMAL)
            {
                switch (Operator)
                {
                    case Operators.PLUS:
                        vm.DecimalAdd(loc++);
                        break;
                    case Operators.MINUS:
                        vm.DecimalSub(loc++);
                        break;
                    default:
                        throw new IVirtualMachine.InternalError(
                            "There's an invalid operator in ASTAddOpr. Operator: " + Operator.ToString());
                }
            }
            else
            {
                throw new IVirtualMachine.InternalError(
                            "There's an invalid operand type in ASTAddOpr. type: " + type.ToString());
            }
            return loc;
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