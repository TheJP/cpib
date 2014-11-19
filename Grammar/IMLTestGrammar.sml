datatype term
  = SEMICOLON
   | ENDPROGRAM
   | PROGRAM
   | IDENT

val string_of_term =
  fn SEMICOLON => "SEMICOLON"
   | ENDPROGRAM => "ENDPROGRAM"
   | PROGRAM => "PROGRAM"
   | IDENT => "IDENT"

datatype nonterm
  = program
   | cmds
   | repCmd

val string_of_nonterm =
  fn program => "program"
   | cmds => "cmds"
   | repCmd => "repCmd"

val string_of_gramsym = (string_of_term, string_of_nonterm)

local
  open FixFoxi.FixFoxiCore
in

val productions =
[
    (program, [
        [T PROGRAM, N cmds, T ENDPROGRAM]
    ]),

    (cmds, [
        [T IDENT, N repCmd]
    ]),

    (repCmd, [
        [T SEMICOLON, T IDENT, N repCmd],
        []
    ])
]

val S = program

val result = fix_foxi productions S string_of_gramsym

end (* local *)
