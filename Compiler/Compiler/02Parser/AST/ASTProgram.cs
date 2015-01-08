using System;
using System.Collections.Generic;

namespace Compiler
{
    public class ASTProgram : IASTNode
    {
        public string Ident { get; set; }

        public List<ASTParam> Params { get; set; }

        public List<ASTCpsDecl> Declarations { get; set; }

        public List<ASTCpsCmd> Commands { get; set; }

        public override string ToString()
        {
            return string.Format("Program {0}", Ident);
        }

        public void GenerateCode(uint block, ref uint loc, MachineCode mc, CheckerInformation info)
        {
            //** Functions and Procedures **//
            uint subblock = MachineCode.FUNCTION_BLOCK_START;
            foreach (string ident in info.ProcFuncs)
            {
                //Generate calling code for this function/procedure
                if (info.Calls.ContainsKey(ident))
                {
                    foreach (Tuple<uint, uint> address in info.Calls[ident])
                    {
                        //block addresses are currently limited to int8, so LOC_LOAD_BLOCKADDR_2 is left as is
                        mc[address.Item1, address.Item2] = new Command(Instructions.MOV_CM_C, (byte)MachineCode.ConstantLocations.LOAD_BLOCKADDR_2, (byte)block); //MOV [LOAD_BLOCKADDR_2], block
                    }
                }
                //Generate Procedures/Functions
                uint subloc = MachineCode.INIT_LOC;
                info.ProcFuncs[ident].Address = (int)subblock;
                info.ProcFuncs[ident].GenerateCode((uint)subblock, ref subloc, mc, info);
                //Add size copy instruction
                mc[subblock, subloc] = new Command(Instructions.MOV_CM_C, (byte)MachineCode.ConstantLocations.LOAD_SIZE, (byte)subloc);
                ++subblock;
                if (subblock > MachineCode.FUNCTION_BLOCK_END)
                {
                    throw new CodeGenerationException("Too many function and procedure definitions. Maximal allowed: " + (MachineCode.FUNCTION_BLOCK_END - MachineCode.FUNCTION_BLOCK_START));
                }
            }
            //** Input code **//
            //Allocate global storage
            mc[block, loc++] = new Command(Instructions.MOV_R_C, (byte)MachineCode.Registers.B, 0x00); //MOV BL, 00
            //Load input params
            foreach (ASTParam param in Params)
            {
                if (param.FlowMode == FlowMode.IN || param.FlowMode == FlowMode.INOUT)
                {
                    //MOV AL, '?' //IO Register
                    mc[block, loc++] = new Command(Instructions.MOV_R_C, (byte)MachineCode.Registers.A, (byte)'?');
                    //MOV CL, 0   //Register which stores the current value. Will be pushed to stack at the end of the input code
                    mc[block, loc++] = new Command(Instructions.MOV_R_C, (byte)MachineCode.Registers.C, 0x00);
                    //MOV DL, 1   //Register which stores the current position in the input
                    mc[block, loc++] = new Command(Instructions.MOV_R_C, (byte)MachineCode.Registers.D, 0x01);
                    //OUT Terminal
                    mc[block, loc++] = new Command(Instructions.OUT, (byte)MachineCode.IO.Terminal);
                    //IN Keyboard
                    mc[block, loc++] = new Command(Instructions.IN, (byte)MachineCode.IO.Keyboard);
                    //OUT Terminal //Write to Terminal what was read
                    mc[block, loc++] = new Command(Instructions.OUT, (byte)MachineCode.IO.Terminal);
                    //CMP AL, 10 //Check for newline (end of input)
                    mc[block, loc++] = new Command(Instructions.CMP_R_C, (byte)MachineCode.Registers.A, (byte)'\n');
                    //JZ 18 //Jump to the push instruction
                    mc[block, loc++] = new Command(Instructions.JZ, 0x18);

                    //Convert ascii to number
                    //SUB AL, '0'
                    mc[block, loc++] = new Command(Instructions.SUB_C, (byte)MachineCode.Registers.A, (byte)'0');
                    //MUL AL, DL
                    mc[block, loc++] = new Command(Instructions.MUL_R, (byte)MachineCode.Registers.A, (byte)MachineCode.Registers.D);
                    //ADD CL, AL
                    mc[block, loc++] = new Command(Instructions.ADD_R, (byte)MachineCode.Registers.C, (byte)MachineCode.Registers.A);
                    //MUL DL, 10
                    mc[block, loc++] = new Command(Instructions.MUL_C, (byte)MachineCode.Registers.D, 10);

                    //JMP XX
                    mc[block, loc++] = new Command(Instructions.JMP, 0xE0);
                    //PUSH CL
                    mc[block, loc++] = new Command(Instructions.PUSH, (byte)MachineCode.Registers.C);

                    //Important: BL didn't change in the code here!
                }
                else
                {
                    mc[block, loc++] = new Command(Instructions.PUSH, (byte)MachineCode.Registers.B); //PUSH BL //BL is set to 0 above
                }
            }
            //TODO
            /*
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
            */
        }
        public void GetUsedIdents(ScopeChecker.UsedIdents usedIdents)
        {
            usedIdents.CurrentNamespace = null;
            Params.ForEach(param => param.GetUsedIdents(usedIdents));
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