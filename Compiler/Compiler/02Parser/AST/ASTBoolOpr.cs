using Compiler._02Parser.AST;

namespace Compiler
{
    public class ASTBoolOpr : IASTNode
    {
        public IASTNode Term { get; set; }
        public IASTNode RepTerm { get; set; }

        public Operators Operator { get; set; } 
    }
}