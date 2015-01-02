namespace Compiler
{
    public interface IASTNode
    {
        int GenerateCode(int loc, IVirtualMachine vm, CheckerInformation info);
    }
}