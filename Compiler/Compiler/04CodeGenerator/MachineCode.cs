using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Compiler
{
    public class MachineCode
    {
        public const int INIT_LOC = 1; //Leave space for initial move command
        public const int MAX_LOC = 32; //128 Byte
        public const int BLOCK_SIZE = 256; //256 Byte = 1 Block
        //TODO: Calculate
        public const int DEBUGOUT_STR_LOC = 0x00;
        public const int DEBUGIN_STR_LOC = 0x00;
        private const int STR_START_LOC = 0x00;
        public const int MAIN_BLOCK = 0;
        public const int FUNCTION_BLOCK_START = 2;
        public const int FUNCTION_BLOCK_END = Byte.MaxValue - 1;
        public const int LOADER_START = 0x08;
        public const int LOADER_SIZE = 0x44;

        private const string FILE_HEAD = "v2.0 raw";

        //Fixed constant location
        public enum ConstantLocations : byte
        {
            LOAD_BLOCKADDR_1 = 0x01,
            LOAD_BLOCKADDR_2 = 0x02,
            LOAD_ADDR        = 0x03,
            LOAD_SIZE        = 0x05,
            LOAD_TO_ADDR     = 0x06,
        }

        public enum Registers : byte
        {
            A = 0x00,
            B = 0x01,
            C = 0x02,
            D = 0x03,
        }

        public enum IO : byte
        {
            Terminal    = 0x01,
            Keyboard    = 0x02,
            Storage     = 0x03,
            StorageAddr = 0x04,
        }

        private Dictionary<uint, Command[]> Commands { get; set; }
        private Dictionary<uint, string> Strings { get; set; }

        public MachineCode()
        {
            Strings = new Dictionary<uint, string>();
            Commands = new Dictionary<uint,Command[]>();
            this[0, 0] = null; //Initialize Block 0
        }

        /// <summary>
        /// Access to the commands defined in the MachineCode.
        /// </summary>
        /// <param name="block"></param>
        /// <param name="loc"></param>
        /// <returns></returns>
        public Command this[uint block, uint loc]
        {
            get
            {
                if (loc >= MAX_LOC) { throw new CodeGenerationException("Code locations equal to or over " + MAX_LOC + " are not allowed"); }
                if (!Commands.ContainsKey(block)) { throw new CodeGenerationException("Requested code block does not exist"); }
                return Commands[block][loc];
            }
            set
            {
                if (loc >= MAX_LOC) { throw new CodeGenerationException("Code locations equal to or over " + MAX_LOC + " are not allowed"); }
                if (!Commands.ContainsKey(block)) { Commands.Add(block, new Command[MAX_LOC]); }
                Commands[block][loc] = value;
            }
        }

        /// <summary>
        /// Blocks which already exist.
        /// </summary>
        /// <returns></returns>
        public ICollection<uint> GetBlocks()
        {
            return Commands.Keys;
        }

        /// <summary>
        /// Adds a string to the 0. code block (used for program in and inout parameters).
        /// </summary>
        /// <param name="str"></param>
        /// <returns>Start location of string</returns>
        public uint AddString(string str)
        {
            return 0;
        }

        public static byte LoaderJumpArgument(uint loc)
        {
            return (byte)-((loc * 4) + (LOADER_SIZE - LOADER_START));
        }

        public void WriteBinary(StreamWriter writer)
        {
            writer.WriteLine(FILE_HEAD);
            uint expectedBlock = 0;
            foreach (var block in Commands)
            {
                //Fill in 0s for missing blocks
                if (block.Key > expectedBlock)
                {
                    writer.WriteLine(((block.Key - expectedBlock) * BLOCK_SIZE) + "*0");
                }
                expectedBlock = block.Key + 1;
                //Write Commands to the file
                foreach (Command cmd in block.Value)
                {
                    if (cmd != null) {
                        writer.Write(((byte)cmd.Instruction).ToString("X2") + " ");
                        int args = cmd.Instruction.ParamCount();
                        for (int arg = 0; arg < args; ++arg)
                        {
                            writer.Write(cmd.GetArg(arg).ToString("X2") + " ");
                        }
                        if (args < 3) { writer.Write((3-args) + "*0"); }
                        writer.WriteLine();
                    }
                    //Write 0s for missing commands
                    else { writer.WriteLine("4*0"); }
                }
                //Fill the end of the block with 0s
                writer.WriteLine((BLOCK_SIZE - (MAX_LOC * 4)) + "*0");
            }
        }
    }
}
