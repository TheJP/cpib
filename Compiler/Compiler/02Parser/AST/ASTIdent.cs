using System;
using System.Linq;

namespace Compiler
{
    public class ASTIdent : ASTExpression
    {
        public string Ident { get; set; }

        public IASTNode OptInitOrExprList { get; set; }

        public bool IsInit { get; set; }

        public override string ToString()
        {
            return string.Format("{0}{1}", IsInit? "init ":"", Ident);
        }
        public override int GenerateLValue(int loc, IVirtualMachine vm, CheckerInformation info)
        {
            //TODO: Could also be a function call!
            if (info.CurrentNamespace != null &&
                info.Namespaces.ContainsKey(info.CurrentNamespace) &&
                info.Namespaces[info.CurrentNamespace].ContainsIdent(Ident))
            {
                IASTStoDecl storage = info.Namespaces[info.CurrentNamespace][Ident];
                if (storage is ASTStoDecl || (storage is ASTParam && ((ASTParam)storage).OptMechmode == MechMode.COPY))
                {
                    //Local Identifier or parameter with mechmode COPY
                    vm.LoadRel(loc++, storage.Address);
                }
                else if (storage is ASTParam)
                {
                    //TODO: What should happen if OptMechmode == null?
                    //Load parameter wirh mechmode REF
                    vm.IntLoad(loc++, storage.Address);
                }
                else
                {
                    //Should never happen as long as no new type is added
                    throw new IVirtualMachine.InternalError("Unknown Identifier Type");
                }
            }
            else if (info.Globals.ContainsIdent(Ident))
            {
                IASTStoDecl storage = info.Globals[Ident];
                vm.IntLoad(loc++, storage.Address);
            }
            else
            {
                throw new IVirtualMachine.InternalError("Access of undeclared Identifier " + Ident);
            }
            return loc;
        }
        public override int GenerateCode(int loc, IVirtualMachine vm, CheckerInformation info)
        {
            //TODO: Could also be a function call!
            GenerateLValue(loc, vm, info);
            vm.Deref(loc++);
            return loc;
        }

        public override Type GetExpressionType(CheckerInformation info)
        {
            //TODO: Could also be a function call!
            if (info.CurrentNamespace != null &&
                info.Namespaces.ContainsKey(info.CurrentNamespace) &&
                info.Namespaces[info.CurrentNamespace].ContainsIdent(Ident))
            {
                return info.Namespaces[info.CurrentNamespace].GetIdent(Ident).Type;
            }

            if (info.Globals.ContainsIdent(Ident))
            {
                return info.Globals.GetIdent(Ident).Type;
            }

            throw new NotImplementedException();
        }
    }
}