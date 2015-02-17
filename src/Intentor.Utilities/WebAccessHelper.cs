/*********************************************
Intentor.Utilities
**********************************************
Copyright © 2009-2012 André "Intentor" Martins
http://intentor.com.br/
*********************************************/

using System;
using System.IO;
using System.Net;
using System.Text;
using System.Web;

namespace Intentor.Utilities {
    /// <summary>
    /// Métodos para acesso a páginas web externas.
    /// </summary>
    public static class WebAccessHelper {
        #region GET

        /// <summary>
        /// Obtém o conteúdo de uma determinada URL como array de bytes.
        /// </summary>
        /// <param name="url">URL a ser acessada.</param>
        /// <returns>Conteúdo da URL como array de bytes.</returns>
        public static byte[] GetAsByte(string url) {
            var wc = new System.Net.WebClient();
            return wc.DownloadData(url);
        }

        /// <summary>
        /// Obtém o conteúdo de uma determinada URL como <see cref="Stream"/>.
        /// </summary>
        /// <param name="url">URL a ser acessada.</param>
        /// <returns>Conteúdo da URL como <see cref="Stream"/>.</returns>
        public static System.IO.Stream GetAsStream(string url) {
            byte[] response = GetAsByte(url);
            return new System.IO.MemoryStream(response);
        }

        /// <summary>
        /// Obtém o conteúdo de uma determinada URL como texto.
        /// </summary>
        /// <param name="url">URL a ser acessada.</param>
        /// <returns>Conteúdo da URL em formato texto.</returns>
        public static string Get(string url) {
            return Get(url, Encoding.UTF8);
        }

        /// <summary>
        /// Obtém o conteúdo de uma determinada URL como texto.
        /// </summary>
        /// <param name="url">URL a ser acessada.</param>
        /// <param name="encoding">Encoding do conteúdo.</param>
        /// <returns>Conteúdo da URL em formato texto.</returns>
        public static string Get(string url, Encoding encoding) {
            byte[] response = GetAsByte(url);
            return encoding.GetString(response);
        }

        #endregion

        #region POST

        /// <summary>
        /// Realiza uma requisição POST a uma URL.
        /// </summary>
        /// <param name="url">URL da página a ser acessada.</param>
        /// <param name="postData">Parâmetros da requisição.</param>
        /// <remarks>Serão utilizados 10 segundos como valor padrão de timeout.</remarks>
        /// <returns>String contendo o retorno do POST realizado.</returns>
        public static string Post(string url
            , PostVariables postData) {
            return Post(url
                , postData
                , 10000
                , "application/x-www-form-urlencoded"
                , CultureHelper.EncodingUtf8);
        }

        /// <summary>
        /// Realiza uma requisição POST a uma URL.
        /// </summary>
        /// <param name="url">URL da página a ser acessada.</param>
        /// <param name="postData">Parâmetros da requisição.</param>
        /// <param name="timeOut">Valor máximo, dado em milissegundos, o qual a resposta a uma requisição será aguardada.</param>
        /// <returns>String contendo o retorno do POST realizado.</returns>
        public static string Post(string url
            , PostVariables postData
            , int timeOut) {
            return Post(url
                , postData
                , timeOut
                , "application/x-www-form-urlencoded"
                , CultureHelper.EncodingUtf8);
        }

        /// <summary>
        /// Realiza uma requisição POST a uma URL.
        /// </summary>
        /// <param name="url">URL da página a ser acessada.</param>
        /// <param name="postData">Parâmetros da requisição.</param>
        /// <param name="encoding">Codificação do conteúdo recebido.</param>
        /// <remarks>Serão utilizados 10 segundos como valor padrão de timeout.</remarks>
        /// <returns>String contendo o retorno do POST realizado.</returns>
        public static string Post(string url
            , PostVariables postData
            , System.Text.Encoding encoding) {
            return Post(url
                , postData
                , 10000
                , "application/x-www-form-urlencoded"
                , encoding);
        }

        /// <summary>
        /// Realiza uma requisição POST a uma URL.
        /// </summary>
        /// <param name="url">URL da página a ser acessada.</param>
        /// <param name="postData">Parâmetros da requisição.</param>
        /// <param name="contentType">ContentType a ser utilizado no post.</param>
        /// <remarks>Serão utilizados 10 segundos como valor padrão de timeout.</remarks>
        /// <returns>String contendo o retorno do POST realizado.</returns>
        public static string Post(string url
            , PostVariables postData
            , string contentType) {
            return Post(url
                , postData
                , 10000
                , contentType
                , CultureHelper.EncodingUtf8);
        }

        /// <summary>
        /// Realiza uma requisição POST a uma URL.
        /// </summary>
        /// <param name="url">URL da página a ser acessada.</param>
        /// <param name="postData">Parâmetros da requisição.</param>
        /// <param name="timeOut">Valor máximo, dado em milissegundos, o qual a resposta a uma requisição será aguardada.</param>
        /// <param name="contentType">ContentType a ser utilizado no post.</param>
        /// <returns>String contendo o retorno do POST realizado.</returns>
        public static string Post(string url
            , PostVariables postData
            , int timeOut
            , string contentType) {
            return Post(url
                , postData
                , timeOut
                , contentType
                , CultureHelper.EncodingUtf8);
        }

        /// <summary>
        /// Realiza uma requisição POST a uma URL.
        /// </summary>
        /// <param name="url">URL da página a ser acessada.</param>
        /// <param name="postData">Parâmetros da requisição.</param>
        /// <param name="timeOut">Valor máximo, dado em milissegundos, o qual a resposta a uma requisição será aguardada.</param>
        /// <param name="contentType">ContentType a ser utilizado no post.</param>
        /// <param name="encoding">Codificação do conteúdo recebido.</param>
        /// <returns>String contendo o retorno do POST realizado.</returns>
        public static string Post(string url
            , PostVariables postData
            , int timeOut
            , string contentType
            , System.Text.Encoding encoding) {
            byte[] dataToPost = encoding.GetBytes(postData.ToString()); //Array de bytes com os parâmetros a serem enviados.
            string responseData = String.Empty;

            var request = (HttpWebRequest)WebRequest.Create(url);

            //Define os parâmetros da requisição.
            request.Method = "POST";
            request.Timeout = timeOut;
            request.ContentLength = dataToPost.Length;
            request.ContentType = contentType;

            //Realiza a requisição ao servidor remoto, enviando os dados solicitados.
            try {
                Stream requestStream = request.GetRequestStream();
                requestStream.Write(dataToPost, 0, dataToPost.Length);
                requestStream.Flush();
                responseData = GetResponse(request, encoding);
            } catch (Exception ex) {
                throw new Exception("Problemas durante a requisição ao servidor remoto. Consulte exceção anexa para maiores detalhes.", ex);
            }

            return responseData;
        }

        #endregion

        #region Apoio

        /// <summary>
        /// Obtém uma resposta HTTP.
        /// </summary>
        /// <param name="request">Objeto de requisição.</param>
        /// <param name="encoding">Codificação do conteúdo recebido.</param>
        /// <returns>Resposta da requisição, em formato string.</returns>
        private static string GetResponse(HttpWebRequest request, System.Text.Encoding encoding) {
            string responseData = String.Empty;

            try {
                using (HttpWebResponse response = (HttpWebResponse)request.GetResponse()) {
                    //Realiza leitura da resposta.
                    using (StreamReader readStream = new StreamReader(response.GetResponseStream(), encoding)) {
                        //Obtém a resposta do servidor em formato texto.
                        responseData = readStream.ReadToEnd();
                    }
                }
            } catch (Exception ex) {
                throw new Exception("Problemas durante obtenção da resposta do servidor remoto. Consulte exceção anexa para maiores detalhes.", ex);
            }

            return responseData;
        }

        #endregion
    }
}
