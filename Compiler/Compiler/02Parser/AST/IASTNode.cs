namespace Compiler._02Parser.AST
{
    public interface IASTNode
    {
        int GenerateCode(int loc, IVirtualMachine vm, CheckerInformation info);
    }
}