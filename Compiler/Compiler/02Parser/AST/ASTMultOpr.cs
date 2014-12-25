using Compiler._02Parser.AST;

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
    }
}