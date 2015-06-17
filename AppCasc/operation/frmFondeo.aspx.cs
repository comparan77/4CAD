using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ModelCasc.webApp;
using System.Data;
using ModelCasc.exception;
using ModelCasc.operation;

namespace AppCasc.operation
{
    public partial class frmFondeo : System.Web.UI.Page
    {
        private void loadFirsTime()
        {
            try
            {
                ControlsMng.fillAduana(ddl_aduana);
            }
            catch
            {
                throw;
            }
        }

        private void cleanControls()
        {
            btn_importar.Visible = false;
            pnl_datosFondeo.Visible = true;
            lbl_NoFolios.Text = string.Empty;
            lbl_NoFoliosMsg.Text = string.Empty;
            lnkFileDup.Visible = false;
            ControlsMng.GridViewClean(grd_reviewFile);
        }

        /// <summary>
        /// Elimina los registros de la tabla fondeo_paso del usuario, regularmente se ocupará cuando no existan códigos o vendors
        /// </summary>
        private void fondeoPasoDlt()
        {
            try
            {
                EntradaCtrl.FondeoPasoDltByUsuario(((MstCasc)this.Master).getUsrLoged().Id);
            }
            catch
            {
                throw;
            }
        }

        private void importFondeo()
        {
            try
            {
                string folioFondeo = string.Empty;
                int rowAffected = EntradaCtrl.FondeoInsertData(ref folioFondeo, ((MstCasc)this.Master).getUsrLoged().Id);
                lbl_NoFolios.Text = "No de Partidas Agregadas: " + rowAffected.ToString() + "; Folio: " + folioFondeo;
            }
            catch
            {
                throw;
            }
        }

        private void validaVendors()
        {
            try
            {
                ControlsMng.GridViewClean(grd_reviewFile);
                List<Entrada_fondeo> lst = EntradaCtrl.FondeoValidaVendors();
                if (lst.Count > 0)
                {
                    fondeoPasoDlt();
                    lbl_NoFoliosMsg.Text = "Partidas con vendors no existentes en el catálogo: " + lst.Count.ToString();
                    grd_reviewFile.AutoGenerateColumns = true;
                    grd_reviewFile.DataSource = lst;
                    grd_reviewFile.DataBind();
                }
                else
                    importFondeo();
            }
            catch
            {
                throw;
            }
        }

        private void validaCodigos()
        {
            try
            {
                ControlsMng.GridViewClean(grd_reviewFile);
                List<Entrada_fondeo> lst = EntradaCtrl.FondeoValidaCodigos();
                if (lst.Count > 0)
                {
                    fondeoPasoDlt();
                    lbl_NoFoliosMsg.Text = "Partidas con codigos no existentes en el catálogo: " + lst.Count.ToString();
                    grd_reviewFile.AutoGenerateColumns = true;
                    grd_reviewFile.DataSource = lst;
                    grd_reviewFile.DataBind();
                }
                else
                    validaVendors();
            }
            catch
            {
                throw;
            }
        }

        private void importFondeoPaso(DataTable dt)
        {
            try
            {
                List<Entrada_fondeo> lst = EntradaCtrl.FondeoFillFromDT(dt);
                string folioFondeo = string.Empty;
                int rowAffected = EntradaCtrl.FondeoPasoInsertData(lst,((MstCasc)this.Master).getUsrLoged().Id);
                //if (EntradaCtrl.FondeoExisteEntradaPrevia())
                //{
                //    throw new Exception("El pedimento proporcionado ya tiene una entrada, deberá ser capturado manualmente por el área de sistemas");
                //}
                //else 
                validaCodigos();
            }
            catch (Exception)
            {
                throw;
            }
        }

        private void showErrorsInFile(DataTable dt)
        {
            int numErr = 0;
            try
            {
                ControlsMng.GridViewClean(grd_reviewFile);
                lbl_NoFoliosMsg.Text = string.Empty;
                DataTable dtErrInFile = EntradaCtrl.FondeoGetInsideErr(dt);
                numErr = dtErrInFile.Rows.Count;
                if (numErr > 0)
                {
                    ControlsMng.GridViewfillNoHtmlEncode(dtErrInFile, grd_reviewFile);
                    throw new ImportException();
                }

                importFondeoPaso(dt);
            }
            catch (ImportException)
            {
                lbl_NoFoliosMsg.Text = "Folios con Error: " + numErr.ToString();
            }
            catch (Exception)
            {
                throw;
            }
        }

        protected void click_btn_processFile(object sender, EventArgs args)
        {
            try
            {
                DataTable dtReviewFile = new DataTable();

                dtReviewFile = EntradaCtrl.FondeoUpLoadData(hf_path.Value, Convert.ToDateTime(txt_fecha_fact.Text), ddl_importador.SelectedItem.Text, ddl_aduana.SelectedValue);

                ControlsMng.GridViewClean(grd_reviewFile);

                lbl_NoFolios.Text = "No Partidas en el Archivo: " + dtReviewFile.Compute("COUNT(No)", "");
                int FoliosError = Convert.ToInt32(dtReviewFile.Compute("COUNT(No)", "HasError=true"));
                lbl_NoFoliosMsg.Text = "No Partidas con Error: " + FoliosError.ToString();

                showErrorsInFile(dtReviewFile);
            }
            catch (Exception e)
            {
                ((MstCasc)this.Master).setError = e.Message;
            }
            finally
            {
                btn_importar.Visible = true;
                pnl_datosFondeo.Visible = false;
            }
        }

        protected void click_btn_importar(object sender, EventArgs args)
        {
            try
            {
                if (fu_Folios.HasFile)
                {
                    string tempPath = System.IO.Path.GetTempFileName();
                    string[] arrFileName = fu_Folios.FileName.Split('.');
                    tempPath = tempPath.Replace(".tmp", "." + arrFileName[arrFileName.Length - 1]);
                    tempPath = tempPath.ToLower();
                    hf_path.Value = tempPath;
                    fu_Folios.SaveAs(tempPath);
                    cleanControls();
                }
                else
                    throw new Exception("El archivo no es válido");
            }
            catch (Exception e)
            {
                ((MstCasc)this.Master).setError = e.Message;
            }
        }

        protected void btn_buscar_click(object sender, EventArgs args)
        {
            try
            {
                List<Entrada_fondeo> lst = EntradaCtrl.FondeoGetByReferencia(txt_dato.Text.Trim(), false);
                grd_fondeo.DataSource = lst;
                grd_fondeo.DataBind();
            }
            catch (Exception e)
            {
                ((MstCasc)this.Master).setError = e.Message;
            }
        }

        protected void Page_load(object sender, EventArgs args)
        {
            try
            {
                hf_MaxRequestLen.Value = ModelCasc.CommonCasc.GetMaxRequestLength().ToString();
                if(!IsPostBack)
                    loadFirsTime();
            }
            catch (Exception e)
            {
                ((MstCasc)this.Master).setError = e.Message;
            }
        }
    }
}