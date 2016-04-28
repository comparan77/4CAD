using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ModelCasc.catalog;
using ModelCasc.operation;

namespace AppCasc
{
    public partial class _Default : System.Web.UI.Page
    {
        private class DiasSel
        {
            public DateTime daySel { get; set; }
            public string strDialSel
            {
                get
                {
                    return ModelCasc.CommonCasc.FormatDate(daySel, "dddd dd \\de MMMM \\de yyyy");
                }
            }
        }

        protected void rep_dayDataBound(object sender, RepeaterItemEventArgs args)
        {
            DateTime fecha = default(DateTime);

            try
            {
                if (args.Item.ItemType == ListItemType.AlternatingItem || args.Item.ItemType == ListItemType.Item)
                {
                    Repeater rep_dayItemCreated = args.Item.FindControl("rep_dayItemCreated") as Repeater;
                    HiddenField hf_DaySel = args.Item.FindControl("hf_DaySel") as HiddenField;
                    DateTime.TryParse(hf_DaySel.Value, out fecha);
                    List<Entrada_inventario> lst = EntradaCtrl.InventarioGetByFechaMaquila(fecha);
                    Repeater rep_OrdTrabajo = args.Item.FindControl("rep_OrdTrabajo") as Repeater;
                    rep_OrdTrabajo.DataSource = lst;
                    rep_OrdTrabajo.DataBind();
                }
            }
            catch (Exception e)
            {
                
                throw e;
            }
        }

        protected void calOrdTrabSelChanged(object sender, EventArgs args)
        {
            try
            {
                List<DiasSel> lstDaySel = new List<DiasSel>();
                foreach (DateTime day in calOrdTrabajo.SelectedDates)
                {
                    DiasSel o = new DiasSel() { daySel = day };
                    //if(o.daySel.DayOfWeek != DayOfWeek.Sunday)
                        lstDaySel.Add(o);
                }
                rep_day.DataSource = lstDaySel;
                rep_day.DataBind();
            }
            catch (Exception e)
            {
                throw e;
            }
        }
    }
}