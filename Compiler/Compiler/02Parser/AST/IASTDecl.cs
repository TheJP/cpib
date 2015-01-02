using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Compiler
{
    public interface IASTDecl
    {
        string Ident { get; set; }
        int Address { get; set; }
    }
}
