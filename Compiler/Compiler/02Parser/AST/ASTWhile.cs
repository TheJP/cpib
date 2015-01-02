using System.Collections.Generic;

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

        public override int GenerateCode(int loc, IVirtualMachine vm, CheckerInformation info)
        {
            throw new System.NotImplementedException();
        }
    }
}