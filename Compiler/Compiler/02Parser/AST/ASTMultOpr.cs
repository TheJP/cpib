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
            //TODO
            /*
            loc = Factor.GenerateCode(loc, vm, info);
            loc = RepFactor.GenerateCode(loc, vm, info);

            var type = GetExpressionType(info);

            if (type == Type.INT32)
            {

                switch (Operator)
                {
                    case Operators.TIMES:
                        vm.IntMult(loc++);
                        break;
                    case Operators.DIV:
                        vm.IntDiv(loc++);
                        break;
                    case Operators.MOD:
                        vm.IntMod(loc++);
                        break;
                    default:
                        throw new IVirtualMachine.InternalError(
                            "There's an invalid operator in ASTMultOpr. Operator: " + Operator.ToString());
                }
            }else if (type == Type.DECIMAL)
            {
                switch (Operator)
                {
                    case Operators.TIMES:
                        vm.DecimalMult(loc++);
                        break;
                    case Operators.DIV:
                        vm.DecimalDiv(loc++);
                        break;
                    case Operators.MOD:
                        vm.DecimalMod(loc++);
                        break;
                    default:
                        throw new IVirtualMachine.InternalError(
                            "There's an invalid operator in ASTMultOpr. Operator: " + Operator.ToString());
                }
            }
            else
            {
                throw new IVirtualMachine.InternalError(
                            "There's an invalid operand in ASTMultOpr. operand: " + type.ToString());
            }
            */
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