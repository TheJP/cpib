using System.Collections;
using System.Collections.Generic;

namespace Compiler
{
    public class ASTProcFuncDecl : ASTCpsDecl
    {
        public List<ASTCpsDecl> Declarations { get; set; }

        public List<ASTGlobalParam> OptGlobImps { get; set; }

        public IList<ASTParam> Params { get; set; }

        public List<ASTCpsCmd> Commands { get; set; }

        public bool IsFunc { get; set; }

        public override string ToString()
        {
            return string.Format("{0} {1}", IsFunc ? "func" : "proc", Ident);
        }

        public override int GenerateCode(int loc, IVirtualMachine vm, CheckerInformation info)
        {
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
            foreach (ASTParam param in Params)
            {
                if (param.OptMechmode == MechMode.COPY && (param.FlowMode == FlowMode.INOUT || param.FlowMode == FlowMode.OUT))
                {
                    vm.CopyOut(loc++, param.Address, param.AddressLocation.Value);
                }
            }
            //Return
            vm.Return(loc++, Params.Count);
            return loc;
        }
    }
}