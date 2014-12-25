using Compiler._02Parser.AST;

namespace Compiler
{
    public class ASTCmdCall : ASTCpsCmd
    {
        public string Ident { get; set; }

        public IASTNode ExprList { get; set; }

        public IASTNode OptGlobInits { get; set; }

        public override string ToString()
        {
            return string.Format("call {0}", this.Ident);
        }
    }
}