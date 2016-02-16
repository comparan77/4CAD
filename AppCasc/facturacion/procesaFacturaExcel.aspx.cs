using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;
using System.Diagnostics;

namespace AppCasc.facturacion
{
    public partial class procesaFacturaExcel : System.Web.UI.Page
    {
        #region factura
        protected void click_btn_importar(object sender, EventArgs args)
        {
            try
            {
                lnkFile.Text = string.Empty;
                lnkFile.NavigateUrl = string.Empty;
                if (fileup_facturacion.HasFile)
                {
                    string tempPath = System.IO.Path.GetTempFileName();
                    string[] arrFileName = fileup_facturacion.FileName.Split('.');
                    tempPath = tempPath.Replace(".tmp", "." + arrFileName[arrFileName.Length - 1]);
                    tempPath = tempPath.ToLower();
                    hf_path.Value = tempPath;
                    fileup_facturacion.SaveAs(tempPath);
                    btn_processFile.Visible = true;
                }
                else
                    throw new Exception("El archivo no es válido");
            }
            catch (Exception e)
            {
                ((MstCasc)this.Master).setError = e.Message;
            }
        }

        protected void click_btn_processFile(object sender, EventArgs args)
        {
            try
            {
                Process app = new Process();
                app.StartInfo.FileName = System.Configuration.ConfigurationManager.AppSettings["ConsolePath"].ToString();
                string result = Path.GetTempFileName().Replace("\\", "/").Replace(".tmp", ".xlsx");
                app.StartInfo.Arguments = hf_path.Value.Replace("\\", "/") + " " + result;

                app.Start();
                bool creado = false;
                int timeToBuild = 0;
                while (!creado)
                {
                    System.Threading.Thread.Sleep(10000);
                    creado = File.Exists(result);

                    if (creado)
                    {
                        string nameFile = result.Split('/').Last().ToString();
                        string pathDirectory = HttpContext.Current.Server.MapPath("~/rpt/facturaAvon/") + nameFile;
                        File.Move(result, pathDirectory);
                        lnkFile.NavigateUrl = "~/rpt/facturaAvon/" + nameFile;
                        lnkFile.Text = "Descargar archivo aqui";
                    }

                    timeToBuild++;
                    if (timeToBuild == 12)
                    {
                        lnkFile.Text = "El archivo no pudo ser creado...!";
                        creado = true;
                    }
                }
            }
            catch (Exception e)
            {
                ((MstCasc)this.Master).setError = e.Message;
            }
            finally
            {
                btn_importar.Visible = true;
                btn_processFile.Visible = false;
            }
        }
        #endregion

        protected void Page_load(object sender, EventArgs args)
        {
            try
            {
                hf_MaxRequestLen.Value = ModelCasc.CommonCasc.GetMaxRequestLength().ToString();
            }
            catch (Exception e)
            {
                ((MstCasc)this.Master).setError = e.Message;
            }
        }
    }
}