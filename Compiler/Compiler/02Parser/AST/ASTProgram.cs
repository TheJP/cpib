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
            int address = 0;
            foreach (string ident in info.Globals)
            {
                IASTStoDecl decl = info.Globals[ident];
                if (decl is ASTParam)
                {
                    ASTParam param = (ASTParam)decl;
                    if (param.FlowMode == FlowMode.IN || param.FlowMode == FlowMode.INOUT)
                    {
                        //Load address where to save the input
                        vm.IntLoad(loc++, address);
                        //Switch between types:
                        switch (param.Type)
                        {
                            case Type.INT32:
                                vm.IntInput(loc++, ident);
                                break;
                            case Type.BOOL:
                                vm.BoolInput(loc++, ident);
                                break;
                            case Type.DECIMAL:
                                vm.DecimalInput(loc++, ident);
                                break;
                        }
                    }
                }
                address++;
            }
            //Generate main code
            foreach (ASTCpsCmd cmd in Commands)
            {
                loc = cmd.GenerateCode(loc, vm, info);
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
                //loc = procFunc.GenerateCode(loc, vm, info);
            }
            return loc;
        }
    }
}