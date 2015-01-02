using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Compiler
{
    public interface Checker
    {
        /// <summary>
        /// Executes the checker.
        /// </summary>
        /// <param name="root">Root node of the abstract syntax tree.</param>
        /// <param name="info">Info in which the checkers write optained information.</param>
        void Check(ASTProgram root, CheckerInformation info);
    }
}
