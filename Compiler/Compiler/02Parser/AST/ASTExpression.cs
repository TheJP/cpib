namespace Compiler._02Parser.AST
{
    public class ASTExpression : IASTNode
    {
        public ASTExpression()
        {
            NextExpression = new ASTEmpty();
        }

        public IASTNode NextExpression { get; set; }
    }
}