using Compiler._02Parser.AST;

namespace Compiler
{
    public class ASTMultOpr : IASTNode
    {
        public Operators Operator { get; set; }

        public IASTNode Factor { get; set; }

        public IASTNode RepFactor { get; set; }

        public override string ToString()
        {
            return string.Format("({0} {1} {2})", Factor, Operator, RepFactor);
        }
    }
}