using Compiler._02Parser.AST;

namespace Compiler
{
    public class ASTCpsCmd : IASTNode
    {
        public ASTCpsCmd()
        {
            NextCmd = new ASTEmpty();
        }
        public IASTNode NextCmd { get; set; }
    }
}