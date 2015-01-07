using System.Collections;
using System.Collections.Generic;

namespace Compiler
{
    public class ASTProcFuncDecl : ASTCpsDecl
    {
        public List<ASTCpsDecl> Declarations { get; set; }

        public List<ASTGlobalParam> OptGlobImps { get; set; }

        public List<ASTParam> Params { get; set; }

        public List<ASTCpsCmd> Commands { get; set; }

        public bool IsFunc { get; set; }

        public override string ToString()
        {
            return string.Format("{0} {1}", IsFunc ? "func" : "proc", Ident);
        }

        public override int GenerateCode(int loc, MachineCode mc, CheckerInformation info)
        {
            //TODO
            /*
            int copies = 0;
            foreach (ASTParam param in Params)
            {
                if (param.OptMechmode == MechMode.COPY && (param.FlowMode == FlowMode.INOUT || param.FlowMode == FlowMode.OUT))
                {
                    ++copies;
                }
            }
            vm.Enter(loc++, copies + Declarations.Count, 0);
            //CopyIn of inout copy parameters
            foreach (ASTParam param in Params)
            {
                if (param.OptMechmode == MechMode.COPY && param.FlowMode == FlowMode.INOUT)
                {
                    vm.CopyIn(loc++, param.AddressLocation.Value, param.Address);
                }
            }
            //Generate body
            foreach (ASTCpsCmd cmd in Commands)
            {
                loc = cmd.GenerateCode(loc, vm, info);
            }
            //CopyOut of inout copy and out copy parameters
            bool omit = IsFunc;
            foreach (ASTParam param in Params)
            {
                if (omit) { omit = false; }
                else
                {
                    if (param.OptMechmode == MechMode.COPY && (param.FlowMode == FlowMode.INOUT || param.FlowMode == FlowMode.OUT))
                    {
                        vm.CopyOut(loc++, param.Address, param.AddressLocation.Value);
                    }
                }
            }
            if (IsFunc)
            {
                //CopyOut return value
                //Can't use standard CopyOut, becuase it works with absolute target address and not with top of stack as needed!
                var paramIttr = Params.GetEnumerator(); paramIttr.MoveNext();
                ASTParam result = paramIttr.Current;
                vm.LoadRel(loc++, result.Address);
                vm.Deref(loc++);
                vm.LoadRel(loc++, result.AddressLocation.Value);
                vm.Store(loc++);
            }
            //Return
            vm.Return(loc++, Params.Count - (IsFunc ? 1 : 0)); //For function the return size is one smaller, leaving the function expression result on the stack!
            */
            return loc;
        }

        public override void GetUsedIdents(ScopeChecker.UsedIdents usedIdents)
        {
            Params.ForEach(param => param.GetUsedIdents(usedIdents));
            OptGlobImps.ForEach(global => global.GetUsedIdents(usedIdents));
            Commands.ForEach(cmd => cmd.GetUsedIdents(usedIdents));
        }
    }
}