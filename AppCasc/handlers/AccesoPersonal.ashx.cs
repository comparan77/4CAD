using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Text;
using Newtonsoft.Json;
using System.IO;
using ModelCasc.catalog.personal;

namespace AppCasc.handlers
{
    /// <summary>
    /// Summary description for AccesoPersonal
    /// </summary>
    public class AccesoPersonal : IHttpHandler
    {
        string response = string.Empty;
        string referencia = string.Empty;
        string jsonData = string.Empty;

        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "application/json";
            context.Response.ContentEncoding = Encoding.UTF8;
            string operation = context.Request["op"].ToString();
            try
            {
                switch (operation)
                {
                    case "personal":
                        response = personal(context);
                        break;
                    default:
                        throw new Exception("La opción " + operation + " no existe");
                }
                context.Response.Write(response);
            }
            catch (Exception e)
            {
                context.Response.Write(JsonConvert.SerializeObject(e.Message));
            }

        }

        private string personal(HttpContext context)
        {
            string response = string.Empty;
            string option = context.Request["opt"].ToString();
            //string email = context.Request["email"].ToString();
            //string pass = context.Request["pass"].ToString();
            int id_bodega = 0;
            switch (option)
            {
                case "Registro":
                    jsonData = new StreamReader(context.Request.InputStream).ReadToEnd();
                    Personal o = JsonConvert.DeserializeObject<Personal>(jsonData);
                    PersonalCtrl.PersonalRegistro(o);
                    response = JsonConvert.SerializeObject(o);
                    break;
                case "UltimoRegistroPorBodega":
                    id_bodega = Convert.ToInt32(context.Request["id_bodega"].ToString());
                    response = JsonConvert.SerializeObject(PersonalCtrl.PersonalUltimoRegistroPorBodega(id_bodega));
                    break;
                default:
                    throw new Exception("La opción " + option + " no existe");
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