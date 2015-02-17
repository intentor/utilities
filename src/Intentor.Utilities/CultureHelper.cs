/*********************************************
Intentor.Utilities
**********************************************
Copyright © 2009-2012 André "Intentor" Martins
http://intentor.com.br/
*********************************************/

using System;
using System.Globalization;
using System.Threading;
using System.Text;
using System.Web;

namespace Intentor.Utilities {
    /// <summary>
    /// Apoio na manipulação de cultura em aplicações web.
    /// </summary>
    public static class CultureHelper {
        #region Campos

        /// <summary>
        /// Nome do cookie utilizado para definção de cultura da aplicação.
        /// </summary>
        private const string _cookieName = "CultureData";

        /// <summary>
        /// Definições de globalização para Português do Brasil (PT-BR).
        /// </summary>
        public static readonly CultureInfo CulturePtBr = new CultureInfo("pt-BR");

        /// <summary>
        /// Definições de globalização para Inglês dos EUA (EN-US).
        /// </summary>
        public static readonly CultureInfo CultureEnUs = new CultureInfo("en-US");

        /// <summary>
        /// Definições de globalização para Inglês dos EUA (EN-US).
        /// </summary>
        public static readonly CultureInfo Invariant = CultureInfo.InvariantCulture;

        /// <summary>
        /// Conjunto de caracteres de idiomas latinos, compatível com Português do Brasil (iso-8859-1).
        /// </summary>
        public static readonly Encoding EncodingIso88591 = Encoding.GetEncoding("iso-8859-1");

        /// <summary>
        /// Conjunto de caracteres do Inglês (utf-8).
        /// </summary>
        public static readonly Encoding EncodingUtf8 = Encoding.UTF8;

        #endregion

        #region Struct

        /// <summary>
        /// Indentifica dados de cultura da aplicação.
        /// </summary>
        public struct CultureData {
            /// <summary>
            /// Idioma da cultura base.
            /// </summary>
            public string Culture {
                get;
                set;
            }

            /// <summary>
            /// Idioma da interface.
            /// </summary>
            public string UICulture {
                get;
                set;
            }
        }

        #endregion

        #region Métodos

        /// <summary>
        /// Obtém informações de cultura do cookie.
        /// </summary>
        /// <returns>Objeto <see cref="CultureData"/>.</returns>
        public static CultureData GetInfoFromCookie() {
            //Cria o objeto de retorno;
            CultureData currentCulture = new CultureData();

            //Verifica se o cookie existe.
            HttpCookie cookie = HttpContext.Current.Request.Cookies[_cookieName];

            if (cookie != null) {
                currentCulture.Culture = cookie.Values["Culture"];
                currentCulture.UICulture = cookie.Values["UICulture"];
            } else {
                if (HttpContext.Current.Request.UserLanguages.Length > 0) {
                    string userLanguage = HttpContext.Current.Request.UserLanguages[0];

                    currentCulture.Culture =
                        currentCulture.UICulture = userLanguage;
                } else {
                    currentCulture.Culture = "pt-br";
                    currentCulture.UICulture = "pt-br";
                }

                //Cria o cookie.
                CultureHelper.SaveCulture(currentCulture.Culture,
                    currentCulture.UICulture);
            }

            return currentCulture;
        }

        /// <summary>
        /// Salva configurações de localização da aplicação para uso durante o acesso à aplicação.
        /// </summary>
        /// <param name="culture">Idioma da cultura base.</param>
        /// <param name="uiCulture">Idioma da interface.</param>
        /// <remarks>A página não é reinicializada ao executar-se tal método.</remarks>
        public static void SaveCulture(string culture,
            string uiCulture) {
            CultureHelper.SaveCulture(culture,
                uiCulture,
                false);
        }

        /// <summary>
        /// Salva configurações de localização da aplicação para uso durante o acesso à aplicação.
        /// </summary>
        /// <param name="culture">Idioma da cultura base.</param>
        /// <param name="uiCulture">Idioma da interface.</param>
        /// <param name="restartPage">Identifica se a página deve ser reinicializada.</param>
        public static void SaveCulture(string culture,
            string uiCulture,
            bool restartPage) {
            HttpCookie aCookie = new HttpCookie(_cookieName);
            aCookie.Values.Add("Culture", culture);
            aCookie.Values.Add("UICulture", uiCulture);
            aCookie.Expires = DateTime.Now.AddDays(10); //Armazena o cookie por 10 dias.

            HttpContext.Current.Response.Cookies.Add(aCookie);

            if (restartPage) {
                //Força a reinicialização da página para execução do evento InitializeCulture()
                HttpContext.Current.Response.Redirect(HttpContext.Current.Request.Url.LocalPath);
            }
        }

        /// <summary>
        /// Configura a aplicação para suporte a um dado idioma.
        /// </summary>
        /// <param name="cultureToSet">Dados da cultura a ser definida.</param>
        public static void SetCulture(CultureData cultureToSet) {
            CultureHelper.SetCulture(cultureToSet.Culture,
                cultureToSet.UICulture);
        }

        /// <summary>
        /// Configura a aplicação para suporte a um dado idioma.
        /// </summary>
        /// <param name="culture">Idioma da cultura base.</param>
        /// <param name="uiCulture">Idioma da interface.</param>
        public static void SetCulture(string culture,
            string uiCulture) {
            Thread.CurrentThread.CurrentCulture =
                CultureInfo.CreateSpecificCulture(culture);

            Thread.CurrentThread.CurrentUICulture = new
                CultureInfo(uiCulture);
        }

        #endregion
    }
}

