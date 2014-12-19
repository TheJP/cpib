using Compiler._02Parser.AST;

namespace Compiler
{
    public class ASTParam : IASTNode
    {
        public ASTParam()
        {
            NextParam = new ASTEmpty();
        }

        public IASTNode NextParam { get; set; }

        public Type Type { get; set; }

        public string Ident { get; set; }

        public FlowMode FlowMode { get; set; }

        public ChangeMode OptChangemode { get; set; }
    }
}