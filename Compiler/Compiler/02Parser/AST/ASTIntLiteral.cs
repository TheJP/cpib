namespace Compiler
{
    public class ASTIntLiteral : ASTExpression
    {
        public ASTIntLiteral(int value)
        {
            this.Value = value;
        }

        public int Value { get; set; }

        public override string ToString()
        {
            return Value.ToString();
        }

        public override void GenerateCode(uint block, ref uint loc, MachineCode mc, CheckerInformation info)
        {
            //TODO
            //vm.IntLoad(loc++, Value);
        }

        public override Type GetExpressionType(CheckerInformation info)
        {
            return Type.INT32;
        }

        public override void GetUsedIdents(ScopeChecker.UsedIdents usedIdents) { }
    }
}