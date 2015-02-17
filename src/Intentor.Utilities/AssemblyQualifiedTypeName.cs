/*********************************************
Intentor.Utilities
**********************************************
Copyright © 2009-2012 André "Intentor" Martins
http://intentor.com.br/
*********************************************/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Intentor.Utilities {
    /// <summary>
    /// Representa um tipo em um assembly.
    /// </summary>
    /// <remarks>
    /// Versão original em NHibernate-1.2.1.GA (parcialmente modificada).
    /// </remarks>
    public sealed class AssemblyQualifiedTypeName {
        #region Campos

        /// <summary>
        /// Nome do tipo.
        /// </summary>
        private string _typeName;

        /// <summary>
        /// Nome completo do assembly.
        /// </summary>
        private string _assemblyName;

        #endregion

        #region Construtor

        /// <summary>
        /// Construtor da classe.
        /// </summary>
        /// <param name="typeName">Nome do tipo.</param>
        /// <param name="assemblyName">Nome completo do assembly.</param>
        public AssemblyQualifiedTypeName(string typeName, string assemblyName) {
            Check.Require(!String.IsNullOrEmpty(typeName), "O tipo do objeto no assembly é obrigatório.");

            _typeName = typeName;
            _assemblyName = assemblyName;
        }

        #endregion

        /// <summary>
        /// Nome do tipo.
        /// </summary>
        public string Type {
            get { return _typeName; }
        }

        /// <summary>
        /// Nome completo do assembly.
        /// </summary>
        public string Assembly {
            get { return _assemblyName; }
        }

        /// <summary>
        /// Compara o nome do assembly com outro.
        /// </summary>
        /// <param name="obj">Objeto a ser comparado.</param>
        /// <returns>Valor booleano indicando a igualdade.</returns>
        public override bool Equals(object obj) {
            AssemblyQualifiedTypeName other = obj as AssemblyQualifiedTypeName;

            if (other == null) return false;

            return string.Equals(_typeName, other.Type)
                   && string.Equals(_assemblyName, other.Assembly);
        }

        /// <summary>
        /// Obtém o HashCode.
        /// </summary>
        /// <returns>HashCode.</returns>
        public override int GetHashCode() {
            return base.GetHashCode();
        }

        /// <summary>
        /// Converte o nome do assembly para string.
        /// </summary>
        /// <returns>Nome do asssembly.</returns>
        public override string ToString() {
            if (String.IsNullOrEmpty(_assemblyName)) {
                return _typeName;
            }

            return string.Concat(_typeName, ", ", _assemblyName);
        }
    }
}
