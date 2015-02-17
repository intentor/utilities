/*********************************************
Intentor.Utilities
**********************************************
Copyright � 2009-2012 Andr� "Intentor" Martins
http://intentor.com.br/
*********************************************/

using System;
using System.IO;
using System.Web;
using System.Web.Caching;

namespace Intentor.Utilities {
    /// <summary>
    /// M�todos de apoio no ambiente servidor.
    /// </summary>
    public static class ServerHelper {
        #region M�todos

        /// <summary>
        /// Realiza a escrita de uma stream na resposta da requisi��o HTTP atual.
        /// </summary>
        /// <param name="context">Contexto HTTP no qual se deve escrever a stream.</param>
        /// <param name="streamToResponse">Objeto <see cref="Stream"/> a ser escrito.</param>
        /// <param name="contentType">Identifica qual o tipo conte�do a ser escrito.</param>
        /// <remarks>A resposta sempre � finalizada ap�s a escrita da stream.</remarks>
        public static void ResponseStream(HttpContext context,
            Stream streamToResponse,
            string contentType) {
            ServerHelper.ResponseStream(context,
                streamToResponse,
                contentType,
                true);
        }

        /// <summary>
        /// Realiza a escrita de uma stream na resposta da requisi��o HTTP atual.
        /// </summary>
        /// <param name="context">Contexto HTTP no qual se deve escrever a stream.</param>
        /// <param name="streamToResponse">Objeto <see cref="Stream"/> a ser escrito.</param>
        /// <param name="contentType">Identifica qual o tipo conte�do a ser escrito.</param>
        /// <param name="endResponse">Idenfifica se se deve finalizar a resposta logo ap�s a escrita do conte�do.</param>
        public static void ResponseStream(HttpContext context,
            Stream streamToResponse,
            string contentType,
            bool endResponse) {
            context.ClearError();
            context.Response.Expires = 0;
            context.Response.Buffer = true;
            context.Response.Clear();

            using (MemoryStream ms = (MemoryStream)streamToResponse) {
                ms.WriteTo(context.Response.OutputStream);
            }

            context.Response.ContentType = contentType;
            context.Response.StatusCode = 200;

            if (endResponse) {
                context.Response.End();
            }
        }

        /// <summary>
        /// Insere um objeto no cache de um contexto HTTP.
        /// </summary>
        /// <param name="context">Contexto HTTP no qual o objeto ser� inserido em cache.</param>
        /// <param name="cacheId">Id do cache.</param>
        /// <param name="objectToInsert">Objeto a ser inserido em cache.</param>
        /// <remarks>Por padr�o, a dura��o do objeto no cache � de 1 minuto.</remarks>
        public static void InsertInCache(HttpContext context,
            string cacheId,
            object objectToInsert) {
            ServerHelper.InsertInCache(context,
                cacheId,
                objectToInsert,
                1);
        }

        /// <summary>
        /// Insere um objeto no cache de um contexto HTTP.
        /// </summary>
        /// <param name="context">Contexto HTTP no qual o objeto ser� inserido em cache.</param>
        /// <param name="cacheId">Id do cache.</param>
        /// <param name="objectToInsert">Objeto a ser inserido em cache.</param>
        /// <param name="minutesToExpiration">Identifica o o per�odo, em minutos, no qual o objeto deve ser mantido em cache.</param>
        public static void InsertInCache(HttpContext context,
            string cacheId,
            object objectToInsert,
            short minutesToExpiration) {
            context.Cache.Insert(cacheId
                , objectToInsert
                , null
                , DateTime.UtcNow.AddMinutes(minutesToExpiration)
                , Cache.NoSlidingExpiration);
        }

        #endregion
    }
}
