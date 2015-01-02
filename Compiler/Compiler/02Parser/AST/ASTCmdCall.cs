using System.Collections.Generic;

using Compiler._02Parser.AST;

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
            foreach (ASTExpression expr in ExprList)
            {
                loc = expr.GenerateCode(loc, vm, info);
                //TODO: Add special case: Ref, out, inout need LValue expressions! -> Write address on the stack instead of value
            }
            //Address of the function is not known.
            //So it has to be stored that at this loc should be a call to the function/procedure.
            if (info.ProcFuncAddresses.ContainsKey(Ident))
            {
                vm.Call(loc, info.ProcFuncAddresses[Ident]);
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