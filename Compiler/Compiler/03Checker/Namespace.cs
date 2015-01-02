using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Compiler
{
    /// <summary>
    /// Defines a namespace which contains identifiers and their declaration.
    /// </summary>
    /// <typeparam name="T">Declaration Type</typeparam>
    public class Namespace<T> : IEnumerable<string> where T : IASTDecl
    {
        private IDictionary<string, T> declarations = new Dictionary<string, T>();
        public T this[string ident]{
            get { return declarations[ident]; }
        }

        public int Count { get { return declarations.Count; } }

        /// <summary>
        /// Resturns if the given ident is contained in this namespace
        /// </summary>
        /// <param name="ident"></param>
        /// <returns></returns>
        public bool ContainsIdent(string ident)
        {
            return declarations.ContainsKey(ident);
        }

        /// <summary>
        /// Add declaration to the namespace. Throws an exception if the declaration already exists in this namespace.
        /// </summary>
        /// <param name="ident"></param>
        /// <param name="declaration"></param>
        public void addDeclaration(T declaration)
        {
            if (declarations.ContainsKey(declaration.Ident)) { throw new CheckerException(String.Format("Duplicate declarations: '{0}' and '{1}'", declaration, declarations[declaration.Ident])); }
            declarations.Add(declaration.Ident, declaration);
        }

        public IEnumerator<string> GetEnumerator()
        {
            return declarations.Keys.GetEnumerator();
        }

        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            return this.GetEnumerator();
        }
    }
}
