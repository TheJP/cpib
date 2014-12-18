using System.Collections.Generic;

using Compiler._02Parser.AST;

public class ASTProgram : IASTNode
{
    public string Ident { get; set; }

    public IASTNode Params { get; set; }

    public IASTNode CpsDecls { get; set; }

    public IASTNode Commands { get; set; }
}