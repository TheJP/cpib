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
   | THEN
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
   | THEN => "THEN"
   | WHILE => "WHILE"
   | IDENT => "IDENT"
   | SENTINEL => "SENTINEL"

datatype nonterm
  = program
   | progParamList
   | optProgParamList
   | repProgParamList
   | cpsCmd
   | repCpsCmd
   | cmd
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
   | expr
   | exprList
   | optGlobInits
   | repIdents
   | term1
   | term2
   | term3
   | repTerm1
   | repTerm2
   | repTerm3
   | factor
   | repFactor
   | optInitOrExprList
   | monadicOpr
   | optExprList
   | repExprList

val string_of_nonterm =
  fn program => "program"
   | progParamList => "progParamList"
   | optProgParamList => "optProgParamList"
   | repProgParamList => "repProgParamList"
   | cpsCmd => "cpsCmd"
   | repCpsCmd => "repCpsCmd"
   | cmd => "cmd"
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
   | expr => "expr"
   | exprList => "exprList"
   | optGlobInits => "optGlobInits"
   | repIdents => "repIdents"
   | term1 => "term1"
   | term2 => "term2"
   | term3 => "term3"
   | repTerm1 => "repTerm1"
   | repTerm2 => "repTerm2"
   | repTerm3 => "repTerm3"
   | factor => "factor"
   | repFactor => "repFactor"
   | optInitOrExprList => "optInitOrExprList"
   | monadicOpr => "monadicOpr"
   | optExprList => "optExprList"
   | repExprList => "repExprList"

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
    ]),

    (cmd, [
        [T SKIP],
        [N expr, T BECOMES, N expr],
        [T IF, N expr, T THEN, N cpsCmd, T ELSE, N cpsCmd, T ENDIF],
        [T WHILE, N expr, T DO, N cpsCmd, T ENDWHILE],
        [T CALL, T IDENT, N exprList, N optGlobInits],
        [T DEBUGIN, N expr],
        [T DEBUGOUT, N expr]
    ]),

    (cpsCmd, [
        [N cmd, N repCpsCmd]
    ]),

    (repCpsCmd, [
        [T SEMICOLON, N cmd, N repCpsCmd],
        []
    ]),

    (optGlobInits, [
        [T INIT, T IDENT, N repIdents],
        []
    ]),

    (repIdents, [
        [T COMMA, T IDENT, N repIdents],
        []
    ]),

    (expr, [
        [N term1, N repTerm1]
    ]),

    (repTerm1, [
        [T BOOLOPR, N term1, N repTerm1],
        []
    ]),

    (term1, [
        [N term2, N repTerm2]
    ]),

    (repTerm2, [
        [T RELOPR, N term2, N repTerm2],
        []
    ]),

    (term2, [
        [N term3, N repTerm3]
    ]),

    (repTerm3, [
        [T ADDOPR, N term3, N repTerm3],
        []
    ]),

    (term3, [
        [N factor, N repFactor]
    ]),

    (repFactor, [
        [T MULTOPR, N factor, N repFactor],
        []
    ]),

    (factor, [
        [T LITERAL],
        [T IDENT, N optInitOrExprList],
        [N monadicOpr, N factor],
        [T LPAREN, N expr, T RPAREN]
    ]),

    (optInitOrExprList, [
        [T INIT],
        [N exprList],
        []
    ]),

    (monadicOpr, [
        [T NOT],
        [T ADDOPR]
    ]),

    (exprList, [
        [T LPAREN, N optExprList, T RPAREN]
    ]),

    (optExprList, [
        [N expr, N repExprList],
        []
    ]),

    (repExprList, [
        [T COMMA, N expr, N repExprList],
        []
    ])
]

val S = program

val result = fix_foxi productions S string_of_gramsym

end (* local *)
