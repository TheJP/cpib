using Compiler._02Parser.AST;

namespace Compiler
{
    public class ASTOptInit : IASTNode
    {
        public string Ident { get; set; }

        public IASTNode NextInit { get; set; }

        public int GenerateCode(int loc, IVirtualMachine vm, CheckerInformation info)
        {
            throw new System.NotImplementedException();
        }
    }
}