using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AppCasc.handlers
{
    /// <summary>
    /// Summary description for AccesoPersonal
    /// </summary>
    public class AccesoPersonal : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "text/plain";
            context.Response.Write("Hello World");
        }

        public bool IsReusable
        {
            get
            {
                return false;
            }
        }
    }
}