﻿using System;
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
        //** Namespace information **//

        //This is used to..
        //  ..check if duplicate declarations occure in the same namespace
        //  ..if an undeclared identifier is used somewhere in the code
        //  ..to get information about a declaration if only its identifier is given
        public Namespace<IASTStoDecl> Globals { get; private set; }
        public Namespace<ASTProcFuncDecl> ProcFuncs { get; private set; }
        public IDictionary<string, Namespace<IASTStoDecl>> Namespaces { get; private set; }
        //Current namespace is set inside of function/procedues to signal that identifiers of this namespace may be used
        public string CurrentNamespace { get; set; }

        //** Call, Function/Procedure Locations **//

        //This stores locations in the code where the function/procedure is called.
        //This is used, if the address is not kown at generation time of the call expression.
        //The call will be stored in the VM when te address of the function/procedure is kown.
        public IDictionary<string, IList<int>> Calls { get; private set; }
        //ProcFuncAddresses stores addresses of functions/procedures as soon as it is known.
        //When a function/procedure is registered here, the Call Command has to use this address.
        public IDictionary<string, int> ProcFuncAddresses { get; private set; }
        public CheckerInformation()
        {
            Globals = new Namespace<IASTStoDecl>();
            ProcFuncs = new Namespace<ASTProcFuncDecl>();
            Namespaces = new Dictionary<string, Namespace<IASTStoDecl>>();
            CurrentNamespace = null;
            Calls = new Dictionary<string, IList<int>>();
            ProcFuncAddresses = new Dictionary<string, int>();
        }
    }
}
