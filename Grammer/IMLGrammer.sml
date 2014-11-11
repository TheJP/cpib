datatype term
  = LPAREN
   | RPAREN
   | COMMA
   | SEMICOLON
   | COLON
   | BECOMES
   | MULTOPR
   | ADDOPR
   | RELOPR
   | BOOLOPR
   | TYPE
   | CALL
   | CHANGEMODE
   | MECHMODE
   | DEBUGIN
   | DEBUGOUT
   | DO
   | ELSE
   | ENDFUN
   | ENDIF
   | ENDPROC
   | ENDPROGRAM
   | ENDWHILE
   | LITERAL
   | FUN
   | GLOBAL
   | IF
   | FLOWMODE
   | INIT
   | LOCAL
   | NOT
   | PROC
   | PROGRAM
   | RETURNS
   | SKIP
   | WHILE
   | IDENT
   | SENTINEL

val string_of_term =
  fn LPAREN => "LPAREN"
   | RPAREN => "RPAREN"
   | COMMA => "COMMA"
   | SEMICOLON => "SEMICOLON"
   | COLON => "COLON"
   | BECOMES => "BECOMES"
   | MULTOPR => "MULTOPR"
   | ADDOPR => "ADDOPR"
   | RELOPR => "RELOPR"
   | BOOLOPR => "BOOLOPR"
   | TYPE => "TYPE"
   | CALL => "CALL"
   | CHANGEMODE => "CHANGEMODE"
   | MECHMODE => "MECHMODE"
   | DEBUGIN => "DEBUGIN"
   | DEBUGOUT => "DEBUGOUT"
   | DO => "DO"
   | ELSE => "ELSE"
   | ENDFUN => "ENDFUN"
   | ENDIF => "ENDIF"
   | ENDPROC => "ENDPROC"
   | ENDPROGRAM => "ENDPROGRAM"
   | ENDWHILE => "ENDWHILE"
   | LITERAL => "LITERAL"
   | FUN => "FUN"
   | GLOBAL => "GLOBAL"
   | IF => "IF"
   | FLOWMODE => "FLOWMODE"
   | INIT => "INIT"
   | LOCAL => "LOCAL"
   | NOT => "NOT"
   | PROC => "PROC"
   | PROGRAM => "PROGRAM"
   | RETURNS => "RETURNS"
   | SKIP => "SKIP"
   | WHILE => "WHILE"
   | IDENT => "IDENT"
   | SENTINEL => "SENTINEL"

datatype nonterm
  = program
   | progParamList
   | cpsCmd
   | cpsDecl
   | optCpsDecl
   | repCpsDecl
   | decl
   | stoDecl
   | funDecl
   | procDecl
   | typedIdent
   | paramList
   | globImps
   | optGlobImps
   | globImp
   | repGlobImps
   | cpsStoDecl
   | optCpsStoDecl
   | repCpsStoDecl
   | optChangemode

val string_of_nonterm =
  fn program => "program"
   | progParamList => "progParamList"
   | cpsCmd => "cpsCmd"
   | cpsDecl => "cpsDecl"
   | optCpsDecl => "optCpsDecl"
   | repCpsDecl => "repCpsDecl"
   | decl => "decl"
   | stoDecl => "stoDecl"
   | funDecl => "funDecl"
   | procDecl => "procDecl"
   | typedIdent => "typedIdent"
   | paramList => "paramList"
   | globImps => "globImps"
   | optGlobImps => "optGlobImps"
   | globImp => "globImp"
   | repGlobImps => "repGlobImps"
   | cpsStoDecl => "cpsStoDecl"
   | optCpsStoDecl => "optCpsStoDecl"
   | repCpsStoDecl => "repCpsStoDecl"
   | optChangemode => "optChangemode"

val string_of_gramsym = (string_of_term, string_of_nonterm)

local
  open FixFoxi.FixFoxiCore
in

val productions =
[
    (program, [
        [T PROGRAM, T IDENT, N progParamList, N optCpsDecl, T DO, N cpsCmd, T ENDPROGRAM]
    ]),

    (decl, [
        [N stoDecl],
        [N funDecl],
        [N procDecl]
    ]),

    (stoDecl, [
        [              N typedIdent],
        [T CHANGEMODE, N typedIdent]
    ]),

    (funDecl, [
        [T FUN, T IDENT, N paramList, T RETURNS, N stoDecl, N optGlobImps, N optCpsStoDecl, T DO, N cpsCmd, T ENDFUN]
    ]),

    (procDecl, [
        [T PROC, T IDENT, N paramList, N optGlobImps, N optCpsStoDecl, T DO, N cpsCmd, T ENDPROC]
    ]),

    (optGlobImps, [
        [T GLOBAL, N globImps],
        []
    ]),

    (globImps, [
        [N globImp, N repGlobImps]
    ]),

    (repGlobImps, [
        [T COMMA, N globImp, N repGlobImps],
        []
    ]),

    (optChangemode, [
        [T CHANGEMODE],
        []
    ]),

    (globImp, [
        [N optChangemode, T IDENT],
        [T FLOWMODE, N optChangemode, T IDENT]
    ]),

    (optCpsDecl, [
        [T GLOBAL, N cpsDecl],
        []
    ]),

    (cpsDecl, [
        [N decl, N repCpsDecl]
    ]),

    (repCpsDecl, [
        [T SEMICOLON, N decl, N repCpsDecl],
        []
    ]),

    (optCpsStoDecl, [
        [T LOCAL, N cpsStoDecl],
        []
    ]),

    (cpsStoDecl, [
        [N stoDecl, N repCpsStoDecl]
    ]),

    (repCpsStoDecl, [
        [T SEMICOLON, N stoDecl, N repCpsStoDecl],
        []
    ])
]

val S = expr

val result = fix_foxi productions S string_of_gramsym

end (* local *)
