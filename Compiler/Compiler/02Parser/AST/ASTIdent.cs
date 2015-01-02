using System;
using System.Collections.Generic;

using Compiler._02Parser.AST;

namespace Compiler
{
    public class ASTIdent : ASTExpression
    {
        public string Ident { get; set; }

        public List<ASTExpression> OptInitOrExprList { get; set; }

        public bool IsInit { get; set; }

        public override string ToString()
        {
            return string.Format("{0}{1}", IsInit? "init ":"", Ident);
        }

        public override int GenerateCode(int loc, IVirtualMachine vm, CheckerInformation info)
        {
            //TODO: Could also be a function call!
            if (info.CurrentNamespace != null &&
                info.Namespaces.ContainsKey(info.CurrentNamespace) &&
                info.Namespaces[info.CurrentNamespace].ContainsIdent(Ident))
            {
                //TODO: Load local ident
                vm.LoadRel(loc++, 0);
            }
            else if (info.Globals.ContainsIdent(Ident))
            {
                //TODO: Load global Ident
                vm.LoadRel(loc++, 0);
            }
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