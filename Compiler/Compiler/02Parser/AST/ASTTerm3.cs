using Compiler._02Parser.AST;

namespace Compiler
{
    public class ASTTerm3 : IASTNode
    {
        public IASTNode Factor { get; set; }

        public IASTNode RepFactor { get; set; }

        public Terminals Type { get; set; }
    }
}