using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Compiler
{
    public abstract class GenericParamToken<E> : Token
    {
        public E Value { get; private set; }
        protected GenericParamToken(Terminals terminal, E value) : base(terminal)
        {
            this.Value = value;
        }
        public override string ToString()
        {
            return String.Format("({0}, {1})", base.ToString(), Value.ToString());
        }
    }
}
