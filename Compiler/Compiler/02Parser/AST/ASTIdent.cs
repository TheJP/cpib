using System;
using System.Collections.Generic;

namespace Compiler
{
    public class ASTIdent : ASTExpression
    {
        public string Ident { get; set; }

        public List<ASTExpression> OptInitOrExprList { get; set; }

        public bool IsFuncCall { get { return OptInitOrExprList.Count > 0; } }

        public bool IsInit { get; set; }

        public override string ToString()
        {
            return string.Format("{0} {1}", IsInit? "init ":"", Ident);
        }
        private void AssertNotConstant(IASTStoDecl sto)
        {
            if (sto is ASTStoDecl) { AssertNotConstant((ASTStoDecl)sto); }
            else if (sto is ASTParam) { AssertNotConstant((ASTParam)sto); }
            else
            {
                //Should never happen as long as no new type is added
                throw new CheckerException("Unknown Identifier Type");
            }
        }
        private void AssertNotConstant(ASTStoDecl sto)
        {
            if (sto.Changemode == ChangeMode.CONST) { if (!IsInit) { throw new CheckerException("Can't modify constant storage '" + Ident + "'"); } }
        }
        private void AssertNotConstant(ASTParam sto)
        {
            if (sto.OptChangemode == null || sto.OptChangemode.Value == ChangeMode.CONST) { if (!IsInit) { throw new CheckerException("Can't modify constant parameter '" + Ident + "'"); } } //Default Changemode is const!
            if (sto.FlowMode == FlowMode.IN && sto.OptMechmode != MechMode.COPY) { throw new CheckerException("Can't modify 'in ref' parameter '" + Ident + "'"); }
        }
        public override void GenerateLValue(uint block, ref uint loc, MachineCode mc, CheckerInformation info, bool deref = false, bool hasToBeLValue = true)
        {
            if (IsFuncCall)
            {
                throw new CheckerException("The result of a function can't be an LValue");
            }
            else
            {
                //Write address to the top of the stack
                if (info.CurrentNamespace != null &&
                    info.Namespaces.ContainsKey(info.CurrentNamespace) &&
                    info.Namespaces[info.CurrentNamespace].ContainsIdent(Ident))
                {
                    IASTStoDecl storage = info.Namespaces[info.CurrentNamespace][Ident];
                    throw new NotImplementedException("Local identifiers are not yet supported");
                    if (hasToBeLValue) { AssertNotConstant(storage); }
                    if (storage is ASTStoDecl || (storage is ASTParam && ((ASTParam)storage).OptMechmode == MechMode.COPY))
                    {
                        //Local Identifier or parameter with mechmode COPY
                        //TODO: vm.LoadRel(loc++, storage.Address);
                    }
                    else if (storage is ASTParam)
                    {
                        //If mechmode is null: default = Mechmode.REF
                        //Load parameter with mechmode REF
                        //TODO: vm.LoadRel(loc++, storage.Address); //Relative Address to fp
                        //TODO: vm.Deref(loc++); //Deref to get global Address
                        //With another Deref the value is loaded
                    }
                    else
                    {
                        //Should never happen as long as no new type is added
                        throw new CodeGenerationException("Unknown Identifier Type");
                    }
                }
                else if (info.Globals.ContainsIdent(Ident))
                {
                    IASTStoDecl storage = info.Globals[Ident];
                    if (hasToBeLValue) { AssertNotConstant(storage); }
                    //Value
                    if (deref)
                    {
                        mc[block, loc++] = new Command(Instructions.MOV_R_CM, (byte)MachineCode.Registers.A, (byte)storage.Address);
                    }
                    //Only address
                    else
                    {
                        mc[block, loc++] = new Command(Instructions.MOV_R_C, (byte)MachineCode.Registers.A, (byte)storage.Address);
                    }
                }
                else
                {
                    throw new CheckerException("Access of undeclared Identifier " + Ident);
                }
            }
        }
        public override void GenerateCode(uint block, ref uint loc, MachineCode mc, CheckerInformation info)
        {
            if (IsFuncCall)
            {
                if (info.ProcFuncs.ContainsIdent(Ident))
                {
                    ASTProcFuncDecl callee = info.ProcFuncs[Ident];
                    if (!callee.IsFunc) { throw new CheckerException("Calls inside expresssions can only be made to functions but never to procedures!"); }
                    ASTCmdCall.GenerateCallingCode(block, ref loc, mc, info, callee, OptInitOrExprList);
                }
                else
                {
                    throw new CheckerException("Call to unkown function '" + Ident + "'");
                }
            }
            else
            {
                GenerateLValue(block, ref loc, mc, info, true, false);
                mc[block, loc++] = new Command(Instructions.PUSH, (byte)MachineCode.Registers.A);
            }
        }

        public override Type GetExpressionType(CheckerInformation info)
        {
            if (IsFuncCall)
            {

                if (info.ProcFuncs.ContainsIdent(Ident))
                {
                    ASTProcFuncDecl callee = info.ProcFuncs[Ident];
                    if (!callee.IsFunc) { throw new CheckerException("Calls inside expresssions can only be made to functions but never to procedures!"); }
                    var paramIttr = callee.Params.GetEnumerator();
                    if (!paramIttr.MoveNext()) { throw new CheckerException("No return param found for function"); }
                    return paramIttr.Current.Type;
                }
                else
                {
                    throw new CheckerException("Call to unkown function '" + Ident + "'");
                }
            }
            else
            {
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

                throw new CheckerException("Use of unkwon identifier '" + Ident + "'");
            }
        }

        public override void GetUsedIdents(ScopeChecker.UsedIdents usedIdents)
        {
            if (IsFuncCall) { usedIdents.AddProcFuncIdent(Ident); }
            else { usedIdents.AddStoIdent(Ident, IsInit); }
        }
    }
}