using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Compiler
{
    public class MachineCode
    {
        //TODO: Calculate
        public const int DEBUGOUT_STR_LOC = 0x00;
        public const int DEBUGIN_STR_LOC = 0x00;
        private const int STR_START_LOC = 0x00;

        private Dictionary<int, string> Strings { get; set; }

        public MachineCode()
        {
            Strings = new Dictionary<int, string>();
        }

        public void AddCommand(int loc, Command cmd)
        {

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
