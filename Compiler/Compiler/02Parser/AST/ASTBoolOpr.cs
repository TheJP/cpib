using Compiler._02Parser.AST;

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
    }
}