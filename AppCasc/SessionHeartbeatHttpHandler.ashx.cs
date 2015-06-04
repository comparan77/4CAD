using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.SessionState;

namespace AppCasc
{
    /// <summary>
    /// Summary description for SessionHeartbeatHttpHandler
    /// </summary>
    public class SessionHeartbeatHttpHandler : IHttpHandler, IRequiresSessionState
    {

        public bool IsReusable { get { return false; } }

        public void ProcessRequest(HttpContext context)
        {
            context.Session["Heartbeat"] = DateTime.Now;
            HttpResponse response = context.Response;
            //response.Write(DateTime.Now.ToString("hh:mm tt"));
            //HttpResponse response = context.Response;
            //response.Write("<html><body><h1>Wow.. We created our first handler");
            //response.Write("</h1></body></html>");
        }
    }
}