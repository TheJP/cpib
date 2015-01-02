namespace Compiler
{
    public class ASTType : IASTNode
    {
        public Type Type { get; set; }

        public IASTNode Expr { get; set; }

        public override string ToString()
        {
            return string.Format("{0}({1})", Type, Expr);
        }

        public int GenerateCode(int loc, IVirtualMachine vm, CheckerInformation info)
        {
            throw new System.NotImplementedException();
        }
    }
}