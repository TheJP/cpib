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
            //Add global parameters
            foreach (ASTParam param in root.Params)
            {
                info.Globals.addDeclaration(param);
            }
            //Add Global Variables, Functions and Procedures
            //And add local variables and parameters of the Function and Procedures
            foreach (ASTCpsDecl declaration in root.Declarations)
            {
                if (declaration is ASTStoDecl)
                {
                    //Add global storage identifier
                    info.Globals.addDeclaration((ASTStoDecl)declaration);
                }
                else if (declaration is ASTProcFuncDecl)
                {
                    //Add function or procedure identifier
                    info.ProcFuncs.addDeclaration((ASTProcFuncDecl)declaration);
                    Namespace<IASTStoDecl> ns = new Namespace<IASTStoDecl>();
                    info.Namespaces.Add(declaration.Ident, ns);
                    //Add local params of this function/procedure
                    foreach (ASTParam localParam in ((ASTProcFuncDecl)declaration).Params)
                    {
                        ns.addDeclaration(localParam);
                    }
                    //Add local storage identifier of this function/procedure
                    foreach (ASTCpsDecl localDeclaration in ((ASTProcFuncDecl)declaration).Declarations)
                    {
                        if (localDeclaration is ASTStoDecl)
                        {
                            ns.addDeclaration((ASTStoDecl)localDeclaration);
                        }
                    }
                }
            }
        }

        private void CheckForUndeclaredIdent(ASTProgram root, CheckerInformation info)
        {
            //TODO: Implement
        }

        public void Check(ASTProgram root, CheckerInformation info)
        {
            //Fill namespaces with declaration identifiers
            //Throws an exception if an identifier is declared more then once in a namespace
            OptainNamespaceInformation(root, info);
            //is any applied identifier declared?
            CheckForUndeclaredIdent(root, info);
        }
    }
}
