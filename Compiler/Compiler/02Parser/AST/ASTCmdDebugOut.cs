using Compiler._02Parser.AST;

namespace Compiler
{
    public class ASTCmdDebugOut : ASTCpsCmd
    {
        public IASTNode Expr { get; set; }

        public override int GenerateCode(int loc, IVirtualMachine vm, CheckerInformation info)
        {
            loc = Expr.GenerateCode(loc, vm, info);
            //TODO: switch between Types! Not only int
            vm.IntOutput(loc++, "DEBUGOUT");
            return loc;
        }
    }
}