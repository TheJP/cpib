using Compiler._02Parser.AST;

namespace Compiler
{
    public class ASTIf : ASTCpsCmd
    {
        public IASTNode Condition { get; set; }

        public IASTNode TrueCommand { get; set; }

        public IASTNode FalseCommand { get; set; }

        public override string ToString()
        {
            if (FalseCommand is ASTEmpty)
            {
                return string.Format(@"if {0} then
    {1}
endif", Condition, TrueCommand);
            }
            else
            {
                return string.Format(@"if {0} then
    {1}
else
    {2}
endif", Condition, TrueCommand, FalseCommand);
            }
        }
    }
}