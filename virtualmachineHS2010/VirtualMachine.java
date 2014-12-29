package ch.fhnw.lederer.virtualmachineHS2010;

/* Dr. Edgar Lederer, Fachhochschule Nordwestschweiz */

import java.io.BufferedReader;
import java.io.IOException;
import java.io.InputStreamReader;

public class VirtualMachine implements IVirtualMachine
{
    private static final String SP_GT_HP=
        "Stack pointer greater than heap pointer.";

    private BufferedReader reader=
        new BufferedReader(new InputStreamReader(System.in));

    public boolean readYesNo() throws ExecutionError {
        String s;
        try {
            s= reader.readLine();
        } catch (IOException e) {
            throw new ExecutionError("Input failed.");
        }
        if (s.equals("no"))
        {
            return false;
        }
        else
        if (s.equals("yes"))
        {
            return true;
        }
        else
        {
            throw new ExecutionError("Not 'yes' or 'no'.");
        }
    }

    private boolean readBool() throws ExecutionError {
        String s;
        try {
            s= reader.readLine();
        } catch (IOException e) {
            throw new ExecutionError("Input failed.");
        }
        if (s.equals("false"))
        {
            return false;
        }
        else
        if (s.equals("true"))
        {
            return true;
        }
        else
        {
            throw new ExecutionError("Not a boolean.");
        }
    }

    private int readInt() throws ExecutionError {
        String s;
        try {
            s= reader.readLine();
        } catch (IOException e) {
            throw new ExecutionError("Input failed.");
        }
        try {
            return Integer.parseInt(s);
        } catch (NumberFormatException e) {
            throw new ExecutionError("Not an integer.");
        }
    }

    // stores the program
    private IInstruction[] code;

    // stores the data
    //  - stack: index 0 upto sp-1
    //  - heap: index store.length - 1 downto hp+1
    private Data.IBaseData[] store;

    // program counter
    private int pc;

    // stack pointer
    //  - points to the first free location on the stack
    //  - stack grows from 0 upwards
    private int sp;

    // heap pointer
    //  - points to the first free location on the heap
    //  - heap grows from store.length - 1 downwards
    private int hp;

    // frame pointer
    //  - provides a reference for each routine incarnation
    private int fp;

    public VirtualMachine(int codeSize, int storeSize)
    {
        code= new IInstruction[codeSize];
        store= new Data.IBaseData[storeSize];
        sp= 0;
        hp= store.length - 1;
        fp= 0;
    }

    public int BoolInitHeapCell() throws HeapTooSmallError
    {
        if (hp < sp)
        {
            throw new HeapTooSmallError();
        }
        store[hp]= Data.boolNew(false);
        return hp--;
    }

    public int IntInitHeapCell() throws HeapTooSmallError
    {
        if (hp < sp)
        {
            throw new HeapTooSmallError();
        }
        store[hp]= Data.intNew(0);
        return hp--;
    }

    public int FloatInitHeapCell() throws HeapTooSmallError
    {
        if (hp < sp)
        {
            throw new HeapTooSmallError();
        }
        store[hp]= Data.floatNew((float)0.0);
        return hp--;
    }

    public void execute() throws ExecutionError
    {
        pc= 0;
        while (pc > -1)
        {
            code[pc].execute();
        }
    }

    public String toString()
    {
        StringBuffer b= new StringBuffer();
        for (int i= 0; i < code.length; i++)
        {
            if (code[i] != null)
            {
                b.append(i + ": " + code[i] + "\n");
            }
        }
        return b.toString();
    }

    private interface IInstruction
    {
        void execute() throws ExecutionError;
    }

    // stop instruction
    private class Stop implements IInstruction
    {
        private Stop() {}

        public void execute()
        {
            pc= -1;
        }

        public String toString() { return "Stop"; }
    }

    public void Stop(int loc) throws CodeTooSmallError
    {
        if (loc >= code.length) { throw new CodeTooSmallError(); }
        code[loc]= new Stop();
    }

    // routine operations

    private class Alloc implements IInstruction
    {
        private int size;

        private Alloc(int size) { this.size= size; }

        public void execute() throws ExecutionError
        {
            sp= sp + size;
            if (sp > hp + 1) { throw new ExecutionError(SP_GT_HP); }
            pc= pc + 1;
        }

        public String toString() { return "Alloc(" + size + ")"; }
    }

    public void Alloc(int loc, int size) throws CodeTooSmallError
    {
        if (loc >= code.length) { throw new CodeTooSmallError(); }
        code[loc]= new Alloc(size);
    }

    private class Call implements IInstruction
    {
        private int address;

        private Call(int address) { this.address= address; }

        public void execute() throws ExecutionError
        {
            if (sp + 2 > hp) { throw new ExecutionError(SP_GT_HP); }
            store[sp]= Data.intNew(fp);
            store[sp + 1]= Data.intNew(0);
            store[sp + 2]= Data.intNew(pc);
            fp= sp;
            pc= address;
        }

        public String toString() { return "Call(" + address + ")"; }
    }

    public void Call(int loc, int address) throws CodeTooSmallError
    {
        if (loc >= code.length) { throw new CodeTooSmallError(); }
        code[loc]= new Call(address);
    }

    private class Return implements IInstruction
    {
        private int size;

        private Return(int size) { this.size= size; }

        public void execute() throws ExecutionError
        {
            sp= fp - size;
            pc= Data.intGet(store[fp + 2]) + 1;
            fp= Data.intGet(store[fp]);
        }

        public String toString() { return "Return(" + size + ")"; }
    }

    public void Return(int loc, int size) throws CodeTooSmallError
    {
        if (loc >= code.length) { throw new CodeTooSmallError(); }
        code[loc]= new Return(size);
    }

    private class CopyIn implements IInstruction
    {
        private int fromAddr;
        private int toAddr;

        private CopyIn(int fromAddr, int toAddr)
        { this.fromAddr= fromAddr; this.toAddr= toAddr; }

        public void execute() throws ExecutionError
        {
            int address= Data.intGet(store[fp + fromAddr]);
            store[fp + toAddr]= store[address];
            pc= pc + 1;
        }

        public String toString()
        { return "CopyIn(" + fromAddr + "," + toAddr + ")"; }
    }

    public void CopyIn(int loc, int fromAddr, int toAddr) throws CodeTooSmallError
    {
        if (loc >= code.length) { throw new CodeTooSmallError(); }
        code[loc]= new CopyIn(fromAddr, toAddr);
    }

    private class CopyOut implements IInstruction
    {
        private int fromAddr;
        private int toAddr;

        private CopyOut(int fromAddr, int toAddr)
        { this.fromAddr= fromAddr; this.toAddr= toAddr; }

        public void execute() throws ExecutionError
        {
            int address= Data.intGet(store[fp + toAddr]);
            store[address]= store[fp + fromAddr];
            pc= pc + 1;
        }

        public String toString()
        { return "CopyOut(" + fromAddr + "," + toAddr + ")"; }
    }

    public void CopyOut(int loc, int fromAddr, int toAddr) throws CodeTooSmallError
    {
        if (loc >= code.length) { throw new CodeTooSmallError(); }
        code[loc]= new CopyOut(fromAddr, toAddr);
    }

    private class Enter implements IInstruction
    {
        private int size;
        private int extreme;

        private Enter(int size, int extreme)
        { this.size= size; this.extreme= extreme; }

        public void execute() throws ExecutionError
        {
            sp= fp + 3 + size;
            if (sp > hp + 1) { throw new ExecutionError(SP_GT_HP); }
            pc= pc + 1;
        }

        public String toString()
        { return "Enter(" + size + "," + extreme + ")"; }
    }

    public void Enter(int loc, int size, int extreme) throws CodeTooSmallError
    {
        if (loc >= code.length) { throw new CodeTooSmallError(); }
        code[loc]= new Enter(size, extreme);
    }

    // load values (value -> stack)

    private class IntLoad implements IInstruction
    {
        private int value;

        private IntLoad(int value) { this.value= value; }

        public void execute() throws ExecutionError
        {
            if (sp > hp) { throw new ExecutionError(SP_GT_HP); }
            store[sp]= Data.intNew(value);
            sp= sp + 1;
            pc= pc + 1;
        }

        public String toString() { return "IntLoad(" + value + ")"; }
    }

    public void IntLoad(int loc, int value) throws CodeTooSmallError
    {
        if (loc >= code.length) { throw new CodeTooSmallError(); }
        code[loc]= new IntLoad(value);
    }

    private class FloatLoad implements IInstruction
    {
        private float value;

        private FloatLoad(float value) { this.value= value; }

        public void execute() throws ExecutionError
        {
            if (sp > hp) { throw new ExecutionError(SP_GT_HP); }
            store[sp]= Data.floatNew(value);
            sp= sp + 1;
            pc= pc + 1;
        }

        public String toString() { return "FloatLoad(" + value + ")"; }
    }

    public void FloatLoad(int loc, float value) throws CodeTooSmallError
    {
        if (loc >= code.length) { throw new CodeTooSmallError(); }
        code[loc]= new FloatLoad(value);
    }

    // load address relative to frame pointer (address -> stack)
    private class LoadRel implements IInstruction
    {
        private int address;

        private LoadRel(int address) { this.address= address; }

        public void execute() throws ExecutionError
        {
            if (sp > hp) { throw new ExecutionError(SP_GT_HP); }
            store[sp]= Data.intNew(address + fp);
            sp= sp + 1;
            pc= pc + 1;
        }

        public String toString() { return "LoadRel(" + address + ")"; }
    }

    public void LoadRel(int loc, int address) throws CodeTooSmallError
    {
        if (loc >= code.length) { throw new CodeTooSmallError(); }
        code[loc]= new LoadRel(address);
    }

    // load instruction with address on stack
    // load (inside stack -> top of stack) operation
    private class Deref implements IInstruction
    {
        private Deref() {}

        public void execute() throws ExecutionError
        {
            int address= Data.intGet(store[sp - 1]);
            store[sp - 1]= store[address];
            pc= pc + 1;
        }

        public String toString() { return "Deref"; }
    }

    public void Deref(int loc) throws CodeTooSmallError
    {
        if (loc >= code.length) { throw new CodeTooSmallError(); }
        code[loc]= new Deref();
    }

    // store instruction with address on stack
    // store (top of stack -> inside stack) operation
    private class Store implements IInstruction
    {
        private Store() {}

        public void execute() throws ExecutionError
        {
            int address= Data.intGet(store[sp - 1]);
            store[address]= store[sp - 2];
            sp= sp - 2;
            pc= pc + 1;
        }

        public String toString() { return "Store"; }
    }

    public void Store(int loc) throws CodeTooSmallError
    {
        if (loc >= code.length) { throw new CodeTooSmallError(); }
        code[loc]= new Store();
    }

    // monadic instructions

    private class IntInv implements IInstruction
    {
        private IntInv() {}

        public void execute()
        {
            store[sp-1]= Data.intInv(store[sp-1]);
            pc= pc + 1;
        }

        public String toString() { return "IntInv"; }
    }

    public void IntInv(int loc) throws CodeTooSmallError
    {
        if (loc >= code.length) { throw new CodeTooSmallError(); }
        code[loc]= new IntInv();
    }

    private class FloatInv implements IInstruction
    {
        private FloatInv() {}

        public void execute()
        {
            store[sp-1]= Data.floatInv(store[sp-1]);
            pc= pc + 1;
        }

        public String toString() { return "FloatInv"; }
    }

    public void FloatInv(int loc) throws CodeTooSmallError
    {
        if (loc >= code.length) { throw new CodeTooSmallError(); }
        code[loc]= new FloatInv();
    }

    // dyadic instructions

    private class IntAdd implements IInstruction
    {
        private IntAdd() {}

        public void execute()
        {
            sp= sp - 1;
            store[sp-1]= Data.intAdd(store[sp-1], store[sp]);
            pc= pc + 1;
        }

        public String toString() { return "IntAdd"; }
    }

    public void IntAdd(int loc) throws CodeTooSmallError
    {
        if (loc >= code.length) { throw new CodeTooSmallError(); }
        code[loc]= new IntAdd();
    }

    private class IntSub implements IInstruction
    {
        private IntSub() {}

        public void execute()
        {
            sp= sp - 1;
            store[sp-1]= Data.intSub(store[sp-1], store[sp]);
            pc= pc + 1;
        }

        public String toString() { return "IntSub"; }
    }

    public void IntSub(int loc) throws CodeTooSmallError
    {
        if (loc >= code.length) { throw new CodeTooSmallError(); }
        code[loc]= new IntSub();
    }

    private class IntMult implements IInstruction
    {
        private IntMult() {}

        public void execute()
        {
            sp= sp - 1;
            store[sp-1]= Data.intMult(store[sp-1], store[sp]);
            pc= pc + 1;
        }

        public String toString() { return "IntMult"; }
    }

    public void IntMult(int loc) throws CodeTooSmallError
    {
        if (loc >= code.length) { throw new CodeTooSmallError(); }
        code[loc]= new IntMult();
    }

    private class IntDiv implements IInstruction
    {
        private IntDiv() {}

        public void execute() throws ExecutionError
        {
            sp= sp - 1;
            store[sp-1]= Data.intDiv(store[sp-1], store[sp]);
            pc= pc + 1;
        }

        public String toString() { return "IntDiv"; }
    }

    public void IntDiv(int loc) throws CodeTooSmallError
    {
        if (loc >= code.length) { throw new CodeTooSmallError(); }
        code[loc]= new IntDiv();
    }

    private class IntMod implements IInstruction
    {
        private IntMod() {}

        public void execute() throws ExecutionError
        {
            sp= sp - 1;
            store[sp-1]= Data.intMod(store[sp-1], store[sp]);
            pc= pc + 1;
        }

        public String toString() { return "IntMod"; }
    }

    public void IntMod(int loc) throws CodeTooSmallError
    {
        if (loc >= code.length) { throw new CodeTooSmallError(); }
        code[loc]= new IntMod();
    }

    private class IntEQ implements IInstruction
    {
        private IntEQ() {}

        public void execute()
        {
            sp= sp - 1;
            store[sp-1]= Data.intEQ(store[sp-1], store[sp]);
            pc= pc + 1;
        }

        public String toString() { return "IntEQ"; }
    }

    public void IntEQ(int loc) throws CodeTooSmallError
    {
        if (loc >= code.length) { throw new CodeTooSmallError(); }
        code[loc]= new IntEQ();
    }

    private class IntNE implements IInstruction
    {
        private IntNE() {}

        public void execute()
        {
            sp= sp - 1;
            store[sp-1]= Data.intNE(store[sp-1], store[sp]);
            pc= pc + 1;
        }

        public String toString() { return "IntNE"; }
    }

    public void IntNE(int loc) throws CodeTooSmallError
    {
        if (loc >= code.length) { throw new CodeTooSmallError(); }
        code[loc]= new IntNE();
    }

    private class IntGT implements IInstruction
    {
        private IntGT() {}

        public void execute()
        {
            sp= sp - 1;
            store[sp-1]= Data.intGT(store[sp-1], store[sp]);
            pc= pc + 1;
        }

        public String toString() { return "IntGT"; }
    }

    public void IntGT(int loc) throws CodeTooSmallError
    {
        if (loc >= code.length) { throw new CodeTooSmallError(); }
        code[loc]= new IntGT();
    }

    private class IntLT implements IInstruction
    {
        private IntLT() {}

        public void execute()
        {
            sp= sp - 1;
            store[sp-1]= Data.intLT(store[sp-1], store[sp]);
            pc= pc + 1;
        }

        public String toString() { return "IntLT"; }
    }

    public void IntLT(int loc) throws CodeTooSmallError
    {
        if (loc >= code.length) { throw new CodeTooSmallError(); }
        code[loc]= new IntLT();
    }

    private class IntGE implements IInstruction
    {
        private IntGE() {}

        public void execute()
        {
            sp= sp - 1;
            store[sp-1]= Data.intGE(store[sp-1], store[sp]);
            pc= pc + 1;
        }

        public String toString() { return "IntGE"; }
    }

    public void IntGE(int loc) throws CodeTooSmallError
    {
        if (loc >= code.length) { throw new CodeTooSmallError(); }
        code[loc]= new IntGE();
    }

    private class IntLE implements IInstruction
    {
        private IntLE() {}

        public void execute()
        {
            sp= sp - 1;
            store[sp-1]= Data.intLE(store[sp-1], store[sp]);
            pc= pc + 1;
        }

        public String toString() { return "IntLE"; }
    }

    public void IntLE(int loc) throws CodeTooSmallError
    {
        if (loc >= code.length) { throw new CodeTooSmallError(); }
        code[loc]= new IntLE();
    }

    // jump instructions

    private class UncondJump implements IInstruction
    {
        private int jumpLoc;

        private UncondJump(int jumpLoc) { this.jumpLoc= jumpLoc; }

        public void execute()
        {
            pc= jumpLoc;
        }

        public String toString() { return "UncondJump(" + jumpLoc + ")"; }
    }

    public void UncondJump(int loc, int jumpLoc) throws CodeTooSmallError
    {
        if (loc >= code.length) { throw new CodeTooSmallError(); }
        code[loc]= new UncondJump(jumpLoc);
    }

    private class CondJump implements IInstruction
    {
        private int jumpLoc;

        private CondJump(int jumpLoc) { this.jumpLoc= jumpLoc; }

        public void execute()
        {
            sp= sp - 1;
            pc= (Data.boolGet(store[sp])) ? pc + 1 : jumpLoc;
        }

        public String toString() { return "CondJump(" + jumpLoc + ")"; }
    }

    public void CondJump(int loc, int jumpLoc) throws CodeTooSmallError
    {
        if (loc >= code.length) { throw new CodeTooSmallError(); }
        code[loc]= new CondJump(jumpLoc);
    }

    // input (input -> stack) and output (stack -> output) instructions

    private class BoolInput implements IInstruction
    {
        private String indicator;

        private BoolInput(String indicator)
        {
            this.indicator= indicator;
        }

        public void execute() throws ExecutionError
        {
            System.out.print("?" + indicator + " : bool = ");
            boolean input= readBool();
            int address= Data.intGet(store[sp - 1]);
            store[address]= Data.boolNew(input);
            sp= sp - 1;
            pc= pc + 1;
        }

        public String toString() { return "BoolInput(\"" + indicator + "\")"; }
    }

    public void BoolInput(int loc, String indicator) throws CodeTooSmallError
    {
        if (loc >= code.length) { throw new CodeTooSmallError(); }
        code[loc]= new BoolInput(indicator);
    }

    private class IntInput implements IInstruction
    {
        private String indicator;

        private IntInput(String indicator)
        {
            this.indicator= indicator;
        }

        public void execute() throws ExecutionError
        {
            System.out.print("?" + indicator + " : int = ");
            int input= readInt();
            int address= Data.intGet(store[sp - 1]);
            store[address]= Data.intNew(input);
            sp= sp - 1;
            pc= pc + 1;
        }

        public String toString() { return "IntInput(\"" + indicator + "\")"; }
    }

    public void IntInput(int loc, String indicator) throws CodeTooSmallError
    {
        if (loc >= code.length) { throw new CodeTooSmallError(); }
        code[loc]= new IntInput(indicator);
    }

    private class BoolOutput implements IInstruction
    {
        private String indicator;

        private BoolOutput(String indicator)
        {
            this.indicator= indicator;
        }

        public void execute()
        {
            sp= sp - 1;
            boolean output= Data.boolGet(store[sp]);
            System.out.println("!" + indicator + " : bool = " + output);
            pc= pc + 1;
        }

        public String toString() { return "BoolOutput(\"" + indicator + "\")"; }
    }

    public void BoolOutput(int loc, String indicator) throws CodeTooSmallError
    {
        if (loc >= code.length) { throw new CodeTooSmallError(); }
        code[loc]= new BoolOutput(indicator);
    }

    private class IntOutput implements IInstruction
    {
        private String indicator;

        private IntOutput(String indicator)
        {
            this.indicator= indicator;
        }

        public void execute()
        {
            sp= sp - 1;
            int output= Data.intGet(store[sp]);
            System.out.println("!" + indicator + " : int = " + output);
            pc= pc + 1;
        }

        public String toString() { return "IntOutput(\"" + indicator + "\")"; }
    }

    public void IntOutput(int loc, String indicator) throws CodeTooSmallError
    {
        if (loc >= code.length) { throw new CodeTooSmallError(); }
        code[loc]= new IntOutput(indicator);
    }
}
