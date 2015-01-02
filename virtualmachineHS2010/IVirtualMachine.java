package ch.fhnw.lederer.virtualmachineHS2010;

/* Dr. Edgar Lederer, Fachhochschule Nordwestschweiz */

public interface IVirtualMachine
{
    boolean readYesNo() throws ExecutionError;

    // an InternalError indicates that the compiler itself is in error
    static class InternalError extends RuntimeException
    {
        InternalError(String errorMessage)
        {
            super("Internal error: " + errorMessage);
        }
    }

    // an ExecutionError indicates that execution of the program is in error
    //  - example: division by zero
    static class ExecutionError extends Exception
    {
        ExecutionError(String errorMessage)
        {
            super("Execution error: " + errorMessage);
        }
    }

    // a HeapTooSmallError indicates that the heap (and thus the store)
    // is too small to hold all stores allocated during program execution
    static class HeapTooSmallError extends Exception {}
    // a CodeTooSmallError indicates that the code is too small
    // to hold the complete program
    static class CodeTooSmallError extends Exception {}

    // executes the program
    void execute() throws ExecutionError;

    // each of the following methods initializes the heap cell
    // for the current value of the heap pointer with a default data object;
    // then decreases the heap pointer
    //  - returns the not yet decreased value of the heap pointer
    int BoolInitHeapCell() throws HeapTooSmallError;
    int IntInitHeapCell() throws HeapTooSmallError;
    int FloatInitHeapCell() throws HeapTooSmallError;

    // each of the following methods creates an instruction and
    // stores the instruction at position loc of code

    // stop operation
    void Stop(int loc) throws CodeTooSmallError;

    // routine operations
    void Alloc(int loc, int size) throws CodeTooSmallError;
    void Call(int loc, int address) throws CodeTooSmallError;
    void Return(int loc, int size) throws CodeTooSmallError;
    void CopyIn(int loc, int fromAddr, int toAddr) throws CodeTooSmallError;
    void CopyOut(int loc, int fromAddr, int toAddr) throws CodeTooSmallError;
    void Enter(int loc, int size, int extreme) throws CodeTooSmallError;

    // load values (value -> stack)
    void IntLoad(int loc, int value) throws CodeTooSmallError;
    void FloatLoad(int loc, float value) throws CodeTooSmallError;

    // load address relative to frame pointer (address -> stack)
    void LoadRel(int loc, int address) throws CodeTooSmallError;

    // load (inside stack -> top of stack) operation
    void Deref(int loc) throws CodeTooSmallError;
    // store (top of stack -> inside stack) operation
    void Store(int loc) throws CodeTooSmallError;

    // monadic operations
    void IntInv(int loc) throws CodeTooSmallError;
    void FloatInv(int loc) throws CodeTooSmallError;

    // dyadic operations
    void IntAdd(int loc) throws CodeTooSmallError;
    void IntSub(int loc) throws CodeTooSmallError;
    void IntMult(int loc) throws CodeTooSmallError;
    void IntDiv(int loc) throws CodeTooSmallError;
    void IntMod(int loc) throws CodeTooSmallError;
    void IntEQ(int loc) throws CodeTooSmallError;
    void IntNE(int loc) throws CodeTooSmallError;
    void IntGT(int loc) throws CodeTooSmallError;
    void IntLT(int loc) throws CodeTooSmallError;
    void IntGE(int loc) throws CodeTooSmallError;
    void IntLE(int loc) throws CodeTooSmallError;

    // jump operations
    void UncondJump(int loc, int jumpLoc) throws CodeTooSmallError;
    void CondJump(int loc, int jumpLoc) throws CodeTooSmallError;

    // input (input -> stack) and output (stack -> output) operations
    void BoolInput(int loc, String indicator) throws CodeTooSmallError;
    void IntInput(int loc, String indicator) throws CodeTooSmallError;
    void BoolOutput(int loc, String indicator) throws CodeTooSmallError;
    void IntOutput(int loc, String indicator) throws CodeTooSmallError;

    String toString();
}
