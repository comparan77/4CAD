using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ModelCasc.catalog;
using System.Data;
using ModelCasc.catalog.almacen;

namespace ModelCasc.webApp
{
    public class ControlsMng
    {
        public static void setEnabledControls(bool IsActive, WebControl[] controls)
        {
            foreach (WebControl ctrl in controls)
            {
                ctrl.Enabled = IsActive;
            }
        }

        public static void GridViewClean(GridView grdView)
        {
            grdView.DataSource = null;
            grdView.DataBind();
        }
        public static void GridViewfillNoHtmlEncode(DataTable dt, GridView grdView)
        {
            try
            {

                foreach (DataColumn col in dt.Columns)
                {
                    BoundField bfield = new BoundField();

                    //Initalize the DataField value.
                    bfield.DataField = col.ColumnName;
                    //Initialize the HeaderText field value.
                    bfield.HeaderText = col.ColumnName;
                    bfield.HtmlEncode = false;
                    grdView.Columns.Add(bfield);
                }

                grdView.DataSource = dt;
                grdView.DataBind();
            }
            catch (Exception)
            {

                throw;
            }
        }
        public static void fillAduana(DropDownList ddlBodega)
        {
            ddlBodega.DataSource = CatalogCtrl.Aduanafill(); ;
            ddlBodega.DataTextField = "nombre";
            ddlBodega.DataValueField = "codigo";
            ddlBodega.DataBind();
        }
        public static void fillBodegaByUser(DropDownList ddlBodega, int id_usuario)
        {
            BodegaMng oMng = new BodegaMng();
            oMng.fillLstByUsuario(id_usuario);
            ddlBodega.DataSource = oMng.Lst;
            ddlBodega.DataTextField = "nombre";
            ddlBodega.DataValueField = "id";
            ddlBodega.DataBind();
        }
        public static void fillBodega(DropDownList ddlBodega)
        {
            BodegaMng oMng = new BodegaMng();
            oMng.fillLst();
            ddlBodega.DataSource = oMng.Lst;
            ddlBodega.DataTextField = "nombre";
            ddlBodega.DataValueField = "id";
            ddlBodega.DataBind();
        }
        public static void fillCortinaByBodega(DropDownList ddlCortina, int Id_bodega)
        {
            CortinaMng oMng = new CortinaMng();
            Cortina o = new Cortina();
            o.Id_bodega = Id_bodega;
            oMng.O_Cortina = o;
            oMng.selByIdBodega();
            ddlCortina.DataSource = oMng.Lst;
            ddlCortina.DataTextField = "nombre";
            ddlCortina.DataValueField = "id";
            ddlCortina.DataBind();
        }
        public static void fillCliente(DropDownList ddlCliente)
        {
            ClienteMng oMng = new ClienteMng();
            oMng.fillLst();
            ddlCliente.Items.Clear();
            foreach (Cliente item in oMng.Lst)
            {
                ListItem li = new ListItem(item.Nombre, item.Id.ToString());
                li.Attributes.Add("mask", item.Mascara);
                li.Attributes.Add("fondeo", (item.Id == Globals.AVON_FONDEO).ToString());
                li.Attributes.Add("documento", item.Documento);
                ddlCliente.Items.Add(li);
            }
        }
        public static List<Documento> fillDocumento(DropDownList ddlDocumento)
        {
            DocumentoMng oMng = new DocumentoMng();
            oMng.fillLst();
            ddlDocumento.Items.Clear();

            foreach (Documento item in oMng.Lst)
            {
                ListItem li = new ListItem(item.Nombre, item.Id.ToString());
                li.Attributes.Add("mask", item.Mascara);
                ddlDocumento.Items.Add(li);
            }
            return oMng.Lst;
        }
        public static List<Cliente_grupo> fillClienteGrupo(DropDownList ddlDocumento)
        {
            Cliente_grupoMng oMng = new Cliente_grupoMng();
            oMng.fillLst();
            ddlDocumento.Items.Clear();
            ddlDocumento.DataSource = oMng.Lst;
            ddlDocumento.DataTextField = "nombre";
            ddlDocumento.DataValueField = "id";
            ddlDocumento.DataBind();
            return oMng.Lst;
        }
        public static void fillTransporte(DropDownList ddlTransporte)
        {
            TransporteMng oMng = new TransporteMng();
            oMng.fillLstInTipoTransporte();
            ddlTransporte.Items.Clear();
            ddlTransporte.DataSource = oMng.Lst;
            ddlTransporte.DataTextField = "nombre";
            ddlTransporte.DataValueField = "id";
            ddlTransporte.DataBind();
        }
        public static void fillTipoTransporte(DropDownList ddlTipo_Transporte)
        {
            try
            {
                Transporte_tipoMng oMng = new Transporte_tipoMng();
                oMng.fillLst();
                ddlTipo_Transporte.Items.Clear();
                foreach (Transporte_tipo item in oMng.Lst)
                {
                    ListItem li = new ListItem(item.Nombre, item.Id.ToString());
                    li.Attributes.Add("placa", item.Requiere_placa.ToString());
                    li.Attributes.Add("caja", item.Requiere_caja.ToString());
                    li.Attributes.Add("caja1", item.Requiere_caja1.ToString());
                    li.Attributes.Add("caja2", item.Requiere_caja2.ToString());
                    ddlTipo_Transporte.Items.Add(li);
                }
                //ddlTipo_Transporte.DataSource = oMng.Lst;
                //ddlTipo_Transporte.DataTextField = "nombre";
                //ddlTipo_Transporte.DataValueField = "id";
                //ddlTipo_Transporte.DataBind();
            }
            catch
            {
                throw;
            }
        }
        public static void fillTipoTransporte(DropDownList ddlTipo_Transporte, DropDownList ddlTransporte)
        {
            try
            {
                Transporte_tipoMng oMng = new Transporte_tipoMng();
                Transporte_tipo o = new Transporte_tipo();
                ddlTipo_Transporte.Items.Clear();
                if (ddlTransporte.Items.Count > 0)
                {
                    o.Id = Convert.ToInt32(ddlTransporte.SelectedValue);
                    oMng.O_Transporte_tipo = o;
                    oMng.getByIdTransporte();
                    ddlTipo_Transporte.Items.Clear();
                    ddlTipo_Transporte.DataSource = oMng.Lst;
                    ddlTipo_Transporte.DataTextField = "nombre";
                    ddlTipo_Transporte.DataValueField = "id";
                    ddlTipo_Transporte.DataBind();
                }
            }
            catch
            {
                throw;
            }
        }
        public static void fillCustodia(DropDownList ddlCustodia)
        {
            CustodiaMng oMng = new CustodiaMng();
            oMng.fillLst();
            ddlCustodia.DataSource = oMng.Lst;
            ddlCustodia.DataTextField = "nombre";
            ddlCustodia.DataValueField = "id";
            ddlCustodia.DataBind();
        }
        public static void fillCuentaTipo(DropDownList ddlCuentaTipo)
        {
            Cuenta_tipoMng oCTMng = new Cuenta_tipoMng();
            oCTMng.fillLst();
            ddlCuentaTipo.DataSource = oCTMng.Lst;
            ddlCuentaTipo.DataTextField = "nombre";
            ddlCuentaTipo.DataValueField = "id";
            ddlCuentaTipo.DataBind();
        }
        //public void fillVigilanteByBodega(int Id_bodega)
        //{
        //    VigilanteMng oMng = new VigilanteMng();
        //    Vigilante o = new Vigilante();
        //    o.Id_bodega = Id_bodega;
        //    oMng.O_Vigilante = o;
        //    oMng.fillLstByBodega();
        //    ddlVigilante.DataSource = oMng.Lst;
        //    ddlVigilante.DataTextField = "nombre";
        //    ddlVigilante.DataValueField = "id";
        //    ddlVigilante.DataBind();
        //}

        public static void fillSalidaDestino(DropDownList ddlDestino)
        {
            Salida_destinoMng OSDMng = new Salida_destinoMng();
            OSDMng.fillLst();
            foreach(Salida_destino item in OSDMng.Lst)
            { 
                ListItem li = new ListItem(item.Destino, item.Id.ToString());
                li.Attributes.Add("direccion", item.Direccion);
                ddlDestino.Items.Add(li);
            }
            //ddlDestino.DataSource = OSDMng.Lst;
            //ddlDestino.DataTextField = "destino";
            //ddlDestino.DataValueField = "id";
            //ddlDestino.Attributes.Add("direccion",
            //ddlDestino.DataBind();
        }

        public static void fillClienteByGrupo(DropDownList ddlCliente, int idGrupo)
        {
            ClienteMng oMng = new ClienteMng();
            oMng.fillLst();
            List<Cliente> lstByGrupo = oMng.Lst.FindAll(p => p.Id_cliente_grupo == idGrupo);
            ddlCliente.DataSource = lstByGrupo;
            ddlCliente.DataTextField = "nombre";
            ddlCliente.DataValueField = "id";
            ddlCliente.DataBind();
        }

        public static void fillUsuarioActivo(DropDownList ddlUsuario)
        {

        }
        public static void fillTipoCarga(DropDownList ddlTipoCarga)
        {
            Tipo_cargaMng oMng = new Tipo_cargaMng();
            oMng.fillLst();
            ddlTipoCarga.DataSource = oMng.Lst;
            ddlTipoCarga.DataTextField = "nombre";
            ddlTipoCarga.DataValueField = "id";
            ddlTipoCarga.DataBind();
        }
        public static void fillNom(DropDownList ddlNom)
        {
            ddlNom.Items.Clear();
            foreach (Nom item in CatalogCtrl.Nomfill())
            {
                ListItem li = new ListItem(item.Nombre, item.Id.ToString());
                li.Attributes.Add("description", item.Descripcion.ToString());
                ddlNom.Items.Add(li);
            }
        }

        public static void fillTarimaAlmacenProveedor(DropDownList ddl)
        {
            ddl.Items.Clear();
            foreach (Tarima_almacen_proveedor item in CatalogCtrl.TarimaAlmacenProveedorFill())
            {
                ListItem li = new ListItem(item.Nombre, item.Id.ToString());
                li.Attributes.Add("description", item.Nombre);
                ddl.Items.Add(li);
            }
        }
        
        public static void fillVigilanciaByBodega(DropDownList ddlVigilante, int id_bodega)
        {
            VigilanteMng oMng = new VigilanteMng() { O_Vigilante = new Vigilante() { Id_bodega = id_bodega } };
            oMng.fillLstByBodega();
            ddlVigilante.DataSource = oMng.Lst;
            ddlVigilante.DataTextField = "nombre";
            ddlVigilante.DataValueField = "id";
            ddlVigilante.DataBind();
        }
        public static void fillEjecutivoByClienteGpo(DropDownList ddlEjecutivo, int id_cliente)
        {
            Cliente_ejecutivoMng oMng = new Cliente_ejecutivoMng()
            {
                O_Cliente_ejecutivo = new Cliente_ejecutivo()
                {
                    Id = id_cliente
                }
            };
            oMng.fillByCliente();
            ddlEjecutivo.DataSource = oMng.Lst;
            ddlEjecutivo.DataTextField = "nombre";
            ddlEjecutivo.DataValueField = "id";
            ddlEjecutivo.DataBind();
        }
    }
}
