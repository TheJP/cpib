using Compiler._02Parser.AST;

namespace Compiler
{
    public class ASTType : ASTExpression
    {
        public Type Type { get; set; }

        public IASTNode Expr { get; set; }

        public override string ToString()
        {
            return string.Format("{0}({1})", Type, Expr);
        }

        public override int GenerateCode(int loc, IVirtualMachine vm, CheckerInformation info)
        {
            throw new System.NotImplementedException();
        }

        public override Type GetExpressionType(CheckerInformation info)
        {
            return this.Type;
        }
    }
}