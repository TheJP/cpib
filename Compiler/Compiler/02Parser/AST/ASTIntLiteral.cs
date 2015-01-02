using Compiler._02Parser.AST;

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

        public override int GenerateCode(int loc, IVirtualMachine vm, CheckerInformation info)
        {
            vm.IntLoad(loc++, Value);
            return loc;
        }

        public override Type GetExpressionType(CheckerInformation info)
        {
            return Type.INT32;
        }
    }
}