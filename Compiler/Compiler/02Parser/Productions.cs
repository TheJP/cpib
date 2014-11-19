using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Compiler
{
    public class Productions
    {
        public IDictionary<NotTerminals, IDictionary<Terminals, Symbol[]>> ParseTable { get; private set; }
        public IDictionary<NotTerminals, IDictionary<Terminals, Func<Treenode>>> Factories { get; private set; }
        public Productions()
        {
            //* Examples *//
            ParseTable = new Dictionary<NotTerminals, IDictionary<Terminals, Symbol[]>>(); //Fix
            ParseTable[NotTerminals.expr] = new Dictionary<Terminals, Symbol[]>(); //For each NTS
            ParseTable[NotTerminals.expr][Terminals.ADDOPR] = new Symbol[]{ //For each Production
                new Symbol(Terminals.ADDOPR, (p,c) => {}),
                new Symbol(NotTerminals.expr, (p,c) => {})
            };
            ParseTable[NotTerminals.program][Terminals.PROGRAM] = new Symbol[]{
                new Symbol(Terminals.PROGRAM, (p,c) => {}),
                new Symbol(Terminals.IDENT, (p,c) => {}),
                //new Symbol(NotTerminals.expr, t => ((ProgramProgram)t).programList)
            };
            Factories = new Dictionary<NotTerminals, IDictionary<Terminals, Func<Treenode>>>(); //Fix
            Factories[NotTerminals.program] = new Dictionary<Terminals, Func<Treenode>>(); //Foreach NTS
            Factories[NotTerminals.program][Terminals.PROGRAM] = () => { return new ProgramPROGRAM(); }; //For each Production


            //******************************************//
            //* Test Grammar                           *//
            //******************************************//
            //* <program> := PROGRAM <cmds> ENDPROGRAM *//
            //* <cmds>    := IDENT <repCmd>            *//
            //* <repCmd>  := SEMICOLON IDENT <repCmd>  *//
            //* <repCmd>  :=                           *//
            //******************************************//

            //* Productions *//

            ParseTable = new Dictionary<NotTerminals, IDictionary<Terminals, Symbol[]>>();
            ParseTable[NotTerminals.program] = new Dictionary<Terminals, Symbol[]>();
            ParseTable[NotTerminals.program][Terminals.PROGRAM] = new Symbol[]{
                new Symbol(Terminals.PROGRAM, (p,s) => {}),
                new Symbol(NotTerminals.cmds, (p,s) => {}),
                new Symbol(Terminals.PROGRAM, (p,s) => {}),
            };

            ParseTable[NotTerminals.cmds] = new Dictionary<Terminals, Symbol[]>();
            ParseTable[NotTerminals.cmds][Terminals.IDENT] = new Symbol[]{
                new Symbol(Terminals.IDENT, (p,s) => {}),
                new Symbol(NotTerminals.repCmd, (p,s) => {}),
            };

            ParseTable[NotTerminals.repCmd] = new Dictionary<Terminals, Symbol[]>();
            ParseTable[NotTerminals.repCmd][Terminals.SEMICOLON] = new Symbol[]{
                new Symbol(Terminals.SEMICOLON, (p,s) => {}),
                new Symbol(Terminals.IDENT, (p,s) => {}),
                new Symbol(NotTerminals.repCmd, (p,s) => {}),
            };
            ParseTable[NotTerminals.repCmd][Terminals.ENDPROGRAM] = new Symbol[]{};

            //* Factories *//

            Factories = new Dictionary<NotTerminals, IDictionary<Terminals, Func<Treenode>>>();
            Factories[NotTerminals.program] = new Dictionary<Terminals, Func<Treenode>>();
            Factories[NotTerminals.program][Terminals.PROGRAM] = () => { return new ProgramPROGRAM(); };

            Factories[NotTerminals.cmds] = new Dictionary<Terminals, Func<Treenode>>();
            Factories[NotTerminals.cmds][Terminals.IDENT] = () => { return new CmdsIDENT(); };

            Factories[NotTerminals.repCmd] = new Dictionary<Terminals, Func<Treenode>>();
            Factories[NotTerminals.repCmd][Terminals.SEMICOLON] = () => { return new RepCmdSEMICOLON(); };
            Factories[NotTerminals.repCmd][Terminals.ENDPROGRAM] = () => { return new RepCmdENDPROGRAM(); };
        }
    }
    //Example
    public interface Expr : Treenode { } //For each NTS
    public class ExprAddopr : Expr { } //For each Production
    //Test
    public interface Program : Treenode { }
    public interface Cmds : Treenode { }
    public interface RepCmd : Treenode { }
    public class ProgramPROGRAM : Program { public Treenode PROGRAM { get; set; } public Cmds Cmds { get; set; } public Treenode ENDPROGRAM { get; set; } }
    public class CmdsIDENT : Cmds { public Treenode IDENT { get; set; } public RepCmd RepCmd { get; set; } }
    public class RepCmdSEMICOLON : RepCmd { public Treenode SEMICOLON { get; set; } public Treenode IDENT { get; set; } public RepCmd RepCmd { get; set; } }
    public class RepCmdENDPROGRAM : RepCmd { }
}
