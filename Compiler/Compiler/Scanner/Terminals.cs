using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Compiler
{
    public enum Terminals
    {
        ///<summary>(</summary>
        LPAREN,
        ///<summary>)</summary>
        RPAREN,
        ///<summary>,</summary>
        COMMA,
        ///<summary>;</summary>
        SEMICOLON,
        ///<summary>:</summary>
        COLON,
        ///<summary>:=</summary>
        BECOMES,
        ///<summary>*, div or mod</summary>
        MULTOPR,
        ///<summary>+ or -</summary>
        ADDOPR,
        ///<summary>=, /=, &lt;, &gt;, &lt;= or &gt;=</summary>
        RELOPR,
        ///<summary>&&, ||, &? or |?</summary>
        BOOLOPR,
        ///<summary>bool or int32</summary>
        TYPE,
        ///<summary>call</summary>
        CALL,
        ///<summary>const or var</summary>
        CHANGEMODE,
        ///<summary>copy or ref</summary>
        MECHMODE,
        ///<summary>debugin</summary>
        DEBUGIN,
        ///<summary>debugout</summary>
        DEBUGOUT,
        ///<summary>do</summary>
        DO,
        ///<summary>else</summary>
        ELSE,
        ///<summary>endfun (no fun for you, sorry)</summary>
        ENDFUN,
        ///<summary>endif</summary>
        ENDIF,
        ///<summary>endproc</summary>
        ENDPROC,
        ///<summary>endprogram</summary>
        ENDPROGRAM,
        ///<summary>endwhile</summary>
        ENDWHILE,
        ///<summary>false, true or (+|-)?[0-9]+</summary>
        LITERAL,
        ///<summary>fun (here, have some fun)</summary>
        FUN,
        ///<summary>global</summary>
        GLOBAL,
        ///<summary>if</summary>
        IF,
        ///<summary>in, inout or out</summary>
        FLOWMODE,
        ///<summary>init</summary>
        INIT,
        ///<summary>local</summary>
        LOCAL,
        ///<summary>not</summary>
        NOT,
        ///<summary>proc</summary>
        PROC,
        ///<summary>program</summary>
        PROGRAM,
        ///<summary>returns</summary>
        RETURNS,
        ///<summary>skip</summary>
        SKIP,
        ///<summary>then</summary>
        THEN,
        ///<summary>while</summary>
        WHILE,
        ///<summary>[a-zA-Z][a-zA-z0-9]*</summary>
        IDENT,
    }
}
