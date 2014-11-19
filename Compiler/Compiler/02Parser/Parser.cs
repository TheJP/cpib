using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Compiler
{
    public class Parser
    {
        public IList<Treenode> Parse(IList<Token> tokenList)
        {
            IEnumerator<Token> tokenItr = tokenList.GetEnumerator();
            Stack<ParserSymbol> parserStack = new Stack<ParserSymbol>();
            IList<Treenode> nodes = new List<Treenode>();
            Productions productions = new Productions();
            Treenode current = productions.Factories[NotTerminals.program][Terminals.PROGRAM]();
            nodes.Add(current);
            parserStack.Push(new ParserSymbol(productions.ParseTable[NotTerminals.program][Terminals.PROGRAM][0], current));
            while (parserStack.Count != 0 && tokenItr.MoveNext())
            {
                ParserSymbol currSymbol = parserStack.Pop();
                if (currSymbol.IsNotTerminal)
                {
                    if(!productions.ParseTable.ContainsKey(currSymbol.S.NotTerminal.Value) ||
                        !productions.ParseTable[currSymbol.S.NotTerminal.Value].ContainsKey(tokenItr.Current.Terminal))
                    {
                        throw new GrammarException(String.Format("Row: {0} Col: {1} Msg: Unexpected token: {2}", tokenItr.Current.Row, tokenItr.Current.Column, tokenItr.Current));
                    }
                    Symbol[] production = productions.ParseTable[currSymbol.S.NotTerminal.Value][tokenItr.Current.Terminal];
                    current = productions.Factories[currSymbol.S.NotTerminal.Value][tokenItr.Current.Terminal]();
                    foreach (Symbol s in production)
                    {
                        parserStack.Push(new ParserSymbol(s, current));
                    }
                }
            }
            return nodes;
        }
    }
}
