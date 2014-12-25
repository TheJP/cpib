using System.Collections;
using System.Collections.Generic;

using Compiler._02Parser.AST;

namespace Compiler
{
    public class ASTProcFuncDecl:ASTCpsDecl
    {
        public string Ident { get; set; }

        public IASTNode OptCpsStoDecl { get; set; }

        public IASTNode OptGlobImps { get; set; }

        public IList<ASTParam> Params { get; set; }

        public IASTNode CpsCmd { get; set; }

        public bool IsFunc { get; set; }

        public override string ToString()
        {
            return string.Format("{0} {1}", IsFunc ? "func" : "proc", Ident);
        }
    }
}