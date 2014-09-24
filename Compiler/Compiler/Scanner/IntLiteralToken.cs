﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Compiler
{
    class IntLiteralToken : Token
    {
        public int Value { get; set; }
        public IntLiteralToken(int value) : base(Terminals.LITERAL)
        {
            this.Value = value;
        }
        public override string ToString()
        {
            return String.Format("({0}, {1})", base.ToString(), Value);
        }
    }
}
