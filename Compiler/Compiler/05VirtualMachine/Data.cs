using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Compiler
{

    /* Dr. Edgar Lederer, Fachhochschule Nordwestschweiz */
    /* Ported to C# by JP */

    public class Data
    {

        public interface IBaseData {}

        #region decimal implementation

        public class DecimalData : IBaseData
        {
            private decimal dec;

            public DecimalData(decimal dec)
            {
                this.dec = dec;
            }

            public decimal getData()
            {
                return dec;
            }
        }

        public static DecimalData decimalNew(decimal dec)
        {
            return new DecimalData(dec);
        }

        // pre: a has type DecimalData
        public static decimal decimalGet(IBaseData a)
        {
            if (a is IntData)
            {
                return ((IntData)a).getData();
            }

            return ((DecimalData)a).getData();
        }

        public static DecimalData decimalInv(IBaseData a)
        {
            return decimalNew(-decimalGet(a));
        }
        
        public static DecimalData decimalAdd(IBaseData a, IBaseData b)
        {
            return decimalNew(decimalGet(a) + decimalGet(b));
        }

        public static DecimalData decimalSub(IBaseData a, IBaseData b)
        {
            return decimalNew(decimalGet(a) - decimalGet(b));
        }

        public static DecimalData decimalMult(IBaseData a, IBaseData b)
        {
            return decimalNew(decimalGet(a) * decimalGet(b));
        }

        public static DecimalData decimalDiv(IBaseData a, IBaseData b)
        {
            try
            {
                return decimalNew(decimalGet(a) / decimalGet(b));
            }
            catch (ArithmeticException)
            {
                throw new IVirtualMachine.ExecutionError("Decimal division by zero.");
            }
        }

        public static DecimalData decimalMod(IBaseData a, IBaseData b)
        {
            try
            {
                return decimalNew(decimalGet(a) % decimalGet(b));
            }
            catch (ArithmeticException)
            {
                throw new IVirtualMachine.ExecutionError("Decimal remainder by zero.");
            }
        }

        public static IntData decimalEQ(IBaseData a, IBaseData b)
        {
            return boolNew(decimalGet(a) == decimalGet(b));
        }

        public static IntData decimalNE(IBaseData a, IBaseData b)
        {
            return boolNew(decimalGet(a) != decimalGet(b));
        }

        public static IntData decimalGT(IBaseData a, IBaseData b)
        {
            return boolNew(decimalGet(a) > decimalGet(b));
        }

        public static IntData decimalLT(IBaseData a, IBaseData b)
        {
            return boolNew(decimalGet(a) < decimalGet(b));
        }

        public static IntData decimalGE(IBaseData a, IBaseData b)
        {
            return boolNew(decimalGet(a) >= decimalGet(b));
        }

        public static IntData decimalLE(IBaseData a, IBaseData b)
        {
            return boolNew(decimalGet(a) <= decimalGet(b));
        }

        #endregion

        public class IntData : IBaseData
        {
            private int i;
            public IntData(int i)
            {
                this.i = i;
            }
            public int getData() { return i; }
        }

        public static IntData intNew(int i)
        {
            return new IntData(i);
        }

        // pre: a has type IntData
        public static int intGet(IBaseData a)
        {
            return ((IntData)a).getData();
        }

        // coding booleans as integers
        public static IntData boolNew(bool b)
        {
            return intNew(b ? 1 : 0);
        }

        // coding booleans as integers
        public static bool boolGet(IBaseData a)
        {
            return ((IntData)a).getData() != 0;
        }

        public class FloatData : IBaseData
        {
            private float f;
            public FloatData(float f) { this.f= f; }
            public float getData() { return f; }
        }

        public static FloatData floatNew(float f)
        {
            return new FloatData(f);
        }

        // pre: a has type FloatData
        public static float floatGet(IBaseData a)
        {
            return ((FloatData)a).getData();
        }

        public static IntData intInv(IBaseData a)
        {
            return intNew(-intGet(a));
        }

        public static FloatData floatInv(IBaseData a)
        {
            return floatNew(-floatGet(a));
        }

        public static IntData intAdd(IBaseData a, IBaseData b)
        {
            return intNew(intGet(a) + intGet(b));
        }

        public static IntData intSub(IBaseData a, IBaseData b)
        {
            return intNew(intGet(a) - intGet(b));
        }

        public static IntData intMult(IBaseData a, IBaseData b)
        {
            return intNew(intGet(a) * intGet(b));
        }

        public static IntData intDiv(IBaseData a, IBaseData b)
        {
            try
            {
                return intNew(intGet(a) / intGet(b));
            }
            catch (ArithmeticException)
            {
                throw new IVirtualMachine.ExecutionError("Integer division by zero.");
            }
        }

        public static IntData intMod(IBaseData a, IBaseData b)
        {
            try
            {
                return intNew(intGet(a) % intGet(b));
            }
            catch (ArithmeticException)
            {
                throw new IVirtualMachine.ExecutionError("Integer remainder by zero.");
            }
        }

        public static IntData intEQ(IBaseData a, IBaseData b)
        {
            return boolNew(intGet(a) == intGet(b));
        }

        public static IntData intNE(IBaseData a, IBaseData b)
        {
            return boolNew(intGet(a) != intGet(b));
        }

        public static IntData intGT(IBaseData a, IBaseData b)
        {
            return boolNew(intGet(a) > intGet(b));
        }

        public static IntData intLT(IBaseData a, IBaseData b)
        {
            return boolNew(intGet(a) < intGet(b));
        }

        public static IntData intGE(IBaseData a, IBaseData b)
        {
            return boolNew(intGet(a) >= intGet(b));
        }

        public static IntData intLE(IBaseData a, IBaseData b)
        {
            return boolNew(intGet(a) <= intGet(b));
        }
    }
}
