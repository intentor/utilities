/*********************************************
Intentor.Utilities
**********************************************
Copyright � 2009-2012 Andr� "Intentor" Martins
http://intentor.com.br/
*********************************************/

using System;
using System.Web;

namespace Intentor.Utilities {
    /// <summary>
    /// Constantes de aplica��o.
    /// </summary>
    public static class AppConstants {
        /// <summary>
        /// Nome do assembly do componente.
        /// </summary>
        internal const string AssemblyName = "Intentor.Utilities";

        /// <summary>
        /// URL base da aplica��o.
        /// </summary>
        public static string BaseUrl {
            get {
                string url = HttpContext.Current.Request.ApplicationPath;

                if (url == "/")
                    return String.Empty;
                else
                    return HttpContext.Current.Request.ApplicationPath;
            }
        }

        /// <summary>
        /// Caminho da aplica��o no servidor.
        /// </summary>
        /// <remarks>O endere�o � retornado sem barra invertida no final.</remarks>
        public static string BaseServerPath {
            get {
                return HttpRuntime.AppDomainAppPath;
            }
        }
    }
}
