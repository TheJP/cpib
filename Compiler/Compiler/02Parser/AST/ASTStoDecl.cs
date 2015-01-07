namespace Compiler
{
    public class ASTStoDecl : ASTCpsDecl, IASTStoDecl
    {
        public Type Type { get; set; }

        public ChangeMode? Changemode { get; set; }

        public override string ToString()
        {
            return string.Format("{0} {1} : {2}", Changemode, Ident, Type);
        }

        public override int GenerateCode(int loc, MachineCode mc, CheckerInformation info)
        {
            throw new CheckerException("ASTStoDecl.GenerateCode was called. This should never happen!");
        }
        public override void GetUsedIdents(ScopeChecker.UsedIdents usedIdents) { }
    }
}