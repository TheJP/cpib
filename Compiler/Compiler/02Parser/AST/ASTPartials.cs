using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;

using Compiler._02Parser.AST;

namespace Compiler
{
    public partial class Tokennode : Treenode
    {
        public virtual IASTNode ToAbstractSyntax()
        {
            throw new NotImplementedException();
        }
    }
    public partial class ProgramPROGRAM : Program
    {
        public virtual IASTNode ToAbstractSyntax()
        {
            var program = new ASTProgram();

            program.Ident = ((IdentToken)this.IDENT.Token).Value;

            program.Params = this.ProgParamList.ToAbstractSyntax();

            program.CpsDecls = this.OptCpsDecl.ToAbstractSyntax();

            program.Commands = this.CpsCmd.ToAbstractSyntax();

            return program;
        }
    }
    public partial class DeclCHANGEMODE : Decl
    {
        public virtual IASTNode ToAbstractSyntax()
        {
            throw new NotImplementedException();
        }
    }
    public partial class DeclIDENT : Decl
    {
        public virtual IASTNode ToAbstractSyntax()
        {
            throw new NotImplementedException();
        }
    }
    public partial class DeclFUN : Decl
    {
        public virtual IASTNode ToAbstractSyntax()
        {
            throw new NotImplementedException();
        }
    }
    public partial class DeclPROC : Decl
    {
        public virtual IASTNode ToAbstractSyntax()
        {
            return this.ProcDecl.ToAbstractSyntax();
        }
    }
    public partial class StoDeclIDENT : StoDecl
    {
        public virtual IASTNode ToAbstractSyntax()
        {
            throw new NotImplementedException();
        }
    }
    public partial class StoDeclCHANGEMODE : StoDecl
    {
        public virtual IASTNode ToAbstractSyntax()
        {
            throw new NotImplementedException();
        }
    }
    public partial class FunDeclFUN : FunDecl
    {
        public virtual IASTNode ToAbstractSyntax()
        {
            throw new NotImplementedException();
        }
    }
    public partial class ProcDeclPROC : ProcDecl
    {
        public virtual IASTNode ToAbstractSyntax()
        {
            var decl = new ASTProcDeclPROC();
            decl.Ident = ((IdentToken)this.IDENT.Token).Value;
            decl.OptCpsStoDecl = this.OptCpsStoDecl.ToAbstractSyntax();
            decl.OptGlobImps = this.OptGlobImps.ToAbstractSyntax();
            decl.ParamList = this.ParamList.ToAbstractSyntax();
            decl.CpsCmd = this.CpsCmd.ToAbstractSyntax();
            return decl;
        }
    }

    public partial class OptGlobImpsGLOBAL : OptGlobImps
    {
        public virtual IASTNode ToAbstractSyntax()
        {
            throw new NotImplementedException();
        }
    }
    public partial class OptGlobImpsDO : OptGlobImps
    {
        public virtual IASTNode ToAbstractSyntax()
        {
            throw new NotImplementedException();
        }
    }
    public partial class OptGlobImpsLOCAL : OptGlobImps
    {
        public virtual IASTNode ToAbstractSyntax()
        {
            throw new NotImplementedException();
        }
    }
    public partial class GlobImpsFLOWMODE : GlobImps
    {
        public virtual IASTNode ToAbstractSyntax()
        {
            throw new NotImplementedException();
        }
    }
    public partial class GlobImpsIDENT : GlobImps
    {
        public virtual IASTNode ToAbstractSyntax()
        {
            throw new NotImplementedException();
        }
    }
    public partial class GlobImpsCHANGEMODE : GlobImps
    {
        public virtual IASTNode ToAbstractSyntax()
        {
            throw new NotImplementedException();
        }
    }
    public partial class RepGlobImpsCOMMA : RepGlobImps
    {
        public virtual IASTNode ToAbstractSyntax()
        {
            throw new NotImplementedException();
        }
    }
    public partial class RepGlobImpsDO : RepGlobImps
    {
        public virtual IASTNode ToAbstractSyntax()
        {
            throw new NotImplementedException();
        }
    }
    public partial class RepGlobImpsLOCAL : RepGlobImps
    {
        public virtual IASTNode ToAbstractSyntax()
        {
            throw new NotImplementedException();
        }
    }
    public partial class OptChangemodeCHANGEMODE : OptChangemode
    {
        public virtual IASTNode ToAbstractSyntax()
        {
            throw new NotImplementedException();
        }
    }
    public partial class OptChangemodeIDENT : OptChangemode
    {
        public virtual IASTNode ToAbstractSyntax()
        {
            throw new NotImplementedException();
        }
    }
    public partial class OptMechmodeMECHMODE : OptMechmode
    {
        public virtual IASTNode ToAbstractSyntax()
        {
            throw new NotImplementedException();
        }
    }
    public partial class OptMechmodeIDENT : OptMechmode
    {
        public virtual IASTNode ToAbstractSyntax()
        {
            throw new NotImplementedException();
        }
    }
    public partial class OptMechmodeCHANGEMODE : OptMechmode
    {
        public virtual IASTNode ToAbstractSyntax()
        {
            throw new NotImplementedException();
        }
    }
    public partial class GlobImpIDENT : GlobImp
    {
        public virtual IASTNode ToAbstractSyntax()
        {
            throw new NotImplementedException();
        }
    }
    public partial class GlobImpCHANGEMODE : GlobImp
    {
        public virtual IASTNode ToAbstractSyntax()
        {
            throw new NotImplementedException();
        }
    }
    public partial class GlobImpFLOWMODE : GlobImp
    {
        public virtual IASTNode ToAbstractSyntax()
        {
            throw new NotImplementedException();
        }
    }
    public partial class OptCpsDeclGLOBAL : OptCpsDecl
    {
        public virtual IASTNode ToAbstractSyntax()
        {
            return this.CpsDecl.ToAbstractSyntax();
        }
    }
    public partial class OptCpsDeclDO : OptCpsDecl
    {
        public virtual IASTNode ToAbstractSyntax()
        {
            return new ASTEmpty();
        }
    }
    public partial class CpsDeclPROC : CpsDecl
    {
        public virtual IASTNode ToAbstractSyntax()
        {
            var decl = (ASTCpsDecl)this.Decl.ToAbstractSyntax();

            decl.NextDecl = this.RepCpsDecl.ToAbstractSyntax();

            return decl;
        }
    }

    public partial class CpsDeclFUN : CpsDecl
    {
        public virtual IASTNode ToAbstractSyntax()
        {
            throw new NotImplementedException();
        }
    }
    public partial class CpsDeclCHANGEMODE : CpsDecl
    {
        public virtual IASTNode ToAbstractSyntax()
        {
            throw new NotImplementedException();
        }
    }
    public partial class CpsDeclIDENT : CpsDecl
    {
        public virtual IASTNode ToAbstractSyntax()
        {
            throw new NotImplementedException();
        }
    }
    public partial class RepCpsDeclSEMICOLON : RepCpsDecl
    {
        public virtual IASTNode ToAbstractSyntax()
        {
            throw new NotImplementedException();
        }
    }
    public partial class RepCpsDeclDO : RepCpsDecl
    {
        public virtual IASTNode ToAbstractSyntax()
        {
            throw new NotImplementedException();
        }
    }
    public partial class OptCpsStoDeclLOCAL : OptCpsStoDecl
    {
        public virtual IASTNode ToAbstractSyntax()
        {
            throw new NotImplementedException();
        }
    }
    public partial class OptCpsStoDeclDO : OptCpsStoDecl
    {
        public virtual IASTNode ToAbstractSyntax()
        {
            throw new NotImplementedException();
        }
    }
    public partial class CpsStoDeclCHANGEMODE : CpsStoDecl
    {
        public virtual IASTNode ToAbstractSyntax()
        {
            throw new NotImplementedException();
        }
    }
    public partial class CpsStoDeclIDENT : CpsStoDecl
    {
        public virtual IASTNode ToAbstractSyntax()
        {
            throw new NotImplementedException();
        }
    }
    public partial class RepCpsStoDeclSEMICOLON : RepCpsStoDecl
    {
        public virtual IASTNode ToAbstractSyntax()
        {
            throw new NotImplementedException();
        }
    }
    public partial class RepCpsStoDeclDO : RepCpsStoDecl
    {
        public virtual IASTNode ToAbstractSyntax()
        {
            throw new NotImplementedException();
        }
    }
    public partial class ProgParamListLPAREN : ProgParamList
    {
        public virtual IASTNode ToAbstractSyntax()
        {
            if (this.OptProgParamList == null)
            {
                return new ASTEmpty();
            }

            return this.OptProgParamList.ToAbstractSyntax();
        }
    }
    public partial class OptProgParamListFLOWMODE : OptProgParamList
    {
        public virtual IASTNode ToAbstractSyntax()
        {
            var result = (ASTProgParam)this.ProgParam.ToAbstractSyntax();
            result.NextParam = this.RepProgParamList.ToAbstractSyntax();

            return result;
        }
    }

    public partial class OptProgParamListIDENT : OptProgParamList
    {
        public virtual IASTNode ToAbstractSyntax()
        {
            throw new NotImplementedException();
        }
    }
    public partial class OptProgParamListCHANGEMODE : OptProgParamList
    {
        public virtual IASTNode ToAbstractSyntax()
        {
            throw new NotImplementedException();
        }
    }
    public partial class OptProgParamListRPAREN : OptProgParamList
    {
        public virtual IASTNode ToAbstractSyntax()
        {
            throw new NotImplementedException();
        }
    }
    public partial class RepProgParamListCOMMA : RepProgParamList
    {
        public virtual IASTNode ToAbstractSyntax()
        {
            var result = (ASTProgParam)this.ProgParam.ToAbstractSyntax();
            result.NextParam = this.RepProgParamList.ToAbstractSyntax();

            return result;
        }
    }
    public partial class RepProgParamListRPAREN : RepProgParamList
    {
        public virtual IASTNode ToAbstractSyntax()
        {
            return new ASTEmpty();
        }
    }
    public partial class ProgParamIDENT : ProgParam
    {
        public virtual IASTNode ToAbstractSyntax()
        {
            throw new NotImplementedException();
        }
    }
    public partial class ProgParamCHANGEMODE : ProgParam
    {
        public virtual IASTNode ToAbstractSyntax()
        {
            throw new NotImplementedException();
        }
    }
    public partial class ProgParamFLOWMODE : ProgParam
    {
        public virtual IASTNode ToAbstractSyntax()
        {
            var param = new ASTProgParam();
            param.Type = ((TypeToken)((TypedIdentIDENT)this.TypedIdent).TYPE.Token).Value;
            param.Ident = ((IdentToken)((TypedIdentIDENT)this.TypedIdent).IDENT.Token).Value;
            param.FlowMode = ((FlowModeToken)this.FLOWMODE.Token).Value;
            if (this.OptChangemode is OptChangemodeCHANGEMODE)
            {
                param.OptChangemode =
                    ((ChangeModeToken)((OptChangemodeCHANGEMODE)this.OptChangemode).CHANGEMODE.Token).Value;
            }

            return param;
        }
    }
    public partial class ParamListLPAREN : ParamList
    {
        public virtual IASTNode ToAbstractSyntax()
        {
            throw new NotImplementedException();
        }
    }
    public partial class OptParamListFLOWMODE : OptParamList
    {
        public virtual IASTNode ToAbstractSyntax()
        {
            throw new NotImplementedException();
        }
    }
    public partial class OptParamListIDENT : OptParamList
    {
        public virtual IASTNode ToAbstractSyntax()
        {
            throw new NotImplementedException();
        }
    }
    public partial class OptParamListCHANGEMODE : OptParamList
    {
        public virtual IASTNode ToAbstractSyntax()
        {
            throw new NotImplementedException();
        }
    }
    public partial class OptParamListMECHMODE : OptParamList
    {
        public virtual IASTNode ToAbstractSyntax()
        {
            throw new NotImplementedException();
        }
    }
    public partial class OptParamListRPAREN : OptParamList
    {
        public virtual IASTNode ToAbstractSyntax()
        {
            throw new NotImplementedException();
        }
    }
    public partial class RepParamListCOMMA : RepParamList
    {
        public virtual IASTNode ToAbstractSyntax()
        {
            throw new NotImplementedException();
        }
    }
    public partial class RepParamListRPAREN : RepParamList
    {
        public virtual IASTNode ToAbstractSyntax()
        {
            throw new NotImplementedException();
        }
    }
    public partial class ParamIDENT : Param
    {
        public virtual IASTNode ToAbstractSyntax()
        {
            throw new NotImplementedException();
        }
    }
    public partial class ParamCHANGEMODE : Param
    {
        public virtual IASTNode ToAbstractSyntax()
        {
            throw new NotImplementedException();
        }
    }
    public partial class ParamMECHMODE : Param
    {
        public virtual IASTNode ToAbstractSyntax()
        {
            throw new NotImplementedException();
        }
    }
    public partial class ParamFLOWMODE : Param
    {
        public virtual IASTNode ToAbstractSyntax()
        {
            throw new NotImplementedException();
        }
    }
    public partial class TypedIdentIDENT : TypedIdent
    {
        public virtual IASTNode ToAbstractSyntax()
        {
            throw new NotImplementedException();
        }
    }
    public partial class CmdSKIP : Cmd
    {
        public virtual IASTNode ToAbstractSyntax()
        {
            throw new NotImplementedException();
        }
    }
    public partial class CmdTYPE : Cmd
    {
        public virtual IASTNode ToAbstractSyntax()
        {
            throw new NotImplementedException();
        }
    }
    public partial class CmdLPAREN : Cmd
    {
        public virtual IASTNode ToAbstractSyntax()
        {
            throw new NotImplementedException();
        }
    }
    public partial class CmdADDOPR : Cmd
    {
        public virtual IASTNode ToAbstractSyntax()
        {
            throw new NotImplementedException();
        }
    }
    public partial class CmdNOT : Cmd
    {
        public virtual IASTNode ToAbstractSyntax()
        {
            throw new NotImplementedException();
        }
    }
    public partial class CmdIDENT : Cmd
    {
        public virtual IASTNode ToAbstractSyntax()
        {
            throw new NotImplementedException();
        }
    }
    public partial class CmdLITERAL : Cmd
    {
        public virtual IASTNode ToAbstractSyntax()
        {
            throw new NotImplementedException();
        }
    }
    public partial class CmdIF : Cmd
    {
        public virtual IASTNode ToAbstractSyntax()
        {
            throw new NotImplementedException();
        }
    }
    public partial class CmdWHILE : Cmd
    {
        public virtual IASTNode ToAbstractSyntax()
        {
            throw new NotImplementedException();
        }
    }
    public partial class CmdCALL : Cmd
    {
        public virtual IASTNode ToAbstractSyntax()
        {
            throw new NotImplementedException();
        }
    }
    public partial class CmdDEBUGIN : Cmd
    {
        public virtual IASTNode ToAbstractSyntax()
        {
            throw new NotImplementedException();
        }
    }
    public partial class CmdDEBUGOUT : Cmd
    {
        public virtual IASTNode ToAbstractSyntax()
        {
            throw new NotImplementedException();
        }
    }
    public partial class CpsCmdDEBUGOUT : CpsCmd
    {
        public virtual IASTNode ToAbstractSyntax()
        {
            throw new NotImplementedException();
        }
    }
    public partial class CpsCmdDEBUGIN : CpsCmd
    {
        public virtual IASTNode ToAbstractSyntax()
        {
            throw new NotImplementedException();
        }
    }
    public partial class CpsCmdCALL : CpsCmd
    {
        public virtual IASTNode ToAbstractSyntax()
        {
            throw new NotImplementedException();
        }
    }
    public partial class CpsCmdWHILE : CpsCmd
    {
        public virtual IASTNode ToAbstractSyntax()
        {
            throw new NotImplementedException();
        }
    }
    public partial class CpsCmdIF : CpsCmd
    {
        public virtual IASTNode ToAbstractSyntax()
        {
            throw new NotImplementedException();
        }
    }
    public partial class CpsCmdTYPE : CpsCmd
    {
        public virtual IASTNode ToAbstractSyntax()
        {
            throw new NotImplementedException();
        }
    }
    public partial class CpsCmdLPAREN : CpsCmd
    {
        public virtual IASTNode ToAbstractSyntax()
        {
            throw new NotImplementedException();
        }
    }
    public partial class CpsCmdADDOPR : CpsCmd
    {
        public virtual IASTNode ToAbstractSyntax()
        {
            throw new NotImplementedException();
        }
    }
    public partial class CpsCmdNOT : CpsCmd
    {
        public virtual IASTNode ToAbstractSyntax()
        {
            throw new NotImplementedException();
        }
    }
    public partial class CpsCmdIDENT : CpsCmd
    {
        public virtual IASTNode ToAbstractSyntax()
        {
            var cmd = (ASTCpsCmd)this.Cmd.ToAbstractSyntax();
            cmd.NextCmd = this.RepCpsCmd.ToAbstractSyntax();
            return cmd;
        }
    }

    public partial class CpsCmdLITERAL : CpsCmd
    {
        public virtual IASTNode ToAbstractSyntax()
        {
            throw new NotImplementedException();
        }
    }
    public partial class CpsCmdSKIP : CpsCmd
    {
        public virtual IASTNode ToAbstractSyntax()
        {
            throw new NotImplementedException();
        }
    }
    public partial class RepCpsCmdSEMICOLON : RepCpsCmd
    {
        public virtual IASTNode ToAbstractSyntax()
        {
            throw new NotImplementedException();
        }
    }
    public partial class RepCpsCmdENDWHILE : RepCpsCmd
    {
        public virtual IASTNode ToAbstractSyntax()
        {
            throw new NotImplementedException();
        }
    }
    public partial class RepCpsCmdENDIF : RepCpsCmd
    {
        public virtual IASTNode ToAbstractSyntax()
        {
            throw new NotImplementedException();
        }
    }
    public partial class RepCpsCmdELSE : RepCpsCmd
    {
        public virtual IASTNode ToAbstractSyntax()
        {
            throw new NotImplementedException();
        }
    }
    public partial class RepCpsCmdENDPROC : RepCpsCmd
    {
        public virtual IASTNode ToAbstractSyntax()
        {
            throw new NotImplementedException();
        }
    }
    public partial class RepCpsCmdENDFUN : RepCpsCmd
    {
        public virtual IASTNode ToAbstractSyntax()
        {
            throw new NotImplementedException();
        }
    }
    public partial class RepCpsCmdENDPROGRAM : RepCpsCmd
    {
        public virtual IASTNode ToAbstractSyntax()
        {
            throw new NotImplementedException();
        }
    }
    public partial class OptGlobInitsINIT : OptGlobInits
    {
        public virtual IASTNode ToAbstractSyntax()
        {
            throw new NotImplementedException();
        }
    }
    public partial class OptGlobInitsENDWHILE : OptGlobInits
    {
        public virtual IASTNode ToAbstractSyntax()
        {
            throw new NotImplementedException();
        }
    }
    public partial class OptGlobInitsENDIF : OptGlobInits
    {
        public virtual IASTNode ToAbstractSyntax()
        {
            throw new NotImplementedException();
        }
    }
    public partial class OptGlobInitsELSE : OptGlobInits
    {
        public virtual IASTNode ToAbstractSyntax()
        {
            throw new NotImplementedException();
        }
    }
    public partial class OptGlobInitsENDPROC : OptGlobInits
    {
        public virtual IASTNode ToAbstractSyntax()
        {
            throw new NotImplementedException();
        }
    }
    public partial class OptGlobInitsENDFUN : OptGlobInits
    {
        public virtual IASTNode ToAbstractSyntax()
        {
            throw new NotImplementedException();
        }
    }
    public partial class OptGlobInitsENDPROGRAM : OptGlobInits
    {
        public virtual IASTNode ToAbstractSyntax()
        {
            throw new NotImplementedException();
        }
    }
    public partial class OptGlobInitsSEMICOLON : OptGlobInits
    {
        public virtual IASTNode ToAbstractSyntax()
        {
            throw new NotImplementedException();
        }
    }
    public partial class RepIdentsCOMMA : RepIdents
    {
        public virtual IASTNode ToAbstractSyntax()
        {
            throw new NotImplementedException();
        }
    }
    public partial class RepIdentsENDWHILE : RepIdents
    {
        public virtual IASTNode ToAbstractSyntax()
        {
            throw new NotImplementedException();
        }
    }
    public partial class RepIdentsENDIF : RepIdents
    {
        public virtual IASTNode ToAbstractSyntax()
        {
            throw new NotImplementedException();
        }
    }
    public partial class RepIdentsELSE : RepIdents
    {
        public virtual IASTNode ToAbstractSyntax()
        {
            throw new NotImplementedException();
        }
    }
    public partial class RepIdentsENDPROC : RepIdents
    {
        public virtual IASTNode ToAbstractSyntax()
        {
            throw new NotImplementedException();
        }
    }
    public partial class RepIdentsENDFUN : RepIdents
    {
        public virtual IASTNode ToAbstractSyntax()
        {
            throw new NotImplementedException();
        }
    }
    public partial class RepIdentsENDPROGRAM : RepIdents
    {
        public virtual IASTNode ToAbstractSyntax()
        {
            throw new NotImplementedException();
        }
    }
    public partial class RepIdentsSEMICOLON : RepIdents
    {
        public virtual IASTNode ToAbstractSyntax()
        {
            throw new NotImplementedException();
        }
    }
    public partial class ExprTYPE : Expr
    {
        public virtual IASTNode ToAbstractSyntax()
        {
            throw new NotImplementedException();
        }
    }
    public partial class ExprLPAREN : Expr
    {
        public virtual IASTNode ToAbstractSyntax()
        {
            throw new NotImplementedException();
        }
    }
    public partial class ExprADDOPR : Expr
    {
        public virtual IASTNode ToAbstractSyntax()
        {
            throw new NotImplementedException();
        }
    }
    public partial class ExprNOT : Expr
    {
        public virtual IASTNode ToAbstractSyntax()
        {
            throw new NotImplementedException();
        }
    }
    public partial class ExprIDENT : Expr
    {
        public virtual IASTNode ToAbstractSyntax()
        {
            throw new NotImplementedException();
        }
    }
    public partial class ExprLITERAL : Expr
    {
        public virtual IASTNode ToAbstractSyntax()
        {
            throw new NotImplementedException();
        }
    }
    public partial class RepTerm1BOOLOPR : RepTerm1
    {
        public virtual IASTNode ToAbstractSyntax()
        {
            throw new NotImplementedException();
        }
    }
    public partial class RepTerm1COMMA : RepTerm1
    {
        public virtual IASTNode ToAbstractSyntax()
        {
            throw new NotImplementedException();
        }
    }
    public partial class RepTerm1RPAREN : RepTerm1
    {
        public virtual IASTNode ToAbstractSyntax()
        {
            throw new NotImplementedException();
        }
    }
    public partial class RepTerm1DO : RepTerm1
    {
        public virtual IASTNode ToAbstractSyntax()
        {
            throw new NotImplementedException();
        }
    }
    public partial class RepTerm1THEN : RepTerm1
    {
        public virtual IASTNode ToAbstractSyntax()
        {
            throw new NotImplementedException();
        }
    }
    public partial class RepTerm1ENDWHILE : RepTerm1
    {
        public virtual IASTNode ToAbstractSyntax()
        {
            throw new NotImplementedException();
        }
    }
    public partial class RepTerm1ENDIF : RepTerm1
    {
        public virtual IASTNode ToAbstractSyntax()
        {
            throw new NotImplementedException();
        }
    }
    public partial class RepTerm1ELSE : RepTerm1
    {
        public virtual IASTNode ToAbstractSyntax()
        {
            throw new NotImplementedException();
        }
    }
    public partial class RepTerm1ENDPROC : RepTerm1
    {
        public virtual IASTNode ToAbstractSyntax()
        {
            throw new NotImplementedException();
        }
    }
    public partial class RepTerm1ENDFUN : RepTerm1
    {
        public virtual IASTNode ToAbstractSyntax()
        {
            throw new NotImplementedException();
        }
    }
    public partial class RepTerm1ENDPROGRAM : RepTerm1
    {
        public virtual IASTNode ToAbstractSyntax()
        {
            throw new NotImplementedException();
        }
    }
    public partial class RepTerm1SEMICOLON : RepTerm1
    {
        public virtual IASTNode ToAbstractSyntax()
        {
            throw new NotImplementedException();
        }
    }
    public partial class RepTerm1BECOMES : RepTerm1
    {
        public virtual IASTNode ToAbstractSyntax()
        {
            throw new NotImplementedException();
        }
    }
    public partial class Term1TYPE : Term1
    {
        public virtual IASTNode ToAbstractSyntax()
        {
            throw new NotImplementedException();
        }
    }
    public partial class Term1LPAREN : Term1
    {
        public virtual IASTNode ToAbstractSyntax()
        {
            throw new NotImplementedException();
        }
    }
    public partial class Term1ADDOPR : Term1
    {
        public virtual IASTNode ToAbstractSyntax()
        {
            throw new NotImplementedException();
        }
    }
    public partial class Term1NOT : Term1
    {
        public virtual IASTNode ToAbstractSyntax()
        {
            throw new NotImplementedException();
        }
    }
    public partial class Term1IDENT : Term1
    {
        public virtual IASTNode ToAbstractSyntax()
        {
            throw new NotImplementedException();
        }
    }
    public partial class Term1LITERAL : Term1
    {
        public virtual IASTNode ToAbstractSyntax()
        {
            throw new NotImplementedException();
        }
    }
    public partial class RepTerm2RELOPR : RepTerm2
    {
        public virtual IASTNode ToAbstractSyntax()
        {
            throw new NotImplementedException();
        }
    }
    public partial class RepTerm2COMMA : RepTerm2
    {
        public virtual IASTNode ToAbstractSyntax()
        {
            throw new NotImplementedException();
        }
    }
    public partial class RepTerm2RPAREN : RepTerm2
    {
        public virtual IASTNode ToAbstractSyntax()
        {
            throw new NotImplementedException();
        }
    }
    public partial class RepTerm2DO : RepTerm2
    {
        public virtual IASTNode ToAbstractSyntax()
        {
            throw new NotImplementedException();
        }
    }
    public partial class RepTerm2THEN : RepTerm2
    {
        public virtual IASTNode ToAbstractSyntax()
        {
            throw new NotImplementedException();
        }
    }
    public partial class RepTerm2ENDWHILE : RepTerm2
    {
        public virtual IASTNode ToAbstractSyntax()
        {
            throw new NotImplementedException();
        }
    }
    public partial class RepTerm2ENDIF : RepTerm2
    {
        public virtual IASTNode ToAbstractSyntax()
        {
            throw new NotImplementedException();
        }
    }
    public partial class RepTerm2ELSE : RepTerm2
    {
        public virtual IASTNode ToAbstractSyntax()
        {
            throw new NotImplementedException();
        }
    }
    public partial class RepTerm2ENDPROC : RepTerm2
    {
        public virtual IASTNode ToAbstractSyntax()
        {
            throw new NotImplementedException();
        }
    }
    public partial class RepTerm2ENDFUN : RepTerm2
    {
        public virtual IASTNode ToAbstractSyntax()
        {
            throw new NotImplementedException();
        }
    }
    public partial class RepTerm2ENDPROGRAM : RepTerm2
    {
        public virtual IASTNode ToAbstractSyntax()
        {
            throw new NotImplementedException();
        }
    }
    public partial class RepTerm2SEMICOLON : RepTerm2
    {
        public virtual IASTNode ToAbstractSyntax()
        {
            throw new NotImplementedException();
        }
    }
    public partial class RepTerm2BECOMES : RepTerm2
    {
        public virtual IASTNode ToAbstractSyntax()
        {
            throw new NotImplementedException();
        }
    }
    public partial class RepTerm2BOOLOPR : RepTerm2
    {
        public virtual IASTNode ToAbstractSyntax()
        {
            throw new NotImplementedException();
        }
    }
    public partial class Term2TYPE : Term2
    {
        public virtual IASTNode ToAbstractSyntax()
        {
            throw new NotImplementedException();
        }
    }
    public partial class Term2LPAREN : Term2
    {
        public virtual IASTNode ToAbstractSyntax()
        {
            throw new NotImplementedException();
        }
    }
    public partial class Term2ADDOPR : Term2
    {
        public virtual IASTNode ToAbstractSyntax()
        {
            throw new NotImplementedException();
        }
    }
    public partial class Term2NOT : Term2
    {
        public virtual IASTNode ToAbstractSyntax()
        {
            throw new NotImplementedException();
        }
    }
    public partial class Term2IDENT : Term2
    {
        public virtual IASTNode ToAbstractSyntax()
        {
            throw new NotImplementedException();
        }
    }
    public partial class Term2LITERAL : Term2
    {
        public virtual IASTNode ToAbstractSyntax()
        {
            throw new NotImplementedException();
        }
    }
    public partial class RepTerm3ADDOPR : RepTerm3
    {
        public virtual IASTNode ToAbstractSyntax()
        {
            throw new NotImplementedException();
        }
    }
    public partial class RepTerm3COMMA : RepTerm3
    {
        public virtual IASTNode ToAbstractSyntax()
        {
            throw new NotImplementedException();
        }
    }
    public partial class RepTerm3RPAREN : RepTerm3
    {
        public virtual IASTNode ToAbstractSyntax()
        {
            throw new NotImplementedException();
        }
    }
    public partial class RepTerm3DO : RepTerm3
    {
        public virtual IASTNode ToAbstractSyntax()
        {
            throw new NotImplementedException();
        }
    }
    public partial class RepTerm3THEN : RepTerm3
    {
        public virtual IASTNode ToAbstractSyntax()
        {
            throw new NotImplementedException();
        }
    }
    public partial class RepTerm3ENDWHILE : RepTerm3
    {
        public virtual IASTNode ToAbstractSyntax()
        {
            throw new NotImplementedException();
        }
    }
    public partial class RepTerm3ENDIF : RepTerm3
    {
        public virtual IASTNode ToAbstractSyntax()
        {
            throw new NotImplementedException();
        }
    }
    public partial class RepTerm3ELSE : RepTerm3
    {
        public virtual IASTNode ToAbstractSyntax()
        {
            throw new NotImplementedException();
        }
    }
    public partial class RepTerm3ENDPROC : RepTerm3
    {
        public virtual IASTNode ToAbstractSyntax()
        {
            throw new NotImplementedException();
        }
    }
    public partial class RepTerm3ENDFUN : RepTerm3
    {
        public virtual IASTNode ToAbstractSyntax()
        {
            throw new NotImplementedException();
        }
    }
    public partial class RepTerm3ENDPROGRAM : RepTerm3
    {
        public virtual IASTNode ToAbstractSyntax()
        {
            throw new NotImplementedException();
        }
    }
    public partial class RepTerm3SEMICOLON : RepTerm3
    {
        public virtual IASTNode ToAbstractSyntax()
        {
            throw new NotImplementedException();
        }
    }
    public partial class RepTerm3BECOMES : RepTerm3
    {
        public virtual IASTNode ToAbstractSyntax()
        {
            throw new NotImplementedException();
        }
    }
    public partial class RepTerm3BOOLOPR : RepTerm3
    {
        public virtual IASTNode ToAbstractSyntax()
        {
            throw new NotImplementedException();
        }
    }
    public partial class RepTerm3RELOPR : RepTerm3
    {
        public virtual IASTNode ToAbstractSyntax()
        {
            throw new NotImplementedException();
        }
    }
    public partial class Term3TYPE : Term3
    {
        public virtual IASTNode ToAbstractSyntax()
        {
            throw new NotImplementedException();
        }
    }
    public partial class Term3LPAREN : Term3
    {
        public virtual IASTNode ToAbstractSyntax()
        {
            throw new NotImplementedException();
        }
    }
    public partial class Term3ADDOPR : Term3
    {
        public virtual IASTNode ToAbstractSyntax()
        {
            throw new NotImplementedException();
        }
    }
    public partial class Term3NOT : Term3
    {
        public virtual IASTNode ToAbstractSyntax()
        {
            throw new NotImplementedException();
        }
    }
    public partial class Term3IDENT : Term3
    {
        public virtual IASTNode ToAbstractSyntax()
        {
            throw new NotImplementedException();
        }
    }
    public partial class Term3LITERAL : Term3
    {
        public virtual IASTNode ToAbstractSyntax()
        {
            throw new NotImplementedException();
        }
    }
    public partial class RepFactorMULTOPR : RepFactor
    {
        public virtual IASTNode ToAbstractSyntax()
        {
            throw new NotImplementedException();
        }
    }
    public partial class RepFactorCOMMA : RepFactor
    {
        public virtual IASTNode ToAbstractSyntax()
        {
            throw new NotImplementedException();
        }
    }
    public partial class RepFactorRPAREN : RepFactor
    {
        public virtual IASTNode ToAbstractSyntax()
        {
            throw new NotImplementedException();
        }
    }
    public partial class RepFactorDO : RepFactor
    {
        public virtual IASTNode ToAbstractSyntax()
        {
            throw new NotImplementedException();
        }
    }
    public partial class RepFactorTHEN : RepFactor
    {
        public virtual IASTNode ToAbstractSyntax()
        {
            throw new NotImplementedException();
        }
    }
    public partial class RepFactorENDWHILE : RepFactor
    {
        public virtual IASTNode ToAbstractSyntax()
        {
            throw new NotImplementedException();
        }
    }
    public partial class RepFactorENDIF : RepFactor
    {
        public virtual IASTNode ToAbstractSyntax()
        {
            throw new NotImplementedException();
        }
    }
    public partial class RepFactorELSE : RepFactor
    {
        public virtual IASTNode ToAbstractSyntax()
        {
            throw new NotImplementedException();
        }
    }
    public partial class RepFactorENDPROC : RepFactor
    {
        public virtual IASTNode ToAbstractSyntax()
        {
            throw new NotImplementedException();
        }
    }
    public partial class RepFactorENDFUN : RepFactor
    {
        public virtual IASTNode ToAbstractSyntax()
        {
            throw new NotImplementedException();
        }
    }
    public partial class RepFactorENDPROGRAM : RepFactor
    {
        public virtual IASTNode ToAbstractSyntax()
        {
            throw new NotImplementedException();
        }
    }
    public partial class RepFactorSEMICOLON : RepFactor
    {
        public virtual IASTNode ToAbstractSyntax()
        {
            throw new NotImplementedException();
        }
    }
    public partial class RepFactorBECOMES : RepFactor
    {
        public virtual IASTNode ToAbstractSyntax()
        {
            throw new NotImplementedException();
        }
    }
    public partial class RepFactorBOOLOPR : RepFactor
    {
        public virtual IASTNode ToAbstractSyntax()
        {
            throw new NotImplementedException();
        }
    }
    public partial class RepFactorRELOPR : RepFactor
    {
        public virtual IASTNode ToAbstractSyntax()
        {
            throw new NotImplementedException();
        }
    }
    public partial class RepFactorADDOPR : RepFactor
    {
        public virtual IASTNode ToAbstractSyntax()
        {
            throw new NotImplementedException();
        }
    }
    public partial class FactorLITERAL : Factor
    {
        public virtual IASTNode ToAbstractSyntax()
        {
            throw new NotImplementedException();
        }
    }
    public partial class FactorIDENT : Factor
    {
        public virtual IASTNode ToAbstractSyntax()
        {
            throw new NotImplementedException();
        }
    }
    public partial class FactorADDOPR : Factor
    {
        public virtual IASTNode ToAbstractSyntax()
        {
            throw new NotImplementedException();
        }
    }
    public partial class FactorNOT : Factor
    {
        public virtual IASTNode ToAbstractSyntax()
        {
            throw new NotImplementedException();
        }
    }
    public partial class FactorLPAREN : Factor
    {
        public virtual IASTNode ToAbstractSyntax()
        {
            throw new NotImplementedException();
        }
    }
    public partial class FactorTYPE : Factor
    {
        public virtual IASTNode ToAbstractSyntax()
        {
            throw new NotImplementedException();
        }
    }
    public partial class OptInitOrExprListINIT : OptInitOrExprList
    {
        public virtual IASTNode ToAbstractSyntax()
        {
            throw new NotImplementedException();
        }
    }
    public partial class OptInitOrExprListLPAREN : OptInitOrExprList
    {
        public virtual IASTNode ToAbstractSyntax()
        {
            throw new NotImplementedException();
        }
    }
    public partial class OptInitOrExprListCOMMA : OptInitOrExprList
    {
        public virtual IASTNode ToAbstractSyntax()
        {
            throw new NotImplementedException();
        }
    }
    public partial class OptInitOrExprListRPAREN : OptInitOrExprList
    {
        public virtual IASTNode ToAbstractSyntax()
        {
            throw new NotImplementedException();
        }
    }
    public partial class OptInitOrExprListDO : OptInitOrExprList
    {
        public virtual IASTNode ToAbstractSyntax()
        {
            throw new NotImplementedException();
        }
    }
    public partial class OptInitOrExprListTHEN : OptInitOrExprList
    {
        public virtual IASTNode ToAbstractSyntax()
        {
            throw new NotImplementedException();
        }
    }
    public partial class OptInitOrExprListENDWHILE : OptInitOrExprList
    {
        public virtual IASTNode ToAbstractSyntax()
        {
            throw new NotImplementedException();
        }
    }
    public partial class OptInitOrExprListENDIF : OptInitOrExprList
    {
        public virtual IASTNode ToAbstractSyntax()
        {
            throw new NotImplementedException();
        }
    }
    public partial class OptInitOrExprListELSE : OptInitOrExprList
    {
        public virtual IASTNode ToAbstractSyntax()
        {
            throw new NotImplementedException();
        }
    }
    public partial class OptInitOrExprListENDPROC : OptInitOrExprList
    {
        public virtual IASTNode ToAbstractSyntax()
        {
            throw new NotImplementedException();
        }
    }
    public partial class OptInitOrExprListENDFUN : OptInitOrExprList
    {
        public virtual IASTNode ToAbstractSyntax()
        {
            throw new NotImplementedException();
        }
    }
    public partial class OptInitOrExprListENDPROGRAM : OptInitOrExprList
    {
        public virtual IASTNode ToAbstractSyntax()
        {
            throw new NotImplementedException();
        }
    }
    public partial class OptInitOrExprListSEMICOLON : OptInitOrExprList
    {
        public virtual IASTNode ToAbstractSyntax()
        {
            throw new NotImplementedException();
        }
    }
    public partial class OptInitOrExprListBECOMES : OptInitOrExprList
    {
        public virtual IASTNode ToAbstractSyntax()
        {
            throw new NotImplementedException();
        }
    }
    public partial class OptInitOrExprListBOOLOPR : OptInitOrExprList
    {
        public virtual IASTNode ToAbstractSyntax()
        {
            throw new NotImplementedException();
        }
    }
    public partial class OptInitOrExprListRELOPR : OptInitOrExprList
    {
        public virtual IASTNode ToAbstractSyntax()
        {
            throw new NotImplementedException();
        }
    }
    public partial class OptInitOrExprListADDOPR : OptInitOrExprList
    {
        public virtual IASTNode ToAbstractSyntax()
        {
            throw new NotImplementedException();
        }
    }
    public partial class OptInitOrExprListMULTOPR : OptInitOrExprList
    {
        public virtual IASTNode ToAbstractSyntax()
        {
            throw new NotImplementedException();
        }
    }
    public partial class MonadicOprNOT : MonadicOpr
    {
        public virtual IASTNode ToAbstractSyntax()
        {
            throw new NotImplementedException();
        }
    }
    public partial class MonadicOprADDOPR : MonadicOpr
    {
        public virtual IASTNode ToAbstractSyntax()
        {
            throw new NotImplementedException();
        }
    }
    public partial class ExprListLPAREN : ExprList
    {
        public virtual IASTNode ToAbstractSyntax()
        {
            throw new NotImplementedException();
        }
    }
    public partial class OptExprListTYPE : OptExprList
    {
        public virtual IASTNode ToAbstractSyntax()
        {
            throw new NotImplementedException();
        }
    }
    public partial class OptExprListLPAREN : OptExprList
    {
        public virtual IASTNode ToAbstractSyntax()
        {
            throw new NotImplementedException();
        }
    }
    public partial class OptExprListADDOPR : OptExprList
    {
        public virtual IASTNode ToAbstractSyntax()
        {
            throw new NotImplementedException();
        }
    }
    public partial class OptExprListNOT : OptExprList
    {
        public virtual IASTNode ToAbstractSyntax()
        {
            throw new NotImplementedException();
        }
    }
    public partial class OptExprListIDENT : OptExprList
    {
        public virtual IASTNode ToAbstractSyntax()
        {
            throw new NotImplementedException();
        }
    }
    public partial class OptExprListLITERAL : OptExprList
    {
        public virtual IASTNode ToAbstractSyntax()
        {
            throw new NotImplementedException();
        }
    }
    public partial class OptExprListRPAREN : OptExprList
    {
        public virtual IASTNode ToAbstractSyntax()
        {
            throw new NotImplementedException();
        }
    }
    public partial class RepExprListCOMMA : RepExprList
    {
        public virtual IASTNode ToAbstractSyntax()
        {
            throw new NotImplementedException();
        }
    }
    public partial class RepExprListRPAREN : RepExprList
    {
        public virtual IASTNode ToAbstractSyntax()
        {
            throw new NotImplementedException();
        }
    }

}