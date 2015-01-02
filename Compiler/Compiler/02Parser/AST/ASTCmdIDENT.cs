namespace Compiler
{
    public class ASTCmdIdent : ASTCpsCmd
    {
        public IASTNode LValue { get; set; }

        public IASTNode RValue { get; set; }

        public override string ToString()
        {
            return string.Format("{0} := {1}", LValue, RValue);
        }

        public override int GenerateCode(int loc, IVirtualMachine vm, CheckerInformation info)
        {
            throw new System.NotImplementedException();
        }
    }
}