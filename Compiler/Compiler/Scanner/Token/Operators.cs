using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Compiler
{
    /// <summary>
    /// Enumeration of operators
    /// </summary>
    public enum Operators
    {
        /// <summary>*</summary>
        TIMES,
        /// <summary>div</summary>
        DIV,
        /// <summary>mod</summary>
        MOD,
        /// <summary>+</summary>
        PLUS,
        /// <summary>-</summary>
        MINUS,
        /// <summary>&lt;</summary>
        LT,
        /// <summary>&gt;</summary>
        GT,
        /// <summary>&lt;=</summary>
        LE,
        /// <summary>&gt;=</summary>
        GE,
        /// <summary>=</summary>
        EQ,
        /// <summary>/=</summary>
        NE,
        /// <summary>not</summary>
        NOT,
        /// <summary>&&</summary>
        AND,
        /// <summary>||</summary>
        OR,
        /// <summary>&?</summary>
        CAND,
        /// <summary>|?</summary>
        COR,
    }
}
