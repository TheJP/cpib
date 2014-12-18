using Compiler._02Parser.AST;

namespace Compiler
{
    public class ASTWhile : ASTCpsCmd
    {
        public IASTNode Condition { get; set; }

        public IASTNode Command { get; set; }
    }
}