using Compiler._02Parser.AST;

namespace Compiler
{
    public class ASTProcFuncDecl:ASTCpsDecl
    {
        public string Ident { get; set; }

        public IASTNode OptCpsStoDecl { get; set; }

        public IASTNode OptGlobImps { get; set; }

        public IASTNode ParamList { get; set; }

        public IASTNode CpsCmd { get; set; }

        public bool IsFunc { get; set; }
    }
}