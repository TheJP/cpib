package ch.fhnw.lederer.virtualmachineHS2010;

/* Dr. Edgar Lederer, Fachhochschule Nordwestschweiz */

class Data
{
    static interface IBaseData {}
	
	static class DecimalData implements IBaseData
	{
		//data[0] contains the exponent and part of the significand, data[1] and data[2] is for the significand, data[3] is the rest of the significand and the sign
		private int[] dec;
		
		DecimalData(int[] dec) { this.dec = dec;}
		int[] getData(){ return dec; }
		
		bool isNegative()
		{
			return ((data[3] & 32) >> 5) == 1;
		}
		
		int exponent(){
			return data[0] & 31;
		}
	}
	
	static IntData decimalGet(IBaseData a)
	{
		...
	}

    static class IntData implements IBaseData
    {
        private int i;
        IntData(int i) { this.i= i; }
        int getData() { return i; }
    }

    static IntData intNew(int i)
    {
        return new IntData(i);
    }

    // pre: a has type IntData
    static int intGet(IBaseData a)
    {
        return ((IntData)a).getData();
    }

    // coding booleans as integers
    static IntData boolNew(boolean b)
    {
        return intNew(b ? 1 : 0);
    }

    // coding booleans as integers
    static boolean boolGet(IBaseData a)
    {
        return ((IntData)a).getData() != 0;
    }

    static class FloatData implements IBaseData
    {
        private float f;
        FloatData(float f) { this.f= f; }
        float getData() { return f; }
    }

    static FloatData floatNew(float f)
    {
        return new FloatData(f);
    }

    // pre: a has type FloatData
    static float floatGet(IBaseData a)
    {
        return ((FloatData)a).getData();
    }

    static IntData intInv(IBaseData a)
    {
        return intNew(-intGet(a));
    }

    static FloatData floatInv(IBaseData a)
    {
        return floatNew(-floatGet(a));
    }

    static IntData intAdd(IBaseData a, IBaseData b)
    {
        return intNew(intGet(a) + intGet(b));
    }

    static IntData intSub(IBaseData a, IBaseData b)
    {
        return intNew(intGet(a) - intGet(b));
    }

    static IntData intMult(IBaseData a, IBaseData b)
    {
        return intNew(intGet(a) * intGet(b));
    }

    static IntData intDiv(IBaseData a, IBaseData b) throws IVirtualMachine.ExecutionError
    {
        try
        {
            return intNew(intGet(a) / intGet(b));
        }
        catch (ArithmeticException e)
        {
            throw new IVirtualMachine.ExecutionError("Integer division by zero.");
        }
    }

    static IntData intMod(IBaseData a, IBaseData b) throws IVirtualMachine.ExecutionError
    {
        try
        {
            return intNew(intGet(a) % intGet(b));
        }
        catch (ArithmeticException e)
        {
            throw new IVirtualMachine.ExecutionError("Integer remainder by zero.");
        }
    }

    static IntData intEQ(IBaseData a, IBaseData b)
    {
        return boolNew(intGet(a) == intGet(b));
    }

    static IntData intNE(IBaseData a, IBaseData b)
    {
        return boolNew(intGet(a) != intGet(b));
    }

    static IntData intGT(IBaseData a, IBaseData b)
    {
        return boolNew(intGet(a) > intGet(b));
    }

    static IntData intLT(IBaseData a, IBaseData b)
    {
        return boolNew(intGet(a) < intGet(b));
    }

    static IntData intGE(IBaseData a, IBaseData b)
    {
        return boolNew(intGet(a) >= intGet(b));
    }

    static IntData intLE(IBaseData a, IBaseData b)
    {
        return boolNew(intGet(a) <= intGet(b));
    }
}
