﻿using System;
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

        /// <summary>
        /// Generates the routine, which is used to read user inputs
        /// </summary>
        private void GenerateInputRoutine(uint block, ref uint loc, MachineCode mc, CheckerInformation info)
        {
            //The code has te be overjumped the first time. It should only be executed at defined times by a CALL command
            uint jumpPlaceholder = loc++;
            //MOV AL, '?' //IO Register
            mc[block, loc++] = new Command(Instructions.MOV_R_C, (byte)MachineCode.Registers.A, (byte)'?');
            //MOV CL, 0   //Register which stores the current value. Will be pushed to stack at the end of the input code
            mc[block, loc++] = new Command(Instructions.MOV_R_C, (byte)MachineCode.Registers.C, 0x00);
            //OUT Terminal
            mc[block, loc++] = new Command(Instructions.OUT, (byte)MachineCode.IO.Terminal);

            //Start of loop:
            uint loopLoc = loc;
            //IN Keyboard
            mc[block, loc++] = new Command(Instructions.IN, (byte)MachineCode.IO.Keyboard);
            //OUT Terminal //Write to Terminal what was read
            mc[block, loc++] = new Command(Instructions.OUT, (byte)MachineCode.IO.Terminal);
            //CMP AL, 10 //Check for newline (end of input)
            mc[block, loc++] = new Command(Instructions.CMP_R_C, (byte)MachineCode.Registers.A, (byte)'\n');
            //JZ 08 //Leave out RET instruction if there's more to read
            mc[block, loc++] = new Command(Instructions.JNZ, 2 * 4);
            //RET
            mc[block, loc++] = new Command(Instructions.RET);

            //Convert ascii to number
            //SUB AL, '0'
            mc[block, loc++] = new Command(Instructions.SUB_C, (byte)MachineCode.Registers.A, (byte)'0');
            //MUL CL, 10 //Ready CL for next digit. (If it's the first digit CL will still be 0, because 0*10 = 0)
            mc[block, loc++] = new Command(Instructions.MUL_C, (byte)MachineCode.Registers.C, 10);
            //ADD CL, AL
            mc[block, loc++] = new Command(Instructions.ADD_R, (byte)MachineCode.Registers.C, (byte)MachineCode.Registers.A);

            //JMP XX
            mc[block, loc] = new Command(Instructions.JMP, (byte)((loopLoc - loc) * 4)); ++loc;

            //Fill in the jump placeholder
            mc[block, jumpPlaceholder] = new Command(Instructions.JMP, (byte)((loc - jumpPlaceholder) * 4));
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
                mc[subblock, MachineCode.INIT_LOC - 1] = new Command(Instructions.MOV_CM_C, (byte)MachineCode.ConstantLocations.LOAD_SIZE, (byte)(subloc * 4));
                ++subblock;
                if (subblock > MachineCode.FUNCTION_BLOCK_END)
                {
                    throw new CodeGenerationException("Too many function and procedure definitions. Maximal allowed: " + (MachineCode.FUNCTION_BLOCK_END - MachineCode.FUNCTION_BLOCK_START));
                }
            }
            //** Input Code **//
            //MOV BL, 00
            mc[block, loc++] = new Command(Instructions.MOV_R_C, (byte)MachineCode.Registers.B, 0x00);
            //Add input machine code if there is an in or inout param
            uint readLoc = MachineCode.LOADER_SIZE + ((loc+1)*4);
            if (Params.Exists(p => p.FlowMode == FlowMode.IN || p.FlowMode == FlowMode.INOUT))
            {
                GenerateInputRoutine(block, ref loc, mc, info);
                //Important: BL didn't change in the code here!
            }
            //Load input params
            foreach (ASTParam param in Params)
            {
                if (param.FlowMode == FlowMode.IN || param.FlowMode == FlowMode.INOUT)
                {
                    //CALL readLoc //Call input code which is defined above
                    mc[block, loc++] = new Command(Instructions.CALL, (byte)readLoc);
                    //PUSH CL
                    mc[block, loc++] = new Command(Instructions.PUSH, (byte)MachineCode.Registers.C);
                }
                else
                {
                    //PUSH BL //BL is set to 0 above
                    mc[block, loc++] = new Command(Instructions.PUSH, (byte)MachineCode.Registers.B);
                }
            }
            //** Main Program Code **//
            foreach (ASTCpsCmd cmd in Commands)
            {
                cmd.GenerateCode(block, ref loc, mc, info);
            }
            //** Output Code **//
            //TODO:
            //Write loading size of the initial programm
            mc[block, MachineCode.INIT_LOC - 1] = new Command(Instructions.MOV_CM_C, (byte)MachineCode.ConstantLocations.LOAD_SIZE, (byte)(loc * 4));
            /*
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