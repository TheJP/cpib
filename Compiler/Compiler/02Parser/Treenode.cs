using Compiler._02Parser.AST;

namespace Compiler
{
    public interface Treenode
    {
        IASTNode ToAbstractSyntax();
    }
}
