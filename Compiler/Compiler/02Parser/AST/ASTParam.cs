using System;

using Compiler._02Parser.AST;

namespace Compiler
{
    public class ASTParam : IASTNode, IASTDecl, IASTStoDecl
    {
        public ASTParam()
        {
            NextParam = new ASTEmpty();
        }

        public IASTNode NextParam { get; set; }

        public Type Type { get; set; }

        public string Ident { get; set; }

        public FlowMode? FlowMode { get; set; }

        public ChangeMode? OptChangemode { get; set; }

        public MechMode? OptMechmode { get; set; }

        public override string ToString()
        {
            return String.Format("{0} {1} {2} {3}", this.OptChangemode, this.FlowMode, this.Type, this.Ident);
        }
    }
}