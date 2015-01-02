using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Compiler
{
    class ProgramMain
    {
        static void Main(string[] args)
        {
            //TestVirtualMachine.Test(); return;
            try
            {
                //Scanner
                Scanner scanner = new Scanner();
                var list = scanner.Scan(new StreamReader("test01.iml"));
                Console.WriteLine("[" + String.Join(", ", list) + "]");
                Console.WriteLine();
                //Parser
                Parser parser = new Parser();
                var tree = parser.Parse(list);
                //Converter
                var ast = tree.ToAbstractSyntax();
                Console.WriteLine(ast.ToString());
                Console.WriteLine();
                if(!(ast is ASTProgram))
                {
                    throw new IVirtualMachine.InternalError("Generation of Abstract Syntax Tree failed.");
                }
                ASTProgram program = (ASTProgram)ast;
                //Checker
                CheckerInformation info = new CheckerInformation();
                ScopeChecker contextChecker = new ScopeChecker();
                contextChecker.Check(program, info);
                //Code Generator
                IVirtualMachine vm = new VirtualMachine(1000, 1000);
                program.GenerateCode(0, vm, info);
                Console.WriteLine(vm.ToString());
                Console.WriteLine();
                //Executuion
                vm.Execute();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Failed: " + ex.Message);
            }
        }
    }
}
