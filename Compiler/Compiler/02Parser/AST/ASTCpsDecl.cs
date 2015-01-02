using Compiler._02Parser.AST;

namespace Compiler
{
    public abstract class ASTCpsDecl : IASTNode, IASTDecl
    {
        public string Ident { get; set; }

        public ASTCpsDecl()
        {
            NextDecl = new ASTEmpty();
        }

        public IASTNode NextDecl { get; set; }

        public abstract int GenerateCode(int loc, IVirtualMachine vm, CheckerInformation info);
    }
}