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
        protected int bodegaCantidad = 0;
        protected int cortinaCantidad = 0;
        protected int clienteCantidad = 0;
        protected int mercanciaCantidad = 0;
        protected int servicioCantidad = 0;

        protected void Page_Load(object sender, EventArgs args)
        {
            try
            {
                Bodega oB = new Bodega();
                bodegaCantidad = CatalogoCtrl.catalogGetAllLst(oB).Cast<Bodega>().ToList().FindAll(p => p.IsActive == true).Count();
                Cortina oCor = new Cortina();
                cortinaCantidad = CatalogoCtrl.catalogGetAllLst(oCor).Cast<Cortina>().ToList().FindAll(p => p.IsActive == true).Count();
                Cliente oC = new Cliente();
                clienteCantidad = CatalogoCtrl.catalogGetAllLst(oC).Cast<Cliente>().ToList().FindAll(p => p.IsActive == true).Count();
                Mercancia oM = new Mercancia();
                mercanciaCantidad = CatalogoCtrl.catalogGetLst(oM).Cast<Mercancia>().ToList().Count();
                Servicio oS = new Servicio();
                servicioCantidad = CatalogoCtrl.catalogGetLst(oS).Cast<Servicio>().ToList().Count();
            }
            catch
            {
                throw;
            }
        }
    }
}