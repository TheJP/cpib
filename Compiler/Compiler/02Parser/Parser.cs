using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Compiler
{
    public class Parser
    {
        public Treenode Parse(IList<Token> tokenList)
        {
            //Token Enumerator: Input of Tokens for the parser
            IEnumerator<Token> tokenItr = tokenList.GetEnumerator();
            //Stack for the recursion
            Stack<ParserSymbol> parserStack = new Stack<ParserSymbol>();
            Treenode root;
            Productions productions = new Productions();
            //Set up recursion with Start NTS program
            Treenode current = productions.Factories[NotTerminals.program][Terminals.PROGRAM](); root = current;
            foreach(Symbol s in productions.ParseTable[NotTerminals.program][Terminals.PROGRAM].Reverse()){
                parserStack.Push(new ParserSymbol(s, current));
            }
            //Get 1. Token
            bool hasToken = tokenItr.MoveNext();
            //Recursion until there are no more productions (until ENDPROGRAM is hit)
            //or until there are no more Tokens in the TokenList
            while (parserStack.Count != 0 && hasToken)
            {
                ParserSymbol currSymbol = parserStack.Pop();
                if (currSymbol.IsNotTerminal)
                {
                    //If the symbol is a NTS: Add all Symbols of the correct parse table entry to the recursion
                    //(next iteration will begin with the first symbol of this parse table entry)
                    if(!productions.ParseTable.ContainsKey(currSymbol.S.NotTerminal.Value) ||
                        !productions.ParseTable[currSymbol.S.NotTerminal.Value].ContainsKey(tokenItr.Current.Terminal))
                    {
                        //Throw a GrammarException if the needed parse table entry does not exist (eta entries count as existing entries)
                        throw new GrammarException(String.Format("Row: {0} Col: {1} Msg: Unexpected token: {2}", tokenItr.Current.Row, tokenItr.Current.Column, tokenItr.Current));
                    }
                    Symbol[] production = productions.ParseTable[currSymbol.S.NotTerminal.Value][tokenItr.Current.Terminal];
                    current = productions.Factories[currSymbol.S.NotTerminal.Value][tokenItr.Current.Terminal]();
                    foreach (Symbol s in production.Reverse())
                    {
                        parserStack.Push(new ParserSymbol(s, current));
                    }
                }
                else
                {
                    //Add Token to the tree
                    if (currSymbol.S.Terminal != tokenItr.Current.Terminal)
                    {
                        //Throw a GrammarException, if the token, which (grammaticaly) has to be the next token, is not the next token
                        throw new GrammarException(String.Format("Row: {0} Col: {1} Msg: Unexpected token: {2}", tokenItr.Current.Row, tokenItr.Current.Column, tokenItr.Current));
                    }
                    current = new Tokennode(tokenItr.Current);
                    //Get next Token (The current Token was consumed)
                    hasToken = tokenItr.MoveNext();
                }
                currSymbol.ExecuteCallback(current);
            }
            //Throw an exception if there are too many or to few tokens to consume
            if (parserStack.Count != 0) { throw new GrammarException("Unexpected end of program"); }
            if (hasToken) { throw new GrammarException(String.Format("Row: {0} Col: {1} Msg: Invalid token after end of program: {2}", tokenItr.Current.Row, tokenItr.Current.Column, tokenItr.Current)); }
            return root;
        }
    }
}
