using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ModelCasc.catalog.personal;
using ModelCasc.webApp;
using System.Drawing;
using System.IO;

namespace AppCasc.personal
{
    public partial class frmPersonal : System.Web.UI.Page
    {
        private void RevisaQR()
        {
            try
            {
                int numero = 0;
                string folioAct = string.Empty;

                if (int.TryParse(hfId.Value, out numero))
                {
                    Personal_qr pqr = PersonalCtrl.PersonalQRGetByIdPersona(numero);
                    if (pqr != null)
                        folioAct = pqr.Idf;
                }

                string folioNew = PersonalCtrl.PersonalQrPivoteGetFolio(((MstCasc)this.Master).getUsrLoged().Id);
                if (folioNew.Length > 0)
                {
                    folioAct = folioNew;
                    lbl_qr.ForeColor = Color.Green;
                }

                lbl_qr.Text = folioAct;
            }
            catch
            {
                throw;
            }
        }

        private void fillDdl()
        {
            ControlsMng.fillPersonalEmpresa(ddl_empresa);
            ControlsMng.fillPersonalRol(ddl_rol);

            //img_photo.ImageUrl
        }

        private void fillForm()
        {
            int Id = 0;
            int.TryParse(hfId.Value, out Id);

            try
            {
                Personal o = PersonalCtrl.PersonalGet(Id);
                hfId.Value = o.Id.ToString();
                txt_nombre.Text = o.Nombre;
                txt_paterno.Text = o.Paterno;
                txt_materno.Text = o.Materno;
                txt_rfc.Text = o.Rfc;
                txt_curp.Text = o.Curp;
                txt_nss.Text = o.Nss;

                ddl_genero.SelectedValue = (o.Genero ? "1" : "0");
                ddl_empresa.SelectedValue = o.Id_personal_empresa.ToString();
                ddl_rol.SelectedValue = o.Id_personal_rol.ToString();

                chk_boletinado.Checked = o.Boletinado;

                Personal_archivos foto = o.lstArchivos.Find(p => p.Id_archivo_tipo == 1);
                if (foto != null)
                {
                    //img_photo.ImageUrl = "~/rpt/" + o.PQr.Idf +"/Foto.jpg";
                    img_photo.ImageUrl = "~/rpt/personal/" + o.PQr.Idf + "/Foto.jpg?" + DateTime.Now.ToString("hhmmssffffff");
                }

                RevisaQR();
            }
            catch
            {
                throw;
            }
        }

        private Personal getFormValues()
        {
            Personal o = new Personal();
            int entero = 0;

            int.TryParse(hfId.Value, out entero);
            o.Id = entero;
            entero = 0;

            o.Nombre = txt_nombre.Text.Trim();
            o.Paterno = txt_paterno.Text.Trim();
            o.Materno = txt_materno.Text.Trim();

            o.Rfc = txt_rfc.Text.Trim();
            o.Curp = txt_curp.Text.Trim();
            o.Nss = txt_nss.Text.Trim();

            o.Genero = (string.Compare(ddl_genero.SelectedValue, "1") == 0 ? true : false);
            
            int.TryParse(ddl_empresa.SelectedValue, out entero);
            o.Id_personal_empresa = entero;
            entero = 0;

            int.TryParse(ddl_rol.SelectedValue, out entero);
            o.Id_personal_rol = entero;
            entero = 0;

            o.Boletinado = chk_boletinado.Checked;
            
            o.lstArchivos = new List<Personal_archivos>();
            if (img_photo.ImageUrl.Length>0)
            {
                Personal_archivos oPA = new Personal_archivos();
                oPA.Id_archivo_tipo = 1;

                string imgFotoUrl = img_photo.ImageUrl;
                if (img_photo.ImageUrl.IndexOf("?") > 0)
                    imgFotoUrl = imgFotoUrl.Split('?')[0];
                FileStream fs = File.OpenRead(HttpContext.Current.Server.MapPath(imgFotoUrl));
                fs.Position = 0;
                oPA.stream = new MemoryStream();
                fs.CopyTo(oPA.stream);
                oPA.Ruta = HttpContext.Current.Server.MapPath("~/rpt/personal/");
                o.lstArchivos.Add(oPA);
            }

            o.PQr = new Personal_qr() { Idf = lbl_qr.Text };
            o.RutaFiles = HttpContext.Current.Server.MapPath("~/rpt/personal/");
            return o;
        }

        private void istPersonal(Personal o)
        {
            try
            {
                PersonalCtrl.PersonalAdd(o, ((MstCasc)this.Master).getUsrLoged().Id);
            }
            catch
            {
                throw;
            }
        }

        private void udtPersonal(Personal o)
        {
            try
            {
                PersonalCtrl.PersonalUdt(o, ((MstCasc)this.Master).getUsrLoged().Id);
            }
            catch
            {
                throw;
            }

        }

        protected void Page_Load(object sender, EventArgs args)
        {
            try
            {
                if (!IsPostBack)
                {
                    fillDdl();
                    PersonalCtrl.PersonalTempDataByUser(((MstCasc)this.Master).getUsrLoged().Id);
                    hfAction.Value = Request["Action"];
                    switch (hfAction.Value)
                    {
                        case "Udt":
                            hfId.Value = Request["Key"];
                            fillForm();
                            break;
                        case "Ist":
                            break;
                        default:
                            Response.Redirect("frmAduanaLst.aspx");
                            break;
                    }
                }
            }
            catch (Exception e)
            {
                ((MstCasc)this.Master).setError = e.Message;
            }
        }

        protected void btnSave_click(object sender, EventArgs args)
        {
            try
            {
                switch (hfAction.Value)
                {
                    case "Udt":
                        udtPersonal(getFormValues());
                        break;
                    case "Ist":
                        istPersonal(getFormValues());
                        break;
                }
                ClientScript.RegisterStartupScript(this.GetType(), "alertSave", "<script type=\"text/javascript\">alert('Se guardo correctamente el registro');window.location.href='frmPersonalLst.aspx'</script>");
            }
            catch (Exception e)
            {
                ((MstCasc)this.Master).setError = e.Message;
            }
        }

        protected void btnCancel_click(object sender, EventArgs args)
        { Response.Redirect("frmPersonalLst.aspx"); }

        protected void udtQr_click(object sender, EventArgs args)
        {
            try
            {
                RevisaQR();
            }
            catch (Exception e)
            {
                ((MstCasc)this.Master).setError = e.Message;
            }
        }

        protected void udtFoto_click(object sender, EventArgs args)
        {
            try
            {
                Personal_foto o = PersonalCtrl.PersonalFotoUdt(((MstCasc)this.Master).getUsrLoged().Id);
                if (o != null)
                {
                    img_photo.ImageUrl = "~/rpt/personal/PERFOTO/" + o.Foto + ".jpg";
                }
            }
            catch (Exception e)
            {
                ((MstCasc)this.Master).setError = e.Message;
            }
        }

    }
}