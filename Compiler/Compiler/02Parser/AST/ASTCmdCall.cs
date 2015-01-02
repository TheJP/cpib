using System.Collections.Generic;

namespace Compiler
{
    public class ASTCmdCall : ASTCpsCmd
    {
        public string Ident { get; set; }

        public List<ASTExpression> ExprList { get; set; }

        public IASTNode OptGlobInits { get; set; }

        public override string ToString()
        {
            return string.Format("call {0}", this.Ident);
        }

        public override int GenerateCode(int loc, IVirtualMachine vm, CheckerInformation info)
        {
            //Get abstract declaration
            ASTProcFuncDecl callee = info.ProcFuncs[Ident];
            //Allocate Return Location on Stack
            if (callee.IsFunc)
            {
                vm.Alloc(loc++, 1);
            }
            //Evaluate argument expressions
            var paramIttr = callee.Params.GetEnumerator();
            foreach (ASTExpression expr in ExprList)
            {
                if (!paramIttr.MoveNext()) { throw new IVirtualMachine.InternalError("Too many arguments"); }
                ASTParam param = paramIttr.Current;
                if (param.OptMechmode != MechMode.COPY || (param.FlowMode == FlowMode.OUT || param.FlowMode == FlowMode.INOUT))
                {
                    loc = expr.GenerateLValue(loc, vm, info); //Load address on the stack
                }
                else
                {
                    loc = expr.GenerateCode(loc, vm, info); //Load value on the stack
                }
            }
            if (paramIttr.MoveNext()) { throw new IVirtualMachine.InternalError("Too few arguments"); }
            //Address of the function is not known.
            //So it has to be stored that at this loc should be a call to the function/procedure.
            if (callee.Address >= 0)
            {
                vm.Call(loc, callee.Address);
            }
            else
            {
                if (!info.Calls.ContainsKey(Ident)) { info.Calls.Add(Ident, new List<int>()); }
                info.Calls[Ident].Add(loc);
            }
            ++loc;
            return loc;
        }
    }
}