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
    /// Métodos diversos de formatação utilizados como extensão.
    /// </summary>
    public static class FormatExtensions {
        /// <summary>
        /// Coloca o conteúdo de todas as propriedades do tipo String de um objeto em minúsculo.
        /// </summary>
        /// <param name="obj">Objeto a ser analisado.</param>
        public static void ToLower(this object obj) {
            FormatHelper.ChangeCasing(obj, true);
        }

        /// <summary>
        /// Coloca o conteúdo de todas as propriedades do tipo String de um objeto em maiúsculo.
        /// </summary>
        /// <param name="obj">Objeto a ser analisado.</param>
        public static void ToUpper(this object obj) {
            FormatHelper.ChangeCasing(obj, false);
        }
    }
}
