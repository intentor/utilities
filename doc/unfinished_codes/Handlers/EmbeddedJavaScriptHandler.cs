/****************************************
Intentor.Utilities
*****************************************
Copyright © 2010 André "Intentor" Martins
http://intentor.com.br/
****************************************/

using System;
using System.IO;
using System.Web;
using System.Web.Caching;
using System.Reflection;
using Intentor.Utilities;

namespace Intentor.Utilities.Handlers
{
    /// <summary>
    /// Classe EmbeddedJavaScriptHandler.
    /// </summary>
    public class EmbeddedJavaScriptHandler :
        IHttpHandler
    {
        private string _fileName;

        public EmbeddedJavaScriptHandler(string fileName)
        {
            _fileName = fileName;
        }

        /// <summary>
        /// Processa a requisição HTTP.
        /// </summary>
        /// <param name="ctx">Contexto da requisição atual.</param>
        public void ProcessRequest(HttpContext ctx)
        {
			ctx.Response.AddHeader("Content-Type", "application/x-javascript");
			ctx.Response.ContentEncoding = System.Text.Encoding.UTF8;
			ctx.Response.Cache.SetCacheability(System.Web.HttpCacheability.Public);

			if (_fileName != null && _fileName.Length > 0)
			{
				Assembly assembly = Assembly.GetExecutingAssembly();
				Stream s;

				s = assembly.GetManifestResourceStream(String.Concat(AppConstants.AssemblyName, ".", _fileName, ".js"));

                if (s != null)
                {
                    System.IO.StreamReader sr = new System.IO.StreamReader(s);
                    ctx.Response.Write(sr.ReadToEnd());
                    sr.Close();
                }
                else
                {
                    ctx.Response.StatusCode = 404;
                }
			}
        }

        public bool IsReusable
        {
            get { return true; }
        }
    }
}
