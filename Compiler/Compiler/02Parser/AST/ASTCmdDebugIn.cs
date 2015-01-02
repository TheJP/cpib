namespace Compiler
{
    public class ASTCmdDebugIn : ASTCpsCmd
    {
        public IASTNode Expr { get; set; }

        public override int GenerateCode(int loc, IVirtualMachine vm, CheckerInformation info)
        {
            throw new System.NotImplementedException();
        }
    }
}