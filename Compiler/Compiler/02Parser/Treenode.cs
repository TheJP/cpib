using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

using Compiler._02Parser.AST;

namespace Compiler
{
    public interface Treenode
    {
        IASTNode ToAbstractSyntax();
    }
}
