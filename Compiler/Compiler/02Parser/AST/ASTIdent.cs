using Compiler._02Parser.AST;

namespace Compiler
{
    public class ASTIdent : ASTExpression
    {
        public string Ident { get; set; }

        public IASTNode OptInitOrExprList { get; set; }

        public bool IsInit { get; set; }

        public override string ToString()
        {
            return string.Format("{0}{1}", IsInit? "init ":"", Ident);
        }

        public override int GenerateCode(int loc, IVirtualMachine vm, CheckerInformation info)
        {
            throw new System.NotImplementedException();
        }
    }
}