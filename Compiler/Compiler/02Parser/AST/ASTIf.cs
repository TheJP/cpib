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
            loc = Condition.GenerateCode(loc, vm, info);
            int condJumpLoc = loc++; //Placeholder
            foreach (ASTCpsCmd cmd in TrueCommands)
            {
                loc = cmd.GenerateCode(loc, vm, info);
            }
            int uncondJumpLoc = loc++; //Placeholder2
            vm.CondJump(condJumpLoc, loc); //Fill in Placeholder
            foreach (ASTCpsCmd cmd in FalseCommands)
            {
                loc = cmd.GenerateCode(loc, vm, info);
            }
            vm.UncondJump(uncondJumpLoc, loc); //Fill in Placeholder2
            return loc;
        }

        public override void GetUsedIdents(ScopeChecker.UsedIdents usedIdents)
        {
            Condition.GetUsedIdents(usedIdents);
            TrueCommands.ForEach(cmd => cmd.GetUsedIdents(usedIdents));
            FalseCommands.ForEach(cmd => cmd.GetUsedIdents(usedIdents));
        }
    }
}