using Compiler._02Parser.AST;

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
            switch (Operator)
            {
                case Operators.PLUS:
                    vm.IntAdd(loc++);
                    break;
                case Operators.MINUS:
                    vm.IntSub(loc++);
                    break;
                default:
                    throw new IVirtualMachine.InternalError("There's an invalid operator in ASTAddOpr. Operator: " + Operator.ToString());
            }
            return loc;
        }
    }
}