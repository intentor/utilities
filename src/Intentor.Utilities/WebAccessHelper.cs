/*********************************************
Intentor.Utilities
**********************************************
Copyright � 2009-2012 Andr� "Intentor" Martins
http://intentor.com.br/
*********************************************/

using System;
using System.IO;
using System.Net;
using System.Text;
using System.Web;

namespace Intentor.Utilities {
    /// <summary>
    /// M�todos para acesso a p�ginas web externas.
    /// </summary>
    public static class WebAccessHelper {
        #region GET

        /// <summary>
        /// Obt�m o conte�do de uma determinada URL como array de bytes.
        /// </summary>
        /// <param name="url">URL a ser acessada.</param>
        /// <returns>Conte�do da URL como array de bytes.</returns>
        public static byte[] GetAsByte(string url) {
            var wc = new System.Net.WebClient();
            return wc.DownloadData(url);
        }

        /// <summary>
        /// Obt�m o conte�do de uma determinada URL como <see cref="Stream"/>.
        /// </summary>
        /// <param name="url">URL a ser acessada.</param>
        /// <returns>Conte�do da URL como <see cref="Stream"/>.</returns>
        public static System.IO.Stream GetAsStream(string url) {
            byte[] response = GetAsByte(url);
            return new System.IO.MemoryStream(response);
        }

        /// <summary>
        /// Obt�m o conte�do de uma determinada URL como texto.
        /// </summary>
        /// <param name="url">URL a ser acessada.</param>
        /// <returns>Conte�do da URL em formato texto.</returns>
        public static string Get(string url) {
            return Get(url, Encoding.UTF8);
        }

        /// <summary>
        /// Obt�m o conte�do de uma determinada URL como texto.
        /// </summary>
        /// <param name="url">URL a ser acessada.</param>
        /// <param name="encoding">Encoding do conte�do.</param>
        /// <returns>Conte�do da URL em formato texto.</returns>
        public static string Get(string url, Encoding encoding) {
            byte[] response = GetAsByte(url);
            return encoding.GetString(response);
        }

        #endregion

        #region POST

        /// <summary>
        /// Realiza uma requisi��o POST a uma URL.
        /// </summary>
        /// <param name="url">URL da p�gina a ser acessada.</param>
        /// <param name="postData">Par�metros da requisi��o.</param>
        /// <remarks>Ser�o utilizados 10 segundos como valor padr�o de timeout.</remarks>
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
        /// Realiza uma requisi��o POST a uma URL.
        /// </summary>
        /// <param name="url">URL da p�gina a ser acessada.</param>
        /// <param name="postData">Par�metros da requisi��o.</param>
        /// <param name="timeOut">Valor m�ximo, dado em milissegundos, o qual a resposta a uma requisi��o ser� aguardada.</param>
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
        /// Realiza uma requisi��o POST a uma URL.
        /// </summary>
        /// <param name="url">URL da p�gina a ser acessada.</param>
        /// <param name="postData">Par�metros da requisi��o.</param>
        /// <param name="encoding">Codifica��o do conte�do recebido.</param>
        /// <remarks>Ser�o utilizados 10 segundos como valor padr�o de timeout.</remarks>
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
        /// Realiza uma requisi��o POST a uma URL.
        /// </summary>
        /// <param name="url">URL da p�gina a ser acessada.</param>
        /// <param name="postData">Par�metros da requisi��o.</param>
        /// <param name="contentType">ContentType a ser utilizado no post.</param>
        /// <remarks>Ser�o utilizados 10 segundos como valor padr�o de timeout.</remarks>
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
        /// Realiza uma requisi��o POST a uma URL.
        /// </summary>
        /// <param name="url">URL da p�gina a ser acessada.</param>
        /// <param name="postData">Par�metros da requisi��o.</param>
        /// <param name="timeOut">Valor m�ximo, dado em milissegundos, o qual a resposta a uma requisi��o ser� aguardada.</param>
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
        /// Realiza uma requisi��o POST a uma URL.
        /// </summary>
        /// <param name="url">URL da p�gina a ser acessada.</param>
        /// <param name="postData">Par�metros da requisi��o.</param>
        /// <param name="timeOut">Valor m�ximo, dado em milissegundos, o qual a resposta a uma requisi��o ser� aguardada.</param>
        /// <param name="contentType">ContentType a ser utilizado no post.</param>
        /// <param name="encoding">Codifica��o do conte�do recebido.</param>
        /// <returns>String contendo o retorno do POST realizado.</returns>
        public static string Post(string url
            , PostVariables postData
            , int timeOut
            , string contentType
            , System.Text.Encoding encoding) {
            byte[] dataToPost = encoding.GetBytes(postData.ToString()); //Array de bytes com os par�metros a serem enviados.
            string responseData = String.Empty;

            var request = (HttpWebRequest)WebRequest.Create(url);

            //Define os par�metros da requisi��o.
            request.Method = "POST";
            request.Timeout = timeOut;
            request.ContentLength = dataToPost.Length;
            request.ContentType = contentType;

            //Realiza a requisi��o ao servidor remoto, enviando os dados solicitados.
            try {
                Stream requestStream = request.GetRequestStream();
                requestStream.Write(dataToPost, 0, dataToPost.Length);
                requestStream.Flush();
                responseData = GetResponse(request, encoding);
            } catch (Exception ex) {
                throw new Exception("Problemas durante a requisi��o ao servidor remoto. Consulte exce��o anexa para maiores detalhes.", ex);
            }

            return responseData;
        }

        #endregion

        #region Apoio

        /// <summary>
        /// Obt�m uma resposta HTTP.
        /// </summary>
        /// <param name="request">Objeto de requisi��o.</param>
        /// <param name="encoding">Codifica��o do conte�do recebido.</param>
        /// <returns>Resposta da requisi��o, em formato string.</returns>
        private static string GetResponse(HttpWebRequest request, System.Text.Encoding encoding) {
            string responseData = String.Empty;

            try {
                using (HttpWebResponse response = (HttpWebResponse)request.GetResponse()) {
                    //Realiza leitura da resposta.
                    using (StreamReader readStream = new StreamReader(response.GetResponseStream(), encoding)) {
                        //Obt�m a resposta do servidor em formato texto.
                        responseData = readStream.ReadToEnd();
                    }
                }
            } catch (Exception ex) {
                throw new Exception("Problemas durante obten��o da resposta do servidor remoto. Consulte exce��o anexa para maiores detalhes.", ex);
            }

            return responseData;
        }

        #endregion
    }
}
