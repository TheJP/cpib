using Compiler._02Parser.AST;

namespace Compiler
{
    public class ASTDecimalLiteral : ASTExpression
    {
        public ASTDecimalLiteral(decimal value)
        {
            this.Value = value;
        }

        public decimal Value { get; set; }

        public override string ToString()
        {
            return string.Format("{0}m", this.Value);
        }

        public override int GenerateCode(int loc, IVirtualMachine vm, CheckerInformation info)
        {
            throw new System.NotImplementedException();
        }
    }
}