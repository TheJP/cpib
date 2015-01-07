using System;
using System.Collections.Generic;

namespace Compiler
{
    public class ASTParam : IASTNode, IASTStoDecl
    {
        public ASTParam()
        {
            NextParam = new ASTEmpty();
            AddressLocation = null;
        }
        public string Ident { get; set; }

        public IASTNode NextParam { get; set; }

        public Type Type { get; set; }

        public int Address { get; set; }

        /// <summary>
        /// Location where the address of this identifier is stored.
        /// This is only used for: out copy and inout copy parameters
        /// </summary>
        public int? AddressLocation { get; set; }


        public FlowMode? FlowMode { get; set; }

        public ChangeMode? OptChangemode { get; set; }

        public MechMode? OptMechmode { get; set; }

        public override string ToString()
        {
            return String.Format("{0} {1} {2} {3}", this.OptChangemode, this.FlowMode, this.Type, this.Ident);
        }

        public void GenerateCode(uint block, ref uint loc, MachineCode mc, CheckerInformation info)
        {
            throw new CheckerException("ASTParam.GenerateCode was called. This should never happen!");
        }
        public void GetUsedIdents(ScopeChecker.UsedIdents usedIdents)
        {
            if (FlowMode != null && (FlowMode.Value == Compiler.FlowMode.IN || FlowMode.Value == Compiler.FlowMode.INOUT))
            {
                usedIdents.AddStoIdent(Ident, true); //In params are initialized
            }
        }
    }
}