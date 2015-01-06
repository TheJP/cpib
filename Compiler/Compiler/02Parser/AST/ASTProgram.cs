using System.Collections.Generic;

namespace Compiler
{
    public class ASTProgram : IASTNode
    {
        public string Ident { get; set; }

        public IList<ASTParam> Params { get; set; }

        public List<ASTCpsDecl> Declarations { get; set; }

        public List<ASTCpsCmd> Commands { get; set; }

        public override string ToString()
        {
            return string.Format("Program {0}", Ident);
        }

        public int GenerateCode(int loc, IVirtualMachine vm, CheckerInformation info)
        {
            //Allocate global storage
            vm.Alloc(loc++, info.Globals.Count);
            //Load input params
            foreach (ASTParam param in Params)
            {
                if (param.FlowMode == FlowMode.IN || param.FlowMode == FlowMode.INOUT)
                {
                    //Load address where to save the input
                    vm.IntLoad(loc++, param.Address);
                    //Switch between types:
                    switch (param.Type)
                    {
                        case Type.INT32:
                            vm.IntInput(loc++, param.Ident);
                            break;
                        case Type.BOOL:
                            vm.BoolInput(loc++, param.Ident);
                            break;
                        case Type.DECIMAL:
                            vm.DecimalInput(loc++, param.Ident);
                            break;
                    }
                }
            }
            //Generate main code
            foreach (ASTCpsCmd cmd in Commands)
            {
                loc = cmd.GenerateCode(loc, vm, info);
            }
            //Add output code
            foreach (ASTParam param in Params)
            {
                if (param.FlowMode == FlowMode.OUT || param.FlowMode == FlowMode.INOUT)
                {
                    //Load output value
                    vm.IntLoad(loc++, param.Address);
                    vm.Deref(loc++);
                    //Switch between types:
                    switch (param.Type)
                    {
                        case Type.INT32:
                            vm.IntOutput(loc++, param.Ident);
                            break;
                        case Type.BOOL:
                            vm.BoolOutput(loc++, param.Ident);
                            break;
                        case Type.DECIMAL:
                            vm.DecimalOutput(loc++, param.Ident);
                            break;
                    }
                }
            }
            //Add stop as last command
            vm.Stop(loc++);
            //Generate functions/procedures
            foreach (string ident in info.ProcFuncs)
            {
                ASTProcFuncDecl procFunc = info.ProcFuncs[ident];
                procFunc.Address = loc;
                //Add calls, now that the function/procedure address is known
                if (info.Calls.ContainsKey(ident) && info.Calls[ident] != null)
                {
                    foreach (int callLoc in info.Calls[ident])
                    {
                        vm.Call(callLoc, procFunc.Address);
                    }
                }
                //Change current namespace
                info.CurrentNamespace = ident;
                //Generate code for function/procedure
                loc = procFunc.GenerateCode(loc, vm, info);
                //Reset namespace
                info.CurrentNamespace = null;
            }
            return loc;
        }
        public void GetUsedIdents(ScopeChecker.UsedIdents usedIdents)
        {
            usedIdents.CurrentNamespace = null;
            Commands.ForEach(cmd => cmd.GetUsedIdents(usedIdents));
            Declarations.ForEach(decl =>
            {
                if (decl is ASTProcFuncDecl) { usedIdents.CurrentNamespace = ((ASTProcFuncDecl)decl).Ident; }
                decl.GetUsedIdents(usedIdents);
                usedIdents.CurrentNamespace = null;
            });
        }
    }
}