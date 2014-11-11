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
   | optProgParamList
   | repProgParamList
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
   | optParamList
   | repParamList
   | globImps
   | optGlobImps
   | globImp
   | repGlobImps
   | cpsStoDecl
   | optCpsStoDecl
   | repCpsStoDecl
   | optChangemode
   | optMechmode
   | progParam
   | param

val string_of_nonterm =
  fn program => "program"
   | progParamList => "progParamList"
   | optProgParamList => "optProgParamList"
   | repProgParamList => "repProgParamList"
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
   | optParamList => "optParamList"
   | repParamList => "repParamList"
   | globImps => "globImps"
   | optGlobImps => "optGlobImps"
   | globImp => "globImp"
   | repGlobImps => "repGlobImps"
   | cpsStoDecl => "cpsStoDecl"
   | optCpsStoDecl => "optCpsStoDecl"
   | repCpsStoDecl => "repCpsStoDecl"
   | optChangemode => "optChangemode"
   | optMechmode => "optMechmode"
   | progParam => "progParam"
   | param => "param"

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

    (optMechmode, [
        [T MECHMODE],
        []
    ]),

    (globImp, [
        [            N optChangemode, T IDENT],
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
    ]),

    (progParamList, [
        [T LPAREN, N optProgParamList, T RPAREN]
    ]),

    (optProgParamList, [
        [N progParam, N repProgParamList],
        []
    ]),

    (repProgParamList, [
        [T COMMA, N progParam, N repProgParamList],
        []
    ]),

    (progParam, [
        [            N optChangemode, N typedIdent],
        [T FLOWMODE, N optChangemode, N typedIdent]
    ]),

    (paramList, [
        [T LPAREN, N optParamList, T RPAREN]
    ]),

    (optParamList, [
        [N param, N repParamList],
        []
    ]),

    (repParamList, [
        [T COMMA, N param, N repParamList],
        []
    ]),

    (param, [
        [            N optMechmode, N optChangemode, N typedIdent],
        [T FLOWMODE, N optMechmode, N optChangemode, N typedIdent]
    ]),

    (typedIdent, [
        [T IDENT, T COLON, T TYPE]
    ])
]

val S = program

val result = fix_foxi productions S string_of_gramsym

end (* local *)
