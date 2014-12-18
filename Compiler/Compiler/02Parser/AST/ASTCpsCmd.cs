using Compiler._02Parser.AST;

namespace Compiler
{
    public class ASTCpsCmd : IASTNode
    {
        public IASTNode NextCmd { get; set; }
    }
}