using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Compiler
{
    public class MachineCode
    {
        public const int MAX_LOC = 128;
        //TODO: Calculate
        public const int DEBUGOUT_STR_LOC = 0x00;
        public const int DEBUGIN_STR_LOC = 0x00;
        private const int STR_START_LOC = 0x00;

        private Dictionary<uint, Command[]> Commands { get; set; }
        private Dictionary<uint, string> Strings { get; set; }

        public MachineCode()
        {
            Strings = new Dictionary<uint, string>();
            Commands = new Dictionary<uint,Command[]>();
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
        public int AddString(string str)
        {
            return 0;
        }
    }
}
