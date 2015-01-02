using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Compiler
{
    public interface IASTStoDecl : IASTDecl
    {
        Type Type { get; set; }
    }
}
