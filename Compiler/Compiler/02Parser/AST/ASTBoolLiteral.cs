using Compiler._02Parser.AST;

namespace Compiler
{
    public class ASTBoolLiteral : ASTExpression
    {
        public ASTBoolLiteral(bool value)
        {
            this.Value = value;
        }

        public bool Value { get; set; }

        public override string ToString()
        {
            return Value.ToString();
        }

        public int GenerateCode(int loc, IVirtualMachine vm, CheckerInformation info)
        {
            vm.IntLoad(loc++, Value ? 1 : 0);
            return loc;
        }
    }
}