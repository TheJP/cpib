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
            //* Productions *//

            ParseTable = new Dictionary<NotTerminals, IDictionary<Terminals, Symbol[]>>();

            ParseTable[NotTerminals.program] = new Dictionary<Terminals, Symbol[]>();
            ParseTable[NotTerminals.program][Terminals.PROGRAM] = new Symbol[]{
    new Symbol(Terminals.PROGRAM, (p,s) => { ((ProgramPROGRAM)p).PROGRAM = (Tokennode)s; }),
    new Symbol(Terminals.IDENT, (p,s) => { ((ProgramPROGRAM)p).IDENT = (Tokennode)s; }),
    new Symbol(NotTerminals.progParamList, (p,s) => { ((ProgramPROGRAM)p).ProgParamList = (ProgParamList)s; }),
    new Symbol(NotTerminals.optCpsDecl, (p,s) => { ((ProgramPROGRAM)p).OptCpsDecl = (OptCpsDecl)s; }),
    new Symbol(Terminals.DO, (p,s) => { ((ProgramPROGRAM)p).DO = (Tokennode)s; }),
    new Symbol(NotTerminals.cpsCmd, (p,s) => { ((ProgramPROGRAM)p).CpsCmd = (CpsCmd)s; }),
    new Symbol(Terminals.ENDPROGRAM, (p,s) => { ((ProgramPROGRAM)p).ENDPROGRAM = (Tokennode)s; }),
};

            ParseTable[NotTerminals.decl] = new Dictionary<Terminals, Symbol[]>();
            ParseTable[NotTerminals.decl][Terminals.CHANGEMODE] = new Symbol[]{
    new Symbol(NotTerminals.stoDecl, (p,s) => { ((DeclCHANGEMODE)p).StoDecl = (StoDecl)s; }),
};

            ParseTable[NotTerminals.decl][Terminals.IDENT] = new Symbol[]{
    new Symbol(NotTerminals.stoDecl, (p,s) => { ((DeclIDENT)p).StoDecl = (StoDecl)s; }),
};

            ParseTable[NotTerminals.decl][Terminals.FUN] = new Symbol[]{
    new Symbol(NotTerminals.funDecl, (p,s) => { ((DeclFUN)p).FunDecl = (FunDecl)s; }),
};

            ParseTable[NotTerminals.decl][Terminals.PROC] = new Symbol[]{
    new Symbol(NotTerminals.procDecl, (p,s) => { ((DeclPROC)p).ProcDecl = (ProcDecl)s; }),
};

            ParseTable[NotTerminals.stoDecl] = new Dictionary<Terminals, Symbol[]>();
            ParseTable[NotTerminals.stoDecl][Terminals.IDENT] = new Symbol[]{
    new Symbol(NotTerminals.typedIdent, (p,s) => { ((StoDeclIDENT)p).TypedIdent = (TypedIdent)s; }),
};

            ParseTable[NotTerminals.stoDecl][Terminals.CHANGEMODE] = new Symbol[]{
    new Symbol(Terminals.CHANGEMODE, (p,s) => { ((StoDeclCHANGEMODE)p).CHANGEMODE = (Tokennode)s; }),
    new Symbol(NotTerminals.typedIdent, (p,s) => { ((StoDeclCHANGEMODE)p).TypedIdent = (TypedIdent)s; }),
};

            ParseTable[NotTerminals.funDecl] = new Dictionary<Terminals, Symbol[]>();
            ParseTable[NotTerminals.funDecl][Terminals.FUN] = new Symbol[]{
    new Symbol(Terminals.FUN, (p,s) => { ((FunDeclFUN)p).FUN = (Tokennode)s; }),
    new Symbol(Terminals.IDENT, (p,s) => { ((FunDeclFUN)p).IDENT = (Tokennode)s; }),
    new Symbol(NotTerminals.paramList, (p,s) => { ((FunDeclFUN)p).ParamList = (ParamList)s; }),
    new Symbol(Terminals.RETURNS, (p,s) => { ((FunDeclFUN)p).RETURNS = (Tokennode)s; }),
    new Symbol(NotTerminals.stoDecl, (p,s) => { ((FunDeclFUN)p).StoDecl = (StoDecl)s; }),
    new Symbol(NotTerminals.optGlobImps, (p,s) => { ((FunDeclFUN)p).OptGlobImps = (OptGlobImps)s; }),
    new Symbol(NotTerminals.optCpsStoDecl, (p,s) => { ((FunDeclFUN)p).OptCpsStoDecl = (OptCpsStoDecl)s; }),
    new Symbol(Terminals.DO, (p,s) => { ((FunDeclFUN)p).DO = (Tokennode)s; }),
    new Symbol(NotTerminals.cpsCmd, (p,s) => { ((FunDeclFUN)p).CpsCmd = (CpsCmd)s; }),
    new Symbol(Terminals.ENDFUN, (p,s) => { ((FunDeclFUN)p).ENDFUN = (Tokennode)s; }),
};

            ParseTable[NotTerminals.procDecl] = new Dictionary<Terminals, Symbol[]>();
            ParseTable[NotTerminals.procDecl][Terminals.PROC] = new Symbol[]{
    new Symbol(Terminals.PROC, (p,s) => { ((ProcDeclPROC)p).PROC = (Tokennode)s; }),
    new Symbol(Terminals.IDENT, (p,s) => { ((ProcDeclPROC)p).IDENT = (Tokennode)s; }),
    new Symbol(NotTerminals.paramList, (p,s) => { ((ProcDeclPROC)p).ParamList = (ParamList)s; }),
    new Symbol(NotTerminals.optGlobImps, (p,s) => { ((ProcDeclPROC)p).OptGlobImps = (OptGlobImps)s; }),
    new Symbol(NotTerminals.optCpsStoDecl, (p,s) => { ((ProcDeclPROC)p).OptCpsStoDecl = (OptCpsStoDecl)s; }),
    new Symbol(Terminals.DO, (p,s) => { ((ProcDeclPROC)p).DO = (Tokennode)s; }),
    new Symbol(NotTerminals.cpsCmd, (p,s) => { ((ProcDeclPROC)p).CpsCmd = (CpsCmd)s; }),
    new Symbol(Terminals.ENDPROC, (p,s) => { ((ProcDeclPROC)p).ENDPROC = (Tokennode)s; }),
};

            ParseTable[NotTerminals.optGlobImps] = new Dictionary<Terminals, Symbol[]>();
            ParseTable[NotTerminals.optGlobImps][Terminals.GLOBAL] = new Symbol[]{
    new Symbol(Terminals.GLOBAL, (p,s) => { ((OptGlobImpsGLOBAL)p).GLOBAL = (Tokennode)s; }),
    new Symbol(NotTerminals.globImps, (p,s) => { ((OptGlobImpsGLOBAL)p).GlobImps = (GlobImps)s; }),
};

            ParseTable[NotTerminals.optGlobImps][Terminals.DO] = new Symbol[]{
};

            ParseTable[NotTerminals.optGlobImps][Terminals.LOCAL] = new Symbol[]{
};

            ParseTable[NotTerminals.globImps] = new Dictionary<Terminals, Symbol[]>();
            ParseTable[NotTerminals.globImps][Terminals.FLOWMODE] = new Symbol[]{
    new Symbol(NotTerminals.globImp, (p,s) => { ((GlobImpsFLOWMODE)p).GlobImp = (GlobImp)s; }),
    new Symbol(NotTerminals.repGlobImps, (p,s) => { ((GlobImpsFLOWMODE)p).RepGlobImps = (RepGlobImps)s; }),
};

            ParseTable[NotTerminals.globImps][Terminals.IDENT] = new Symbol[]{
    new Symbol(NotTerminals.globImp, (p,s) => { ((GlobImpsIDENT)p).GlobImp = (GlobImp)s; }),
    new Symbol(NotTerminals.repGlobImps, (p,s) => { ((GlobImpsIDENT)p).RepGlobImps = (RepGlobImps)s; }),
};

            ParseTable[NotTerminals.globImps][Terminals.CHANGEMODE] = new Symbol[]{
    new Symbol(NotTerminals.globImp, (p,s) => { ((GlobImpsCHANGEMODE)p).GlobImp = (GlobImp)s; }),
    new Symbol(NotTerminals.repGlobImps, (p,s) => { ((GlobImpsCHANGEMODE)p).RepGlobImps = (RepGlobImps)s; }),
};

            ParseTable[NotTerminals.repGlobImps] = new Dictionary<Terminals, Symbol[]>();
            ParseTable[NotTerminals.repGlobImps][Terminals.COMMA] = new Symbol[]{
    new Symbol(Terminals.COMMA, (p,s) => { ((RepGlobImpsCOMMA)p).COMMA = (Tokennode)s; }),
    new Symbol(NotTerminals.globImp, (p,s) => { ((RepGlobImpsCOMMA)p).GlobImp = (GlobImp)s; }),
    new Symbol(NotTerminals.repGlobImps, (p,s) => { ((RepGlobImpsCOMMA)p).RepGlobImps = (RepGlobImps)s; }),
};

            ParseTable[NotTerminals.repGlobImps][Terminals.DO] = new Symbol[]{
};

            ParseTable[NotTerminals.repGlobImps][Terminals.LOCAL] = new Symbol[]{
};

            ParseTable[NotTerminals.optChangemode] = new Dictionary<Terminals, Symbol[]>();
            ParseTable[NotTerminals.optChangemode][Terminals.CHANGEMODE] = new Symbol[]{
    new Symbol(Terminals.CHANGEMODE, (p,s) => { ((OptChangemodeCHANGEMODE)p).CHANGEMODE = (Tokennode)s; }),
};

            ParseTable[NotTerminals.optChangemode][Terminals.IDENT] = new Symbol[]{
};

            ParseTable[NotTerminals.optMechmode] = new Dictionary<Terminals, Symbol[]>();
            ParseTable[NotTerminals.optMechmode][Terminals.MECHMODE] = new Symbol[]{
    new Symbol(Terminals.MECHMODE, (p,s) => { ((OptMechmodeMECHMODE)p).MECHMODE = (Tokennode)s; }),
};

            ParseTable[NotTerminals.optMechmode][Terminals.IDENT] = new Symbol[]{
};

            ParseTable[NotTerminals.optMechmode][Terminals.CHANGEMODE] = new Symbol[]{
};

            ParseTable[NotTerminals.globImp] = new Dictionary<Terminals, Symbol[]>();
            ParseTable[NotTerminals.globImp][Terminals.IDENT] = new Symbol[]{
    new Symbol(NotTerminals.optChangemode, (p,s) => { ((GlobImpIDENT)p).OptChangemode = (OptChangemode)s; }),
    new Symbol(Terminals.IDENT, (p,s) => { ((GlobImpIDENT)p).IDENT = (Tokennode)s; }),
};

            ParseTable[NotTerminals.globImp][Terminals.CHANGEMODE] = new Symbol[]{
    new Symbol(NotTerminals.optChangemode, (p,s) => { ((GlobImpCHANGEMODE)p).OptChangemode = (OptChangemode)s; }),
    new Symbol(Terminals.IDENT, (p,s) => { ((GlobImpCHANGEMODE)p).IDENT = (Tokennode)s; }),
};

            ParseTable[NotTerminals.globImp][Terminals.FLOWMODE] = new Symbol[]{
    new Symbol(Terminals.FLOWMODE, (p,s) => { ((GlobImpFLOWMODE)p).FLOWMODE = (Tokennode)s; }),
    new Symbol(NotTerminals.optChangemode, (p,s) => { ((GlobImpFLOWMODE)p).OptChangemode = (OptChangemode)s; }),
    new Symbol(Terminals.IDENT, (p,s) => { ((GlobImpFLOWMODE)p).IDENT = (Tokennode)s; }),
};

            ParseTable[NotTerminals.optCpsDecl] = new Dictionary<Terminals, Symbol[]>();
            ParseTable[NotTerminals.optCpsDecl][Terminals.GLOBAL] = new Symbol[]{
    new Symbol(Terminals.GLOBAL, (p,s) => { ((OptCpsDeclGLOBAL)p).GLOBAL = (Tokennode)s; }),
    new Symbol(NotTerminals.cpsDecl, (p,s) => { ((OptCpsDeclGLOBAL)p).CpsDecl = (CpsDecl)s; }),
};

            ParseTable[NotTerminals.optCpsDecl][Terminals.DO] = new Symbol[]{
};

            ParseTable[NotTerminals.cpsDecl] = new Dictionary<Terminals, Symbol[]>();
            ParseTable[NotTerminals.cpsDecl][Terminals.PROC] = new Symbol[]{
    new Symbol(NotTerminals.decl, (p,s) => { ((CpsDeclPROC)p).Decl = (Decl)s; }),
    new Symbol(NotTerminals.repCpsDecl, (p,s) => { ((CpsDeclPROC)p).RepCpsDecl = (RepCpsDecl)s; }),
};

            ParseTable[NotTerminals.cpsDecl][Terminals.FUN] = new Symbol[]{
    new Symbol(NotTerminals.decl, (p,s) => { ((CpsDeclFUN)p).Decl = (Decl)s; }),
    new Symbol(NotTerminals.repCpsDecl, (p,s) => { ((CpsDeclFUN)p).RepCpsDecl = (RepCpsDecl)s; }),
};

            ParseTable[NotTerminals.cpsDecl][Terminals.CHANGEMODE] = new Symbol[]{
    new Symbol(NotTerminals.decl, (p,s) => { ((CpsDeclCHANGEMODE)p).Decl = (Decl)s; }),
    new Symbol(NotTerminals.repCpsDecl, (p,s) => { ((CpsDeclCHANGEMODE)p).RepCpsDecl = (RepCpsDecl)s; }),
};

            ParseTable[NotTerminals.cpsDecl][Terminals.IDENT] = new Symbol[]{
    new Symbol(NotTerminals.decl, (p,s) => { ((CpsDeclIDENT)p).Decl = (Decl)s; }),
    new Symbol(NotTerminals.repCpsDecl, (p,s) => { ((CpsDeclIDENT)p).RepCpsDecl = (RepCpsDecl)s; }),
};

            ParseTable[NotTerminals.repCpsDecl] = new Dictionary<Terminals, Symbol[]>();
            ParseTable[NotTerminals.repCpsDecl][Terminals.SEMICOLON] = new Symbol[]{
    new Symbol(Terminals.SEMICOLON, (p,s) => { ((RepCpsDeclSEMICOLON)p).SEMICOLON = (Tokennode)s; }),
    new Symbol(NotTerminals.decl, (p,s) => { ((RepCpsDeclSEMICOLON)p).Decl = (Decl)s; }),
    new Symbol(NotTerminals.repCpsDecl, (p,s) => { ((RepCpsDeclSEMICOLON)p).RepCpsDecl = (RepCpsDecl)s; }),
};

            ParseTable[NotTerminals.repCpsDecl][Terminals.DO] = new Symbol[]{
};

            ParseTable[NotTerminals.optCpsStoDecl] = new Dictionary<Terminals, Symbol[]>();
            ParseTable[NotTerminals.optCpsStoDecl][Terminals.LOCAL] = new Symbol[]{
    new Symbol(Terminals.LOCAL, (p,s) => { ((OptCpsStoDeclLOCAL)p).LOCAL = (Tokennode)s; }),
    new Symbol(NotTerminals.cpsStoDecl, (p,s) => { ((OptCpsStoDeclLOCAL)p).CpsStoDecl = (CpsStoDecl)s; }),
};

            ParseTable[NotTerminals.optCpsStoDecl][Terminals.DO] = new Symbol[]{
};

            ParseTable[NotTerminals.cpsStoDecl] = new Dictionary<Terminals, Symbol[]>();
            ParseTable[NotTerminals.cpsStoDecl][Terminals.CHANGEMODE] = new Symbol[]{
    new Symbol(NotTerminals.stoDecl, (p,s) => { ((CpsStoDeclCHANGEMODE)p).StoDecl = (StoDecl)s; }),
    new Symbol(NotTerminals.repCpsStoDecl, (p,s) => { ((CpsStoDeclCHANGEMODE)p).RepCpsStoDecl = (RepCpsStoDecl)s; }),
};

            ParseTable[NotTerminals.cpsStoDecl][Terminals.IDENT] = new Symbol[]{
    new Symbol(NotTerminals.stoDecl, (p,s) => { ((CpsStoDeclIDENT)p).StoDecl = (StoDecl)s; }),
    new Symbol(NotTerminals.repCpsStoDecl, (p,s) => { ((CpsStoDeclIDENT)p).RepCpsStoDecl = (RepCpsStoDecl)s; }),
};

            ParseTable[NotTerminals.repCpsStoDecl] = new Dictionary<Terminals, Symbol[]>();
            ParseTable[NotTerminals.repCpsStoDecl][Terminals.SEMICOLON] = new Symbol[]{
    new Symbol(Terminals.SEMICOLON, (p,s) => { ((RepCpsStoDeclSEMICOLON)p).SEMICOLON = (Tokennode)s; }),
    new Symbol(NotTerminals.stoDecl, (p,s) => { ((RepCpsStoDeclSEMICOLON)p).StoDecl = (StoDecl)s; }),
    new Symbol(NotTerminals.repCpsStoDecl, (p,s) => { ((RepCpsStoDeclSEMICOLON)p).RepCpsStoDecl = (RepCpsStoDecl)s; }),
};

            ParseTable[NotTerminals.repCpsStoDecl][Terminals.DO] = new Symbol[]{
};

            ParseTable[NotTerminals.progParamList] = new Dictionary<Terminals, Symbol[]>();
            ParseTable[NotTerminals.progParamList][Terminals.LPAREN] = new Symbol[]{
    new Symbol(Terminals.LPAREN, (p,s) => { ((ProgParamListLPAREN)p).LPAREN = (Tokennode)s; }),
    new Symbol(NotTerminals.optProgParamList, (p,s) => { ((ProgParamListLPAREN)p).OptProgParamList = (OptProgParamList)s; }),
    new Symbol(Terminals.RPAREN, (p,s) => { ((ProgParamListLPAREN)p).RPAREN = (Tokennode)s; }),
};

            ParseTable[NotTerminals.optProgParamList] = new Dictionary<Terminals, Symbol[]>();
            ParseTable[NotTerminals.optProgParamList][Terminals.FLOWMODE] = new Symbol[]{
    new Symbol(NotTerminals.progParam, (p,s) => { ((OptProgParamListFLOWMODE)p).ProgParam = (ProgParam)s; }),
    new Symbol(NotTerminals.repProgParamList, (p,s) => { ((OptProgParamListFLOWMODE)p).RepProgParamList = (RepProgParamList)s; }),
};

            ParseTable[NotTerminals.optProgParamList][Terminals.IDENT] = new Symbol[]{
    new Symbol(NotTerminals.progParam, (p,s) => { ((OptProgParamListIDENT)p).ProgParam = (ProgParam)s; }),
    new Symbol(NotTerminals.repProgParamList, (p,s) => { ((OptProgParamListIDENT)p).RepProgParamList = (RepProgParamList)s; }),
};

            ParseTable[NotTerminals.optProgParamList][Terminals.CHANGEMODE] = new Symbol[]{
    new Symbol(NotTerminals.progParam, (p,s) => { ((OptProgParamListCHANGEMODE)p).ProgParam = (ProgParam)s; }),
    new Symbol(NotTerminals.repProgParamList, (p,s) => { ((OptProgParamListCHANGEMODE)p).RepProgParamList = (RepProgParamList)s; }),
};

            ParseTable[NotTerminals.optProgParamList][Terminals.RPAREN] = new Symbol[]{
};

            ParseTable[NotTerminals.repProgParamList] = new Dictionary<Terminals, Symbol[]>();
            ParseTable[NotTerminals.repProgParamList][Terminals.COMMA] = new Symbol[]{
    new Symbol(Terminals.COMMA, (p,s) => { ((RepProgParamListCOMMA)p).COMMA = (Tokennode)s; }),
    new Symbol(NotTerminals.progParam, (p,s) => { ((RepProgParamListCOMMA)p).ProgParam = (ProgParam)s; }),
    new Symbol(NotTerminals.repProgParamList, (p,s) => { ((RepProgParamListCOMMA)p).RepProgParamList = (RepProgParamList)s; }),
};

            ParseTable[NotTerminals.repProgParamList][Terminals.RPAREN] = new Symbol[]{
};

            ParseTable[NotTerminals.progParam] = new Dictionary<Terminals, Symbol[]>();
            ParseTable[NotTerminals.progParam][Terminals.IDENT] = new Symbol[]{
    new Symbol(NotTerminals.optChangemode, (p,s) => { ((ProgParamIDENT)p).OptChangemode = (OptChangemode)s; }),
    new Symbol(NotTerminals.typedIdent, (p,s) => { ((ProgParamIDENT)p).TypedIdent = (TypedIdent)s; }),
};

            ParseTable[NotTerminals.progParam][Terminals.CHANGEMODE] = new Symbol[]{
    new Symbol(NotTerminals.optChangemode, (p,s) => { ((ProgParamCHANGEMODE)p).OptChangemode = (OptChangemode)s; }),
    new Symbol(NotTerminals.typedIdent, (p,s) => { ((ProgParamCHANGEMODE)p).TypedIdent = (TypedIdent)s; }),
};

            ParseTable[NotTerminals.progParam][Terminals.FLOWMODE] = new Symbol[]{
    new Symbol(Terminals.FLOWMODE, (p,s) => { ((ProgParamFLOWMODE)p).FLOWMODE = (Tokennode)s; }),
    new Symbol(NotTerminals.optChangemode, (p,s) => { ((ProgParamFLOWMODE)p).OptChangemode = (OptChangemode)s; }),
    new Symbol(NotTerminals.typedIdent, (p,s) => { ((ProgParamFLOWMODE)p).TypedIdent = (TypedIdent)s; }),
};

            ParseTable[NotTerminals.paramList] = new Dictionary<Terminals, Symbol[]>();
            ParseTable[NotTerminals.paramList][Terminals.LPAREN] = new Symbol[]{
    new Symbol(Terminals.LPAREN, (p,s) => { ((ParamListLPAREN)p).LPAREN = (Tokennode)s; }),
    new Symbol(NotTerminals.optParamList, (p,s) => { ((ParamListLPAREN)p).OptParamList = (OptParamList)s; }),
    new Symbol(Terminals.RPAREN, (p,s) => { ((ParamListLPAREN)p).RPAREN = (Tokennode)s; }),
};

            ParseTable[NotTerminals.optParamList] = new Dictionary<Terminals, Symbol[]>();
            ParseTable[NotTerminals.optParamList][Terminals.FLOWMODE] = new Symbol[]{
    new Symbol(NotTerminals.param, (p,s) => { ((OptParamListFLOWMODE)p).Param = (Param)s; }),
    new Symbol(NotTerminals.repParamList, (p,s) => { ((OptParamListFLOWMODE)p).RepParamList = (RepParamList)s; }),
};

            ParseTable[NotTerminals.optParamList][Terminals.IDENT] = new Symbol[]{
    new Symbol(NotTerminals.param, (p,s) => { ((OptParamListIDENT)p).Param = (Param)s; }),
    new Symbol(NotTerminals.repParamList, (p,s) => { ((OptParamListIDENT)p).RepParamList = (RepParamList)s; }),
};

            ParseTable[NotTerminals.optParamList][Terminals.CHANGEMODE] = new Symbol[]{
    new Symbol(NotTerminals.param, (p,s) => { ((OptParamListCHANGEMODE)p).Param = (Param)s; }),
    new Symbol(NotTerminals.repParamList, (p,s) => { ((OptParamListCHANGEMODE)p).RepParamList = (RepParamList)s; }),
};

            ParseTable[NotTerminals.optParamList][Terminals.MECHMODE] = new Symbol[]{
    new Symbol(NotTerminals.param, (p,s) => { ((OptParamListMECHMODE)p).Param = (Param)s; }),
    new Symbol(NotTerminals.repParamList, (p,s) => { ((OptParamListMECHMODE)p).RepParamList = (RepParamList)s; }),
};

            ParseTable[NotTerminals.optParamList][Terminals.RPAREN] = new Symbol[]{
};

            ParseTable[NotTerminals.repParamList] = new Dictionary<Terminals, Symbol[]>();
            ParseTable[NotTerminals.repParamList][Terminals.COMMA] = new Symbol[]{
    new Symbol(Terminals.COMMA, (p,s) => { ((RepParamListCOMMA)p).COMMA = (Tokennode)s; }),
    new Symbol(NotTerminals.param, (p,s) => { ((RepParamListCOMMA)p).Param = (Param)s; }),
    new Symbol(NotTerminals.repParamList, (p,s) => { ((RepParamListCOMMA)p).RepParamList = (RepParamList)s; }),
};

            ParseTable[NotTerminals.repParamList][Terminals.RPAREN] = new Symbol[]{
};

            ParseTable[NotTerminals.param] = new Dictionary<Terminals, Symbol[]>();
            ParseTable[NotTerminals.param][Terminals.IDENT] = new Symbol[]{
    new Symbol(NotTerminals.optMechmode, (p,s) => { ((ParamIDENT)p).OptMechmode = (OptMechmode)s; }),
    new Symbol(NotTerminals.optChangemode, (p,s) => { ((ParamIDENT)p).OptChangemode = (OptChangemode)s; }),
    new Symbol(NotTerminals.typedIdent, (p,s) => { ((ParamIDENT)p).TypedIdent = (TypedIdent)s; }),
};

            ParseTable[NotTerminals.param][Terminals.CHANGEMODE] = new Symbol[]{
    new Symbol(NotTerminals.optMechmode, (p,s) => { ((ParamCHANGEMODE)p).OptMechmode = (OptMechmode)s; }),
    new Symbol(NotTerminals.optChangemode, (p,s) => { ((ParamCHANGEMODE)p).OptChangemode = (OptChangemode)s; }),
    new Symbol(NotTerminals.typedIdent, (p,s) => { ((ParamCHANGEMODE)p).TypedIdent = (TypedIdent)s; }),
};

            ParseTable[NotTerminals.param][Terminals.MECHMODE] = new Symbol[]{
    new Symbol(NotTerminals.optMechmode, (p,s) => { ((ParamMECHMODE)p).OptMechmode = (OptMechmode)s; }),
    new Symbol(NotTerminals.optChangemode, (p,s) => { ((ParamMECHMODE)p).OptChangemode = (OptChangemode)s; }),
    new Symbol(NotTerminals.typedIdent, (p,s) => { ((ParamMECHMODE)p).TypedIdent = (TypedIdent)s; }),
};

            ParseTable[NotTerminals.param][Terminals.FLOWMODE] = new Symbol[]{
    new Symbol(Terminals.FLOWMODE, (p,s) => { ((ParamFLOWMODE)p).FLOWMODE = (Tokennode)s; }),
    new Symbol(NotTerminals.optMechmode, (p,s) => { ((ParamFLOWMODE)p).OptMechmode = (OptMechmode)s; }),
    new Symbol(NotTerminals.optChangemode, (p,s) => { ((ParamFLOWMODE)p).OptChangemode = (OptChangemode)s; }),
    new Symbol(NotTerminals.typedIdent, (p,s) => { ((ParamFLOWMODE)p).TypedIdent = (TypedIdent)s; }),
};

            ParseTable[NotTerminals.typedIdent] = new Dictionary<Terminals, Symbol[]>();
            ParseTable[NotTerminals.typedIdent][Terminals.IDENT] = new Symbol[]{
    new Symbol(Terminals.IDENT, (p,s) => { ((TypedIdentIDENT)p).IDENT = (Tokennode)s; }),
    new Symbol(Terminals.COLON, (p,s) => { ((TypedIdentIDENT)p).COLON = (Tokennode)s; }),
    new Symbol(Terminals.TYPE, (p,s) => { ((TypedIdentIDENT)p).TYPE = (Tokennode)s; }),
};

            ParseTable[NotTerminals.cmd] = new Dictionary<Terminals, Symbol[]>();
            ParseTable[NotTerminals.cmd][Terminals.SKIP] = new Symbol[]{
    new Symbol(Terminals.SKIP, (p,s) => { ((CmdSKIP)p).SKIP = (Tokennode)s; }),
};

            ParseTable[NotTerminals.cmd][Terminals.TYPE] = new Symbol[]{
    new Symbol(NotTerminals.expr, (p,s) => { ((CmdTYPE)p).Expr = (Expr)s; }),
    new Symbol(Terminals.BECOMES, (p,s) => { ((CmdTYPE)p).BECOMES = (Tokennode)s; }),
    new Symbol(NotTerminals.expr, (p,s) => { ((CmdTYPE)p).Expr2 = (Expr)s; }),
};

            ParseTable[NotTerminals.cmd][Terminals.LPAREN] = new Symbol[]{
    new Symbol(NotTerminals.expr, (p,s) => { ((CmdLPAREN)p).Expr = (Expr)s; }),
    new Symbol(Terminals.BECOMES, (p,s) => { ((CmdLPAREN)p).BECOMES = (Tokennode)s; }),
    new Symbol(NotTerminals.expr, (p,s) => { ((CmdLPAREN)p).Expr2 = (Expr)s; }),
};

            ParseTable[NotTerminals.cmd][Terminals.ADDOPR] = new Symbol[]{
    new Symbol(NotTerminals.expr, (p,s) => { ((CmdADDOPR)p).Expr = (Expr)s; }),
    new Symbol(Terminals.BECOMES, (p,s) => { ((CmdADDOPR)p).BECOMES = (Tokennode)s; }),
    new Symbol(NotTerminals.expr, (p,s) => { ((CmdADDOPR)p).Expr2 = (Expr)s; }),
};

            ParseTable[NotTerminals.cmd][Terminals.NOT] = new Symbol[]{
    new Symbol(NotTerminals.expr, (p,s) => { ((CmdNOT)p).Expr = (Expr)s; }),
    new Symbol(Terminals.BECOMES, (p,s) => { ((CmdNOT)p).BECOMES = (Tokennode)s; }),
    new Symbol(NotTerminals.expr, (p,s) => { ((CmdNOT)p).Expr2 = (Expr)s; }),
};

            ParseTable[NotTerminals.cmd][Terminals.IDENT] = new Symbol[]{
    new Symbol(NotTerminals.expr, (p,s) => { ((CmdIDENT)p).Expr = (Expr)s; }),
    new Symbol(Terminals.BECOMES, (p,s) => { ((CmdIDENT)p).BECOMES = (Tokennode)s; }),
    new Symbol(NotTerminals.expr, (p,s) => { ((CmdIDENT)p).Expr2 = (Expr)s; }),
};

            ParseTable[NotTerminals.cmd][Terminals.LITERAL] = new Symbol[]{
    new Symbol(NotTerminals.expr, (p,s) => { ((CmdLITERAL)p).Expr = (Expr)s; }),
    new Symbol(Terminals.BECOMES, (p,s) => { ((CmdLITERAL)p).BECOMES = (Tokennode)s; }),
    new Symbol(NotTerminals.expr, (p,s) => { ((CmdLITERAL)p).Expr2 = (Expr)s; }),
};

            ParseTable[NotTerminals.cmd][Terminals.IF] = new Symbol[]{
    new Symbol(Terminals.IF, (p,s) => { ((CmdIF)p).IF = (Tokennode)s; }),
    new Symbol(NotTerminals.expr, (p,s) => { ((CmdIF)p).Expr = (Expr)s; }),
    new Symbol(Terminals.THEN, (p,s) => { ((CmdIF)p).THEN = (Tokennode)s; }),
    new Symbol(NotTerminals.cpsCmd, (p,s) => { ((CmdIF)p).CpsCmd = (CpsCmd)s; }),
    new Symbol(Terminals.ELSE, (p,s) => { ((CmdIF)p).ELSE = (Tokennode)s; }),
    new Symbol(NotTerminals.cpsCmd, (p,s) => { ((CmdIF)p).CpsCmd2 = (CpsCmd)s; }),
    new Symbol(Terminals.ENDIF, (p,s) => { ((CmdIF)p).ENDIF = (Tokennode)s; }),
};

            ParseTable[NotTerminals.cmd][Terminals.WHILE] = new Symbol[]{
    new Symbol(Terminals.WHILE, (p,s) => { ((CmdWHILE)p).WHILE = (Tokennode)s; }),
    new Symbol(NotTerminals.expr, (p,s) => { ((CmdWHILE)p).Expr = (Expr)s; }),
    new Symbol(Terminals.DO, (p,s) => { ((CmdWHILE)p).DO = (Tokennode)s; }),
    new Symbol(NotTerminals.cpsCmd, (p,s) => { ((CmdWHILE)p).CpsCmd = (CpsCmd)s; }),
    new Symbol(Terminals.ENDWHILE, (p,s) => { ((CmdWHILE)p).ENDWHILE = (Tokennode)s; }),
};

            ParseTable[NotTerminals.cmd][Terminals.CALL] = new Symbol[]{
    new Symbol(Terminals.CALL, (p,s) => { ((CmdCALL)p).CALL = (Tokennode)s; }),
    new Symbol(Terminals.IDENT, (p,s) => { ((CmdCALL)p).IDENT = (Tokennode)s; }),
    new Symbol(NotTerminals.exprList, (p,s) => { ((CmdCALL)p).ExprList = (ExprList)s; }),
    new Symbol(NotTerminals.optGlobInits, (p,s) => { ((CmdCALL)p).OptGlobInits = (OptGlobInits)s; }),
};

            ParseTable[NotTerminals.cmd][Terminals.DEBUGIN] = new Symbol[]{
    new Symbol(Terminals.DEBUGIN, (p,s) => { ((CmdDEBUGIN)p).DEBUGIN = (Tokennode)s; }),
    new Symbol(NotTerminals.expr, (p,s) => { ((CmdDEBUGIN)p).Expr = (Expr)s; }),
};

            ParseTable[NotTerminals.cmd][Terminals.DEBUGOUT] = new Symbol[]{
    new Symbol(Terminals.DEBUGOUT, (p,s) => { ((CmdDEBUGOUT)p).DEBUGOUT = (Tokennode)s; }),
    new Symbol(NotTerminals.expr, (p,s) => { ((CmdDEBUGOUT)p).Expr = (Expr)s; }),
};

            ParseTable[NotTerminals.cpsCmd] = new Dictionary<Terminals, Symbol[]>();
            ParseTable[NotTerminals.cpsCmd][Terminals.DEBUGOUT] = new Symbol[]{
    new Symbol(NotTerminals.cmd, (p,s) => { ((CpsCmdDEBUGOUT)p).Cmd = (Cmd)s; }),
    new Symbol(NotTerminals.repCpsCmd, (p,s) => { ((CpsCmdDEBUGOUT)p).RepCpsCmd = (RepCpsCmd)s; }),
};

            ParseTable[NotTerminals.cpsCmd][Terminals.DEBUGIN] = new Symbol[]{
    new Symbol(NotTerminals.cmd, (p,s) => { ((CpsCmdDEBUGIN)p).Cmd = (Cmd)s; }),
    new Symbol(NotTerminals.repCpsCmd, (p,s) => { ((CpsCmdDEBUGIN)p).RepCpsCmd = (RepCpsCmd)s; }),
};

            ParseTable[NotTerminals.cpsCmd][Terminals.CALL] = new Symbol[]{
    new Symbol(NotTerminals.cmd, (p,s) => { ((CpsCmdCALL)p).Cmd = (Cmd)s; }),
    new Symbol(NotTerminals.repCpsCmd, (p,s) => { ((CpsCmdCALL)p).RepCpsCmd = (RepCpsCmd)s; }),
};

            ParseTable[NotTerminals.cpsCmd][Terminals.WHILE] = new Symbol[]{
    new Symbol(NotTerminals.cmd, (p,s) => { ((CpsCmdWHILE)p).Cmd = (Cmd)s; }),
    new Symbol(NotTerminals.repCpsCmd, (p,s) => { ((CpsCmdWHILE)p).RepCpsCmd = (RepCpsCmd)s; }),
};

            ParseTable[NotTerminals.cpsCmd][Terminals.IF] = new Symbol[]{
    new Symbol(NotTerminals.cmd, (p,s) => { ((CpsCmdIF)p).Cmd = (Cmd)s; }),
    new Symbol(NotTerminals.repCpsCmd, (p,s) => { ((CpsCmdIF)p).RepCpsCmd = (RepCpsCmd)s; }),
};

            ParseTable[NotTerminals.cpsCmd][Terminals.TYPE] = new Symbol[]{
    new Symbol(NotTerminals.cmd, (p,s) => { ((CpsCmdTYPE)p).Cmd = (Cmd)s; }),
    new Symbol(NotTerminals.repCpsCmd, (p,s) => { ((CpsCmdTYPE)p).RepCpsCmd = (RepCpsCmd)s; }),
};

            ParseTable[NotTerminals.cpsCmd][Terminals.LPAREN] = new Symbol[]{
    new Symbol(NotTerminals.cmd, (p,s) => { ((CpsCmdLPAREN)p).Cmd = (Cmd)s; }),
    new Symbol(NotTerminals.repCpsCmd, (p,s) => { ((CpsCmdLPAREN)p).RepCpsCmd = (RepCpsCmd)s; }),
};

            ParseTable[NotTerminals.cpsCmd][Terminals.ADDOPR] = new Symbol[]{
    new Symbol(NotTerminals.cmd, (p,s) => { ((CpsCmdADDOPR)p).Cmd = (Cmd)s; }),
    new Symbol(NotTerminals.repCpsCmd, (p,s) => { ((CpsCmdADDOPR)p).RepCpsCmd = (RepCpsCmd)s; }),
};

            ParseTable[NotTerminals.cpsCmd][Terminals.NOT] = new Symbol[]{
    new Symbol(NotTerminals.cmd, (p,s) => { ((CpsCmdNOT)p).Cmd = (Cmd)s; }),
    new Symbol(NotTerminals.repCpsCmd, (p,s) => { ((CpsCmdNOT)p).RepCpsCmd = (RepCpsCmd)s; }),
};

            ParseTable[NotTerminals.cpsCmd][Terminals.IDENT] = new Symbol[]{
    new Symbol(NotTerminals.cmd, (p,s) => { ((CpsCmdIDENT)p).Cmd = (Cmd)s; }),
    new Symbol(NotTerminals.repCpsCmd, (p,s) => { ((CpsCmdIDENT)p).RepCpsCmd = (RepCpsCmd)s; }),
};

            ParseTable[NotTerminals.cpsCmd][Terminals.LITERAL] = new Symbol[]{
    new Symbol(NotTerminals.cmd, (p,s) => { ((CpsCmdLITERAL)p).Cmd = (Cmd)s; }),
    new Symbol(NotTerminals.repCpsCmd, (p,s) => { ((CpsCmdLITERAL)p).RepCpsCmd = (RepCpsCmd)s; }),
};

            ParseTable[NotTerminals.cpsCmd][Terminals.SKIP] = new Symbol[]{
    new Symbol(NotTerminals.cmd, (p,s) => { ((CpsCmdSKIP)p).Cmd = (Cmd)s; }),
    new Symbol(NotTerminals.repCpsCmd, (p,s) => { ((CpsCmdSKIP)p).RepCpsCmd = (RepCpsCmd)s; }),
};

            ParseTable[NotTerminals.repCpsCmd] = new Dictionary<Terminals, Symbol[]>();
            ParseTable[NotTerminals.repCpsCmd][Terminals.SEMICOLON] = new Symbol[]{
    new Symbol(Terminals.SEMICOLON, (p,s) => { ((RepCpsCmdSEMICOLON)p).SEMICOLON = (Tokennode)s; }),
    new Symbol(NotTerminals.cmd, (p,s) => { ((RepCpsCmdSEMICOLON)p).Cmd = (Cmd)s; }),
    new Symbol(NotTerminals.repCpsCmd, (p,s) => { ((RepCpsCmdSEMICOLON)p).RepCpsCmd = (RepCpsCmd)s; }),
};

            ParseTable[NotTerminals.repCpsCmd][Terminals.ENDWHILE] = new Symbol[]{
};

            ParseTable[NotTerminals.repCpsCmd][Terminals.ENDIF] = new Symbol[]{
};

            ParseTable[NotTerminals.repCpsCmd][Terminals.ELSE] = new Symbol[]{
};

            ParseTable[NotTerminals.repCpsCmd][Terminals.ENDPROC] = new Symbol[]{
};

            ParseTable[NotTerminals.repCpsCmd][Terminals.ENDFUN] = new Symbol[]{
};

            ParseTable[NotTerminals.repCpsCmd][Terminals.ENDPROGRAM] = new Symbol[]{
};

            ParseTable[NotTerminals.optGlobInits] = new Dictionary<Terminals, Symbol[]>();
            ParseTable[NotTerminals.optGlobInits][Terminals.INIT] = new Symbol[]{
    new Symbol(Terminals.INIT, (p,s) => { ((OptGlobInitsINIT)p).INIT = (Tokennode)s; }),
    new Symbol(Terminals.IDENT, (p,s) => { ((OptGlobInitsINIT)p).IDENT = (Tokennode)s; }),
    new Symbol(NotTerminals.repIdents, (p,s) => { ((OptGlobInitsINIT)p).RepIdents = (RepIdents)s; }),
};

            ParseTable[NotTerminals.optGlobInits][Terminals.ENDWHILE] = new Symbol[]{
};

            ParseTable[NotTerminals.optGlobInits][Terminals.ENDIF] = new Symbol[]{
};

            ParseTable[NotTerminals.optGlobInits][Terminals.ELSE] = new Symbol[]{
};

            ParseTable[NotTerminals.optGlobInits][Terminals.ENDPROC] = new Symbol[]{
};

            ParseTable[NotTerminals.optGlobInits][Terminals.ENDFUN] = new Symbol[]{
};

            ParseTable[NotTerminals.optGlobInits][Terminals.ENDPROGRAM] = new Symbol[]{
};

            ParseTable[NotTerminals.optGlobInits][Terminals.SEMICOLON] = new Symbol[]{
};

            ParseTable[NotTerminals.repIdents] = new Dictionary<Terminals, Symbol[]>();
            ParseTable[NotTerminals.repIdents][Terminals.COMMA] = new Symbol[]{
    new Symbol(Terminals.COMMA, (p,s) => { ((RepIdentsCOMMA)p).COMMA = (Tokennode)s; }),
    new Symbol(Terminals.IDENT, (p,s) => { ((RepIdentsCOMMA)p).IDENT = (Tokennode)s; }),
    new Symbol(NotTerminals.repIdents, (p,s) => { ((RepIdentsCOMMA)p).RepIdents = (RepIdents)s; }),
};

            ParseTable[NotTerminals.repIdents][Terminals.ENDWHILE] = new Symbol[]{
};

            ParseTable[NotTerminals.repIdents][Terminals.ENDIF] = new Symbol[]{
};

            ParseTable[NotTerminals.repIdents][Terminals.ELSE] = new Symbol[]{
};

            ParseTable[NotTerminals.repIdents][Terminals.ENDPROC] = new Symbol[]{
};

            ParseTable[NotTerminals.repIdents][Terminals.ENDFUN] = new Symbol[]{
};

            ParseTable[NotTerminals.repIdents][Terminals.ENDPROGRAM] = new Symbol[]{
};

            ParseTable[NotTerminals.repIdents][Terminals.SEMICOLON] = new Symbol[]{
};

            ParseTable[NotTerminals.expr] = new Dictionary<Terminals, Symbol[]>();
            ParseTable[NotTerminals.expr][Terminals.TYPE] = new Symbol[]{
    new Symbol(NotTerminals.term1, (p,s) => { ((ExprTYPE)p).Term1 = (Term1)s; }),
    new Symbol(NotTerminals.repTerm1, (p,s) => { ((ExprTYPE)p).RepTerm1 = (RepTerm1)s; }),
};

            ParseTable[NotTerminals.expr][Terminals.LPAREN] = new Symbol[]{
    new Symbol(NotTerminals.term1, (p,s) => { ((ExprLPAREN)p).Term1 = (Term1)s; }),
    new Symbol(NotTerminals.repTerm1, (p,s) => { ((ExprLPAREN)p).RepTerm1 = (RepTerm1)s; }),
};

            ParseTable[NotTerminals.expr][Terminals.ADDOPR] = new Symbol[]{
    new Symbol(NotTerminals.term1, (p,s) => { ((ExprADDOPR)p).Term1 = (Term1)s; }),
    new Symbol(NotTerminals.repTerm1, (p,s) => { ((ExprADDOPR)p).RepTerm1 = (RepTerm1)s; }),
};

            ParseTable[NotTerminals.expr][Terminals.NOT] = new Symbol[]{
    new Symbol(NotTerminals.term1, (p,s) => { ((ExprNOT)p).Term1 = (Term1)s; }),
    new Symbol(NotTerminals.repTerm1, (p,s) => { ((ExprNOT)p).RepTerm1 = (RepTerm1)s; }),
};

            ParseTable[NotTerminals.expr][Terminals.IDENT] = new Symbol[]{
    new Symbol(NotTerminals.term1, (p,s) => { ((ExprIDENT)p).Term1 = (Term1)s; }),
    new Symbol(NotTerminals.repTerm1, (p,s) => { ((ExprIDENT)p).RepTerm1 = (RepTerm1)s; }),
};

            ParseTable[NotTerminals.expr][Terminals.LITERAL] = new Symbol[]{
    new Symbol(NotTerminals.term1, (p,s) => { ((ExprLITERAL)p).Term1 = (Term1)s; }),
    new Symbol(NotTerminals.repTerm1, (p,s) => { ((ExprLITERAL)p).RepTerm1 = (RepTerm1)s; }),
};

            ParseTable[NotTerminals.repTerm1] = new Dictionary<Terminals, Symbol[]>();
            ParseTable[NotTerminals.repTerm1][Terminals.BOOLOPR] = new Symbol[]{
    new Symbol(Terminals.BOOLOPR, (p,s) => { ((RepTerm1BOOLOPR)p).BOOLOPR = (Tokennode)s; }),
    new Symbol(NotTerminals.term1, (p,s) => { ((RepTerm1BOOLOPR)p).Term1 = (Term1)s; }),
    new Symbol(NotTerminals.repTerm1, (p,s) => { ((RepTerm1BOOLOPR)p).RepTerm1 = (RepTerm1)s; }),
};

            ParseTable[NotTerminals.repTerm1][Terminals.COMMA] = new Symbol[]{
};

            ParseTable[NotTerminals.repTerm1][Terminals.RPAREN] = new Symbol[]{
};

            ParseTable[NotTerminals.repTerm1][Terminals.DO] = new Symbol[]{
};

            ParseTable[NotTerminals.repTerm1][Terminals.THEN] = new Symbol[]{
};

            ParseTable[NotTerminals.repTerm1][Terminals.ENDWHILE] = new Symbol[]{
};

            ParseTable[NotTerminals.repTerm1][Terminals.ENDIF] = new Symbol[]{
};

            ParseTable[NotTerminals.repTerm1][Terminals.ELSE] = new Symbol[]{
};

            ParseTable[NotTerminals.repTerm1][Terminals.ENDPROC] = new Symbol[]{
};

            ParseTable[NotTerminals.repTerm1][Terminals.ENDFUN] = new Symbol[]{
};

            ParseTable[NotTerminals.repTerm1][Terminals.ENDPROGRAM] = new Symbol[]{
};

            ParseTable[NotTerminals.repTerm1][Terminals.SEMICOLON] = new Symbol[]{
};

            ParseTable[NotTerminals.repTerm1][Terminals.BECOMES] = new Symbol[]{
};

            ParseTable[NotTerminals.term1] = new Dictionary<Terminals, Symbol[]>();
            ParseTable[NotTerminals.term1][Terminals.TYPE] = new Symbol[]{
    new Symbol(NotTerminals.term2, (p,s) => { ((Term1TYPE)p).Term2 = (Term2)s; }),
    new Symbol(NotTerminals.repTerm2, (p,s) => { ((Term1TYPE)p).RepTerm2 = (RepTerm2)s; }),
};

            ParseTable[NotTerminals.term1][Terminals.LPAREN] = new Symbol[]{
    new Symbol(NotTerminals.term2, (p,s) => { ((Term1LPAREN)p).Term2 = (Term2)s; }),
    new Symbol(NotTerminals.repTerm2, (p,s) => { ((Term1LPAREN)p).RepTerm2 = (RepTerm2)s; }),
};

            ParseTable[NotTerminals.term1][Terminals.ADDOPR] = new Symbol[]{
    new Symbol(NotTerminals.term2, (p,s) => { ((Term1ADDOPR)p).Term2 = (Term2)s; }),
    new Symbol(NotTerminals.repTerm2, (p,s) => { ((Term1ADDOPR)p).RepTerm2 = (RepTerm2)s; }),
};

            ParseTable[NotTerminals.term1][Terminals.NOT] = new Symbol[]{
    new Symbol(NotTerminals.term2, (p,s) => { ((Term1NOT)p).Term2 = (Term2)s; }),
    new Symbol(NotTerminals.repTerm2, (p,s) => { ((Term1NOT)p).RepTerm2 = (RepTerm2)s; }),
};

            ParseTable[NotTerminals.term1][Terminals.IDENT] = new Symbol[]{
    new Symbol(NotTerminals.term2, (p,s) => { ((Term1IDENT)p).Term2 = (Term2)s; }),
    new Symbol(NotTerminals.repTerm2, (p,s) => { ((Term1IDENT)p).RepTerm2 = (RepTerm2)s; }),
};

            ParseTable[NotTerminals.term1][Terminals.LITERAL] = new Symbol[]{
    new Symbol(NotTerminals.term2, (p,s) => { ((Term1LITERAL)p).Term2 = (Term2)s; }),
    new Symbol(NotTerminals.repTerm2, (p,s) => { ((Term1LITERAL)p).RepTerm2 = (RepTerm2)s; }),
};

            ParseTable[NotTerminals.repTerm2] = new Dictionary<Terminals, Symbol[]>();
            ParseTable[NotTerminals.repTerm2][Terminals.RELOPR] = new Symbol[]{
    new Symbol(Terminals.RELOPR, (p,s) => { ((RepTerm2RELOPR)p).RELOPR = (Tokennode)s; }),
    new Symbol(NotTerminals.term2, (p,s) => { ((RepTerm2RELOPR)p).Term2 = (Term2)s; }),
    new Symbol(NotTerminals.repTerm2, (p,s) => { ((RepTerm2RELOPR)p).RepTerm2 = (RepTerm2)s; }),
};

            ParseTable[NotTerminals.repTerm2][Terminals.COMMA] = new Symbol[]{
};

            ParseTable[NotTerminals.repTerm2][Terminals.RPAREN] = new Symbol[]{
};

            ParseTable[NotTerminals.repTerm2][Terminals.DO] = new Symbol[]{
};

            ParseTable[NotTerminals.repTerm2][Terminals.THEN] = new Symbol[]{
};

            ParseTable[NotTerminals.repTerm2][Terminals.ENDWHILE] = new Symbol[]{
};

            ParseTable[NotTerminals.repTerm2][Terminals.ENDIF] = new Symbol[]{
};

            ParseTable[NotTerminals.repTerm2][Terminals.ELSE] = new Symbol[]{
};

            ParseTable[NotTerminals.repTerm2][Terminals.ENDPROC] = new Symbol[]{
};

            ParseTable[NotTerminals.repTerm2][Terminals.ENDFUN] = new Symbol[]{
};

            ParseTable[NotTerminals.repTerm2][Terminals.ENDPROGRAM] = new Symbol[]{
};

            ParseTable[NotTerminals.repTerm2][Terminals.SEMICOLON] = new Symbol[]{
};

            ParseTable[NotTerminals.repTerm2][Terminals.BECOMES] = new Symbol[]{
};

            ParseTable[NotTerminals.repTerm2][Terminals.BOOLOPR] = new Symbol[]{
};

            ParseTable[NotTerminals.term2] = new Dictionary<Terminals, Symbol[]>();
            ParseTable[NotTerminals.term2][Terminals.TYPE] = new Symbol[]{
    new Symbol(NotTerminals.term3, (p,s) => { ((Term2TYPE)p).Term3 = (Term3)s; }),
    new Symbol(NotTerminals.repTerm3, (p,s) => { ((Term2TYPE)p).RepTerm3 = (RepTerm3)s; }),
};

            ParseTable[NotTerminals.term2][Terminals.LPAREN] = new Symbol[]{
    new Symbol(NotTerminals.term3, (p,s) => { ((Term2LPAREN)p).Term3 = (Term3)s; }),
    new Symbol(NotTerminals.repTerm3, (p,s) => { ((Term2LPAREN)p).RepTerm3 = (RepTerm3)s; }),
};

            ParseTable[NotTerminals.term2][Terminals.ADDOPR] = new Symbol[]{
    new Symbol(NotTerminals.term3, (p,s) => { ((Term2ADDOPR)p).Term3 = (Term3)s; }),
    new Symbol(NotTerminals.repTerm3, (p,s) => { ((Term2ADDOPR)p).RepTerm3 = (RepTerm3)s; }),
};

            ParseTable[NotTerminals.term2][Terminals.NOT] = new Symbol[]{
    new Symbol(NotTerminals.term3, (p,s) => { ((Term2NOT)p).Term3 = (Term3)s; }),
    new Symbol(NotTerminals.repTerm3, (p,s) => { ((Term2NOT)p).RepTerm3 = (RepTerm3)s; }),
};

            ParseTable[NotTerminals.term2][Terminals.IDENT] = new Symbol[]{
    new Symbol(NotTerminals.term3, (p,s) => { ((Term2IDENT)p).Term3 = (Term3)s; }),
    new Symbol(NotTerminals.repTerm3, (p,s) => { ((Term2IDENT)p).RepTerm3 = (RepTerm3)s; }),
};

            ParseTable[NotTerminals.term2][Terminals.LITERAL] = new Symbol[]{
    new Symbol(NotTerminals.term3, (p,s) => { ((Term2LITERAL)p).Term3 = (Term3)s; }),
    new Symbol(NotTerminals.repTerm3, (p,s) => { ((Term2LITERAL)p).RepTerm3 = (RepTerm3)s; }),
};

            ParseTable[NotTerminals.repTerm3] = new Dictionary<Terminals, Symbol[]>();
            ParseTable[NotTerminals.repTerm3][Terminals.ADDOPR] = new Symbol[]{
    new Symbol(Terminals.ADDOPR, (p,s) => { ((RepTerm3ADDOPR)p).ADDOPR = (Tokennode)s; }),
    new Symbol(NotTerminals.term3, (p,s) => { ((RepTerm3ADDOPR)p).Term3 = (Term3)s; }),
    new Symbol(NotTerminals.repTerm3, (p,s) => { ((RepTerm3ADDOPR)p).RepTerm3 = (RepTerm3)s; }),
};

            ParseTable[NotTerminals.repTerm3][Terminals.COMMA] = new Symbol[]{
};

            ParseTable[NotTerminals.repTerm3][Terminals.RPAREN] = new Symbol[]{
};

            ParseTable[NotTerminals.repTerm3][Terminals.DO] = new Symbol[]{
};

            ParseTable[NotTerminals.repTerm3][Terminals.THEN] = new Symbol[]{
};

            ParseTable[NotTerminals.repTerm3][Terminals.ENDWHILE] = new Symbol[]{
};

            ParseTable[NotTerminals.repTerm3][Terminals.ENDIF] = new Symbol[]{
};

            ParseTable[NotTerminals.repTerm3][Terminals.ELSE] = new Symbol[]{
};

            ParseTable[NotTerminals.repTerm3][Terminals.ENDPROC] = new Symbol[]{
};

            ParseTable[NotTerminals.repTerm3][Terminals.ENDFUN] = new Symbol[]{
};

            ParseTable[NotTerminals.repTerm3][Terminals.ENDPROGRAM] = new Symbol[]{
};

            ParseTable[NotTerminals.repTerm3][Terminals.SEMICOLON] = new Symbol[]{
};

            ParseTable[NotTerminals.repTerm3][Terminals.BECOMES] = new Symbol[]{
};

            ParseTable[NotTerminals.repTerm3][Terminals.BOOLOPR] = new Symbol[]{
};

            ParseTable[NotTerminals.repTerm3][Terminals.RELOPR] = new Symbol[]{
};

            ParseTable[NotTerminals.term3] = new Dictionary<Terminals, Symbol[]>();
            ParseTable[NotTerminals.term3][Terminals.TYPE] = new Symbol[]{
    new Symbol(NotTerminals.factor, (p,s) => { ((Term3TYPE)p).Factor = (Factor)s; }),
    new Symbol(NotTerminals.repFactor, (p,s) => { ((Term3TYPE)p).RepFactor = (RepFactor)s; }),
};

            ParseTable[NotTerminals.term3][Terminals.LPAREN] = new Symbol[]{
    new Symbol(NotTerminals.factor, (p,s) => { ((Term3LPAREN)p).Factor = (Factor)s; }),
    new Symbol(NotTerminals.repFactor, (p,s) => { ((Term3LPAREN)p).RepFactor = (RepFactor)s; }),
};

            ParseTable[NotTerminals.term3][Terminals.ADDOPR] = new Symbol[]{
    new Symbol(NotTerminals.factor, (p,s) => { ((Term3ADDOPR)p).Factor = (Factor)s; }),
    new Symbol(NotTerminals.repFactor, (p,s) => { ((Term3ADDOPR)p).RepFactor = (RepFactor)s; }),
};

            ParseTable[NotTerminals.term3][Terminals.NOT] = new Symbol[]{
    new Symbol(NotTerminals.factor, (p,s) => { ((Term3NOT)p).Factor = (Factor)s; }),
    new Symbol(NotTerminals.repFactor, (p,s) => { ((Term3NOT)p).RepFactor = (RepFactor)s; }),
};

            ParseTable[NotTerminals.term3][Terminals.IDENT] = new Symbol[]{
    new Symbol(NotTerminals.factor, (p,s) => { ((Term3IDENT)p).Factor = (Factor)s; }),
    new Symbol(NotTerminals.repFactor, (p,s) => { ((Term3IDENT)p).RepFactor = (RepFactor)s; }),
};

            ParseTable[NotTerminals.term3][Terminals.LITERAL] = new Symbol[]{
    new Symbol(NotTerminals.factor, (p,s) => { ((Term3LITERAL)p).Factor = (Factor)s; }),
    new Symbol(NotTerminals.repFactor, (p,s) => { ((Term3LITERAL)p).RepFactor = (RepFactor)s; }),
};

            ParseTable[NotTerminals.repFactor] = new Dictionary<Terminals, Symbol[]>();
            ParseTable[NotTerminals.repFactor][Terminals.MULTOPR] = new Symbol[]{
    new Symbol(Terminals.MULTOPR, (p,s) => { ((RepFactorMULTOPR)p).MULTOPR = (Tokennode)s; }),
    new Symbol(NotTerminals.factor, (p,s) => { ((RepFactorMULTOPR)p).Factor = (Factor)s; }),
    new Symbol(NotTerminals.repFactor, (p,s) => { ((RepFactorMULTOPR)p).RepFactor = (RepFactor)s; }),
};

            ParseTable[NotTerminals.repFactor][Terminals.COMMA] = new Symbol[]{
};

            ParseTable[NotTerminals.repFactor][Terminals.RPAREN] = new Symbol[]{
};

            ParseTable[NotTerminals.repFactor][Terminals.DO] = new Symbol[]{
};

            ParseTable[NotTerminals.repFactor][Terminals.THEN] = new Symbol[]{
};

            ParseTable[NotTerminals.repFactor][Terminals.ENDWHILE] = new Symbol[]{
};

            ParseTable[NotTerminals.repFactor][Terminals.ENDIF] = new Symbol[]{
};

            ParseTable[NotTerminals.repFactor][Terminals.ELSE] = new Symbol[]{
};

            ParseTable[NotTerminals.repFactor][Terminals.ENDPROC] = new Symbol[]{
};

            ParseTable[NotTerminals.repFactor][Terminals.ENDFUN] = new Symbol[]{
};

            ParseTable[NotTerminals.repFactor][Terminals.ENDPROGRAM] = new Symbol[]{
};

            ParseTable[NotTerminals.repFactor][Terminals.SEMICOLON] = new Symbol[]{
};

            ParseTable[NotTerminals.repFactor][Terminals.BECOMES] = new Symbol[]{
};

            ParseTable[NotTerminals.repFactor][Terminals.BOOLOPR] = new Symbol[]{
};

            ParseTable[NotTerminals.repFactor][Terminals.RELOPR] = new Symbol[]{
};

            ParseTable[NotTerminals.repFactor][Terminals.ADDOPR] = new Symbol[]{
};

            ParseTable[NotTerminals.factor] = new Dictionary<Terminals, Symbol[]>();
            ParseTable[NotTerminals.factor][Terminals.LITERAL] = new Symbol[]{
    new Symbol(Terminals.LITERAL, (p,s) => { ((FactorLITERAL)p).LITERAL = (Tokennode)s; }),
};

            ParseTable[NotTerminals.factor][Terminals.IDENT] = new Symbol[]{
    new Symbol(Terminals.IDENT, (p,s) => { ((FactorIDENT)p).IDENT = (Tokennode)s; }),
    new Symbol(NotTerminals.optInitOrExprList, (p,s) => { ((FactorIDENT)p).OptInitOrExprList = (OptInitOrExprList)s; }),
};

            ParseTable[NotTerminals.factor][Terminals.ADDOPR] = new Symbol[]{
    new Symbol(NotTerminals.monadicOpr, (p,s) => { ((FactorADDOPR)p).MonadicOpr = (MonadicOpr)s; }),
    new Symbol(NotTerminals.factor, (p,s) => { ((FactorADDOPR)p).Factor = (Factor)s; }),
};

            ParseTable[NotTerminals.factor][Terminals.NOT] = new Symbol[]{
    new Symbol(NotTerminals.monadicOpr, (p,s) => { ((FactorNOT)p).MonadicOpr = (MonadicOpr)s; }),
    new Symbol(NotTerminals.factor, (p,s) => { ((FactorNOT)p).Factor = (Factor)s; }),
};

            ParseTable[NotTerminals.factor][Terminals.LPAREN] = new Symbol[]{
    new Symbol(Terminals.LPAREN, (p,s) => { ((FactorLPAREN)p).LPAREN = (Tokennode)s; }),
    new Symbol(NotTerminals.expr, (p,s) => { ((FactorLPAREN)p).Expr = (Expr)s; }),
    new Symbol(Terminals.RPAREN, (p,s) => { ((FactorLPAREN)p).RPAREN = (Tokennode)s; }),
};

            ParseTable[NotTerminals.factor][Terminals.TYPE] = new Symbol[]{
    new Symbol(Terminals.TYPE, (p,s) => { ((FactorTYPE)p).TYPE = (Tokennode)s; }),
    new Symbol(Terminals.LPAREN, (p,s) => { ((FactorTYPE)p).LPAREN = (Tokennode)s; }),
    new Symbol(NotTerminals.expr, (p,s) => { ((FactorTYPE)p).Expr = (Expr)s; }),
    new Symbol(Terminals.RPAREN, (p,s) => { ((FactorTYPE)p).RPAREN = (Tokennode)s; }),
};

            ParseTable[NotTerminals.optInitOrExprList] = new Dictionary<Terminals, Symbol[]>();
            ParseTable[NotTerminals.optInitOrExprList][Terminals.INIT] = new Symbol[]{
    new Symbol(Terminals.INIT, (p,s) => { ((OptInitOrExprListINIT)p).INIT = (Tokennode)s; }),
};

            ParseTable[NotTerminals.optInitOrExprList][Terminals.LPAREN] = new Symbol[]{
    new Symbol(NotTerminals.exprList, (p,s) => { ((OptInitOrExprListLPAREN)p).ExprList = (ExprList)s; }),
};

            ParseTable[NotTerminals.optInitOrExprList][Terminals.COMMA] = new Symbol[]{
};

            ParseTable[NotTerminals.optInitOrExprList][Terminals.RPAREN] = new Symbol[]{
};

            ParseTable[NotTerminals.optInitOrExprList][Terminals.DO] = new Symbol[]{
};

            ParseTable[NotTerminals.optInitOrExprList][Terminals.THEN] = new Symbol[]{
};

            ParseTable[NotTerminals.optInitOrExprList][Terminals.ENDWHILE] = new Symbol[]{
};

            ParseTable[NotTerminals.optInitOrExprList][Terminals.ENDIF] = new Symbol[]{
};

            ParseTable[NotTerminals.optInitOrExprList][Terminals.ELSE] = new Symbol[]{
};

            ParseTable[NotTerminals.optInitOrExprList][Terminals.ENDPROC] = new Symbol[]{
};

            ParseTable[NotTerminals.optInitOrExprList][Terminals.ENDFUN] = new Symbol[]{
};

            ParseTable[NotTerminals.optInitOrExprList][Terminals.ENDPROGRAM] = new Symbol[]{
};

            ParseTable[NotTerminals.optInitOrExprList][Terminals.SEMICOLON] = new Symbol[]{
};

            ParseTable[NotTerminals.optInitOrExprList][Terminals.BECOMES] = new Symbol[]{
};

            ParseTable[NotTerminals.optInitOrExprList][Terminals.BOOLOPR] = new Symbol[]{
};

            ParseTable[NotTerminals.optInitOrExprList][Terminals.RELOPR] = new Symbol[]{
};

            ParseTable[NotTerminals.optInitOrExprList][Terminals.ADDOPR] = new Symbol[]{
};

            ParseTable[NotTerminals.optInitOrExprList][Terminals.MULTOPR] = new Symbol[]{
};

            ParseTable[NotTerminals.monadicOpr] = new Dictionary<Terminals, Symbol[]>();
            ParseTable[NotTerminals.monadicOpr][Terminals.NOT] = new Symbol[]{
    new Symbol(Terminals.NOT, (p,s) => { ((MonadicOprNOT)p).NOT = (Tokennode)s; }),
};

            ParseTable[NotTerminals.monadicOpr][Terminals.ADDOPR] = new Symbol[]{
    new Symbol(Terminals.ADDOPR, (p,s) => { ((MonadicOprADDOPR)p).ADDOPR = (Tokennode)s; }),
};

            ParseTable[NotTerminals.exprList] = new Dictionary<Terminals, Symbol[]>();
            ParseTable[NotTerminals.exprList][Terminals.LPAREN] = new Symbol[]{
    new Symbol(Terminals.LPAREN, (p,s) => { ((ExprListLPAREN)p).LPAREN = (Tokennode)s; }),
    new Symbol(NotTerminals.optExprList, (p,s) => { ((ExprListLPAREN)p).OptExprList = (OptExprList)s; }),
    new Symbol(Terminals.RPAREN, (p,s) => { ((ExprListLPAREN)p).RPAREN = (Tokennode)s; }),
};

            ParseTable[NotTerminals.optExprList] = new Dictionary<Terminals, Symbol[]>();
            ParseTable[NotTerminals.optExprList][Terminals.TYPE] = new Symbol[]{
    new Symbol(NotTerminals.expr, (p,s) => { ((OptExprListTYPE)p).Expr = (Expr)s; }),
    new Symbol(NotTerminals.repExprList, (p,s) => { ((OptExprListTYPE)p).RepExprList = (RepExprList)s; }),
};

            ParseTable[NotTerminals.optExprList][Terminals.LPAREN] = new Symbol[]{
    new Symbol(NotTerminals.expr, (p,s) => { ((OptExprListLPAREN)p).Expr = (Expr)s; }),
    new Symbol(NotTerminals.repExprList, (p,s) => { ((OptExprListLPAREN)p).RepExprList = (RepExprList)s; }),
};

            ParseTable[NotTerminals.optExprList][Terminals.ADDOPR] = new Symbol[]{
    new Symbol(NotTerminals.expr, (p,s) => { ((OptExprListADDOPR)p).Expr = (Expr)s; }),
    new Symbol(NotTerminals.repExprList, (p,s) => { ((OptExprListADDOPR)p).RepExprList = (RepExprList)s; }),
};

            ParseTable[NotTerminals.optExprList][Terminals.NOT] = new Symbol[]{
    new Symbol(NotTerminals.expr, (p,s) => { ((OptExprListNOT)p).Expr = (Expr)s; }),
    new Symbol(NotTerminals.repExprList, (p,s) => { ((OptExprListNOT)p).RepExprList = (RepExprList)s; }),
};

            ParseTable[NotTerminals.optExprList][Terminals.IDENT] = new Symbol[]{
    new Symbol(NotTerminals.expr, (p,s) => { ((OptExprListIDENT)p).Expr = (Expr)s; }),
    new Symbol(NotTerminals.repExprList, (p,s) => { ((OptExprListIDENT)p).RepExprList = (RepExprList)s; }),
};

            ParseTable[NotTerminals.optExprList][Terminals.LITERAL] = new Symbol[]{
    new Symbol(NotTerminals.expr, (p,s) => { ((OptExprListLITERAL)p).Expr = (Expr)s; }),
    new Symbol(NotTerminals.repExprList, (p,s) => { ((OptExprListLITERAL)p).RepExprList = (RepExprList)s; }),
};

            ParseTable[NotTerminals.optExprList][Terminals.RPAREN] = new Symbol[]{
};

            ParseTable[NotTerminals.repExprList] = new Dictionary<Terminals, Symbol[]>();
            ParseTable[NotTerminals.repExprList][Terminals.COMMA] = new Symbol[]{
    new Symbol(Terminals.COMMA, (p,s) => { ((RepExprListCOMMA)p).COMMA = (Tokennode)s; }),
    new Symbol(NotTerminals.expr, (p,s) => { ((RepExprListCOMMA)p).Expr = (Expr)s; }),
    new Symbol(NotTerminals.repExprList, (p,s) => { ((RepExprListCOMMA)p).RepExprList = (RepExprList)s; }),
};

            ParseTable[NotTerminals.repExprList][Terminals.RPAREN] = new Symbol[]{
};

            //* Factories *//

            Factories = new Dictionary<NotTerminals, IDictionary<Terminals, Func<Treenode>>>();

            Factories[NotTerminals.program] = new Dictionary<Terminals, Func<Treenode>>();
            Factories[NotTerminals.program][Terminals.PROGRAM] = () => { return new ProgramPROGRAM(); };

            Factories[NotTerminals.decl] = new Dictionary<Terminals, Func<Treenode>>();
            Factories[NotTerminals.decl][Terminals.CHANGEMODE] = () => { return new DeclCHANGEMODE(); };
            Factories[NotTerminals.decl][Terminals.IDENT] = () => { return new DeclIDENT(); };
            Factories[NotTerminals.decl][Terminals.FUN] = () => { return new DeclFUN(); };
            Factories[NotTerminals.decl][Terminals.PROC] = () => { return new DeclPROC(); };

            Factories[NotTerminals.stoDecl] = new Dictionary<Terminals, Func<Treenode>>();
            Factories[NotTerminals.stoDecl][Terminals.IDENT] = () => { return new StoDeclIDENT(); };
            Factories[NotTerminals.stoDecl][Terminals.CHANGEMODE] = () => { return new StoDeclCHANGEMODE(); };

            Factories[NotTerminals.funDecl] = new Dictionary<Terminals, Func<Treenode>>();
            Factories[NotTerminals.funDecl][Terminals.FUN] = () => { return new FunDeclFUN(); };

            Factories[NotTerminals.procDecl] = new Dictionary<Terminals, Func<Treenode>>();
            Factories[NotTerminals.procDecl][Terminals.PROC] = () => { return new ProcDeclPROC(); };

            Factories[NotTerminals.optGlobImps] = new Dictionary<Terminals, Func<Treenode>>();
            Factories[NotTerminals.optGlobImps][Terminals.GLOBAL] = () => { return new OptGlobImpsGLOBAL(); };
            Factories[NotTerminals.optGlobImps][Terminals.DO] = () => { return new OptGlobImpsDO(); };
            Factories[NotTerminals.optGlobImps][Terminals.LOCAL] = () => { return new OptGlobImpsLOCAL(); };

            Factories[NotTerminals.globImps] = new Dictionary<Terminals, Func<Treenode>>();
            Factories[NotTerminals.globImps][Terminals.FLOWMODE] = () => { return new GlobImpsFLOWMODE(); };
            Factories[NotTerminals.globImps][Terminals.IDENT] = () => { return new GlobImpsIDENT(); };
            Factories[NotTerminals.globImps][Terminals.CHANGEMODE] = () => { return new GlobImpsCHANGEMODE(); };

            Factories[NotTerminals.repGlobImps] = new Dictionary<Terminals, Func<Treenode>>();
            Factories[NotTerminals.repGlobImps][Terminals.COMMA] = () => { return new RepGlobImpsCOMMA(); };
            Factories[NotTerminals.repGlobImps][Terminals.DO] = () => { return new RepGlobImpsDO(); };
            Factories[NotTerminals.repGlobImps][Terminals.LOCAL] = () => { return new RepGlobImpsLOCAL(); };

            Factories[NotTerminals.optChangemode] = new Dictionary<Terminals, Func<Treenode>>();
            Factories[NotTerminals.optChangemode][Terminals.CHANGEMODE] = () => { return new OptChangemodeCHANGEMODE(); };
            Factories[NotTerminals.optChangemode][Terminals.IDENT] = () => { return new OptChangemodeIDENT(); };

            Factories[NotTerminals.optMechmode] = new Dictionary<Terminals, Func<Treenode>>();
            Factories[NotTerminals.optMechmode][Terminals.MECHMODE] = () => { return new OptMechmodeMECHMODE(); };
            Factories[NotTerminals.optMechmode][Terminals.IDENT] = () => { return new OptMechmodeIDENT(); };
            Factories[NotTerminals.optMechmode][Terminals.CHANGEMODE] = () => { return new OptMechmodeCHANGEMODE(); };

            Factories[NotTerminals.globImp] = new Dictionary<Terminals, Func<Treenode>>();
            Factories[NotTerminals.globImp][Terminals.IDENT] = () => { return new GlobImpIDENT(); };
            Factories[NotTerminals.globImp][Terminals.CHANGEMODE] = () => { return new GlobImpCHANGEMODE(); };
            Factories[NotTerminals.globImp][Terminals.FLOWMODE] = () => { return new GlobImpFLOWMODE(); };

            Factories[NotTerminals.optCpsDecl] = new Dictionary<Terminals, Func<Treenode>>();
            Factories[NotTerminals.optCpsDecl][Terminals.GLOBAL] = () => { return new OptCpsDeclGLOBAL(); };
            Factories[NotTerminals.optCpsDecl][Terminals.DO] = () => { return new OptCpsDeclDO(); };

            Factories[NotTerminals.cpsDecl] = new Dictionary<Terminals, Func<Treenode>>();
            Factories[NotTerminals.cpsDecl][Terminals.PROC] = () => { return new CpsDeclPROC(); };
            Factories[NotTerminals.cpsDecl][Terminals.FUN] = () => { return new CpsDeclFUN(); };
            Factories[NotTerminals.cpsDecl][Terminals.CHANGEMODE] = () => { return new CpsDeclCHANGEMODE(); };
            Factories[NotTerminals.cpsDecl][Terminals.IDENT] = () => { return new CpsDeclIDENT(); };

            Factories[NotTerminals.repCpsDecl] = new Dictionary<Terminals, Func<Treenode>>();
            Factories[NotTerminals.repCpsDecl][Terminals.SEMICOLON] = () => { return new RepCpsDeclSEMICOLON(); };
            Factories[NotTerminals.repCpsDecl][Terminals.DO] = () => { return new RepCpsDeclDO(); };

            Factories[NotTerminals.optCpsStoDecl] = new Dictionary<Terminals, Func<Treenode>>();
            Factories[NotTerminals.optCpsStoDecl][Terminals.LOCAL] = () => { return new OptCpsStoDeclLOCAL(); };
            Factories[NotTerminals.optCpsStoDecl][Terminals.DO] = () => { return new OptCpsStoDeclDO(); };

            Factories[NotTerminals.cpsStoDecl] = new Dictionary<Terminals, Func<Treenode>>();
            Factories[NotTerminals.cpsStoDecl][Terminals.CHANGEMODE] = () => { return new CpsStoDeclCHANGEMODE(); };
            Factories[NotTerminals.cpsStoDecl][Terminals.IDENT] = () => { return new CpsStoDeclIDENT(); };

            Factories[NotTerminals.repCpsStoDecl] = new Dictionary<Terminals, Func<Treenode>>();
            Factories[NotTerminals.repCpsStoDecl][Terminals.SEMICOLON] = () => { return new RepCpsStoDeclSEMICOLON(); };
            Factories[NotTerminals.repCpsStoDecl][Terminals.DO] = () => { return new RepCpsStoDeclDO(); };

            Factories[NotTerminals.progParamList] = new Dictionary<Terminals, Func<Treenode>>();
            Factories[NotTerminals.progParamList][Terminals.LPAREN] = () => { return new ProgParamListLPAREN(); };

            Factories[NotTerminals.optProgParamList] = new Dictionary<Terminals, Func<Treenode>>();
            Factories[NotTerminals.optProgParamList][Terminals.FLOWMODE] = () => { return new OptProgParamListFLOWMODE(); };
            Factories[NotTerminals.optProgParamList][Terminals.IDENT] = () => { return new OptProgParamListIDENT(); };
            Factories[NotTerminals.optProgParamList][Terminals.CHANGEMODE] = () => { return new OptProgParamListCHANGEMODE(); };
            Factories[NotTerminals.optProgParamList][Terminals.RPAREN] = () => { return new OptProgParamListRPAREN(); };

            Factories[NotTerminals.repProgParamList] = new Dictionary<Terminals, Func<Treenode>>();
            Factories[NotTerminals.repProgParamList][Terminals.COMMA] = () => { return new RepProgParamListCOMMA(); };
            Factories[NotTerminals.repProgParamList][Terminals.RPAREN] = () => { return new RepProgParamListRPAREN(); };

            Factories[NotTerminals.progParam] = new Dictionary<Terminals, Func<Treenode>>();
            Factories[NotTerminals.progParam][Terminals.IDENT] = () => { return new ProgParamIDENT(); };
            Factories[NotTerminals.progParam][Terminals.CHANGEMODE] = () => { return new ProgParamCHANGEMODE(); };
            Factories[NotTerminals.progParam][Terminals.FLOWMODE] = () => { return new ProgParamFLOWMODE(); };

            Factories[NotTerminals.paramList] = new Dictionary<Terminals, Func<Treenode>>();
            Factories[NotTerminals.paramList][Terminals.LPAREN] = () => { return new ParamListLPAREN(); };

            Factories[NotTerminals.optParamList] = new Dictionary<Terminals, Func<Treenode>>();
            Factories[NotTerminals.optParamList][Terminals.FLOWMODE] = () => { return new OptParamListFLOWMODE(); };
            Factories[NotTerminals.optParamList][Terminals.IDENT] = () => { return new OptParamListIDENT(); };
            Factories[NotTerminals.optParamList][Terminals.CHANGEMODE] = () => { return new OptParamListCHANGEMODE(); };
            Factories[NotTerminals.optParamList][Terminals.MECHMODE] = () => { return new OptParamListMECHMODE(); };
            Factories[NotTerminals.optParamList][Terminals.RPAREN] = () => { return new OptParamListRPAREN(); };

            Factories[NotTerminals.repParamList] = new Dictionary<Terminals, Func<Treenode>>();
            Factories[NotTerminals.repParamList][Terminals.COMMA] = () => { return new RepParamListCOMMA(); };
            Factories[NotTerminals.repParamList][Terminals.RPAREN] = () => { return new RepParamListRPAREN(); };

            Factories[NotTerminals.param] = new Dictionary<Terminals, Func<Treenode>>();
            Factories[NotTerminals.param][Terminals.IDENT] = () => { return new ParamIDENT(); };
            Factories[NotTerminals.param][Terminals.CHANGEMODE] = () => { return new ParamCHANGEMODE(); };
            Factories[NotTerminals.param][Terminals.MECHMODE] = () => { return new ParamMECHMODE(); };
            Factories[NotTerminals.param][Terminals.FLOWMODE] = () => { return new ParamFLOWMODE(); };

            Factories[NotTerminals.typedIdent] = new Dictionary<Terminals, Func<Treenode>>();
            Factories[NotTerminals.typedIdent][Terminals.IDENT] = () => { return new TypedIdentIDENT(); };

            Factories[NotTerminals.cmd] = new Dictionary<Terminals, Func<Treenode>>();
            Factories[NotTerminals.cmd][Terminals.SKIP] = () => { return new CmdSKIP(); };
            Factories[NotTerminals.cmd][Terminals.TYPE] = () => { return new CmdTYPE(); };
            Factories[NotTerminals.cmd][Terminals.LPAREN] = () => { return new CmdLPAREN(); };
            Factories[NotTerminals.cmd][Terminals.ADDOPR] = () => { return new CmdADDOPR(); };
            Factories[NotTerminals.cmd][Terminals.NOT] = () => { return new CmdNOT(); };
            Factories[NotTerminals.cmd][Terminals.IDENT] = () => { return new CmdIDENT(); };
            Factories[NotTerminals.cmd][Terminals.LITERAL] = () => { return new CmdLITERAL(); };
            Factories[NotTerminals.cmd][Terminals.IF] = () => { return new CmdIF(); };
            Factories[NotTerminals.cmd][Terminals.WHILE] = () => { return new CmdWHILE(); };
            Factories[NotTerminals.cmd][Terminals.CALL] = () => { return new CmdCALL(); };
            Factories[NotTerminals.cmd][Terminals.DEBUGIN] = () => { return new CmdDEBUGIN(); };
            Factories[NotTerminals.cmd][Terminals.DEBUGOUT] = () => { return new CmdDEBUGOUT(); };

            Factories[NotTerminals.cpsCmd] = new Dictionary<Terminals, Func<Treenode>>();
            Factories[NotTerminals.cpsCmd][Terminals.DEBUGOUT] = () => { return new CpsCmdDEBUGOUT(); };
            Factories[NotTerminals.cpsCmd][Terminals.DEBUGIN] = () => { return new CpsCmdDEBUGIN(); };
            Factories[NotTerminals.cpsCmd][Terminals.CALL] = () => { return new CpsCmdCALL(); };
            Factories[NotTerminals.cpsCmd][Terminals.WHILE] = () => { return new CpsCmdWHILE(); };
            Factories[NotTerminals.cpsCmd][Terminals.IF] = () => { return new CpsCmdIF(); };
            Factories[NotTerminals.cpsCmd][Terminals.TYPE] = () => { return new CpsCmdTYPE(); };
            Factories[NotTerminals.cpsCmd][Terminals.LPAREN] = () => { return new CpsCmdLPAREN(); };
            Factories[NotTerminals.cpsCmd][Terminals.ADDOPR] = () => { return new CpsCmdADDOPR(); };
            Factories[NotTerminals.cpsCmd][Terminals.NOT] = () => { return new CpsCmdNOT(); };
            Factories[NotTerminals.cpsCmd][Terminals.IDENT] = () => { return new CpsCmdIDENT(); };
            Factories[NotTerminals.cpsCmd][Terminals.LITERAL] = () => { return new CpsCmdLITERAL(); };
            Factories[NotTerminals.cpsCmd][Terminals.SKIP] = () => { return new CpsCmdSKIP(); };

            Factories[NotTerminals.repCpsCmd] = new Dictionary<Terminals, Func<Treenode>>();
            Factories[NotTerminals.repCpsCmd][Terminals.SEMICOLON] = () => { return new RepCpsCmdSEMICOLON(); };
            Factories[NotTerminals.repCpsCmd][Terminals.ENDWHILE] = () => { return new RepCpsCmdENDWHILE(); };
            Factories[NotTerminals.repCpsCmd][Terminals.ENDIF] = () => { return new RepCpsCmdENDIF(); };
            Factories[NotTerminals.repCpsCmd][Terminals.ELSE] = () => { return new RepCpsCmdELSE(); };
            Factories[NotTerminals.repCpsCmd][Terminals.ENDPROC] = () => { return new RepCpsCmdENDPROC(); };
            Factories[NotTerminals.repCpsCmd][Terminals.ENDFUN] = () => { return new RepCpsCmdENDFUN(); };
            Factories[NotTerminals.repCpsCmd][Terminals.ENDPROGRAM] = () => { return new RepCpsCmdENDPROGRAM(); };

            Factories[NotTerminals.optGlobInits] = new Dictionary<Terminals, Func<Treenode>>();
            Factories[NotTerminals.optGlobInits][Terminals.INIT] = () => { return new OptGlobInitsINIT(); };
            Factories[NotTerminals.optGlobInits][Terminals.ENDWHILE] = () => { return new OptGlobInitsENDWHILE(); };
            Factories[NotTerminals.optGlobInits][Terminals.ENDIF] = () => { return new OptGlobInitsENDIF(); };
            Factories[NotTerminals.optGlobInits][Terminals.ELSE] = () => { return new OptGlobInitsELSE(); };
            Factories[NotTerminals.optGlobInits][Terminals.ENDPROC] = () => { return new OptGlobInitsENDPROC(); };
            Factories[NotTerminals.optGlobInits][Terminals.ENDFUN] = () => { return new OptGlobInitsENDFUN(); };
            Factories[NotTerminals.optGlobInits][Terminals.ENDPROGRAM] = () => { return new OptGlobInitsENDPROGRAM(); };
            Factories[NotTerminals.optGlobInits][Terminals.SEMICOLON] = () => { return new OptGlobInitsSEMICOLON(); };

            Factories[NotTerminals.repIdents] = new Dictionary<Terminals, Func<Treenode>>();
            Factories[NotTerminals.repIdents][Terminals.COMMA] = () => { return new RepIdentsCOMMA(); };
            Factories[NotTerminals.repIdents][Terminals.ENDWHILE] = () => { return new RepIdentsENDWHILE(); };
            Factories[NotTerminals.repIdents][Terminals.ENDIF] = () => { return new RepIdentsENDIF(); };
            Factories[NotTerminals.repIdents][Terminals.ELSE] = () => { return new RepIdentsELSE(); };
            Factories[NotTerminals.repIdents][Terminals.ENDPROC] = () => { return new RepIdentsENDPROC(); };
            Factories[NotTerminals.repIdents][Terminals.ENDFUN] = () => { return new RepIdentsENDFUN(); };
            Factories[NotTerminals.repIdents][Terminals.ENDPROGRAM] = () => { return new RepIdentsENDPROGRAM(); };
            Factories[NotTerminals.repIdents][Terminals.SEMICOLON] = () => { return new RepIdentsSEMICOLON(); };

            Factories[NotTerminals.expr] = new Dictionary<Terminals, Func<Treenode>>();
            Factories[NotTerminals.expr][Terminals.TYPE] = () => { return new ExprTYPE(); };
            Factories[NotTerminals.expr][Terminals.LPAREN] = () => { return new ExprLPAREN(); };
            Factories[NotTerminals.expr][Terminals.ADDOPR] = () => { return new ExprADDOPR(); };
            Factories[NotTerminals.expr][Terminals.NOT] = () => { return new ExprNOT(); };
            Factories[NotTerminals.expr][Terminals.IDENT] = () => { return new ExprIDENT(); };
            Factories[NotTerminals.expr][Terminals.LITERAL] = () => { return new ExprLITERAL(); };

            Factories[NotTerminals.repTerm1] = new Dictionary<Terminals, Func<Treenode>>();
            Factories[NotTerminals.repTerm1][Terminals.BOOLOPR] = () => { return new RepTerm1BOOLOPR(); };
            Factories[NotTerminals.repTerm1][Terminals.COMMA] = () => { return new RepTerm1COMMA(); };
            Factories[NotTerminals.repTerm1][Terminals.RPAREN] = () => { return new RepTerm1RPAREN(); };
            Factories[NotTerminals.repTerm1][Terminals.DO] = () => { return new RepTerm1DO(); };
            Factories[NotTerminals.repTerm1][Terminals.THEN] = () => { return new RepTerm1THEN(); };
            Factories[NotTerminals.repTerm1][Terminals.ENDWHILE] = () => { return new RepTerm1ENDWHILE(); };
            Factories[NotTerminals.repTerm1][Terminals.ENDIF] = () => { return new RepTerm1ENDIF(); };
            Factories[NotTerminals.repTerm1][Terminals.ELSE] = () => { return new RepTerm1ELSE(); };
            Factories[NotTerminals.repTerm1][Terminals.ENDPROC] = () => { return new RepTerm1ENDPROC(); };
            Factories[NotTerminals.repTerm1][Terminals.ENDFUN] = () => { return new RepTerm1ENDFUN(); };
            Factories[NotTerminals.repTerm1][Terminals.ENDPROGRAM] = () => { return new RepTerm1ENDPROGRAM(); };
            Factories[NotTerminals.repTerm1][Terminals.SEMICOLON] = () => { return new RepTerm1SEMICOLON(); };
            Factories[NotTerminals.repTerm1][Terminals.BECOMES] = () => { return new RepTerm1BECOMES(); };

            Factories[NotTerminals.term1] = new Dictionary<Terminals, Func<Treenode>>();
            Factories[NotTerminals.term1][Terminals.TYPE] = () => { return new Term1TYPE(); };
            Factories[NotTerminals.term1][Terminals.LPAREN] = () => { return new Term1LPAREN(); };
            Factories[NotTerminals.term1][Terminals.ADDOPR] = () => { return new Term1ADDOPR(); };
            Factories[NotTerminals.term1][Terminals.NOT] = () => { return new Term1NOT(); };
            Factories[NotTerminals.term1][Terminals.IDENT] = () => { return new Term1IDENT(); };
            Factories[NotTerminals.term1][Terminals.LITERAL] = () => { return new Term1LITERAL(); };

            Factories[NotTerminals.repTerm2] = new Dictionary<Terminals, Func<Treenode>>();
            Factories[NotTerminals.repTerm2][Terminals.RELOPR] = () => { return new RepTerm2RELOPR(); };
            Factories[NotTerminals.repTerm2][Terminals.COMMA] = () => { return new RepTerm2COMMA(); };
            Factories[NotTerminals.repTerm2][Terminals.RPAREN] = () => { return new RepTerm2RPAREN(); };
            Factories[NotTerminals.repTerm2][Terminals.DO] = () => { return new RepTerm2DO(); };
            Factories[NotTerminals.repTerm2][Terminals.THEN] = () => { return new RepTerm2THEN(); };
            Factories[NotTerminals.repTerm2][Terminals.ENDWHILE] = () => { return new RepTerm2ENDWHILE(); };
            Factories[NotTerminals.repTerm2][Terminals.ENDIF] = () => { return new RepTerm2ENDIF(); };
            Factories[NotTerminals.repTerm2][Terminals.ELSE] = () => { return new RepTerm2ELSE(); };
            Factories[NotTerminals.repTerm2][Terminals.ENDPROC] = () => { return new RepTerm2ENDPROC(); };
            Factories[NotTerminals.repTerm2][Terminals.ENDFUN] = () => { return new RepTerm2ENDFUN(); };
            Factories[NotTerminals.repTerm2][Terminals.ENDPROGRAM] = () => { return new RepTerm2ENDPROGRAM(); };
            Factories[NotTerminals.repTerm2][Terminals.SEMICOLON] = () => { return new RepTerm2SEMICOLON(); };
            Factories[NotTerminals.repTerm2][Terminals.BECOMES] = () => { return new RepTerm2BECOMES(); };
            Factories[NotTerminals.repTerm2][Terminals.BOOLOPR] = () => { return new RepTerm2BOOLOPR(); };

            Factories[NotTerminals.term2] = new Dictionary<Terminals, Func<Treenode>>();
            Factories[NotTerminals.term2][Terminals.TYPE] = () => { return new Term2TYPE(); };
            Factories[NotTerminals.term2][Terminals.LPAREN] = () => { return new Term2LPAREN(); };
            Factories[NotTerminals.term2][Terminals.ADDOPR] = () => { return new Term2ADDOPR(); };
            Factories[NotTerminals.term2][Terminals.NOT] = () => { return new Term2NOT(); };
            Factories[NotTerminals.term2][Terminals.IDENT] = () => { return new Term2IDENT(); };
            Factories[NotTerminals.term2][Terminals.LITERAL] = () => { return new Term2LITERAL(); };

            Factories[NotTerminals.repTerm3] = new Dictionary<Terminals, Func<Treenode>>();
            Factories[NotTerminals.repTerm3][Terminals.ADDOPR] = () => { return new RepTerm3ADDOPR(); };
            Factories[NotTerminals.repTerm3][Terminals.COMMA] = () => { return new RepTerm3COMMA(); };
            Factories[NotTerminals.repTerm3][Terminals.RPAREN] = () => { return new RepTerm3RPAREN(); };
            Factories[NotTerminals.repTerm3][Terminals.DO] = () => { return new RepTerm3DO(); };
            Factories[NotTerminals.repTerm3][Terminals.THEN] = () => { return new RepTerm3THEN(); };
            Factories[NotTerminals.repTerm3][Terminals.ENDWHILE] = () => { return new RepTerm3ENDWHILE(); };
            Factories[NotTerminals.repTerm3][Terminals.ENDIF] = () => { return new RepTerm3ENDIF(); };
            Factories[NotTerminals.repTerm3][Terminals.ELSE] = () => { return new RepTerm3ELSE(); };
            Factories[NotTerminals.repTerm3][Terminals.ENDPROC] = () => { return new RepTerm3ENDPROC(); };
            Factories[NotTerminals.repTerm3][Terminals.ENDFUN] = () => { return new RepTerm3ENDFUN(); };
            Factories[NotTerminals.repTerm3][Terminals.ENDPROGRAM] = () => { return new RepTerm3ENDPROGRAM(); };
            Factories[NotTerminals.repTerm3][Terminals.SEMICOLON] = () => { return new RepTerm3SEMICOLON(); };
            Factories[NotTerminals.repTerm3][Terminals.BECOMES] = () => { return new RepTerm3BECOMES(); };
            Factories[NotTerminals.repTerm3][Terminals.BOOLOPR] = () => { return new RepTerm3BOOLOPR(); };
            Factories[NotTerminals.repTerm3][Terminals.RELOPR] = () => { return new RepTerm3RELOPR(); };

            Factories[NotTerminals.term3] = new Dictionary<Terminals, Func<Treenode>>();
            Factories[NotTerminals.term3][Terminals.TYPE] = () => { return new Term3TYPE(); };
            Factories[NotTerminals.term3][Terminals.LPAREN] = () => { return new Term3LPAREN(); };
            Factories[NotTerminals.term3][Terminals.ADDOPR] = () => { return new Term3ADDOPR(); };
            Factories[NotTerminals.term3][Terminals.NOT] = () => { return new Term3NOT(); };
            Factories[NotTerminals.term3][Terminals.IDENT] = () => { return new Term3IDENT(); };
            Factories[NotTerminals.term3][Terminals.LITERAL] = () => { return new Term3LITERAL(); };

            Factories[NotTerminals.repFactor] = new Dictionary<Terminals, Func<Treenode>>();
            Factories[NotTerminals.repFactor][Terminals.MULTOPR] = () => { return new RepFactorMULTOPR(); };
            Factories[NotTerminals.repFactor][Terminals.COMMA] = () => { return new RepFactorCOMMA(); };
            Factories[NotTerminals.repFactor][Terminals.RPAREN] = () => { return new RepFactorRPAREN(); };
            Factories[NotTerminals.repFactor][Terminals.DO] = () => { return new RepFactorDO(); };
            Factories[NotTerminals.repFactor][Terminals.THEN] = () => { return new RepFactorTHEN(); };
            Factories[NotTerminals.repFactor][Terminals.ENDWHILE] = () => { return new RepFactorENDWHILE(); };
            Factories[NotTerminals.repFactor][Terminals.ENDIF] = () => { return new RepFactorENDIF(); };
            Factories[NotTerminals.repFactor][Terminals.ELSE] = () => { return new RepFactorELSE(); };
            Factories[NotTerminals.repFactor][Terminals.ENDPROC] = () => { return new RepFactorENDPROC(); };
            Factories[NotTerminals.repFactor][Terminals.ENDFUN] = () => { return new RepFactorENDFUN(); };
            Factories[NotTerminals.repFactor][Terminals.ENDPROGRAM] = () => { return new RepFactorENDPROGRAM(); };
            Factories[NotTerminals.repFactor][Terminals.SEMICOLON] = () => { return new RepFactorSEMICOLON(); };
            Factories[NotTerminals.repFactor][Terminals.BECOMES] = () => { return new RepFactorBECOMES(); };
            Factories[NotTerminals.repFactor][Terminals.BOOLOPR] = () => { return new RepFactorBOOLOPR(); };
            Factories[NotTerminals.repFactor][Terminals.RELOPR] = () => { return new RepFactorRELOPR(); };
            Factories[NotTerminals.repFactor][Terminals.ADDOPR] = () => { return new RepFactorADDOPR(); };

            Factories[NotTerminals.factor] = new Dictionary<Terminals, Func<Treenode>>();
            Factories[NotTerminals.factor][Terminals.LITERAL] = () => { return new FactorLITERAL(); };
            Factories[NotTerminals.factor][Terminals.IDENT] = () => { return new FactorIDENT(); };
            Factories[NotTerminals.factor][Terminals.ADDOPR] = () => { return new FactorADDOPR(); };
            Factories[NotTerminals.factor][Terminals.NOT] = () => { return new FactorNOT(); };
            Factories[NotTerminals.factor][Terminals.LPAREN] = () => { return new FactorLPAREN(); };
            Factories[NotTerminals.factor][Terminals.TYPE] = () => { return new FactorTYPE(); };

            Factories[NotTerminals.optInitOrExprList] = new Dictionary<Terminals, Func<Treenode>>();
            Factories[NotTerminals.optInitOrExprList][Terminals.INIT] = () => { return new OptInitOrExprListINIT(); };
            Factories[NotTerminals.optInitOrExprList][Terminals.LPAREN] = () => { return new OptInitOrExprListLPAREN(); };
            Factories[NotTerminals.optInitOrExprList][Terminals.COMMA] = () => { return new OptInitOrExprListCOMMA(); };
            Factories[NotTerminals.optInitOrExprList][Terminals.RPAREN] = () => { return new OptInitOrExprListRPAREN(); };
            Factories[NotTerminals.optInitOrExprList][Terminals.DO] = () => { return new OptInitOrExprListDO(); };
            Factories[NotTerminals.optInitOrExprList][Terminals.THEN] = () => { return new OptInitOrExprListTHEN(); };
            Factories[NotTerminals.optInitOrExprList][Terminals.ENDWHILE] = () => { return new OptInitOrExprListENDWHILE(); };
            Factories[NotTerminals.optInitOrExprList][Terminals.ENDIF] = () => { return new OptInitOrExprListENDIF(); };
            Factories[NotTerminals.optInitOrExprList][Terminals.ELSE] = () => { return new OptInitOrExprListELSE(); };
            Factories[NotTerminals.optInitOrExprList][Terminals.ENDPROC] = () => { return new OptInitOrExprListENDPROC(); };
            Factories[NotTerminals.optInitOrExprList][Terminals.ENDFUN] = () => { return new OptInitOrExprListENDFUN(); };
            Factories[NotTerminals.optInitOrExprList][Terminals.ENDPROGRAM] = () => { return new OptInitOrExprListENDPROGRAM(); };
            Factories[NotTerminals.optInitOrExprList][Terminals.SEMICOLON] = () => { return new OptInitOrExprListSEMICOLON(); };
            Factories[NotTerminals.optInitOrExprList][Terminals.BECOMES] = () => { return new OptInitOrExprListBECOMES(); };
            Factories[NotTerminals.optInitOrExprList][Terminals.BOOLOPR] = () => { return new OptInitOrExprListBOOLOPR(); };
            Factories[NotTerminals.optInitOrExprList][Terminals.RELOPR] = () => { return new OptInitOrExprListRELOPR(); };
            Factories[NotTerminals.optInitOrExprList][Terminals.ADDOPR] = () => { return new OptInitOrExprListADDOPR(); };
            Factories[NotTerminals.optInitOrExprList][Terminals.MULTOPR] = () => { return new OptInitOrExprListMULTOPR(); };

            Factories[NotTerminals.monadicOpr] = new Dictionary<Terminals, Func<Treenode>>();
            Factories[NotTerminals.monadicOpr][Terminals.NOT] = () => { return new MonadicOprNOT(); };
            Factories[NotTerminals.monadicOpr][Terminals.ADDOPR] = () => { return new MonadicOprADDOPR(); };

            Factories[NotTerminals.exprList] = new Dictionary<Terminals, Func<Treenode>>();
            Factories[NotTerminals.exprList][Terminals.LPAREN] = () => { return new ExprListLPAREN(); };

            Factories[NotTerminals.optExprList] = new Dictionary<Terminals, Func<Treenode>>();
            Factories[NotTerminals.optExprList][Terminals.TYPE] = () => { return new OptExprListTYPE(); };
            Factories[NotTerminals.optExprList][Terminals.LPAREN] = () => { return new OptExprListLPAREN(); };
            Factories[NotTerminals.optExprList][Terminals.ADDOPR] = () => { return new OptExprListADDOPR(); };
            Factories[NotTerminals.optExprList][Terminals.NOT] = () => { return new OptExprListNOT(); };
            Factories[NotTerminals.optExprList][Terminals.IDENT] = () => { return new OptExprListIDENT(); };
            Factories[NotTerminals.optExprList][Terminals.LITERAL] = () => { return new OptExprListLITERAL(); };
            Factories[NotTerminals.optExprList][Terminals.RPAREN] = () => { return new OptExprListRPAREN(); };

            Factories[NotTerminals.repExprList] = new Dictionary<Terminals, Func<Treenode>>();
            Factories[NotTerminals.repExprList][Terminals.COMMA] = () => { return new RepExprListCOMMA(); };
            Factories[NotTerminals.repExprList][Terminals.RPAREN] = () => { return new RepExprListRPAREN(); };

        }
    }

    public class Tokennode : Treenode { public Token Token { get; private set; } public Tokennode(Token token) { this.Token = token; } }
    public interface Program : Treenode { }
    public class ProgramPROGRAM : Program
    {
        public Tokennode PROGRAM { get; set; }
        public Tokennode IDENT { get; set; }
        public ProgParamList ProgParamList { get; set; }
        public OptCpsDecl OptCpsDecl { get; set; }
        public Tokennode DO { get; set; }
        public CpsCmd CpsCmd { get; set; }
        public Tokennode ENDPROGRAM { get; set; }
    }
    public interface Decl : Treenode { }
    public class DeclCHANGEMODE : Decl
    {
        public StoDecl StoDecl { get; set; }
    }
    public class DeclIDENT : Decl
    {
        public StoDecl StoDecl { get; set; }
    }
    public class DeclFUN : Decl
    {
        public FunDecl FunDecl { get; set; }
    }
    public class DeclPROC : Decl
    {
        public ProcDecl ProcDecl { get; set; }
    }
    public interface StoDecl : Treenode { }
    public class StoDeclIDENT : StoDecl
    {
        public TypedIdent TypedIdent { get; set; }
    }
    public class StoDeclCHANGEMODE : StoDecl
    {
        public Tokennode CHANGEMODE { get; set; }
        public TypedIdent TypedIdent { get; set; }
    }
    public interface FunDecl : Treenode { }
    public class FunDeclFUN : FunDecl
    {
        public Tokennode FUN { get; set; }
        public Tokennode IDENT { get; set; }
        public ParamList ParamList { get; set; }
        public Tokennode RETURNS { get; set; }
        public StoDecl StoDecl { get; set; }
        public OptGlobImps OptGlobImps { get; set; }
        public OptCpsStoDecl OptCpsStoDecl { get; set; }
        public Tokennode DO { get; set; }
        public CpsCmd CpsCmd { get; set; }
        public Tokennode ENDFUN { get; set; }
    }
    public interface ProcDecl : Treenode { }
    public class ProcDeclPROC : ProcDecl
    {
        public Tokennode PROC { get; set; }
        public Tokennode IDENT { get; set; }
        public ParamList ParamList { get; set; }
        public OptGlobImps OptGlobImps { get; set; }
        public OptCpsStoDecl OptCpsStoDecl { get; set; }
        public Tokennode DO { get; set; }
        public CpsCmd CpsCmd { get; set; }
        public Tokennode ENDPROC { get; set; }
    }
    public interface OptGlobImps : Treenode { }
    public class OptGlobImpsGLOBAL : OptGlobImps
    {
        public Tokennode GLOBAL { get; set; }
        public GlobImps GlobImps { get; set; }
    }
    public class OptGlobImpsDO : OptGlobImps
    {
    }
    public class OptGlobImpsLOCAL : OptGlobImps
    {
    }
    public interface GlobImps : Treenode { }
    public class GlobImpsFLOWMODE : GlobImps
    {
        public GlobImp GlobImp { get; set; }
        public RepGlobImps RepGlobImps { get; set; }
    }
    public class GlobImpsIDENT : GlobImps
    {
        public GlobImp GlobImp { get; set; }
        public RepGlobImps RepGlobImps { get; set; }
    }
    public class GlobImpsCHANGEMODE : GlobImps
    {
        public GlobImp GlobImp { get; set; }
        public RepGlobImps RepGlobImps { get; set; }
    }
    public interface RepGlobImps : Treenode { }
    public class RepGlobImpsCOMMA : RepGlobImps
    {
        public Tokennode COMMA { get; set; }
        public GlobImp GlobImp { get; set; }
        public RepGlobImps RepGlobImps { get; set; }
    }
    public class RepGlobImpsDO : RepGlobImps
    {
    }
    public class RepGlobImpsLOCAL : RepGlobImps
    {
    }
    public interface OptChangemode : Treenode { }
    public class OptChangemodeCHANGEMODE : OptChangemode
    {
        public Tokennode CHANGEMODE { get; set; }
    }
    public class OptChangemodeIDENT : OptChangemode
    {
    }
    public interface OptMechmode : Treenode { }
    public class OptMechmodeMECHMODE : OptMechmode
    {
        public Tokennode MECHMODE { get; set; }
    }
    public class OptMechmodeIDENT : OptMechmode
    {
    }
    public class OptMechmodeCHANGEMODE : OptMechmode
    {
    }
    public interface GlobImp : Treenode { }
    public class GlobImpIDENT : GlobImp
    {
        public OptChangemode OptChangemode { get; set; }
        public Tokennode IDENT { get; set; }
    }
    public class GlobImpCHANGEMODE : GlobImp
    {
        public OptChangemode OptChangemode { get; set; }
        public Tokennode IDENT { get; set; }
    }
    public class GlobImpFLOWMODE : GlobImp
    {
        public Tokennode FLOWMODE { get; set; }
        public OptChangemode OptChangemode { get; set; }
        public Tokennode IDENT { get; set; }
    }
    public interface OptCpsDecl : Treenode { }
    public class OptCpsDeclGLOBAL : OptCpsDecl
    {
        public Tokennode GLOBAL { get; set; }
        public CpsDecl CpsDecl { get; set; }
    }
    public class OptCpsDeclDO : OptCpsDecl
    {
    }
    public interface CpsDecl : Treenode { }
    public class CpsDeclPROC : CpsDecl
    {
        public Decl Decl { get; set; }
        public RepCpsDecl RepCpsDecl { get; set; }
    }
    public class CpsDeclFUN : CpsDecl
    {
        public Decl Decl { get; set; }
        public RepCpsDecl RepCpsDecl { get; set; }
    }
    public class CpsDeclCHANGEMODE : CpsDecl
    {
        public Decl Decl { get; set; }
        public RepCpsDecl RepCpsDecl { get; set; }
    }
    public class CpsDeclIDENT : CpsDecl
    {
        public Decl Decl { get; set; }
        public RepCpsDecl RepCpsDecl { get; set; }
    }
    public interface RepCpsDecl : Treenode { }
    public class RepCpsDeclSEMICOLON : RepCpsDecl
    {
        public Tokennode SEMICOLON { get; set; }
        public Decl Decl { get; set; }
        public RepCpsDecl RepCpsDecl { get; set; }
    }
    public class RepCpsDeclDO : RepCpsDecl
    {
    }
    public interface OptCpsStoDecl : Treenode { }
    public class OptCpsStoDeclLOCAL : OptCpsStoDecl
    {
        public Tokennode LOCAL { get; set; }
        public CpsStoDecl CpsStoDecl { get; set; }
    }
    public class OptCpsStoDeclDO : OptCpsStoDecl
    {
    }
    public interface CpsStoDecl : Treenode { }
    public class CpsStoDeclCHANGEMODE : CpsStoDecl
    {
        public StoDecl StoDecl { get; set; }
        public RepCpsStoDecl RepCpsStoDecl { get; set; }
    }
    public class CpsStoDeclIDENT : CpsStoDecl
    {
        public StoDecl StoDecl { get; set; }
        public RepCpsStoDecl RepCpsStoDecl { get; set; }
    }
    public interface RepCpsStoDecl : Treenode { }
    public class RepCpsStoDeclSEMICOLON : RepCpsStoDecl
    {
        public Tokennode SEMICOLON { get; set; }
        public StoDecl StoDecl { get; set; }
        public RepCpsStoDecl RepCpsStoDecl { get; set; }
    }
    public class RepCpsStoDeclDO : RepCpsStoDecl
    {
    }
    public interface ProgParamList : Treenode { }
    public class ProgParamListLPAREN : ProgParamList
    {
        public Tokennode LPAREN { get; set; }
        public OptProgParamList OptProgParamList { get; set; }
        public Tokennode RPAREN { get; set; }
    }
    public interface OptProgParamList : Treenode { }
    public class OptProgParamListFLOWMODE : OptProgParamList
    {
        public ProgParam ProgParam { get; set; }
        public RepProgParamList RepProgParamList { get; set; }
    }
    public class OptProgParamListIDENT : OptProgParamList
    {
        public ProgParam ProgParam { get; set; }
        public RepProgParamList RepProgParamList { get; set; }
    }
    public class OptProgParamListCHANGEMODE : OptProgParamList
    {
        public ProgParam ProgParam { get; set; }
        public RepProgParamList RepProgParamList { get; set; }
    }
    public class OptProgParamListRPAREN : OptProgParamList
    {
    }
    public interface RepProgParamList : Treenode { }
    public class RepProgParamListCOMMA : RepProgParamList
    {
        public Tokennode COMMA { get; set; }
        public ProgParam ProgParam { get; set; }
        public RepProgParamList RepProgParamList { get; set; }
    }
    public class RepProgParamListRPAREN : RepProgParamList
    {
    }
    public interface ProgParam : Treenode { }
    public class ProgParamIDENT : ProgParam
    {
        public OptChangemode OptChangemode { get; set; }
        public TypedIdent TypedIdent { get; set; }
    }
    public class ProgParamCHANGEMODE : ProgParam
    {
        public OptChangemode OptChangemode { get; set; }
        public TypedIdent TypedIdent { get; set; }
    }
    public class ProgParamFLOWMODE : ProgParam
    {
        public Tokennode FLOWMODE { get; set; }
        public OptChangemode OptChangemode { get; set; }
        public TypedIdent TypedIdent { get; set; }
    }
    public interface ParamList : Treenode { }
    public class ParamListLPAREN : ParamList
    {
        public Tokennode LPAREN { get; set; }
        public OptParamList OptParamList { get; set; }
        public Tokennode RPAREN { get; set; }
    }
    public interface OptParamList : Treenode { }
    public class OptParamListFLOWMODE : OptParamList
    {
        public Param Param { get; set; }
        public RepParamList RepParamList { get; set; }
    }
    public class OptParamListIDENT : OptParamList
    {
        public Param Param { get; set; }
        public RepParamList RepParamList { get; set; }
    }
    public class OptParamListCHANGEMODE : OptParamList
    {
        public Param Param { get; set; }
        public RepParamList RepParamList { get; set; }
    }
    public class OptParamListMECHMODE : OptParamList
    {
        public Param Param { get; set; }
        public RepParamList RepParamList { get; set; }
    }
    public class OptParamListRPAREN : OptParamList
    {
    }
    public interface RepParamList : Treenode { }
    public class RepParamListCOMMA : RepParamList
    {
        public Tokennode COMMA { get; set; }
        public Param Param { get; set; }
        public RepParamList RepParamList { get; set; }
    }
    public class RepParamListRPAREN : RepParamList
    {
    }
    public interface Param : Treenode { }
    public class ParamIDENT : Param
    {
        public OptMechmode OptMechmode { get; set; }
        public OptChangemode OptChangemode { get; set; }
        public TypedIdent TypedIdent { get; set; }
    }
    public class ParamCHANGEMODE : Param
    {
        public OptMechmode OptMechmode { get; set; }
        public OptChangemode OptChangemode { get; set; }
        public TypedIdent TypedIdent { get; set; }
    }
    public class ParamMECHMODE : Param
    {
        public OptMechmode OptMechmode { get; set; }
        public OptChangemode OptChangemode { get; set; }
        public TypedIdent TypedIdent { get; set; }
    }
    public class ParamFLOWMODE : Param
    {
        public Tokennode FLOWMODE { get; set; }
        public OptMechmode OptMechmode { get; set; }
        public OptChangemode OptChangemode { get; set; }
        public TypedIdent TypedIdent { get; set; }
    }
    public interface TypedIdent : Treenode { }
    public class TypedIdentIDENT : TypedIdent
    {
        public Tokennode IDENT { get; set; }
        public Tokennode COLON { get; set; }
        public Tokennode TYPE { get; set; }
    }
    public interface Cmd : Treenode { }
    public class CmdSKIP : Cmd
    {
        public Tokennode SKIP { get; set; }
    }
    public class CmdTYPE : Cmd
    {
        public Expr Expr { get; set; }
        public Tokennode BECOMES { get; set; }
        public Expr Expr2 { get; set; }
    }
    public class CmdLPAREN : Cmd
    {
        public Expr Expr { get; set; }
        public Tokennode BECOMES { get; set; }
        public Expr Expr2 { get; set; }
    }
    public class CmdADDOPR : Cmd
    {
        public Expr Expr { get; set; }
        public Tokennode BECOMES { get; set; }
        public Expr Expr2 { get; set; }
    }
    public class CmdNOT : Cmd
    {
        public Expr Expr { get; set; }
        public Tokennode BECOMES { get; set; }
        public Expr Expr2 { get; set; }
    }
    public class CmdIDENT : Cmd
    {
        public Expr Expr { get; set; }
        public Tokennode BECOMES { get; set; }
        public Expr Expr2 { get; set; }
    }
    public class CmdLITERAL : Cmd
    {
        public Expr Expr { get; set; }
        public Tokennode BECOMES { get; set; }
        public Expr Expr2 { get; set; }
    }
    public class CmdIF : Cmd
    {
        public Tokennode IF { get; set; }
        public Expr Expr { get; set; }
        public Tokennode THEN { get; set; }
        public CpsCmd CpsCmd { get; set; }
        public Tokennode ELSE { get; set; }
        public CpsCmd CpsCmd2 { get; set; }
        public Tokennode ENDIF { get; set; }
    }
    public class CmdWHILE : Cmd
    {
        public Tokennode WHILE { get; set; }
        public Expr Expr { get; set; }
        public Tokennode DO { get; set; }
        public CpsCmd CpsCmd { get; set; }
        public Tokennode ENDWHILE { get; set; }
    }
    public class CmdCALL : Cmd
    {
        public Tokennode CALL { get; set; }
        public Tokennode IDENT { get; set; }
        public ExprList ExprList { get; set; }
        public OptGlobInits OptGlobInits { get; set; }
    }
    public class CmdDEBUGIN : Cmd
    {
        public Tokennode DEBUGIN { get; set; }
        public Expr Expr { get; set; }
    }
    public class CmdDEBUGOUT : Cmd
    {
        public Tokennode DEBUGOUT { get; set; }
        public Expr Expr { get; set; }
    }
    public interface CpsCmd : Treenode { }
    public class CpsCmdDEBUGOUT : CpsCmd
    {
        public Cmd Cmd { get; set; }
        public RepCpsCmd RepCpsCmd { get; set; }
    }
    public class CpsCmdDEBUGIN : CpsCmd
    {
        public Cmd Cmd { get; set; }
        public RepCpsCmd RepCpsCmd { get; set; }
    }
    public class CpsCmdCALL : CpsCmd
    {
        public Cmd Cmd { get; set; }
        public RepCpsCmd RepCpsCmd { get; set; }
    }
    public class CpsCmdWHILE : CpsCmd
    {
        public Cmd Cmd { get; set; }
        public RepCpsCmd RepCpsCmd { get; set; }
    }
    public class CpsCmdIF : CpsCmd
    {
        public Cmd Cmd { get; set; }
        public RepCpsCmd RepCpsCmd { get; set; }
    }
    public class CpsCmdTYPE : CpsCmd
    {
        public Cmd Cmd { get; set; }
        public RepCpsCmd RepCpsCmd { get; set; }
    }
    public class CpsCmdLPAREN : CpsCmd
    {
        public Cmd Cmd { get; set; }
        public RepCpsCmd RepCpsCmd { get; set; }
    }
    public class CpsCmdADDOPR : CpsCmd
    {
        public Cmd Cmd { get; set; }
        public RepCpsCmd RepCpsCmd { get; set; }
    }
    public class CpsCmdNOT : CpsCmd
    {
        public Cmd Cmd { get; set; }
        public RepCpsCmd RepCpsCmd { get; set; }
    }
    public class CpsCmdIDENT : CpsCmd
    {
        public Cmd Cmd { get; set; }
        public RepCpsCmd RepCpsCmd { get; set; }
    }
    public class CpsCmdLITERAL : CpsCmd
    {
        public Cmd Cmd { get; set; }
        public RepCpsCmd RepCpsCmd { get; set; }
    }
    public class CpsCmdSKIP : CpsCmd
    {
        public Cmd Cmd { get; set; }
        public RepCpsCmd RepCpsCmd { get; set; }
    }
    public interface RepCpsCmd : Treenode { }
    public class RepCpsCmdSEMICOLON : RepCpsCmd
    {
        public Tokennode SEMICOLON { get; set; }
        public Cmd Cmd { get; set; }
        public RepCpsCmd RepCpsCmd { get; set; }
    }
    public class RepCpsCmdENDWHILE : RepCpsCmd
    {
    }
    public class RepCpsCmdENDIF : RepCpsCmd
    {
    }
    public class RepCpsCmdELSE : RepCpsCmd
    {
    }
    public class RepCpsCmdENDPROC : RepCpsCmd
    {
    }
    public class RepCpsCmdENDFUN : RepCpsCmd
    {
    }
    public class RepCpsCmdENDPROGRAM : RepCpsCmd
    {
    }
    public interface OptGlobInits : Treenode { }
    public class OptGlobInitsINIT : OptGlobInits
    {
        public Tokennode INIT { get; set; }
        public Tokennode IDENT { get; set; }
        public RepIdents RepIdents { get; set; }
    }
    public class OptGlobInitsENDWHILE : OptGlobInits
    {
    }
    public class OptGlobInitsENDIF : OptGlobInits
    {
    }
    public class OptGlobInitsELSE : OptGlobInits
    {
    }
    public class OptGlobInitsENDPROC : OptGlobInits
    {
    }
    public class OptGlobInitsENDFUN : OptGlobInits
    {
    }
    public class OptGlobInitsENDPROGRAM : OptGlobInits
    {
    }
    public class OptGlobInitsSEMICOLON : OptGlobInits
    {
    }
    public interface RepIdents : Treenode { }
    public class RepIdentsCOMMA : RepIdents
    {
        public Tokennode COMMA { get; set; }
        public Tokennode IDENT { get; set; }
        public RepIdents RepIdents { get; set; }
    }
    public class RepIdentsENDWHILE : RepIdents
    {
    }
    public class RepIdentsENDIF : RepIdents
    {
    }
    public class RepIdentsELSE : RepIdents
    {
    }
    public class RepIdentsENDPROC : RepIdents
    {
    }
    public class RepIdentsENDFUN : RepIdents
    {
    }
    public class RepIdentsENDPROGRAM : RepIdents
    {
    }
    public class RepIdentsSEMICOLON : RepIdents
    {
    }
    public interface Expr : Treenode { }
    public class ExprTYPE : Expr
    {
        public Term1 Term1 { get; set; }
        public RepTerm1 RepTerm1 { get; set; }
    }
    public class ExprLPAREN : Expr
    {
        public Term1 Term1 { get; set; }
        public RepTerm1 RepTerm1 { get; set; }
    }
    public class ExprADDOPR : Expr
    {
        public Term1 Term1 { get; set; }
        public RepTerm1 RepTerm1 { get; set; }
    }
    public class ExprNOT : Expr
    {
        public Term1 Term1 { get; set; }
        public RepTerm1 RepTerm1 { get; set; }
    }
    public class ExprIDENT : Expr
    {
        public Term1 Term1 { get; set; }
        public RepTerm1 RepTerm1 { get; set; }
    }
    public class ExprLITERAL : Expr
    {
        public Term1 Term1 { get; set; }
        public RepTerm1 RepTerm1 { get; set; }
    }
    public interface RepTerm1 : Treenode { }
    public class RepTerm1BOOLOPR : RepTerm1
    {
        public Tokennode BOOLOPR { get; set; }
        public Term1 Term1 { get; set; }
        public RepTerm1 RepTerm1 { get; set; }
    }
    public class RepTerm1COMMA : RepTerm1
    {
    }
    public class RepTerm1RPAREN : RepTerm1
    {
    }
    public class RepTerm1DO : RepTerm1
    {
    }
    public class RepTerm1THEN : RepTerm1
    {
    }
    public class RepTerm1ENDWHILE : RepTerm1
    {
    }
    public class RepTerm1ENDIF : RepTerm1
    {
    }
    public class RepTerm1ELSE : RepTerm1
    {
    }
    public class RepTerm1ENDPROC : RepTerm1
    {
    }
    public class RepTerm1ENDFUN : RepTerm1
    {
    }
    public class RepTerm1ENDPROGRAM : RepTerm1
    {
    }
    public class RepTerm1SEMICOLON : RepTerm1
    {
    }
    public class RepTerm1BECOMES : RepTerm1
    {
    }
    public interface Term1 : Treenode { }
    public class Term1TYPE : Term1
    {
        public Term2 Term2 { get; set; }
        public RepTerm2 RepTerm2 { get; set; }
    }
    public class Term1LPAREN : Term1
    {
        public Term2 Term2 { get; set; }
        public RepTerm2 RepTerm2 { get; set; }
    }
    public class Term1ADDOPR : Term1
    {
        public Term2 Term2 { get; set; }
        public RepTerm2 RepTerm2 { get; set; }
    }
    public class Term1NOT : Term1
    {
        public Term2 Term2 { get; set; }
        public RepTerm2 RepTerm2 { get; set; }
    }
    public class Term1IDENT : Term1
    {
        public Term2 Term2 { get; set; }
        public RepTerm2 RepTerm2 { get; set; }
    }
    public class Term1LITERAL : Term1
    {
        public Term2 Term2 { get; set; }
        public RepTerm2 RepTerm2 { get; set; }
    }
    public interface RepTerm2 : Treenode { }
    public class RepTerm2RELOPR : RepTerm2
    {
        public Tokennode RELOPR { get; set; }
        public Term2 Term2 { get; set; }
        public RepTerm2 RepTerm2 { get; set; }
    }
    public class RepTerm2COMMA : RepTerm2
    {
    }
    public class RepTerm2RPAREN : RepTerm2
    {
    }
    public class RepTerm2DO : RepTerm2
    {
    }
    public class RepTerm2THEN : RepTerm2
    {
    }
    public class RepTerm2ENDWHILE : RepTerm2
    {
    }
    public class RepTerm2ENDIF : RepTerm2
    {
    }
    public class RepTerm2ELSE : RepTerm2
    {
    }
    public class RepTerm2ENDPROC : RepTerm2
    {
    }
    public class RepTerm2ENDFUN : RepTerm2
    {
    }
    public class RepTerm2ENDPROGRAM : RepTerm2
    {
    }
    public class RepTerm2SEMICOLON : RepTerm2
    {
    }
    public class RepTerm2BECOMES : RepTerm2
    {
    }
    public class RepTerm2BOOLOPR : RepTerm2
    {
    }
    public interface Term2 : Treenode { }
    public class Term2TYPE : Term2
    {
        public Term3 Term3 { get; set; }
        public RepTerm3 RepTerm3 { get; set; }
    }
    public class Term2LPAREN : Term2
    {
        public Term3 Term3 { get; set; }
        public RepTerm3 RepTerm3 { get; set; }
    }
    public class Term2ADDOPR : Term2
    {
        public Term3 Term3 { get; set; }
        public RepTerm3 RepTerm3 { get; set; }
    }
    public class Term2NOT : Term2
    {
        public Term3 Term3 { get; set; }
        public RepTerm3 RepTerm3 { get; set; }
    }
    public class Term2IDENT : Term2
    {
        public Term3 Term3 { get; set; }
        public RepTerm3 RepTerm3 { get; set; }
    }
    public class Term2LITERAL : Term2
    {
        public Term3 Term3 { get; set; }
        public RepTerm3 RepTerm3 { get; set; }
    }
    public interface RepTerm3 : Treenode { }
    public class RepTerm3ADDOPR : RepTerm3
    {
        public Tokennode ADDOPR { get; set; }
        public Term3 Term3 { get; set; }
        public RepTerm3 RepTerm3 { get; set; }
    }
    public class RepTerm3COMMA : RepTerm3
    {
    }
    public class RepTerm3RPAREN : RepTerm3
    {
    }
    public class RepTerm3DO : RepTerm3
    {
    }
    public class RepTerm3THEN : RepTerm3
    {
    }
    public class RepTerm3ENDWHILE : RepTerm3
    {
    }
    public class RepTerm3ENDIF : RepTerm3
    {
    }
    public class RepTerm3ELSE : RepTerm3
    {
    }
    public class RepTerm3ENDPROC : RepTerm3
    {
    }
    public class RepTerm3ENDFUN : RepTerm3
    {
    }
    public class RepTerm3ENDPROGRAM : RepTerm3
    {
    }
    public class RepTerm3SEMICOLON : RepTerm3
    {
    }
    public class RepTerm3BECOMES : RepTerm3
    {
    }
    public class RepTerm3BOOLOPR : RepTerm3
    {
    }
    public class RepTerm3RELOPR : RepTerm3
    {
    }
    public interface Term3 : Treenode { }
    public class Term3TYPE : Term3
    {
        public Factor Factor { get; set; }
        public RepFactor RepFactor { get; set; }
    }
    public class Term3LPAREN : Term3
    {
        public Factor Factor { get; set; }
        public RepFactor RepFactor { get; set; }
    }
    public class Term3ADDOPR : Term3
    {
        public Factor Factor { get; set; }
        public RepFactor RepFactor { get; set; }
    }
    public class Term3NOT : Term3
    {
        public Factor Factor { get; set; }
        public RepFactor RepFactor { get; set; }
    }
    public class Term3IDENT : Term3
    {
        public Factor Factor { get; set; }
        public RepFactor RepFactor { get; set; }
    }
    public class Term3LITERAL : Term3
    {
        public Factor Factor { get; set; }
        public RepFactor RepFactor { get; set; }
    }
    public interface RepFactor : Treenode { }
    public class RepFactorMULTOPR : RepFactor
    {
        public Tokennode MULTOPR { get; set; }
        public Factor Factor { get; set; }
        public RepFactor RepFactor { get; set; }
    }
    public class RepFactorCOMMA : RepFactor
    {
    }
    public class RepFactorRPAREN : RepFactor
    {
    }
    public class RepFactorDO : RepFactor
    {
    }
    public class RepFactorTHEN : RepFactor
    {
    }
    public class RepFactorENDWHILE : RepFactor
    {
    }
    public class RepFactorENDIF : RepFactor
    {
    }
    public class RepFactorELSE : RepFactor
    {
    }
    public class RepFactorENDPROC : RepFactor
    {
    }
    public class RepFactorENDFUN : RepFactor
    {
    }
    public class RepFactorENDPROGRAM : RepFactor
    {
    }
    public class RepFactorSEMICOLON : RepFactor
    {
    }
    public class RepFactorBECOMES : RepFactor
    {
    }
    public class RepFactorBOOLOPR : RepFactor
    {
    }
    public class RepFactorRELOPR : RepFactor
    {
    }
    public class RepFactorADDOPR : RepFactor
    {
    }
    public interface Factor : Treenode { }
    public class FactorLITERAL : Factor
    {
        public Tokennode LITERAL { get; set; }
    }
    public class FactorIDENT : Factor
    {
        public Tokennode IDENT { get; set; }
        public OptInitOrExprList OptInitOrExprList { get; set; }
    }
    public class FactorADDOPR : Factor
    {
        public MonadicOpr MonadicOpr { get; set; }
        public Factor Factor { get; set; }
    }
    public class FactorNOT : Factor
    {
        public MonadicOpr MonadicOpr { get; set; }
        public Factor Factor { get; set; }
    }
    public class FactorLPAREN : Factor
    {
        public Tokennode LPAREN { get; set; }
        public Expr Expr { get; set; }
        public Tokennode RPAREN { get; set; }
    }
    public class FactorTYPE : Factor
    {
        public Tokennode TYPE { get; set; }
        public Tokennode LPAREN { get; set; }
        public Expr Expr { get; set; }
        public Tokennode RPAREN { get; set; }
    }
    public interface OptInitOrExprList : Treenode { }
    public class OptInitOrExprListINIT : OptInitOrExprList
    {
        public Tokennode INIT { get; set; }
    }
    public class OptInitOrExprListLPAREN : OptInitOrExprList
    {
        public ExprList ExprList { get; set; }
    }
    public class OptInitOrExprListCOMMA : OptInitOrExprList
    {
    }
    public class OptInitOrExprListRPAREN : OptInitOrExprList
    {
    }
    public class OptInitOrExprListDO : OptInitOrExprList
    {
    }
    public class OptInitOrExprListTHEN : OptInitOrExprList
    {
    }
    public class OptInitOrExprListENDWHILE : OptInitOrExprList
    {
    }
    public class OptInitOrExprListENDIF : OptInitOrExprList
    {
    }
    public class OptInitOrExprListELSE : OptInitOrExprList
    {
    }
    public class OptInitOrExprListENDPROC : OptInitOrExprList
    {
    }
    public class OptInitOrExprListENDFUN : OptInitOrExprList
    {
    }
    public class OptInitOrExprListENDPROGRAM : OptInitOrExprList
    {
    }
    public class OptInitOrExprListSEMICOLON : OptInitOrExprList
    {
    }
    public class OptInitOrExprListBECOMES : OptInitOrExprList
    {
    }
    public class OptInitOrExprListBOOLOPR : OptInitOrExprList
    {
    }
    public class OptInitOrExprListRELOPR : OptInitOrExprList
    {
    }
    public class OptInitOrExprListADDOPR : OptInitOrExprList
    {
    }
    public class OptInitOrExprListMULTOPR : OptInitOrExprList
    {
    }
    public interface MonadicOpr : Treenode { }
    public class MonadicOprNOT : MonadicOpr
    {
        public Tokennode NOT { get; set; }
    }
    public class MonadicOprADDOPR : MonadicOpr
    {
        public Tokennode ADDOPR { get; set; }
    }
    public interface ExprList : Treenode { }
    public class ExprListLPAREN : ExprList
    {
        public Tokennode LPAREN { get; set; }
        public OptExprList OptExprList { get; set; }
        public Tokennode RPAREN { get; set; }
    }
    public interface OptExprList : Treenode { }
    public class OptExprListTYPE : OptExprList
    {
        public Expr Expr { get; set; }
        public RepExprList RepExprList { get; set; }
    }
    public class OptExprListLPAREN : OptExprList
    {
        public Expr Expr { get; set; }
        public RepExprList RepExprList { get; set; }
    }
    public class OptExprListADDOPR : OptExprList
    {
        public Expr Expr { get; set; }
        public RepExprList RepExprList { get; set; }
    }
    public class OptExprListNOT : OptExprList
    {
        public Expr Expr { get; set; }
        public RepExprList RepExprList { get; set; }
    }
    public class OptExprListIDENT : OptExprList
    {
        public Expr Expr { get; set; }
        public RepExprList RepExprList { get; set; }
    }
    public class OptExprListLITERAL : OptExprList
    {
        public Expr Expr { get; set; }
        public RepExprList RepExprList { get; set; }
    }
    public class OptExprListRPAREN : OptExprList
    {
    }
    public interface RepExprList : Treenode { }
    public class RepExprListCOMMA : RepExprList
    {
        public Tokennode COMMA { get; set; }
        public Expr Expr { get; set; }
        public RepExprList RepExprList { get; set; }
    }
    public class RepExprListRPAREN : RepExprList
    {
    }
}
