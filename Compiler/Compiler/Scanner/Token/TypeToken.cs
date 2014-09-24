using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Compiler
{
    public class TypeToken : GenericParamToken<Type>
    {
        public TypeToken(Type value) : base(Terminals.TYPE, value) { }
    }
}
