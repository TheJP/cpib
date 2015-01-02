namespace Compiler
{
    public class ASTGlobalParam : IASTNode
    {
        public ASTGlobalParam()
        {
            NextParam = new ASTEmpty();
        }

        public IASTNode NextParam { get; set; }

        public string Ident { get; set; }

        public ChangeMode? OptChangemode { get; set; }

        public FlowMode? FlowMode { get; set; }

        public override string ToString()
        {
            return string.Format("{0} {1} {2}", FlowMode, OptChangemode, Ident);
        }

        public int GenerateCode(int loc, IVirtualMachine vm, CheckerInformation info)
        {
            throw new IVirtualMachine.InternalError("ASTGlobalParam.GenerateCode was called. This should never happen!");
        }
    }
}