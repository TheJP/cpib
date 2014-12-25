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
    }
}