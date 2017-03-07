using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Text;
using Newtonsoft.Json;
using ModelCasc.catalog;
using ModelCasc.operation;
using System.IO;

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
                    case "usuario":
                        response = security(context);
                        break;
                    case "entrada":
                        response = entrada(context);
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
            //string email = context.Request["email"].ToString();
            //string pass = context.Request["pass"].ToString();
            switch (option)
            {
                case "CredencialesValidas":
                    jsonData = new StreamReader(context.Request.InputStream).ReadToEnd();
                    Usuario o = JsonConvert.DeserializeObject<Usuario>(jsonData);
                    response = JsonConvert.SerializeObject(CatalogCtrl.UsuarioCredencialesValidas(o.Email, o.Contrasenia));
                    break;
            }

            return response;
        }

        private string entrada(HttpContext context)
        {
            string response = string.Empty;
            string referencia = string.Empty;
            string option = context.Request["opt"].ToString();
            //string referencia = context.Request["referencia"].ToString();
            switch (option)
            {
                case "precargaGetByRef":
                    jsonData = new StreamReader(context.Request.InputStream).ReadToEnd();
                    referencia = jsonData.ToString();
                    response = JsonConvert.SerializeObject(EntradaCtrl.EntradaPreCargaGetByRef(referencia));
                    break;
                case "AudUniAdd":
                    jsonData = new StreamReader(context.Request.InputStream).ReadToEnd();
                    Entrada_aud_uni o = JsonConvert.DeserializeObject<Entrada_aud_uni>(jsonData);
                    EntradaCtrl.EntradaAudUniAdd(o);
                    response = JsonConvert.SerializeObject("Se guardo el registro correctamente");
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