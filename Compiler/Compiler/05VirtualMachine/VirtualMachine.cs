using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Compiler
{

    /* Dr. Edgar Lederer, Fachhochschule Nordwestschweiz */
    /* Ported to C# by JP */

    public class VirtualMachine : IVirtualMachine
    {
        private const String SP_GT_HP = "Stack pointer greater than heap pointer.";

        public override bool ReadYesNo(){
            String s;
            try {
                s = Console.ReadLine();
            } catch (Exception) {
                throw new IVirtualMachine.ExecutionError("Input failed.");
            }
            if (s.Equals("no"))
            {
                return false;
            }
            else
            if (s.Equals("yes"))
            {
                return true;
            }
            else
            {
                throw new IVirtualMachine.ExecutionError("Not 'yes' or 'no'.");
            }
        }

        private bool ReadBool(){
            String s;
            try {
                s = Console.ReadLine();
            } catch (Exception) {
                throw new IVirtualMachine.ExecutionError("Input failed.");
            }
            if (s.Equals("false"))
            {
                return false;
            }
            else
            if (s.Equals("true"))
            {
                return true;
            }
            else
            {
                throw new IVirtualMachine.ExecutionError("Not a boolean.");
            }
        }

        private int ReadInt(){
            String s;
            try {
                s = Console.ReadLine();
            } catch (Exception) {
                throw new IVirtualMachine.ExecutionError("Input failed.");
            }
            try {
                return Int32.Parse(s);
            } catch (Exception) {
                throw new IVirtualMachine.ExecutionError("Not an integer.");
            }
        }

        private decimal ReadDecimal()
        {
            String s;
            try
            {
                s = Console.ReadLine();
            }
            catch (Exception)
            {
                throw new IVirtualMachine.ExecutionError("Input failed.");
            }
            try
            {
                return Decimal.Parse(s);
            }
            catch (Exception)
            {
                throw new IVirtualMachine.ExecutionError("Not a decimal.");
            }
        }

        // stores the program
        private KeyValuePair<Action, string>[] code;

        // stores the data
        //  - stack: index 0 upto sp-1
        //  - heap: index store.Length - 1 downto hp+1
        private Data.IBaseData[] store;

        // program counter
        private int pc;

        // stack pointer
        //  - points to the first free location on the stack
        //  - stack grows from 0 upwards
        private int sp;

        // heap pointer
        //  - points to the first free location on the heap
        //  - heap grows from store.Length - 1 downwards
        private int hp;

        // frame pointer
        //  - provides a reference for each routine incarnation
        private int fp;

        public VirtualMachine(int codeSize, int storeSize)
        {
            code = new KeyValuePair<Action, string>[codeSize];
            store = new Data.IBaseData[storeSize];
            sp = 0;
            hp = store.Length - 1;
            fp = 0;
        }

        public override int BoolInitHeapCell()
        {
            if (hp < sp)
            {
                throw new IVirtualMachine.HeapTooSmallError();
            }
            store[hp] = Data.boolNew(false);
            return hp--;
        }

        public override int DecimalInitHeapCell()
        {
            if (hp < sp)
            {
                throw new IVirtualMachine.HeapTooSmallError();
            }
            store[hp] = Data.intNew(0);
            return hp--;
        }

        public override int IntInitHeapCell()
        {
            if (hp < sp)
            {
                throw new IVirtualMachine.HeapTooSmallError();
            }
            store[hp] = Data.intNew(0);
            return hp--;
        }

        public override int FloatInitHeapCell()
        {
            if (hp < sp)
            {
                throw new IVirtualMachine.HeapTooSmallError();
            }
            store[hp] = Data.floatNew((float)0.0);
            return hp--;
        }

        public override void Execute()
        {
            pc= 0;
            while (pc > -1)
            {
                code[pc].Key();
            }
        }

        public override String ToString()
        {
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < code.Length; i++)
            {
                if (code[i].Value != null)
                {
                    sb.Append(i);
                    sb.Append(": ");
                    sb.AppendLine(code[i].Value);
                }
            }
            return sb.ToString();
        }

        // stop instruction
        private void Stop()
        {
            pc = -1;
        }

        public override void Stop(int loc)
        {
            if (loc >= code.Length) { throw new IVirtualMachine.CodeTooSmallError(); }
            code[loc] = new KeyValuePair<Action, string>(() => Stop(), "Stop");
        }

        // routine operations

        private void Alloc(int size)
        {
            sp = sp + size;
            if (sp > hp + 1) { throw new IVirtualMachine.ExecutionError(SP_GT_HP); }
            pc = pc + 1;
        }

        public override void Alloc(int loc, int size)
        {
            if (loc >= code.Length) { throw new IVirtualMachine.CodeTooSmallError(); }
            code[loc] = new KeyValuePair<Action, string>(() => Alloc(size), "Alloc(" + size + ")");
        }

        private void Call(int address)
        {
            if (sp + 2 > hp) { throw new IVirtualMachine.ExecutionError(SP_GT_HP); }
            store[sp] = Data.intNew(fp);
            store[sp + 1] = Data.intNew(0);
            store[sp + 2] = Data.intNew(pc);
            fp = sp;
            pc = address;
        }

        public override void Call(int loc, int address)
        {
            if (loc >= code.Length) { throw new IVirtualMachine.CodeTooSmallError(); }
            code[loc] = new KeyValuePair<Action, string>(() => Call(address), "Call(" + address + ")");
        }

        private void Return(int size)
        {
            sp = fp - size;
            pc = Data.intGet(store[fp + 2]) + 1;
            fp = Data.intGet(store[fp]);
        }

        public override void Return(int loc, int size)
        {
            if (loc >= code.Length) { throw new IVirtualMachine.CodeTooSmallError(); }
            code[loc] = new KeyValuePair<Action, string>(() => Return(size), "Return(" + size + ")");
        }

        private void CopyIn(int fromAddr, int toAddr)
        {
            int address = Data.intGet(store[fp + fromAddr]);
            store[fp + toAddr] = store[address];
            pc = pc + 1;
        }

        public override void CopyIn(int loc, int fromAddr, int toAddr)
        {
            if (loc >= code.Length) { throw new IVirtualMachine.CodeTooSmallError(); }
            code[loc] = new KeyValuePair<Action, string>(() => CopyIn(fromAddr, toAddr), "CopyIn(" + fromAddr + ", " + toAddr + ")");
        }

        private void CopyOut(int fromAddr, int toAddr)
        {
            int address = Data.intGet(store[fp + toAddr]);
            store[address] = store[fp + fromAddr];
            pc = pc + 1;
        }

        public override void CopyOut(int loc, int fromAddr, int toAddr)
        {
            if (loc >= code.Length) { throw new IVirtualMachine.CodeTooSmallError(); }
            code[loc] = new KeyValuePair<Action, string>(() => CopyOut(fromAddr, toAddr), "CopyOut(" + fromAddr + ", " + toAddr + ")");
        }

        private void Enter(int size, int extreme)
        {
            sp = fp + 3 + size;
            if (sp > hp + 1) { throw new IVirtualMachine.ExecutionError(SP_GT_HP); }
            pc = pc + 1;
        }

        public override void Enter(int loc, int size, int extreme)
        {
            if (loc >= code.Length) { throw new IVirtualMachine.CodeTooSmallError(); }
            code[loc] = new KeyValuePair<Action, string>(() => Enter(size, extreme), "Enter(" + size + "," + extreme + ")");
        }

        // load values (value -> stack)

        private void DecimalLoad(decimal value)
        {
            if (sp > hp) { throw new IVirtualMachine.ExecutionError(SP_GT_HP); }
            store[sp] = Data.decimalNew(value);
            sp = sp + 1;
            pc = pc + 1;
        }

        private void IntLoad(int value)
        {
            if (sp > hp) { throw new IVirtualMachine.ExecutionError(SP_GT_HP); }
            store[sp] = Data.intNew(value);
            sp = sp + 1;
            pc = pc + 1;
        }

        public override void DecimalLoad(int loc, decimal value)
        {
            if (loc >= code.Length) { throw new IVirtualMachine.CodeTooSmallError(); }
            code[loc] = new KeyValuePair<Action, string>(() => DecimalLoad(value), "DecimalLoad(" + value + ")");
        }

        public override void IntLoad(int loc, int value)
        {
            if (loc >= code.Length) { throw new IVirtualMachine.CodeTooSmallError(); }
            code[loc] = new KeyValuePair<Action, string>(() => IntLoad(value), "IntLoad(" + value + ")");
        }

        private void FloatLoad(float value)
        {
            if (sp > hp) { throw new IVirtualMachine.ExecutionError(SP_GT_HP); }
            store[sp] = Data.floatNew(value);
            sp = sp + 1;
            pc = pc + 1;
        }

        public override void FloatLoad(int loc, float value)
        {
            if (loc >= code.Length) { throw new IVirtualMachine.CodeTooSmallError(); }
            code[loc] = new KeyValuePair<Action, string>(() => FloatLoad(value), "FloatLoad(" + value + ")");
        }

        // load address relative to frame pointer (address -> stack)
        private void LoadRel(int address)
        {
            if (sp > hp) { throw new IVirtualMachine.ExecutionError(SP_GT_HP); }
            store[sp] = Data.intNew(address + fp);
            sp = sp + 1;
            pc = pc + 1;
        }

        public override void LoadRel(int loc, int address)
        {
            if (loc >= code.Length) { throw new IVirtualMachine.CodeTooSmallError(); }
            code[loc] = new KeyValuePair<Action, string>(() => LoadRel(address), "LoadRel(" + address + ")");
        }

        // load instruction with address on stack
        // load (inside stack -> top of stack) operation
        private void Deref()
        {
            int address = Data.intGet(store[sp - 1]);
            store[sp - 1] = store[address];
            pc = pc + 1;
        }

        public override void Deref(int loc)
        {
            if (loc >= code.Length) { throw new IVirtualMachine.CodeTooSmallError(); }
            code[loc] = new KeyValuePair<Action, string>(() => Deref(), "Deref");
        }

        // store instruction with address on stack
        // store (top of stack -> inside stack) operation
        private void Store()
        {
            int address = Data.intGet(store[sp - 1]);
            store[address] = store[sp - 2];
            sp = sp - 2;
            pc = pc + 1;
        }

        public override void Store(int loc)
        {
            if (loc >= code.Length) { throw new IVirtualMachine.CodeTooSmallError(); }
            code[loc] = new KeyValuePair<Action, string>(() => Store(), "Store");
        }

        // monadic instructions

        private void DecimalInv()
        {
            store[sp - 1] = Data.decimalInv(store[sp - 1]);
            pc = pc + 1;
        }

        public override void DecimalInv(int loc)
        {
            if (loc >= code.Length) { throw new IVirtualMachine.CodeTooSmallError(); }
            code[loc] = new KeyValuePair<Action, string>(() => DecimalInv(), "DecimalInv");
        }

        private void IntInv()
        {
            store[sp-1] = Data.intInv(store[sp-1]);
            pc = pc + 1;
        }

        public override void IntInv(int loc)
        {
            if (loc >= code.Length) { throw new IVirtualMachine.CodeTooSmallError(); }
            code[loc] = new KeyValuePair<Action, string>(() => IntInv(), "IntInv");
        }

        private void FloatInv()
        {
            store[sp-1] = Data.floatInv(store[sp-1]);
            pc = pc + 1;
        }

        public override void FloatInv(int loc)
        {
            if (loc >= code.Length) { throw new IVirtualMachine.CodeTooSmallError(); }
            code[loc] = new KeyValuePair<Action, string>(() => FloatInv(), "FloatInv");
        }

        // dyadic instructions

        private void DecimalAdd()
        {
            sp = sp - 1;
            store[sp - 1] = Data.decimalAdd(store[sp - 1], store[sp]);
            pc = pc + 1;
        }

        public override void DecimalAdd(int loc)
        {
            if (loc >= code.Length) { throw new IVirtualMachine.CodeTooSmallError(); }
            code[loc] = new KeyValuePair<Action, string>(() => DecimalAdd(), "DecimalAdd");
        }

        private void DecimalSub()
        {
            sp = sp - 1;
            store[sp - 1] = Data.decimalSub(store[sp - 1], store[sp]);
            pc = pc + 1;
        }

        public override void DecimalSub(int loc)
        {
            if (loc >= code.Length) { throw new IVirtualMachine.CodeTooSmallError(); }
            code[loc] = new KeyValuePair<Action, string>(() => DecimalSub(), "DecimalSub");
        }

        private void DecimalMult()
        {
            sp = sp - 1;
            store[sp - 1] = Data.decimalMult(store[sp - 1], store[sp]);
            pc = pc + 1;
        }

        public override void DecimalMult(int loc)
        {
            if (loc >= code.Length) { throw new IVirtualMachine.CodeTooSmallError(); }
            code[loc] = new KeyValuePair<Action, string>(() => DecimalMult(), "DecimalMult");
        }

        private void DecimalDiv()
        {
            sp = sp - 1;
            store[sp - 1] = Data.decimalDiv(store[sp - 1], store[sp]);
            pc = pc + 1;
        }

        public override void DecimalDiv(int loc)
        {
            if (loc >= code.Length) { throw new IVirtualMachine.CodeTooSmallError(); }
            code[loc] = new KeyValuePair<Action, string>(() => DecimalDiv(), "DecimalDiv");
        }

        private void DecimalMod()
        {
            sp = sp - 1;
            store[sp - 1] = Data.decimalMod(store[sp - 1], store[sp]);
            pc = pc + 1;
        }

        public override void DecimalMod(int loc)
        {
            if (loc >= code.Length) { throw new IVirtualMachine.CodeTooSmallError(); }
            code[loc] = new KeyValuePair<Action, string>(() => DecimalMod(), "DecimalMod");
        }

        private void DecimalEQ()
        {
            sp = sp - 1;
            store[sp - 1] = Data.decimalEQ(store[sp - 1], store[sp]);
            pc = pc + 1;
        }

        public override void DecimalEQ(int loc)
        {
            if (loc >= code.Length) { throw new IVirtualMachine.CodeTooSmallError(); }
            code[loc] = new KeyValuePair<Action, string>(() => DecimalEQ(), "INtEQ");
        }

        private void DecimalNE()
        {
            sp = sp - 1;
            store[sp - 1] = Data.decimalNE(store[sp - 1], store[sp]);
            pc = pc + 1;
        }

        public override void DecimalNE(int loc)
        {
            if (loc >= code.Length) { throw new IVirtualMachine.CodeTooSmallError(); }
            code[loc] = new KeyValuePair<Action, string>(() => DecimalNE(), "DecimalNE");
        }

        private void DecimalGT()
        {
            sp = sp - 1;
            store[sp - 1] = Data.decimalGT(store[sp - 1], store[sp]);
            pc = pc + 1;
        }

        public override void DecimalGT(int loc)
        {
            if (loc >= code.Length) { throw new IVirtualMachine.CodeTooSmallError(); }
            code[loc] = new KeyValuePair<Action, string>(() => DecimalGT(), "DecimalGT");
        }

        private void DecimalLT()
        {
            sp = sp - 1;
            store[sp - 1] = Data.decimalLT(store[sp - 1], store[sp]);
            pc = pc + 1;
        }

        public override void DecimalLT(int loc)
        {
            if (loc >= code.Length) { throw new IVirtualMachine.CodeTooSmallError(); }
            code[loc] = new KeyValuePair<Action, string>(() => DecimalLT(), "DecimalLT");
        }

        private void DecimalGE()
        {
            sp = sp - 1;
            store[sp - 1] = Data.decimalGE(store[sp - 1], store[sp]);
            pc = pc + 1;
        }

        public override void DecimalGE(int loc)
        {
            if (loc >= code.Length) { throw new IVirtualMachine.CodeTooSmallError(); }
            code[loc] = new KeyValuePair<Action, string>(() => DecimalGE(), "DecimalGE");
        }

        private void DecimalLE()
        {
            sp = sp - 1;
            store[sp - 1] = Data.decimalLE(store[sp - 1], store[sp]);
            pc = pc + 1;
        }

        public override void DecimalLE(int loc)
        {
            if (loc >= code.Length) { throw new IVirtualMachine.CodeTooSmallError(); }
            code[loc] = new KeyValuePair<Action, string>(() => DecimalLE(), "DecimalLE");
        }


        private void IntAdd()
        {
            sp = sp - 1;
            store[sp-1] = Data.intAdd(store[sp-1], store[sp]);
            pc = pc + 1;
        }

        public override void IntAdd(int loc)
        {
            if (loc >= code.Length) { throw new IVirtualMachine.CodeTooSmallError(); }
            code[loc] = new KeyValuePair<Action, string>(() => IntAdd(), "IntAdd");
        }

        private void IntSub()
        {
            sp = sp - 1;
            store[sp-1] = Data.intSub(store[sp-1], store[sp]);
            pc = pc + 1;
        }

        public override void IntSub(int loc)
        {
            if (loc >= code.Length) { throw new IVirtualMachine.CodeTooSmallError(); }
            code[loc] = new KeyValuePair<Action, string>(() => IntSub(), "IntSub");
        }

        private void IntMult()
        {
            sp = sp - 1;
            store[sp-1] = Data.intMult(store[sp-1], store[sp]);
            pc = pc + 1;
        }

        public override void IntMult(int loc)
        {
            if (loc >= code.Length) { throw new IVirtualMachine.CodeTooSmallError(); }
            code[loc] = new KeyValuePair<Action, string>(() => IntMult(), "IntMult");
        }

        private void IntDiv()
        {
            sp = sp - 1;
            store[sp-1] = Data.intDiv(store[sp-1], store[sp]);
            pc = pc + 1;
        }

        public override void IntDiv(int loc)
        {
            if (loc >= code.Length) { throw new IVirtualMachine.CodeTooSmallError(); }
            code[loc] = new KeyValuePair<Action, string>(() => IntDiv(), "IntDiv");
        }

        private void IntMod()
        {
            sp = sp - 1;
            store[sp-1] = Data.intMod(store[sp-1], store[sp]);
            pc = pc + 1;
        }

        public override void IntMod(int loc)
        {
            if (loc >= code.Length) { throw new IVirtualMachine.CodeTooSmallError(); }
            code[loc] = new KeyValuePair<Action, string>(() => IntMod(), "IntMod");
        }

        private void IntEQ()
        {
            sp = sp - 1;
            store[sp-1] = Data.intEQ(store[sp-1], store[sp]);
            pc = pc + 1;
        }

        public override void IntEQ(int loc)
        {
            if (loc >= code.Length) { throw new IVirtualMachine.CodeTooSmallError(); }
            code[loc] = new KeyValuePair<Action, string>(() => IntEQ(), "INtEQ");
        }

        private void IntNE()
        {
            sp = sp - 1;
            store[sp-1] = Data.intNE(store[sp-1], store[sp]);
            pc = pc + 1;
        }

        public override void IntNE(int loc)
        {
            if (loc >= code.Length) { throw new IVirtualMachine.CodeTooSmallError(); }
            code[loc] = new KeyValuePair<Action, string>(() => IntNE(), "IntNE");
        }

        private void IntGT()
        {
            sp = sp - 1;
            store[sp-1] = Data.intGT(store[sp-1], store[sp]);
            pc = pc + 1;
        }

        public override void IntGT(int loc)
        {
            if (loc >= code.Length) { throw new IVirtualMachine.CodeTooSmallError(); }
            code[loc] = new KeyValuePair<Action, string>(() => IntGT(), "IntGT");
        }

        private void IntLT()
        {
            sp = sp - 1;
            store[sp-1] = Data.intLT(store[sp-1], store[sp]);
            pc = pc + 1;
        }

        public override void IntLT(int loc)
        {
            if (loc >= code.Length) { throw new IVirtualMachine.CodeTooSmallError(); }
            code[loc] = new KeyValuePair<Action, string>(() => IntLT(), "IntLT");
        }

        private void IntGE()
        {
            sp = sp - 1;
            store[sp-1] = Data.intGE(store[sp-1], store[sp]);
            pc = pc + 1;
        }

        public override void IntGE(int loc)
        {
            if (loc >= code.Length) { throw new IVirtualMachine.CodeTooSmallError(); }
            code[loc] = new KeyValuePair<Action, string>(() => IntGE(), "IntGE");
        }

        private void IntLE()
        {
            sp = sp - 1;
            store[sp-1] = Data.intLE(store[sp-1], store[sp]);
            pc = pc + 1;
        }

        public override void IntLE(int loc)
        {
            if (loc >= code.Length) { throw new IVirtualMachine.CodeTooSmallError(); }
            code[loc] = new KeyValuePair<Action, string>(() => IntLE(), "IntLE");
        }

        // jump instructions

        private void UncondJump(int jumpLoc)
        {
            pc = jumpLoc;
        }

        public override void UncondJump(int loc, int jumpLoc)
        {
            if (loc >= code.Length) { throw new IVirtualMachine.CodeTooSmallError(); }
            code[loc] = new KeyValuePair<Action, string>(() => UncondJump(jumpLoc), "UncondJump(" + jumpLoc + ")");
        }

        private void CondJump(int jumpLoc)
        {
            sp = sp - 1;
            pc = (Data.boolGet(store[sp])) ? pc + 1 : jumpLoc;
        }

        public override void CondJump(int loc, int jumpLoc)
        {
            if (loc >= code.Length) { throw new IVirtualMachine.CodeTooSmallError(); }
            code[loc] = new KeyValuePair<Action, string>(() => CondJump(jumpLoc), "CondJump(" + jumpLoc + ")");
        }

        // input (input -> stack) and output (stack -> output) instructions

        private void BoolInput(String indicator)
        {
            Console.Write("?" + indicator + " : bool = ");
            bool input = ReadBool();
            int address = Data.intGet(store[sp - 1]);
            store[address] = Data.boolNew(input);
            sp = sp - 1;
            pc = pc + 1;
        }

        public override void BoolInput(int loc, String indicator)
        {
            if (loc >= code.Length) { throw new IVirtualMachine.CodeTooSmallError(); }
            code[loc] = new KeyValuePair<Action, string>(() => BoolInput(indicator), "BoolInput(\"" + indicator + "\")");
        }

        private void IntInput(String indicator)
        {
            Console.Write("?" + indicator + " : int = ");
            int input = ReadInt();
            int address = Data.intGet(store[sp - 1]);
            store[address] = Data.intNew(input);
            sp = sp - 1;
            pc = pc + 1;
        }

        public override void IntInput(int loc, String indicator)
        {
            if (loc >= code.Length) { throw new IVirtualMachine.CodeTooSmallError(); }
            code[loc] = new KeyValuePair<Action, string>(() => IntInput(indicator), "IntInput(\"" + indicator + "\")");
        }

        private void BoolOutput(String indicator)
        {
            sp = sp - 1;
            bool output = Data.boolGet(store[sp]);
            Console.WriteLine("!" + indicator + " : bool = " + output);
            pc = pc + 1;
        }

        public override void BoolOutput(int loc, String indicator)
        {
            if (loc >= code.Length) { throw new IVirtualMachine.CodeTooSmallError(); }
            code[loc] = new KeyValuePair<Action, string>(() => BoolOutput(indicator), "BoolOutput(\"" + indicator + "\")");
        }

        private void IntOutput(String indicator)
        {
            sp = sp - 1;
            int output = Data.intGet(store[sp]);
            Console.WriteLine("!" + indicator + " : int = " + output);
            pc = pc + 1;
        }

        public override void IntOutput(int loc, String indicator)
        {
            if (loc >= code.Length) { throw new IVirtualMachine.CodeTooSmallError(); }
            code[loc] = new KeyValuePair<Action, string>(() => IntOutput(indicator), "IntOutput(\"" + indicator + "\")");
        }

        private void DecimalOutput(String indicator)
        {
            sp = sp - 1;
            decimal output = Data.decimalGet(store[sp]);
            Console.WriteLine("!" + indicator + " : decimal = " + output);
            pc = pc + 1;
        }

        public override void DecimalOutput(int loc, String indicator)
        {
            if (loc >= code.Length) { throw new IVirtualMachine.CodeTooSmallError(); }
            code[loc] = new KeyValuePair<Action, string>(() => DecimalOutput(indicator), "DecimalOutput(\"" + indicator + "\")");
        }

        private void DecimalInput(String indicator)
        {
            Console.Write("?" + indicator + " : decimal = ");
            decimal input = ReadDecimal();
            int address = Data.intGet(store[sp - 1]);
            store[address] = Data.decimalNew(input);
            sp = sp - 1;
            pc = pc + 1;
        }

        public override void DecimalInput(int loc, String indicator)
        {
            if (loc >= code.Length) { throw new IVirtualMachine.CodeTooSmallError(); }
            code[loc] = new KeyValuePair<Action, string>(() => DecimalInput(indicator), "DecimalInput(\"" + indicator + "\")");
        }

        private void DecimalToInt()
        {
            decimal data = Data.decimalGet(store[sp - 1]);
            store[sp - 1] = Data.intNew((int)data);
            pc = pc + 1;
        }

        public override void DecimalToInt(int loc)
        {
            code[loc] = new KeyValuePair<Action, string>(() => DecimalToInt(), "DecimalToInt");
        }
        private void IntToDecimal()
        {
            int data = Data.intGet(store[sp - 1]);
            store[sp - 1] = Data.decimalNew((decimal)data);
            pc = pc + 1;
        }

        public override void IntToDecimal(int loc)
        {
            code[loc] = new KeyValuePair<Action, string>(() => IntToDecimal(), "IntToDecimal");
        }
    }

}
