using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using logisticaModel.catalog;
using logisticaModel.controller.catalog;

namespace logistica.pages.catalog
{
    public partial class frmCatalogos : System.Web.UI.Page
    {
        //protected int bodegaCantidad = 0;
        //protected int cortinaCantidad = 0;
        //protected int clienteCantidad = 0;
        //protected int mercanciaCantidad = 0;
        //protected int servicioCantidad = 0;
        //protected int destinatarioCantidad = 0;
        //protected int transportistaCantidad = 0;
        //protected int vendorCantidad = 0;
        //protected int bodegaZonaCantidad = 0;

        protected void Page_Load(object sender, EventArgs args)
        {
            try
            {
                //bodegaCantidad = CatalogoCtrl.catalogGetAllLst(new Bodega()).Cast<Bodega>().ToList().FindAll(p => p.IsActive == true).Count();
                //cortinaCantidad = CatalogoCtrl.catalogGetAllLst(new Cortina()).Cast<Cortina>().ToList().FindAll(p => p.IsActive == true).Count();
                //clienteCantidad = CatalogoCtrl.catalogGetAllLst(new Cliente()).Cast<Cliente>().ToList().FindAll(p => p.IsActive == true).Count();
                //mercanciaCantidad = CatalogoCtrl.catalogGetLst(new Mercancia()).Cast<Mercancia>().ToList().Count();
                //servicioCantidad = CatalogoCtrl.catalogGetLst(new Servicio()).Cast<Servicio>().ToList().Count();
                //destinatarioCantidad = CatalogoCtrl.catalogGetLst(new Destinatario()).Cast<Destinatario>().ToList().FindAll(p => p.IsActive == true).Count();
                //transportistaCantidad = CatalogoCtrl.catalogGetLst(new Transporte()).Cast<Transporte>().ToList().FindAll(p => p.IsActive == true).Count();
                //vendorCantidad = CatalogoCtrl.catalogGetLst(new Vendor()).Cast<Vendor>().ToList().FindAll(p => p.IsActive == true).Count();
            }
            catch
            {
                throw;
            }
        }
    }
}