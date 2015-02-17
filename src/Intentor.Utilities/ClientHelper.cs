/*********************************************
Intentor.Utilities
**********************************************
Copyright � 2009-2012 Andr� "Intentor" Martins
http://intentor.com.br/
*********************************************/

using System;
using System.Web.UI;

namespace Intentor.Utilities {
    /// <summary>
    /// M�todos de apoio na manipula��o de conte�do no cliente web (navegador).
    /// </summary>
    public static class ClientHelper {
        #region M�todos

        #region MessageBox

        /// <summary>
        /// Exibe caixa de mensagem no cliente.
        /// </summary>
        /// <param name="texto">Texto a ser exibido na caixa de mensagem.</param>
        public static void MessageBox(string texto) {
            texto = texto.Replace("\n", "");

            string script = "alert('" + texto + "');";

            ClientHelper.RunStartupScript(script);
        }

        /// <summary>
        /// Exibe caixa de mensagem no cliente a partir de uma requisi��o AJAX realizada atrav�s do AJAX.Net.
        /// </summary>
        /// <param name="texto">Texto a ser exibido na caixa de mensagem.</param>
        public static void MessageBoxOverAjax(string texto) {
            texto = texto.Replace("\n", "");

            string script = "alert('" + texto + "');";

            ClientHelper.RunStartupScriptOverAjax(script);
        }

        #endregion

        #region RunStartupScript

        /// <summary>
        /// Executa c�digos JavaScript na inicializa��o da p�gina.
        /// </summary>
        /// <param name="script">Script a ser executado no cliente.</param>
        /// <remarks>As tags script s�o adicionadas automaticamente.</remarks>
        public static void RunStartupScript(string script) {
            System.Web.UI.Page pagina = (System.Web.UI.Page)System.Web.HttpContext.Current.Handler;

            pagina.ClientScript.RegisterStartupScript(pagina.GetType(),
                Guid.NewGuid().ToString(),
                script,
                true);
        }

        /// <summary>
        /// Executa c�digos JavaScript no retorno de uma requisi��o AJAX realizada atrav�s do AJAX.Net.
        /// </summary>
        /// <param name="script">Script a ser executado no cliente.</param>
        /// <remarks>As tags script s�o adicionadas automaticamente.</remarks>
        public static void RunStartupScriptOverAjax(string script) {
            System.Web.UI.Page pagina = (System.Web.UI.Page)System.Web.HttpContext.Current.Handler;

            ScriptManager.RegisterStartupScript(pagina,
                pagina.GetType(),
                Guid.NewGuid().ToString(),
                script,
                true);
        }

        #endregion

        #endregion
    }
}
