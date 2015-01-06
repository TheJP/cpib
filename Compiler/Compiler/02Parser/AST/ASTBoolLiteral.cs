using System;

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

        public override int GenerateCode(int loc, IVirtualMachine vm, CheckerInformation info)
        {
            vm.IntLoad(loc++, Value ? 1 : 0);
            return loc;
        }

        public override Type GetExpressionType(CheckerInformation info)
        {
            return Type.BOOL;
        }

        public override void GetUsedIdents(ScopeChecker.UsedIdents usedIdents) { }
    }
}