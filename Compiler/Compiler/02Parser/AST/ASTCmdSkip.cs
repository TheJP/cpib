namespace Compiler
{
    public class ASTCmdSkip : ASTCpsCmd
    {
        public override void GenerateCode(uint block, ref uint loc, MachineCode mc, CheckerInformation info) { }

        public override void GetUsedIdents(ScopeChecker.UsedIdents usedIdents) { }
    }
}