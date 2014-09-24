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
        /// Runs a state machine to execute the lexical analysis
        /// </summary>
        /// <param name="reader">IML source code input</param>
        /// <returns>Token list as result of the lexical analysis</returns>
        public IList<Token> Scan(StreamReader reader)
        {
            lock (this)
            {
                TokenList = new List<Token>();
                CurrentState = new DefaultState();
                while (!reader.EndOfStream)
                {
                    string line = reader.ReadLine();
                    foreach (char c in line)
                    {
                        CurrentState.Handle(this, c);
                    }
                }
                CurrentState.Handle(this, ' '); //More user friendly than to require a new line at the end of the file
                if (!(CurrentState is DefaultState)) { throw new LexicalException("Unexpected EOF (end of file)"); }
                return TokenList;
            }
        }
        /// <summary>
        /// Add given Token to the token list
        /// </summary>
        /// <param name="token">Token to append to the token list</param>
        public void AddToken(Token token)
        {
            TokenList.Add(token);
        }
    }
}
