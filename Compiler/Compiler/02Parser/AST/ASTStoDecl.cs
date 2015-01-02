namespace Compiler
{
    public class ASTStoDecl : ASTCpsDecl, IASTStoDecl
    {
        public Type Type { get; set; }

        public ChangeMode Changemode { get; set; }

        public override string ToString()
        {
            return string.Format("{0} {1} : {2}", Changemode, Ident, Type);
        }

        public override int GenerateCode(int loc, IVirtualMachine vm, CheckerInformation info)
        {
            throw new IVirtualMachine.InternalError("ASTStoDecl.GenerateCode was called. This should never happen!");
        }
    }
}