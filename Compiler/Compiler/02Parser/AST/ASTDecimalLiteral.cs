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
            vm.DecimalLoad(loc++, Value);
            return loc;
        }

        public override Type GetExpressionType(CheckerInformation info)
        {
            return Type.DECIMAL;
        }

        public override void GetUsedIdents(ScopeChecker.UsedIdents usedIdents) { }
    }
}