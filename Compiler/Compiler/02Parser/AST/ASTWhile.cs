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
            int conditionLoc = loc;
            loc = Condition.GenerateCode(loc, vm, info);
            int condJumpLoc = loc++; //Placeholder for conditonal jump out of while loop
            foreach (ASTCpsCmd cmd in Commands)
            {
                loc = cmd.GenerateCode(loc, vm, info);
            }
            vm.UncondJump(loc++, conditionLoc);
            //Fill in placeholder
            vm.CondJump(condJumpLoc, loc);
            return loc;
        }
    }
}