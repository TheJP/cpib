namespace Compiler._02Parser.AST
{
    public class ASTEmpty : IASTNode
    {
        public int GenerateCode(int loc, IVirtualMachine vm, CheckerInformation info) { }
    }
}