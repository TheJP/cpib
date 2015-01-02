using Compiler._02Parser.AST;

namespace Compiler
{
    public class ASTRelOpr : ASTExpression
    {
        public ASTRelOpr()
        {
            Term = new ASTEmpty();
            RepTerm = new ASTEmpty();
        }
        public Operators Operator { get; set; }

        public IASTNode Term { get; set; }

        public IASTNode RepTerm { get; set; }

        public override string ToString()
        {
            return string.Format("({0} {1} {2})", Term, Operator, RepTerm);
        }

        public override int GenerateCode(int loc, IVirtualMachine vm, CheckerInformation info)
        {
            loc = Term.GenerateCode(loc, vm, info);
            loc = RepTerm.GenerateCode(loc, vm, info);
            switch (Operator)
            {
                case Operators.AND:
                    break;
            }
            return loc;
        }
    }
}