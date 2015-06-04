﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ModelCasc.catalog;

namespace AppCasc.catalog
{
    public partial class frmTipoCargaLst : System.Web.UI.Page
    {
        private void fillCatalog(object datasource)
        {
            repRows.DataSource = datasource;
            repRows.DataBind();
        }

        protected void Page_Load(object sender, EventArgs args)
        {
            if (!IsPostBack)
            {
                try
                {
                    fillCatalog(CatalogCtrl.Tipo_cargafillEvenInactive());
                }
                catch (Exception e)
                {
                    ((MstCasc)this.Master).setError = e.Message;
                }
            }
        }

        protected void lnk_change_status_click(object sender, CommandEventArgs args)
        {
            try
            {
                int Id = 0;
                int.TryParse(args.CommandName, out Id);
                bool status = false;
                bool.TryParse(args.CommandArgument.ToString(), out status);

                Tipo_carga o = new Tipo_carga();
                o.Id = Id;
                CatalogCtrl.Tipo_cargaChangeStatus(o, status);
                fillCatalog(CatalogCtrl.Tipo_cargafillEvenInactive());
            }
            catch (Exception e)
            {
                ((MstCasc)this.Master).setError = e.Message;
            }
        }
    }
}