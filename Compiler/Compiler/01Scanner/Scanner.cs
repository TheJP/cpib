using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Compiler
{
    public class Scanner
    {
        /// <summary>
        /// Token list, which is the result of the state machine
        /// </summary>
        private IList<Token> TokenList { get; set; }
        /// <summary>
        /// Current State of the state machine
        /// </summary>
        public State CurrentState { get; set; }
        /// <summary>
        /// Definition of all keywords and corresponding tokens
        /// </summary>
        public Dictionary<string, Token> Keywords { get; private set; }
        /// <summary>
        /// Current row the scanner is in (starting by 1)
        /// </summary>
        public int Row { get; private set; }
        /// <summary>
        /// Current column the scanner is in (starting by 1)
        /// </summary>
        public int Col { get; private set; }
        public Scanner()
        {
            Keywords = new Dictionary<string, Token>();
            Keywords.Add("div", new OperatorToken(Terminals.MULTOPR, Operators.DIV));
            Keywords.Add("mod", new OperatorToken(Terminals.MULTOPR, Operators.MOD));
            Keywords.Add("bool", new TypeToken(Type.BOOL));
            Keywords.Add("int32", new TypeToken(Type.INT32));
            Keywords.Add("decimal", new TypeToken(Type.DECIMAL));
            Keywords.Add("call", new Token(Terminals.CALL));
            Keywords.Add("const", new ChangeModeToken(ChangeMode.CONST));
            Keywords.Add("var", new ChangeModeToken(ChangeMode.VAR));
            Keywords.Add("copy", new MechModeToken(MechMode.COPY));
            Keywords.Add("ref", new MechModeToken(MechMode.REF));
            Keywords.Add("debugin", new Token(Terminals.DEBUGIN));
            Keywords.Add("debugout", new Token(Terminals.DEBUGOUT));
            Keywords.Add("do", new Token(Terminals.DO));
            Keywords.Add("else", new Token(Terminals.ELSE));
            Keywords.Add("endfun", new Token(Terminals.ENDFUN));
            Keywords.Add("endif", new Token(Terminals.ENDIF));
            Keywords.Add("endproc", new Token(Terminals.ENDPROC));
            Keywords.Add("endprogram", new Token(Terminals.ENDPROGRAM));
            Keywords.Add("endwhile", new Token(Terminals.ENDWHILE));
            Keywords.Add("fun", new Token(Terminals.FUN)); //so funny
            Keywords.Add("global", new Token(Terminals.GLOBAL));
            Keywords.Add("if", new Token(Terminals.IF));
            Keywords.Add("in", new FlowModeToken(FlowMode.IN));
            Keywords.Add("inout", new FlowModeToken(FlowMode.INOUT));
            Keywords.Add("out", new FlowModeToken(FlowMode.OUT));
            Keywords.Add("init", new Token(Terminals.INIT));
            Keywords.Add("local", new Token(Terminals.LOCAL));
            Keywords.Add("not", new Token(Terminals.NOT));
            Keywords.Add("proc", new Token(Terminals.PROC));
            Keywords.Add("program", new Token(Terminals.PROGRAM));
            Keywords.Add("returns", new Token(Terminals.RETURNS));
            Keywords.Add("skip", new Token(Terminals.SKIP));
            Keywords.Add("then", new Token(Terminals.THEN));
            Keywords.Add("while", new Token(Terminals.WHILE));
        }
        /// <summary>
        /// Runs a state machine to execute the lexical analysis
        /// </summary>
        /// <param name="reader">IML source code input</param>
        /// <returns>Token list as result of the lexical analysis</returns>
        public IList<Token> Scan(StreamReader reader)
        {
            lock (this)
            {
                TokenList = new List<Token>();
                try
                {
                    CurrentState = new DefaultState();
                    Row = 0;
                    while (!reader.EndOfStream)
                    {
                        ++Row; Col = 0;
                        string line = reader.ReadLine();
                        foreach (char c in line)
                        {
                            ++Col;
                            CurrentState.Handle(this, c);
                        }
                        CurrentState.Handle(this, '\n'); //newline char is omitted in the ReadLine method
                    }
                    CurrentState.Handle(this, '\n'); //More user friendly than to require a new line at the end of the file
                    if (!(CurrentState is DefaultState)) { throw new LexicalException("Unexpected EOF (end of file)"); }
                }
                catch (LexicalException ex)
                {
                    throw new LexicalException(String.Format("Row: {0} Col: {1} Msg: {2}", Row, Col, ex.Message));
                }
                finally
                {
                    reader.Close();
                }
                return TokenList;
            }
        }
        /// <summary>
        /// Add given Token to the token list
        /// </summary>
        /// <param name="token">Token to append to the token list</param>
        public void AddToken(Token token)
        {
            token.Row = Row;
            token.Column = Col;
            TokenList.Add(token);
        }
    }
}
