using Compiler._02Parser.AST;

namespace Compiler
{
    public class ASTGlobalParam : IASTNode
    {
        public ASTGlobalParam()
        {
            NextParam = new ASTEmpty();
        }

        public IASTNode NextParam { get; set; }

        public string Ident { get; set; }

        public ChangeMode? OptChangemode { get; set; }

        public FlowMode? FlowMode { get; set; }
    }
}