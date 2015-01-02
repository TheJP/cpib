using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Compiler
{
    /// <summary>
    /// Contains information which was collected by the checkers. This information will be used for the code generation.
    /// </summary>
    public class CheckerInformation
    {
        public Namespace<IASTStoDecl> Globals { get; set; }
        public Namespace<ASTProcFuncDecl> ProcFuncs { get; set; }
        public IDictionary<string, Namespace<IASTStoDecl>> Namespaces { get; set; }

        public CheckerInformation()
        {
            Globals = new Namespace<IASTStoDecl>();
            ProcFuncs = new Namespace<ASTProcFuncDecl>();
            Namespaces = new Dictionary<string, Namespace<IASTStoDecl>>();
        }
    }
}
