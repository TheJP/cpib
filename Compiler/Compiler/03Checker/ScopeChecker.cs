using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Compiler
{
    public class ScopeChecker : Checker
    {
        private void OptainNamespaceInformation(ASTProgram root, CheckerInformation info)
        {
            int globalAddress = 0;
            //Add global parameters
            foreach (ASTParam param in root.Params)
            {
                param.Address = globalAddress++;
                info.Globals.addDeclaration(param);
            }
            //Add Global Variables, Functions and Procedures
            //And add local variables and parameters of the Function and Procedures
            foreach (ASTCpsDecl declaration in root.Declarations)
            {
                if (declaration is ASTStoDecl)
                {
                    //Add global storage identifier
                    declaration.Address = globalAddress++;
                    info.Globals.addDeclaration((ASTStoDecl)declaration);
                }
                else if (declaration is ASTProcFuncDecl)
                {
                    ASTProcFuncDecl procFunc = (ASTProcFuncDecl)declaration;
                    //Add function or procedure identifier
                    declaration.Address = -1;
                    info.ProcFuncs.addDeclaration(procFunc);
                    Namespace<IASTStoDecl> ns = new Namespace<IASTStoDecl>();
                    info.Namespaces.Add(declaration.Ident, ns);
                    //Relative address: The framepointer is one above the last parameter. Meaning the last parameter has the relative address -1 and the first -Params.Count
                    int paramAddress = -procFunc.Params.Count;
                    //Relative address: out copy, inout copy and local identifiers. Starting 3 addresses behind the frame pointer.
                    int localAddress = 3;
                    //Add local params of this function/procedure
                    foreach (ASTParam localParam in procFunc.Params)
                    {
                        if (localParam.OptMechmode == MechMode.COPY && (localParam.FlowMode == FlowMode.OUT || localParam.FlowMode == FlowMode.INOUT))
                        {
                            localParam.Address = localAddress++;
                            localParam.AddressLocation = paramAddress++;
                        }
                        else
                        {
                            localParam.Address = paramAddress++;
                        }
                        ns.addDeclaration(localParam);
                    }
                    //Add local storage identifier of this function/procedure
                    foreach (ASTCpsDecl localDeclaration in ((ASTProcFuncDecl)declaration).Declarations)
                    {
                        if (localDeclaration is ASTStoDecl)
                        {
                            localDeclaration.Address = localAddress++;
                            ns.addDeclaration((ASTStoDecl)localDeclaration);
                        }
                    }
                }
            }
        }

        /// <summary>
        /// Used to collect, which idents are used in a node.
        /// </summary>
        public class UsedIdents
        {
            private CheckerInformation info;
            public List<string> GlobalInitialized { get; private set; }
            public Dictionary<string, List<string>> LocalInitialized { get; private set; }
            public bool AllowInit { get; set; }
            public UsedIdents(CheckerInformation info)
            {
                AllowInit = true;
                this.info = info;
                GlobalInitialized = new List<string>();
                LocalInitialized = new Dictionary<string, List<string>>();
            }
            /// <summary>
            /// Allows the modification of the current namespace.
            /// </summary>
            public string CurrentNamespace
            {
                get { return info.CurrentNamespace; }
                set { info.CurrentNamespace = value; }
            }
            /// <summary>
            /// Marks given Ident as used as ProcFuncIdent by the node.
            /// </summary>
            /// <param name="ident">Ident, which is used</param>
            public void AddProcFuncIdent(string ident)
            {
                if (!info.ProcFuncs.ContainsIdent(ident))
                {
                    throw new CheckerException("Use of undeclared procedure/function identifier '" + ident + "'");
                }
            }
            /// <summary>
            /// Marks given Ident as used as StoIdent by the node.
            /// </summary>
            /// <param name="ident">Ident, which is used</param>
            public void AddStoIdent(string ident, bool isInit)
            {
                if (info.CurrentNamespace != null && info.Namespaces[info.CurrentNamespace].ContainsIdent(ident))
                {
                    //Check for initialization
                    if (!LocalInitialized.ContainsKey(CurrentNamespace)) { LocalInitialized.Add(CurrentNamespace, new List<string>()); }
                    if (isInit)
                    {
                        if (!AllowInit) { throw new CheckerException("Initialization is not allowed inside if or while statements: '" + ident + "'"); }
                        if (LocalInitialized[CurrentNamespace].Contains(ident)) { throw new CheckerException("Initialization of already initialized local identifier '" + ident + "'"); }
                        LocalInitialized[CurrentNamespace].Add(ident);
                    }
                    else if (!LocalInitialized[CurrentNamespace].Contains(ident)) { throw new CheckerException("Use of not initialized local identifier '" + ident + "'"); }
                }
                else if (info.Globals.ContainsIdent(ident))
                {
                    //Check for initialization
                    if (isInit)
                    {
                        if (!AllowInit) { throw new CheckerException("Initialization is not allowed inside if or while statements: '" + ident + "'"); }
                        if (GlobalInitialized.Contains(ident)) { throw new CheckerException("Initialization of already initialized global identifier '" + ident + "'"); }
                        GlobalInitialized.Add(ident);
                    }
                    else if (!GlobalInitialized.Contains(ident)) { throw new CheckerException("Use of not initialized global identifier '" + ident + "'"); }
                }
                else
                {
                    throw new CheckerException("Use of undeclared storage identifier '" + ident + "'");
                }
            }

            /// <summary>
            /// Fork used identifier for the usage in an if/else command.
            /// </summary>
            /// <returns>2nd instance of this class which is a deep copies of this instance</returns>
            public UsedIdents ForkForIf()
            {
                UsedIdents b = new UsedIdents(info);
                b.AllowInit = this.AllowInit;
                this.GlobalInitialized.ForEach(g => b.GlobalInitialized.Add(g));
                foreach (var element in this.LocalInitialized)
                {
                    b.LocalInitialized.Add(element.Key, new List<string>());
                    element.Value.ForEach(l => b.LocalInitialized[element.Key].Add(l));
                }
                return b;
            }

            /// <summary>
            /// Merges previously forked UserIdents.
            /// Throws an exception if the UserIdents are not compatible.
            /// </summary>
            /// <param name="b"></param>
            public void MergeForIf(UsedIdents b)
            {
                CheckerException ifException = new CheckerException("Not both branches of the if/else command initialize the same identifiers");
                if (this.GlobalInitialized.Count != b.GlobalInitialized.Count) { throw ifException; }
                if (this.LocalInitialized.Count != b.LocalInitialized.Count) { throw ifException; }
                foreach (string global in this.GlobalInitialized)
                {
                    if (!b.GlobalInitialized.Contains(global)) { throw ifException; }
                }
                foreach (var local in this.LocalInitialized)
                {
                    if (!b.LocalInitialized.ContainsKey(local.Key)) { throw ifException; }
                    if (local.Value.Count != b.LocalInitialized[local.Key].Count) { throw ifException; }
                    foreach(string localIdent in local.Value){
                        if (!b.LocalInitialized[local.Key].Contains(localIdent)) { throw ifException; }
                    }
                }
            }
        }

        private void CheckForUndeclaredIdent(ASTProgram root, CheckerInformation info)
        {
            UsedIdents usedIdents = new UsedIdents(info);
            root.GetUsedIdents(usedIdents);
            info.CurrentNamespace = null;
            //Check to see if each local outflow parameter was initialized
            foreach (string ident in info.ProcFuncs)
            {
                foreach (ASTParam param in info.ProcFuncs[ident].Params)
                {
                    if (param.FlowMode == FlowMode.OUT && (!usedIdents.LocalInitialized.ContainsKey(ident) || !usedIdents.LocalInitialized[ident].Contains(param.Ident)))
                    {
                        throw new CheckerException("The outflow parameter '" + param.Ident + "' of the procedure/function '" + ident + "' was not initialized");
                    }
                }
            }
            //Check if each global outflow parameter was initialized
            foreach (string global in info.Globals)
            {
                if (info.Globals[global] is ASTParam)
                {
                    ASTParam param = (ASTParam)info.Globals[global];
                    if (param.FlowMode == FlowMode.OUT && !usedIdents.GlobalInitialized.Contains(param.Ident))
                    {
                        throw new CheckerException("The global outflow parameter '" + param.Ident + "' of the program was not initialized");
                    }
                }
            }
        }

        private void CheckFunctionParameter(ASTProgram root, CheckerInformation info)
        {
            //Functions can only have one out/inout param (this has to be the first one)
            foreach (string ident in info.ProcFuncs)
            {
                if (info.ProcFuncs[ident].IsFunc)
                {
                    var paramIttr = info.ProcFuncs[ident].Params.GetEnumerator();
                    paramIttr.MoveNext();
                    while (paramIttr.MoveNext())
                    {
                        if (paramIttr.Current.FlowMode == FlowMode.OUT || paramIttr.Current.FlowMode == FlowMode.INOUT)
                        {
                            throw new CheckerException("A function can not have out/inout parameter.");
                        }
                    }
                }
            }
        }

        public void Check(ASTProgram root, CheckerInformation info)
        {
            //Fill namespaces with declaration identifiers
            //Throws an exception if an identifier is declared more then once in a namespace
            OptainNamespaceInformation(root, info);
            //Is any applied identifier declared and initialized?
            CheckForUndeclaredIdent(root, info);
            //Checks if the function parameters are valid
            CheckFunctionParameter(root, info);

            //TODO: Check: if only specified globals are accessed in procedures/functions
            //TODO: Check: Only const globals are allowed in functions
            //TODO: Check: Are only different parameters provided to a procedure as references (Never more then one reference to the same storage)
            //TODO: test05.iml: local constant variables should not be modifiable
        }
    }
}
