/****************************************
Intentor.Utilities
*****************************************
Copyright © 2010 André "Intentor" Martins
http://intentor.com.br/
****************************************/

using System;
using System.IO;
using System.Web;
using System.Web.Management;

namespace Intentor.Utilities.Handlers
{
    /// <summary>
    /// Fábrica para geração de objetos <see cref="IHttpHandlerFactory"/> utilizados por Intentor.Utilities.
    /// </summary>
    /// <remarks>
    /// Deve-se adicionar, na subseção "httpHandlers" de "system.web" uma referência ao
    /// handler da Intentor.Utilities, conforme exemplo abaixo:
    /// 
    ///     &#60;system.web&#62;
    /// 
    ///         &#60;httpHandlers&#62;
    ///             &#60;add verb="POST,GET" path="intentor/*.*" type="Intentor.Utilities.Handlers.HandlerFactory, Intentor.Utilities"/&#62;
    ///         &#60;/httpHandlers&#62;
    ///         
    ///         (...)
    /// 
    ///     &#60;/system.web&#62;
    /// </remarks>
    public class HandlerFactory : 
        IHttpHandlerFactory
    {
        #region IHttpHandlerFactory Members

        /// <summary>
        /// Enables a factory to reuse an existing handler instance.
        /// </summary>
        /// <param name="handler">
        /// The <see cref="T:System.Web.IHttpHandler"></see> object to release.
        /// </param>
        public void ReleaseHandler(IHttpHandler handler)
        {
            
        }

        /// <summary>
        /// Returns an instance of a class that implements the 
        /// <see cref="T:System.Web.IHttpHandler"></see> interface.
        /// </summary>
        /// <param name="context">
        /// An instance of the <see cref="T:System.Web.HttpContext"></see> 
        /// class that provides references to intrinsic server objects 
        /// (for example, Request, Response, Session, and Server) used to 
        /// service HTTP requests.
        /// </param>
        /// <param name="requestType">
        /// The HTTP data transfer method (GET or POST) that the client uses.
        /// </param>
        /// <param name="url">
        /// The <see cref="P:System.Web.HttpRequest.RawUrl"></see> of the 
        /// requested resource.
        /// </param>
        /// <param name="pathTranslated">
        /// The <see cref="P:System.Web.HttpRequest.PhysicalApplicationPath"></see> 
        /// to the requested resource.
        /// </param>
        /// <returns>
        /// A new <see cref="T:System.Web.IHttpHandler"></see> object that 
        /// processes the request.
        /// </returns>
        public IHttpHandler GetHandler(HttpContext context, 
            string requestType, 
            string url, 
            string pathTranslated)
        {            
            //Obtém o nome do arquivo solicitado.
            string filename = Path.GetFileNameWithoutExtension(context.Request.Path);

            IHttpHandler handler = null;

            if (Path.GetExtension(context.Request.Path) == "js")
            {
                handler = new EmbeddedJavaScriptHandler(filename);
            }
            else
            {
                /*
                switch (filename.ToLower())
                {
                    case "chart":
                        {
                            handler = new ChartHandler();
                        }
                        break;
                }
                */
            }

            return handler;
        }

        #endregion
    }
}
