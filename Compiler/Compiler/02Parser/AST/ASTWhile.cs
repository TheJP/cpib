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

        public override void GenerateCode(uint block, ref uint loc, MachineCode mc, CheckerInformation info)
        {
            uint conditionLoc = loc;
            Condition.GenerateCode(block, ref loc, mc, info);
            mc[block, loc++] = new Command(Instructions.POP, (byte)MachineCode.Registers.C);
            mc[block, loc++] = new Command(Instructions.CMP_R_C, (byte)MachineCode.Registers.C, 0);
            uint condJumpLoc = loc++;
            foreach (ASTCpsCmd cmd in Commands)
            {
                cmd.GenerateCode(block, ref loc, mc, info);
            }
            mc[block, loc] = new Command(Instructions.JMP, (short)((conditionLoc - loc) * 4)); ++loc;
            mc[block, condJumpLoc] = new Command(Instructions.JZ, (short)((loc - condJumpLoc) * 4));
        }

        public override void GetUsedIdents(ScopeChecker.UsedIdents usedIdents)
        {
            bool tmp = usedIdents.AllowInit;
            usedIdents.AllowInit = false;
            Condition.GetUsedIdents(usedIdents);
            Commands.ForEach(cmd => cmd.GetUsedIdents(usedIdents));
            usedIdents.AllowInit = tmp;
        }
    }
}