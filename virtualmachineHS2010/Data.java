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
			return ((data[3] & 32) >> 5);
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
	
	static DecimalData decimalMult(IBaseData a, IBaseData b)
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
		
		if(result[5].equals(0) && result[4].equals(0) && result[3].equals(0) && result[2].equals(0) && result[1].equals(0) && result[0].equals(0)){
			int[] actualResult = new int[4];
			actualResult[0] = 0;
			actualResult[1] = 0;
			actualResult[2] = 0;
			actualResult[3] = 0;
			
			return decimalNew(actualResult);
		}
		
		while( result[5] > 0 || result[4] > 0 || result[3] > 0 || exponent > 28)
		{
			//divide by 10 to make the result fit
			exponent--;
			
			if(exponent < 0)
			{
				throw new Exception("Overflow!");
			}else if(result[5].equals(0) && result[4].equals(0) && result[3].equals(0) && result[2].equals(0) && result[1].equals(0) && result[0].equals(0))
			{
				throw new Exception("Underflow Error");
			}
			
			carry = 0;
						
			for(int i = 5; i >= 0; i--)
			{
				long current = (long)result[i] + carry;
				result[i] = (int)(current / 10);
				
				carry = ((long)(result[i] % 10))<<32;				
			}
			
			carry = carry >> 32;
		}
		
		//rounding
		bool addOne = false;
		
		if(carry > 5 || carry.equals(5) && (result[0] & 1) == 1)
		{
			int ccarry = 1;
			
			for(int i = 0; i <= 5 && ccarry.equals(1); i++)
			{
				if(result[i].equaks(Integer.MAX_VALUE) && ccarry.equals(1))
				{
					result[i] = 0;
					ccarry = 1;
				}else{
					result[i] += ccarry;
					ccarry = 0;
				}
			}
		}
		
		int[] actualResult = new int[4];
		actualResult[0] = result[0];
		actualResult[1] = result[1];
		actualResult[2] = result[2];
		actualResult[3] = exponent & ((decimalSignGet(a)^decimalSignGet(b))<<5);
		
		return decimalNew(actualResult); 
    }
	
	static DecimalData decimalAdd(IBaseData a, IBaseData b)
	{
		int[] dec1 = decimalGet(a);
		int[] dec2 = decimalGet(b);
		
		int exponent1 = decimalExponentGet(a);
		int exponent2 = decimalExponentGet(b);
		
		int sign1 = decimalSignGet(a);
		int sign2 = decimalSignGet(b);
		
		//check if it's actually a subtraction
		if(sign1 != sign2)
		{
			if(sign1.equals(0))
			{
				dec2[3] = dec2[3] ^ (1<<5);
				
				return decimalSub(a,decimalNew(dec2));
			}else
			{
				dec1[3] = dec1[3] ^ (1<<5);
				
				return decimalSub(b,decimalNew(dec1));
			}
		}
		
		if(exponent1 != exponent2)
		{
			if(exponent2 > exponent1)
			{
				int tempInt = exponent1;
				exponent1 = exponent2;
				exponent2 = tempInt;
				int[] tempDec = dec1;
				dec1 = dec2;
				dec2 = tempDec;
			}
			
			//adjust the exponent so they're equal
			int carry = 0;
			int[] dec2temp;
			while( exponent1 != exponent2)
			{
				//divide by 10 to make the result fit
				exponent2++;
				dec2temp = dec2;
				if(exponent2 < 0)
				{
					throw new Exception("Overflow!");
				}
				
				carry = 0;
							
				for(int i = 0; i <= 2; i++)
				{
					long current = 10 * (long)dec2temp[i] + carry;
					dec2temp[i] = (int)current;
					
					carry = (int)(current>>32);				
				}
				
				if(carry > 0)
				{
					//we can't make the bigger number have a smaller exponent, otherwise we lose significant digits
					//so we raise the smaller number to the exponent of the larger number, losing precision in the least significant digits
					while( exponent1 != exponent2)
					{
						//divide by 10 to make the result fit
						exponent1--;
						
						if(exponent1 < 0)
						{
							throw new Exception("Overflow!");
						}
						
						carry = 0;
									
						for(int i = 2; i >= 0; i--)
						{
							long current = (long)dec1[i] + carry;
							dec1[i] = (int)(current / 10);
							
							carry = ((long)(dec1[i] % 10))<<32;				
						}
					}
				}else{
					dec2 = dec2temp;
				}
			}
			
		}
		
		int[] result = new int[4];
		
		long temp = ((long)dec1[0])+((long)dec2[0]);
		long temp2 = ((long)dec1[1])+((long)dec2[1]);
		long temp3 = ((long)dec1[2])+((long)dec2[2]);
		
		result[0] = (int)temp;
		result[1] = (int)(temp >> 32 ) + (int)temp2;
		result[2] = (int)(temp2 >> 32 ) + (int)temp3;
		result[3] = (int)(temp3 >> 32 );
		
		while( result[3] > 0 || exponent1 > 28)
		{
			//divide by 10 to make the result fit
			exponent1--;
			
			if(exponent1 < 0)
			{
				throw new Exception("Overflow!");
			}else if(result[3].equals(0) && result[2].equals(0) && result[1].equals(0) && result[0].equals(0))
			{
				throw new Exception("Underflow Error");
			}
			
			carry = 0;
						
			for(int i = 3; i >= 0; i--)
			{
				long current = (long)result[i] + carry;
				result[i] = (int)(current / 10);
				
				carry = ((long)(result[i] % 10))<<32;				
			}
			
			carry = carry >> 32;
		}
		
		//rounding
		bool addOne = false;
		
		if(carry > 5 || carry.equals(5) && (result[0] & 1) == 1)
		{
			int ccarry = 1;
			
			for(int i = 0; i <= 5 && ccarry.equals(1); i++)
			{
				if(result[i].equaks(Integer.MAX_VALUE) && ccarry.equals(1))
				{
					result[i] = 0;
					ccarry = 1;
				}else{
					result[i] += ccarry;
					ccarry = 0;
				}
			}
		}
		
		int[] actualResult = new int[4];
		actualResult[0] = result[0];
		actualResult[1] = result[1];
		actualResult[2] = result[2];
		actualResult[3] = exponent & (sign1<<5);
		
		return decimalNew(actualResult); 
	}
	
	static DecimalData decimalSub(IBaseData a, IBaseData b)
	{
		int[] dec1 = decimalGet(a);
		int[] dec2 = decimalGet(b);
		
		int exponent1 = decimalExponentGet(a);
		int exponent2 = decimalExponentGet(b);
		
		int sign1 = decimalSignGet(a);
		int sign2 = decimalSignGet(b);
		
		//check if it's actually a subtraction
		if(sign1 != sign2)
		{
			if(sign1.equals(0))
			{
				dec2[3] = dec2[3] ^ (1<<5);
				
				return decimalAdd(a,decimalNew(dec2));
			}else
			{
				dec2[3] = dec2[3] ^ (1<<5);
				
				return decimalAdd(a,decimalNew(dec2));
			}
		}
		
		bool switched = false;
		
		if(exponent1 != exponent2)
		{
			if(exponent2 > exponent1)
			{
				int tempInt = exponent1;
				exponent1 = exponent2;
				exponent2 = tempInt;
				int[] tempDec = dec1;
				dec1 = dec2;
				dec2 = tempDec;
				
				switched = true;
			}
			
			//adjust the exponent so they're equal
			int carry = 0;
			int[] dec2temp;
			while( exponent1 != exponent2)
			{
				//divide by 10 to make the result fit
				exponent2++;
				dec2temp = dec2;
				if(exponent2 < 0)
				{
					throw new Exception("Overflow!");
				}
				
				carry = 0;
							
				for(int i = 0; i <= 2; i++)
				{
					long current = 10 * (long)dec2temp[i] + carry;
					dec2temp[i] = (int)current;
					
					carry = (int)(current>>32);				
				}
				
				if(carry > 0)
				{
					//we can't make the bigger number have a smaller exponent, otherwise we lose significant digits
					//so we raise the smaller number to the exponent of the larger number, losing precision in the least significant digits
					while( exponent1 != exponent2)
					{
						//divide by 10 to make the result fit
						exponent1--;
						
						if(exponent1 < 0)
						{
							throw new Exception("Overflow!");
						}
						
						carry = 0;
									
						for(int i = 2; i >= 0; i--)
						{
							long current = (long)dec1[i] + carry;
							dec1[i] = (int)(current / 10);
							
							carry = ((long)(dec1[i] % 10))<<32;				
						}
						
						//rounding
						bool addOne = false;
						
						carry = carry>>32;
						
						if(carry > 5 || carry.equals(5) && (dec1[0] & 1) == 1)
						{
							int ccarry = 1;
							
							for(int i = 0; i <= 2 && ccarry.equals(1); i++)
							{
								if(dec1[i].equals(Integer.MAX_VALUE) && ccarry.equals(1))
								{
									dec1[i] = 0;
									ccarry = 1;
								}else{
									dec1[i] += ccarry;
									ccarry = 0;
								}
							}
						}
					}
				}else{
					dec2 = dec2temp;
				}
			}
			
		}
		
		if(switched)
		{
			//switch back the numbers to the original order
			int tempInt = exponent1;
			exponent1 = exponent2;
			exponent2 = tempInt;
			int[] tempDec = dec1;
			dec1 = dec2;
			dec2 = tempDec;
		}
		
		switched = false;
		
		if(dec1[2] < dec2[2] || (dec1[2].equals(dec2[2]) && dec1[1] < dec2[1]) || (dec1[2].equals(dec2[2]) && (dec1[1].equals(dec2[1]) && dec1[0] < dec2[0])
		{
			//switch again to subtract the smaller number from the larger
			
			switched = true;
			int tempInt = exponent1;
			exponent1 = exponent2;
			exponent2 = tempInt;
			int[] tempDec = dec1;
			dec1 = dec2;
			dec2 = tempDec;
		}
		
		int[] result = new int[4];
		int carry = 0;
		
		long temp = ((long)dec1[0]) - ((long)dec2[0]);
		if(temp < 0)
		{
			carry = 1;
			temp += 2<<31;
		}
		
		result[0] = (int)temp;
		
		long temp2 = ((long)dec1[1]) - ((long)dec2[1]) - carry;
		
		carry = 0;
		
		if(temp2 < 0)
		{
			carry = 1;
			temp2 += 2<<31;
		}
		
		result[1] = (int)temp2;
		
		result[2] =(int)( ((long)dec1[2]) - ((long)dec2[2]) - carry);
		
		if(switched)
		{
			sign1 = sign1 ^ 1;
		}
		
		int[] actualResult = new int[4];
		actualResult[0] = result[0];
		actualResult[1] = result[1];
		actualResult[2] = result[2];
		actualResult[3] = exponent1 & (sign1)<<5);
		
		return decimalNew(actualResult); 
	}
	
	static DecimalData decimalDiv(IBaseData a, IBaseData b)
	{
		//sample applications can be found at:
		// http://opensource.apple.com/source/clang/clang-137/src/projects/compiler-rt/lib/udivmoddi4.c
		// https://github.com/gbarnett/shared-source-cli-2.0/blob/master/clr/src/vm/comdecimal.cpp
		
		//since this is way too complex/difficult to implement in this context, use java BigDecimal instead
		int[] dec1 = decimalGet(a);
		int[] dec2 = decimalGet(b);
		
		int exponent1 = decimalExponentGet(a);
		int exponent2 = decimalExponentGet(b);
		
		int sign1 = decimalSignGet(a);
		int sign2 = decimalSignGet(b);
		
		byte[] dec1Bytes = ByteBuffer.allocate(12).putInt(dec1[0]).putInt(dec1[1]).putInt(dec1[2]).array();
		byte[] dec2Bytes = ByteBuffer.allocate(12).putInt(dec2[0]).putInt(dec2[1]).putInt(dec2[2]).array();
		
		BigInteger bigint1 = new BigInteger(dec1Bytes);
		BigInteger bigint2 = new BigInteger(dec2Bytes);
		
		BigDecimal javaDec1 = new BigDecimal(bigint1, exponent1);
		BigDecimal javaDec2 = new BigDecimal(bigint2, exponent2);
		
		if(sign1.equals(1)){
			javaDec1 = javaDec1.negate();
		}
		
		if(sign2.equals(1)){
			javaDec2 = javaDec2.negate();
		}
		
		BigDecimal result = javaDec1.divide(javaDec2, ROUND_HALF_EVEN);
		
		byte[] resultbigint = result.unscaledValue().toByteArray();
		int scale = result.scale() & 31;
		int sign = result.signum().equals(-1) ? 1 : 0;
		
		
		
		int[] actualResult = new int[4];
		actualResult[0] = resultbigint[0] + resultbigint[1]<<8 + resultbigint[2]<<16+resultbigint[3]<<24;
		actualResult[1] = resultbigint[4] + resultbigint[5]<<8 + resultbigint[6]<<16+resultbigint[7]<<24;
		actualResult[2] = resultbigint[8] + resultbigint[9]<<8 + resultbigint[10]<<16+resultbigint[11]<<24;
		actualResult[3] = exponent1 & (sign1)<<5);
		
		return decimalNew(actualResult); 
	}
	
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
