namespace Compiler
{
    public class ASTCmdSkip : ASTCpsCmd
    {
        public override int GenerateCode(int loc, IVirtualMachine vm, CheckerInformation info)
        {
            return loc;
        }
    }
}