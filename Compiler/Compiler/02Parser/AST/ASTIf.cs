using System.Collections.Generic;

namespace Compiler
{
    public class ASTIf : ASTCpsCmd
    {
        public IASTNode Condition { get; set; }

        public List<ASTCpsCmd> TrueCommands { get; set; }

        public List<ASTCpsCmd> FalseCommands { get; set; }

        public override string ToString()
        {
            return string.Format(@"if {0} then", Condition);
        }

        public override int GenerateCode(int loc, IVirtualMachine vm, CheckerInformation info)
        {
            throw new System.NotImplementedException();
        }
    }
}