using Compiler._02Parser.AST;

namespace Compiler
{
    public class ASTTerm2 : IASTNode
    {
        public IASTNode Term { get; set; }

        public IASTNode RepTerm { get; set; }

        public Terminals Type { get; set; }
    }
}