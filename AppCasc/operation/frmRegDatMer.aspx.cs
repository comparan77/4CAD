using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ModelCasc.operation;
using ModelCasc.operation.liverpool;

namespace AppCasc.operation
{
    public partial class frmRegDatMer : System.Web.UI.Page
    {
        protected void click_load(object sender, EventArgs args)
        {
            try
            {
                List<Entrada_liverpool> lst = EntradaCtrl.EntradaLiverpoolImport(txt_data.Text);
                var grp = from c in lst
                          group c by new
                          {
                              c.Proveedor,
                              c.Trafico,
                              c.Fecha_confirma
                          } into d
                          select new Entrada_liverpool()
                          {
                              Proveedor = d.Key.Proveedor,
                              Trafico = d.Key.Trafico,
                              Pedido = d.ToList().Count,
                              Piezas = d.ToList().Sum(p => p.Piezas),
                              Fecha_confirma = d.Key.Fecha_confirma
                          };

                grdProcesados.DataSource = grp;
                grdProcesados.DataBind();
            }
            catch (Exception e)
            {
                ((MstCasc)this.Master).setError = e.Message;
            }
        }

        protected void Page_Load(object sender, EventArgs args)
        {
            try
            {

            }
            catch (Exception e)
            {
                ((MstCasc)this.Master).setError = e.Message;
            }
        }
    }
}