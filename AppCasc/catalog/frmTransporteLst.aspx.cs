using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ModelCasc.catalog;

namespace AppCasc.catalog
{
    public partial class frmTransporteLst : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs args)
        {
            if (!IsPostBack)
            {
                try
                {
                    TransporteMng oTMng = new TransporteMng();
                    oTMng.fillAllLst();
                    fillCatalog(oTMng.Lst);
                }
                catch (Exception e)
                {
                    ((MstCasc)this.Master).setError = e.Message;
                }
            }
        }

        private void fillCatalog(object datasource)
        {
            repRows.DataSource = datasource;
            repRows.DataBind();
        }

        protected void lnk_change_status_click(object sender, CommandEventArgs args)
        {
            try
            {
                int Id = 0;
                int.TryParse(args.CommandName, out Id);
                bool status = false;
                bool.TryParse(args.CommandArgument.ToString(), out status);

                Transporte oT = new Transporte();
                oT.Id = Id;

                //Valida que no existe en la tabla Transporte_tipo_transporte
                Transporte_tipo_transporte oTTT = new Transporte_tipo_transporte();
                oTTT.Id_transporte = oT.Id;
                Transporte_tipo_transporteMng oTTTMng = new Transporte_tipo_transporteMng();
                oTTTMng.O_Transporte_tipo_transporte = oTTT;
                oTTTMng.selByIdTransporte();
                if (oTTT.Id > 0)
                    throw new Exception("Es necesario eliminar este tipo de trasnporte en la relacion de Transportes-Tipos");

                TransporteMng oTMng = new TransporteMng();
                oTMng.O_Transporte = oT;

                if (status)
                    oTMng.dlt();
                else
                    oTMng.reactive();

                oTMng = new TransporteMng();
                oTMng.fillAllLst();
                fillCatalog(oTMng.Lst);
            }
            catch (Exception e)
            {
                ((MstCasc)this.Master).setError = e.Message;
            }
        }
    }
}