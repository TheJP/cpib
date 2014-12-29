package ch.fhnw.lederer.virtualmachineHS2010;

/* Dr. Edgar Lederer, Fachhochschule Nordwestschweiz */

class Data
{
    static interface IBaseData {}
	
	//Decimal Implementation start
	static class DecimalData implements IBaseData
	{
		//data[0] - data[2] contains the base, with data[2] containing the most significant bits, data[3] contains the exponent (least significant 5 bits) and the sign (the 6th bit)
		private int[] dec;
		
		DecimalData(int[] dec) { this.dec = dec;}
		int[] getData(){ return dec; }
		
		int getDecimalSign()//returns 0 or 1, 1 = negative
		{
			return ((data[3] & 32) >> 5) == 1;
		}
		
		int getExponent(){
			return data[3] & 31;
		}
		
		
	}
	
	static DecimalData decimalNew(int[] dec)
	{
		return new DecimalData(dec);
	}
	
	static int[] decimalGet(IBaseData a)
	{
		return ((DecimalData)a).getData();
	}
	
	static int decimalExponentGet(IBaseData a)
	{
		return ((DecimalData)a).getExponent();
	}
	
	static int decimalSignGet(IBaseData a)
	{
		return ((DecimalData)a).getDecimalSign();
	}
	
	static DecimalData intMult(IBaseData a, IBaseData b)
    {
        int[] dec1 = decimalGet(a);
		int[] dec2 = decimalGet(b);
		
		int[] result = new int[6];
		
		//terminology h, m, l = high, middle, low, 1 = dec1, 2=dec 2, so l2 = low 32 bits of dec2
		
		//l1 * l2
		long temp = ((long)dec1[0])*((long)dec2[0]);
		
		result[0] += (int)temp;
		result[1] += (int)(temp >> 32 );
		
		//m1 * l2
		temp = ((long)dec1[1])*((long)dec2[0]);
		
		result[1] += (int)temp;
		result[2] += (int)(temp >> 32 );
		
		//h1 * l2
		temp = ((long)dec1[2])*((long)dec2[0]);
		
		result[2] += (int)temp;
		result[3] += (int)(temp >> 32 );
		
		//l1 * m2
		temp = ((long)dec1[0])*((long)dec2[1]);
		
		result[1] += (int)temp;
		result[2] += (int)(temp >> 32 );
		
		//m1 * m2
		temp = ((long)dec1[1])*((long)dec2[1]);
		result[2] += (int)temp;
		result[3] += (int)(temp >> 32 );
		
		//h1 * m2
		temp = ((long)dec1[2])*((long)dec2[1]);
		result[3] += (int)temp;
		result[4] += (int)(temp >> 32 );
		
		//l1 * h2
		temp = ((long)dec1[0])*((long)dec2[2]);
		
		result[2] += (int)temp;
		result[3] += (int)(temp >> 32 );
		
		//m1 * h2
		temp = ((long)dec1[1])*((long)dec2[2]);
		result[3] += (int)temp;
		result[4] += (int)(temp >> 32 );
		
		//h1 * h2
		temp = ((long)dec1[2])*((long)dec2[2]);
		result[4] += (int)temp;
		result[5] += (int)(temp >> 32 );
		
		int exponent = decimalExponentGet(a) + decimalExponentGet(b);		
		
		long carry = 0;
		
		while( result[5] > 0 || result[4] > 0 || result[3] > 0)
		{
			//divide by 10 to make the result fit
			exponent--;
			
			if(exponent < 0)
			{
				throw new Exception("Overflow!");
			}
			
			carry = 0;
						
			for(int i = 5; i >= 0; i--)
			{
				int current = (long)result[i] + carry;
				result[i] = (int)(current / 10);
				
				carry = ((long)(result[i] % 10))<<32;				
			}
			
			carry = carry >> 32;
		}
		
		//rounding
		bool addOne = false;
		
		if(carry > 5 || carry == 5 && result[0] & 1 == 1)
		{
			int ccarry = 1;
			
			for(int i = 0; i <= 5; i++)
			{
				if(result[i] == Integer.MAX_VALUE)
				{
					result[i] = 0;
					ccarry = 1;
				}else{
					result[i] += 1;
				}
			}
		}
		
		int[] actualResult = new int[4];
		actualResult[0] = result[0];
		actualResult[1] = result[1];
		actualResult[2] = result[2];
		actualResult[3] = exponent & ((decimalSignGet(a)&decimalSignGet(b))<<5);
		
		return decimalNew(actualResult); 
    }
	
	static void mult64bitInt(int[] dec1, int[] dec2, int[] result)
	
	//Decimal Implementation end

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
