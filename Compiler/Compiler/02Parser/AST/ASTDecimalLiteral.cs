using System;

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

        public override void GenerateCode(uint block, ref uint loc, MachineCode mc, CheckerInformation info)
        {
            throw new NotSupportedException("Decimals are not supported in the logisim cpu");
        }

        public override Type GetExpressionType(CheckerInformation info)
        {
            return Type.DECIMAL;
        }

        public override void GetUsedIdents(ScopeChecker.UsedIdents usedIdents) { }
    }
}