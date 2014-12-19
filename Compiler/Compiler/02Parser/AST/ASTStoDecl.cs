using Compiler._02Parser.AST;

namespace Compiler
{
    public class ASTStoDecl : ASTCpsDecl
    {
        public string Ident { get; set; }

        public Type Type { get; set; }

        public ChangeMode Changemode { get; set; }
    }
}