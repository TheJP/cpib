using System.Collections.Generic;

using Compiler._02Parser.AST;

namespace Compiler
{
    public class ASTWhile : ASTCpsCmd
    {
        public IASTNode Condition { get; set; }

        public List<ASTCpsCmd> Commands { get; set; }

        public override string ToString()
        {
            return string.Format("while {0} do", Condition);
        }
    }
}