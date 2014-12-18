using Compiler._02Parser.AST;

namespace Compiler
{
    public class ASTCpsDecl : IASTNode
    {
        public IASTNode NextDecl { get; set; }
    }
}