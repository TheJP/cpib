using Compiler._02Parser.AST;

namespace Compiler
{
    public class ASTProgParam : IASTNode
    {
        public IASTNode NextParam;

        public Type Type { get; set; }

        public string Ident { get; set; }

        public FlowMode FlowMode { get; set; }

        public ChangeMode OptChangemode { get; set; }
    }
}