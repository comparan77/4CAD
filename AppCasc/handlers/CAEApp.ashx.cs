using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Text;
using Newtonsoft.Json;
using ModelCasc.catalog;

namespace AppCasc.handlers
{
    /// <summary>
    /// Summary description for CAEApp
    /// </summary>
    public class CAEApp : IHttpHandler
    {
        string response = string.Empty;
        string referencia = string.Empty;
        string jsonData = string.Empty;
        int id = 0;

        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "application/json";
            context.Response.ContentEncoding = Encoding.UTF8;
            string operation = context.Request["op"].ToString();

            try
            {
                switch (operation)
                {
                    case "security":
                        response = security(context);
                        break;
                    default:
                        break;
                }
                context.Response.Write(response);
            }
            catch (Exception e)
            {
                context.Response.Write(JsonConvert.SerializeObject(e.Message));
            }
        }

        private string security(HttpContext context)
        {
            string response = string.Empty;
            string option = context.Request["opt"].ToString();
            string email = context.Request["email"].ToString();
            string pass = context.Request["pass"].ToString();
            switch (option)
            {
                case "login":
                    response = JsonConvert.SerializeObject(CatalogCtrl.UsuarioCredencialesValidas(email, pass));
                    break;
            }

            return response;
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