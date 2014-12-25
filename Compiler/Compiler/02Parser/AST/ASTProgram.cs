using System.Collections.Generic;

using Compiler;
using Compiler._02Parser.AST;

public class ASTProgram : IASTNode
{
    public string Ident { get; set; }

    public IList<ASTParam> Params { get; set; }

    public List<ASTCpsDecl> Declarations { get; set; }

    public List<ASTCpsCmd> Commands { get; set; }

    public override string ToString()
    {
        return string.Format("Program {0}", Ident);
    }
}