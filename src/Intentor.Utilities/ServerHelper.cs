/*********************************************
Intentor.Utilities
**********************************************
Copyright © 2009-2012 André "Intentor" Martins
http://intentor.com.br/
*********************************************/

using System;
using System.IO;
using System.Web;
using System.Web.Caching;

namespace Intentor.Utilities {
    /// <summary>
    /// Métodos de apoio no ambiente servidor.
    /// </summary>
    public static class ServerHelper {
        #region Métodos

        /// <summary>
        /// Realiza a escrita de uma stream na resposta da requisição HTTP atual.
        /// </summary>
        /// <param name="context">Contexto HTTP no qual se deve escrever a stream.</param>
        /// <param name="streamToResponse">Objeto <see cref="Stream"/> a ser escrito.</param>
        /// <param name="contentType">Identifica qual o tipo conteúdo a ser escrito.</param>
        /// <remarks>A resposta sempre é finalizada após a escrita da stream.</remarks>
        public static void ResponseStream(HttpContext context,
            Stream streamToResponse,
            string contentType) {
            ServerHelper.ResponseStream(context,
                streamToResponse,
                contentType,
                true);
        }

        /// <summary>
        /// Realiza a escrita de uma stream na resposta da requisição HTTP atual.
        /// </summary>
        /// <param name="context">Contexto HTTP no qual se deve escrever a stream.</param>
        /// <param name="streamToResponse">Objeto <see cref="Stream"/> a ser escrito.</param>
        /// <param name="contentType">Identifica qual o tipo conteúdo a ser escrito.</param>
        /// <param name="endResponse">Idenfifica se se deve finalizar a resposta logo após a escrita do conteúdo.</param>
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
        /// <param name="context">Contexto HTTP no qual o objeto será inserido em cache.</param>
        /// <param name="cacheId">Id do cache.</param>
        /// <param name="objectToInsert">Objeto a ser inserido em cache.</param>
        /// <remarks>Por padrão, a duração do objeto no cache é de 1 minuto.</remarks>
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
        /// <param name="context">Contexto HTTP no qual o objeto será inserido em cache.</param>
        /// <param name="cacheId">Id do cache.</param>
        /// <param name="objectToInsert">Objeto a ser inserido em cache.</param>
        /// <param name="minutesToExpiration">Identifica o o período, em minutos, no qual o objeto deve ser mantido em cache.</param>
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
