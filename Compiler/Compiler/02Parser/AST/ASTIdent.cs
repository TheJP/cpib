using Compiler._02Parser.AST;

namespace Compiler
{
    public class ASTIdent : IASTNode
    {
        public string Ident { get; set; }

        public IASTNode OptInitOrExprList { get; set; }

        public bool IsInit { get; set; }
    }
}