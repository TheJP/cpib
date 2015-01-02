using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Compiler
{

    /* Dr. Edgar Lederer, Fachhochschule Nordwestschweiz */
    /* Ported to C# by JP */

    //Using abstract class instead of interface so inner exceptions can be declared
    public abstract class IVirtualMachine
    {
        public abstract bool ReadYesNo();

        // an InternalError indicates that the compiler itself is in error
        public class InternalError : Exception
        {
            public InternalError(String errorMessage) : base("Internal error: " + errorMessage) { }
        }

        // an ExecutionError indicates that execution of the program is in error
        //  - example: division by zero
        public class ExecutionError : Exception
        {
            public ExecutionError(String errorMessage) : base("Execution error: " + errorMessage){}
        }

        // a HeapTooSmallError indicates that the heap (and thus the store)
        // is too small to hold all stores allocated during program execution
        public class HeapTooSmallError : Exception { }
        // a CodeTooSmallError indicates that the code is too small
        // to hold the complete program
        public class CodeTooSmallError : Exception { }

        // executes the program
        public abstract void Execute();

        // each of the following methods initializes the heap cell
        // for the current value of the heap pointer with a default data object;
        // then decreases the heap pointer
        //  - returns the not yet decreased value of the heap pointer
        public abstract int BoolInitHeapCell();
        public abstract int IntInitHeapCell();
        public abstract int FloatInitHeapCell();
        public abstract int DecimalInitHeapCell();

        // each of the following methods creates an instruction and
        // stores the instruction at position loc of code

        // stop operation
        public abstract void Stop(int loc);

        // routine operations
        public abstract void Alloc(int loc, int size);
        public abstract void Call(int loc, int address);
        public abstract void Return(int loc, int size);
        public abstract void CopyIn(int loc, int fromAddr, int toAddr);
        public abstract void CopyOut(int loc, int fromAddr, int toAddr);
        public abstract void Enter(int loc, int size, int extreme);

        // load values (value -> stack)
        public abstract void DecimalLoad(int loc, decimal value);
        public abstract void IntLoad(int loc, int value);
        public abstract void FloatLoad(int loc, float value);

        // load address relative to frame pointer (address -> stack)
        public abstract void LoadRel(int loc, int address);

        // load (inside stack -> top of stack) operation
        public abstract void Deref(int loc);
        // store (top of stack -> inside stack) operation
        public abstract void Store(int loc);

        // monadic operations
        public abstract void DecimalInv(int loc);
        public abstract void IntInv(int loc);
        public abstract void FloatInv(int loc);

        // dyadic operations
        public abstract void DecimalAdd(int loc);
        public abstract void DecimalSub(int loc);
        public abstract void DecimalMult(int loc);
        public abstract void DecimalDiv(int loc);
        public abstract void DecimalMod(int loc);
        public abstract void DecimalEQ(int loc);
        public abstract void DecimalNE(int loc);
        public abstract void DecimalGT(int loc);
        public abstract void DecimalLT(int loc);
        public abstract void DecimalGE(int loc);
        public abstract void DecimalLE(int loc);

        public abstract void IntAdd(int loc);
        public abstract void IntSub(int loc);
        public abstract void IntMult(int loc);
        public abstract void IntDiv(int loc);
        public abstract void IntMod(int loc);
        public abstract void IntEQ(int loc);
        public abstract void IntNE(int loc);
        public abstract void IntGT(int loc);
        public abstract void IntLT(int loc);
        public abstract void IntGE(int loc);
        public abstract void IntLE(int loc);

        // jump operations
        public abstract void UncondJump(int loc, int jumpLoc);
        public abstract void CondJump(int loc, int jumpLoc);

        // input (input -> stack) and output (stack -> output) operations
        public abstract void BoolInput(int loc, String indicator);
        public abstract void IntInput(int loc, String indicator);
        public abstract void BoolOutput(int loc, String indicator);
        public abstract void IntOutput(int loc, String indicator);

        public abstract void DecimalInput(int loc, String indicator);
        public abstract void DecimalOutput(int loc, String indicator);

        public override abstract String ToString();
    }
}
