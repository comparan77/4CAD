﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ModelCasc.catalog;

namespace AppCasc.catalog
{
    public partial class frmClienteLst : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs args)
        {
            if (!IsPostBack)
            {
                try
                {
                    ClienteMng oCMng = new ClienteMng();
                    oCMng.fillAllLst();
                    fillCatalog(oCMng.Lst);
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

                Cliente oC = new Cliente();
                oC.Id = Id;
                ClienteMng oCMng = new ClienteMng();

                oCMng.O_Cliente = oC;
                if (status)
                    oCMng.dlt();
                else
                    oCMng.reactive();

                oCMng = new ClienteMng();
                oCMng.fillAllLst();
                fillCatalog(oCMng.Lst);
            }
            catch (Exception e)
            {
                ((MstCasc)this.Master).setError = e.Message;
            }
        }
    }
}