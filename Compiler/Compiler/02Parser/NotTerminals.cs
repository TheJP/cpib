using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Compiler
{
    public enum NotTerminals
    {
        program,
        decl,
        stoDecl,
        funDecl,
        procDecl,
        optGlobImps,
        globImps,
        repGlobImps,
        optChangemode,
        optMechmode,
        globImp,
        optCpsDecl,
        cpsDecl,
        repCpsDecl,
        optCpsStoDecl,
        cpsStoDecl,
        repCpsStoDecl,
        progParamList,
        optProgParamList,
        repProgParamList,
        progParam,
        paramList,
        optParamList,
        repParamList,
        param,
        typedIdent,
        cmd,
        cpsCmd,
        repCpsCmd,
        optGlobInits,
        repIdents,
        expr,
        repTerm1,
        term1,
        repTerm2,
        term2,
        repTerm3,
        term3,
        repFactor,
        factor,
        optInitOrExprList,
        monadicOpr,
        exprList,
        optExprList,
        repExprList,
    }
}
