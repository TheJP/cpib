using System.Collections;
using System.Collections.Generic;

using Compiler._02Parser.AST;

namespace Compiler
{
    public class ASTProcFuncDecl : ASTCpsDecl
    {
        public List<ASTCpsDecl> Declarations { get; set; }

        public List<ASTGlobalParam> OptGlobImps { get; set; }

        public IList<ASTParam> Params { get; set; }

        public List<ASTCpsCmd> Commands { get; set; }

        public bool IsFunc { get; set; }

        public override string ToString()
        {
            return string.Format("{0} {1}", IsFunc ? "func" : "proc", Ident);
        }
    }
}