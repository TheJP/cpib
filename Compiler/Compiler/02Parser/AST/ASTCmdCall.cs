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

        public static int GenerateCallingCode(int loc, IVirtualMachine vm, CheckerInformation info, ASTProcFuncDecl callee, List<ASTExpression> exprList)
        {
            //Allocate Return Location on Stack
            if (callee.IsFunc)
            {
                vm.Alloc(loc++, 1);
            }
            //Evaluate argument expressions
            var paramIttr = callee.Params.GetEnumerator();
            //Omit return param for functions
            if (callee.IsFunc)
            {
                if (!paramIttr.MoveNext()) { throw new IVirtualMachine.InternalError("No return param found for function"); }
            }
            foreach (ASTExpression expr in exprList)
            {
                if (!paramIttr.MoveNext()) { throw new IVirtualMachine.InternalError("Too many arguments"); }
                ASTParam param = paramIttr.Current;
                if (param.OptMechmode != MechMode.COPY || (param.FlowMode == FlowMode.OUT || param.FlowMode == FlowMode.INOUT))
                {
                    bool modifiable = param.OptChangemode == ChangeMode.VAR;
                    loc = expr.GenerateLValue(loc, vm, info, modifiable); //Load address on the stack
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
                if (!info.Calls.ContainsKey(callee.Ident)) { info.Calls.Add(callee.Ident, new List<int>()); }
                info.Calls[callee.Ident].Add(loc);
            }
            ++loc;
            return loc;
        }

        public override int GenerateCode(int loc, IVirtualMachine vm, CheckerInformation info)
        {
            //Get abstract declaration
            ASTProcFuncDecl callee = info.ProcFuncs[Ident];
            loc = ASTCmdCall.GenerateCallingCode(loc, vm, info, callee, ExprList);
            return loc;
        }

        public override void GetUsedIdents(ScopeChecker.UsedIdents usedIdents)
        {
            ExprList.ForEach(cmd => cmd.GetUsedIdents(usedIdents));
            usedIdents.AddProcFuncIdent(Ident);
        }
    }
}