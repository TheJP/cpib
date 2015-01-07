using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Compiler
{
    public enum Instructions
    {
        //** Move **//
        /// <summary>MOV AL, 15 | Register = Constant</summary>
        MOV_R_C = 0xD0,
        /// <summary>MOV AL, [15] | Register = Constant Memory</summary>
        MOV_R_CM = 0xD1,
        /// <summary>MOV [15], CL | Constant Memory = Register</summary>
        MOV_CM_R = 0xD2,
        /// <summary>MOV DL, [AL] | Register = Variable Memory</summary>
        MOV_R_VM = 0xD3,
        /// <summary>MOV [CL], AL | Variable Memory = Register</summary>
        MOV_VM_R = 0xD4,
        /// <summary>MOV BL, DL | Register = Register</summary>
        MOV_R_R = 0xD5,
        /// <summary>MOV [15], 5 | Constant Memory = Constant</summary>
        MOV_CM_C = 0xD6,
        /// <summary>MOV [BL], 5 | Variable Memory = Constant</summary>
        MOV_VM_C = 0xD7,

        //** Arithmetic **//
        ADD_R = 0xA0,
        ADD_C = 0xB0,
        SUB_R = 0xA1,
        SUB_C = 0xB1,
        MUL_R = 0xA2,
        MUL_C = 0xB2,
        DIV_R = 0xA3,
        DIV_C = 0xB3,
        INC   = 0xA4,
        DEC   = 0xA5,
        MOD_R = 0xA6,
        MOD_C = 0xB6,

        AND_R  = 0xAA,
        AND_C  = 0xBA,
        OR_R   = 0xAB,
        OR_C   = 0xBB,
        XOR_R  = 0xAC,
        XOR_C  = 0xBC,
        NOT    = 0xAD,

        ROL = 0x9A,
        ROR = 0x9B,
        SHL = 0x9C,
        SHR = 0x9D,

        //** Compare **//
        /// <summary>CMP AL, BL</summary>
        CMP_R_R = 0xDA,
        /// <summary>CMP AL, 13</summary>
        CMP_R_C = 0xDB,
        /// <summary>CMP AL, [20]</summary>
        CMP_R_CM = 0xDC,

        //** Jump **//
        JMP = 0xC0,
        JZ  = 0xC1,
        JNZ = 0xC2,
        JS  = 0xC3,
        JNS = 0xC4,
        JO  = 0xC5,
        JNO = 0xC6,

        //** Subroutines **//
        CALL = 0xCA,
        RET  = 0xCB,
        INT  = 0xCC,
        IRET = 0xCD,

        //** Stack **//
        PUSH  = 0xE0,
        POP   = 0xE1,
        PUSHF = 0xEA,
        POPF  = 0xEB,

        //** IO **//
        IN  = 0xF0,
        OUT = 0xF1,

        //** MISC **//
        HALT = 0x00,
        NOP  = 0xFF,

        //** Flag **//
        STI = 0xFC,
        CLI = 0xFD,
    }
    public static class InstructionsExtensions
    {
        /// <summary>
        /// Returns how many arguments a command of this instruction type has.
        /// </summary>
        /// <param name="instruction"></param>
        /// <returns></returns>
        public static int ParamCount(this Instructions instruction)
        {
            switch (instruction)
            {
                case Instructions.RET:
                case Instructions.IRET:
                case Instructions.PUSHF:
                case Instructions.POPF:
                case Instructions.HALT:
                case Instructions.NOP:
                case Instructions.STI:
                case Instructions.CLI:
                    return 0;
                case Instructions.INC:
                case Instructions.DEC:
                case Instructions.NOT:
                case Instructions.ROL:
                case Instructions.ROR:
                case Instructions.SHL:
                case Instructions.SHR:
                case Instructions.JMP:
                case Instructions.JZ:
                case Instructions.JNZ:
                case Instructions.JS:
                case Instructions.JNS:
                case Instructions.JO:
                case Instructions.JNO:
                case Instructions.CALL:
                case Instructions.INT:
                case Instructions.PUSH:
                case Instructions.POP:
                case Instructions.IN:
                case Instructions.OUT:
                    return 1;
                default:
                    return 2;
            }
        }
    }
}
