namespace Compiler._02Parser.AST
{
    public abstract class ASTExpression : IASTNode
    {
        public ASTExpression()
        {
            NextExpression = new ASTEmpty();
        }

        public IASTNode NextExpression { get; set; }
        public abstract int GenerateCode(int loc, IVirtualMachine vm, CheckerInformation info);
    }
}