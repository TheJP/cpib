using Compiler._02Parser.AST;

namespace Compiler
{
    public class ASTBoolOpr : ASTExpression
    {
        public IASTNode Term { get; set; }
        public IASTNode RepTerm { get; set; }

        public Operators Operator { get; set; }

        public void SetLeftChild(IASTNode node)
        {
            if (Term is ASTEmpty)
            {
                Term = node;
            }
            else if(Term is ASTBoolOpr)
            {
                ((ASTBoolOpr)Term).SetLeftChild(node);
            }
        }

        public override string ToString()
        {
            return string.Format("({0} {1} {2})", Term, Operator, RepTerm);
        }

        public override int GenerateCode(int loc, IVirtualMachine vm, CheckerInformation info)
        {
            loc = Term.GenerateCode(loc, vm, info);
            //Only execute RepTerm if no conditional BoolOpr or if the left hand side is not enough to determine result
            if (Operator != Operators.COR && Operator != Operators.CAND)
            {
                loc = RepTerm.GenerateCode(loc, vm, info);
            }

            var type = GetExpressionType(info);

            if (type == Type.BOOL)
            {
                switch (Operator)
                {
                    case Operators.AND:
                        //Can only work with int operations:
                        //If both terms are true (=1), they result in 2 (1+1=2)
                        vm.IntAdd(loc++);
                        //Test if result == 2
                        vm.IntLoad(loc++, 2);
                        vm.IntEQ(loc++);
                        break;
                    case Operators.OR:
                        vm.IntAdd(loc++);
                        vm.IntLoad(loc++, 1);
                        vm.IntGE(loc++); //>=1 => only one term has to be true
                        break;
                    case Operators.CAND:
                        //If true is on the top of the stack, the second condition has to be evaluated
                        //1. Invert top of stack:
                        vm.IntLoad(loc++, 1);
                        vm.IntNE(loc++);
                        //2. Jump if left hand side was false
                        int conditionalJump = loc++;
                        //3. a. Evaluate right hand side if left hand side was true
                        loc = RepTerm.GenerateCode(loc, vm, info);
                        vm.CondJump(conditionalJump, loc + 1); //Step 2: Only now the jump destination is known!
                        vm.UncondJump(loc, loc + 2);
                        ++loc; //Leap over the load(0) instruction
                        //3. b. Evaluate to false
                        vm.IntLoad(loc++, 0);
                        break;
                    case Operators.COR:
                        //If false is on the top of the stack, the second condition has to be evaluated
                        //1. Jump if left hand side was true
                        int conditionalJump2 = loc++;
                        //2. a. Evaluate right hand side if left hand side was false
                        loc = RepTerm.GenerateCode(loc, vm, info);
                        vm.CondJump(conditionalJump2, loc + 1);
                        vm.UncondJump(loc, loc + 2);
                        ++loc;
                        //2. b. Evaluate to true (without evaluating right hand side)
                        vm.IntLoad(loc++, 1);
                        break;
                    default:
                        throw new IVirtualMachine.InternalError(
                            "There's an invalid operator in ASTBoolOpr. Operator: " + Operator.ToString());
                }
            }
            else
            {
                throw new IVirtualMachine.InternalError(
                            "There's an invalid operand in ASTBoolOpr. Oprand: " + type.ToString());
            }
            return loc;
        }

        public override Type GetExpressionType(CheckerInformation info)
        {
            var termType = ((ASTExpression)Term).GetExpressionType(info);
            var repTermType = ((ASTExpression)RepTerm).GetExpressionType(info);

            if (termType == Type.BOOL && repTermType == Type.BOOL)
            {
                return Type.BOOL;
            }

            throw new GrammarException(string.Format("Types {0}, {1} are not a valid combination for Bool Operation {2}", termType, repTermType, this.ToString()));
        }
    }
}