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

        public override void GenerateCode(uint block, ref uint loc, MachineCode mc, CheckerInformation info)
        {
            Condition.GenerateCode(block, ref loc, mc, info);
            mc[block, loc++] = new Command(Instructions.POP, (byte)MachineCode.Registers.C);
            mc[block, loc++] = new Command(Instructions.CMP_R_C, (byte)MachineCode.Registers.C, 0);
            uint condJumpLoc = loc++;
            foreach (ASTCpsCmd cmd in TrueCommands)
            {
                cmd.GenerateCode(block, ref loc, mc, info);
            }
            uint unccondJumpLoc = loc++;
            mc[block, condJumpLoc] = new Command(Instructions.JZ, (byte)((loc - condJumpLoc) * 4));
            foreach (ASTCpsCmd cmd in FalseCommands)
            {
                cmd.GenerateCode(block, ref loc, mc, info);
            }
            mc[block, unccondJumpLoc] = new Command(Instructions.JZ, (byte)((loc - unccondJumpLoc) * 4));
        }

        public override void GetUsedIdents(ScopeChecker.UsedIdents usedIdents)
        {
            Condition.GetUsedIdents(usedIdents);
            //Fork usedIdents
            var usedIdentsFork = usedIdents.ForkForIf();
            //Give the branches its own UsedIdents instance
            TrueCommands.ForEach(cmd => cmd.GetUsedIdents(usedIdents));
            FalseCommands.ForEach(cmd => cmd.GetUsedIdents(usedIdentsFork));
            //Merge the branches again
            usedIdents.MergeForIf(usedIdentsFork);
        }
    }
}