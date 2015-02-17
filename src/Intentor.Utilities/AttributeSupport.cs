/*********************************************
Intentor.Utilities
**********************************************
Copyright � 2009-2012 Andr� "Intentor" Martins
http://intentor.com.br/
*********************************************/

using System;
using System.Reflection;

namespace Intentor.Utilities {
    /// <summary>
    /// Classe para apoio na utiliza��o de atributos.
    /// </summary>
    public static class AttributeSupport {
        #region M�todos

        /// <summary>
        /// Verifica se um determinado atributo existe em um objeto.
        /// </summary>
        /// <param name="obj">Objeto o qual se deseja verificar a exist�ncia do atributo.</param>
        /// <param name="att">Atributo a ser verificado.</param>
        /// <returns>Valor booleano indicando a exist�ncia do atributo.</returns>
        public static bool CheckAttributeExistence(object obj,
            Type att) {
            return AttributeSupport.CheckAttributeExistence(obj,
                att,
                false);
        }

        /// <summary>
        /// Verifica se um determinado atributo existe em um objeto.
        /// </summary>
        /// <param name="obj">Objeto o qual se deseja verificar a exist�ncia do atributo.</param>
        /// <param name="att">Atributo a ser verificado.</param>
        /// <param name="useBaseType">Indica se se deve utilizar o tipo-base do objeto.</param>
        /// <returns>Valor booleano indicando a exist�ncia do atributo.</returns>
        public static bool CheckAttributeExistence(object obj,
            Type att,
            bool useBaseType) {
            bool exists = false;

            if (obj != null) {
                exists = CheckAttributeExistence(obj.GetType()
                    , att
                    , useBaseType);
            }

            return exists;
        }

        /// <summary>
        /// Verifica se um determinado atributo existe em um objeto.
        /// </summary>
        /// <param name="obj">Tipo do objeto o qual se deseja verificar a exist�ncia do atributo.</param>
        /// <param name="att">Atributo a ser verificado.</param>
        /// <param name="useBaseType">Indica se se deve utilizar o tipo-base do objeto.</param>
        /// <returns>Valor booleano indicando a exist�ncia do atributo.</returns>
        public static bool CheckAttributeExistence(Type obj,
            Type att,
            bool useBaseType) {
            bool exists = false;

            MemberInfo typeInfo;

            if (useBaseType) {
                typeInfo = obj.BaseType;
            } else {
                typeInfo = obj;
            }

            if (typeInfo.GetCustomAttributes(att, false).Length > 0) {
                exists = true;
            }

            return exists;
        }

        #endregion
    }
}
