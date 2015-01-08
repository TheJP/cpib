using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Compiler
{
    public class Command
    {
        public Instructions Instruction { get; private set; }
        private byte[] Args { get; set; }
        public Command(Instructions instruction, params byte[] args)
        {
            if (args.Length != instruction.ParamCount())
            {
                throw new ArgumentException("The instruction " + instruction + " has " + instruction.ParamCount() + " parameters but was called with " + args.Length);
            }
            this.Instruction = instruction;
            this.Args = args;
        }
        public byte GetArg(int index)
        {
            return Args[index];
        }
    }
}
