using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using Model;

namespace ModelCasc.operation
{
    internal class Entrada_inventarioMng: dbTable
    {
        #region Campos
        protected Entrada_inventario _oEntrada_inventario;
        protected List<Entrada_inventario> _lst;
        #endregion

        #region Propiedades
        public Entrada_inventario O_Entrada_inventario { get { return _oEntrada_inventario; } set { _oEntrada_inventario = value; } }
        public List<Entrada_inventario> Lst { get { return _lst; } set { _lst = value; } }
        #endregion

        #region Constructores
        public Entrada_inventarioMng()
        {
            this._oEntrada_inventario = new Entrada_inventario();
            this._lst = new List<Entrada_inventario>();
        }
        #endregion

        #region Metodos
        protected override void addParameters(int opcion)
        {
            GenericDataAccess.AddInParameter(this.comm, "?P_opcion", DbType.Int32, opcion);
            GenericDataAccess.AddInOutParameter(this.comm, "?P_id", DbType.Int32, this._oEntrada_inventario.Id);
            GenericDataAccess.AddInParameter(this.comm, "?P_id_entrada", DbType.Int32, this._oEntrada_inventario.Id_entrada);
            GenericDataAccess.AddInParameter(this.comm, "?P_id_usuario", DbType.Int32, this._oEntrada_inventario.Id_usuario);
            GenericDataAccess.AddInParameter(this.comm, "?P_id_entrada_fondeo", DbType.Int32, this._oEntrada_inventario.Id_entrada_fondeo);
            GenericDataAccess.AddInParameter(this.comm, "?P_codigo_cliente", DbType.String, this._oEntrada_inventario.Codigo_cliente);
            GenericDataAccess.AddInParameter(this.comm, "?P_referencia", DbType.String, this._oEntrada_inventario.Referencia);
            GenericDataAccess.AddInParameter(this.comm, "?P_orden_compra", DbType.String, this._oEntrada_inventario.Orden_compra);
            GenericDataAccess.AddInParameter(this.comm, "?P_codigo", DbType.String, this._oEntrada_inventario.Codigo);
            //GenericDataAccess.AddInParameter(this.comm, "?P_id_ubicacion", DbType.Int32, this._oEntrada_inventario.Id_ubicacion);
            //GenericDataAccess.AddInParameter(this.comm, "?P_id_comprador", DbType.Int32, this._oEntrada_inventario.Id_comprador);
            GenericDataAccess.AddInParameter(this.comm, "?P_id_vendor", DbType.Int32, this._oEntrada_inventario.Id_vendor);
            GenericDataAccess.AddInParameter(this.comm, "?P_mercancia", DbType.String, this._oEntrada_inventario.Mercancia);
            GenericDataAccess.AddInParameter(this.comm, "?P_id_nom", DbType.Int32, this._oEntrada_inventario.Id_nom);
            GenericDataAccess.AddInParameter(this.comm, "?P_solicitud", DbType.String, this._oEntrada_inventario.Solicitud);
            GenericDataAccess.AddInParameter(this.comm, "?P_factura", DbType.String, this._oEntrada_inventario.Factura);
            GenericDataAccess.AddInParameter(this.comm, "?P_valor_unitario", DbType.Double, this._oEntrada_inventario.Valor_unitario);
            GenericDataAccess.AddInParameter(this.comm, "?P_valor_factura", DbType.Double, this._oEntrada_inventario.Valor_factura);
            GenericDataAccess.AddInParameter(this.comm, "?P_piezas", DbType.Int32, this._oEntrada_inventario.Piezas);
            GenericDataAccess.AddInParameter(this.comm, "?P_bultos", DbType.Int32, this._oEntrada_inventario.Bultos);
            GenericDataAccess.AddInParameter(this.comm, "?P_bultosxpallet", DbType.Int32, this._oEntrada_inventario.Bultosxpallet);
            GenericDataAccess.AddInParameter(this.comm, "?P_pallets", DbType.Int32, this._oEntrada_inventario.Pallets);
            GenericDataAccess.AddInParameter(this.comm, "?P_piezas_recibidas", DbType.Int32, this._oEntrada_inventario.Piezas_recibidas);
            GenericDataAccess.AddInParameter(this.comm, "?P_bultos_recibidos", DbType.Int32, this._oEntrada_inventario.Bultos_recibidos);
            GenericDataAccess.AddInParameter(this.comm, "?P_piezas_falt", DbType.Int32, this._oEntrada_inventario.Piezas_falt);
            GenericDataAccess.AddInParameter(this.comm, "?P_piezas_sobr", DbType.Int32, this._oEntrada_inventario.Piezas_sobr);
            GenericDataAccess.AddInParameter(this.comm, "?P_bultos_falt", DbType.Int32, this._oEntrada_inventario.Bultos_falt);
            GenericDataAccess.AddInParameter(this.comm, "?P_bultos_sobr", DbType.Int32, this._oEntrada_inventario.Bultos_sobr);
            GenericDataAccess.AddInParameter(this.comm, "?P_observaciones", DbType.String, this._oEntrada_inventario.Observaciones);
            GenericDataAccess.AddInParameter(this.comm, "?P_fecha_maquila", DbType.DateTime, this._oEntrada_inventario.Fecha_maquila);
            GenericDataAccess.AddInParameter(this.comm, "?P_maquila_abierta", DbType.Boolean, this._oEntrada_inventario.Maquila_abierta);
        }

        public void BindByDataRow(DataRow dr, Entrada_inventario o)
        {
            try
            {
                int.TryParse(dr["id"].ToString(), out entero);
                o.Id = entero;
                entero = 0;
                if (dr["id_entrada"] != DBNull.Value)
                {
                    int.TryParse(dr["id_entrada"].ToString(), out entero);
                    o.Id_entrada = entero;
                    entero = 0;
                }
                if (dr["id_usuario"] != DBNull.Value)
                {
                    int.TryParse(dr["id_usuario"].ToString(), out entero);
                    o.Id_usuario = entero;
                    entero = 0;
                }
                if (dr["id_entrada_fondeo"] != DBNull.Value)
                {
                    int.TryParse(dr["id_entrada_fondeo"].ToString(), out entero);
                    o.Id_entrada_fondeo = entero;
                    entero = 0;
                }
                o.Codigo_cliente = dr["codigo_cliente"].ToString();
                o.Referencia = dr["referencia"].ToString();
                o.Orden_compra = dr["orden_compra"].ToString();
                o.Codigo = dr["codigo"].ToString();
                //if (dr["id_ubicacion"] != DBNull.Value)
                //{
                //    int.TryParse(dr["id_ubicacion"].ToString(), out entero);
                //    o.Id_ubicacion = entero;
                //    entero = 0;
                //}
                //if (dr["id_comprador"] != DBNull.Value)
                //{
                //    int.TryParse(dr["id_comprador"].ToString(), out entero);
                //    o.Id_comprador = entero;
                //    entero = 0;
                //}
                if (dr["id_vendor"] != DBNull.Value)
                {
                    int.TryParse(dr["id_vendor"].ToString(), out entero);
                    o.Id_vendor = entero;
                    entero = 0;
                }
                o.Mercancia = dr["mercancia"].ToString();
                if (dr["id_nom"] != DBNull.Value)
                {
                    int.TryParse(dr["id_nom"].ToString(), out entero);
                    o.Id_nom = entero;
                    entero = 0;
                }
                o.Solicitud = dr["solicitud"].ToString();
                
                o.Factura = dr["factura"].ToString();
                if (dr["valor_unitario"] != DBNull.Value)
                {
                    Double.TryParse(dr["valor_unitario"].ToString(), out doble);
                    o.Valor_unitario = doble;
                    doble = 0;
                }
                if (dr["valor_factura"] != DBNull.Value)
                {
                    Double.TryParse(dr["valor_factura"].ToString(), out doble);
                    o.Valor_factura = doble;
                    doble = 0;
                }
                if (dr["piezas"] != DBNull.Value)
                {
                    int.TryParse(dr["piezas"].ToString(), out entero);
                    o.Piezas = entero;
                    entero = 0;
                }
                if (dr["bultos"] != DBNull.Value)
                {
                    int.TryParse(dr["bultos"].ToString(), out entero);
                    o.Bultos = entero;
                    entero = 0;
                }
                if (dr["bultosxpallet"] != DBNull.Value)
                {
                    int.TryParse(dr["bultosxpallet"].ToString(), out entero);
                    o.Bultosxpallet = entero;
                    entero = 0;
                }
                if (dr["pallets"] != DBNull.Value)
                {
                    int.TryParse(dr["pallets"].ToString(), out entero);
                    o.Pallets = entero;
                    entero = 0;
                }

                if (dr["piezas_recibidas"] != DBNull.Value)
                {
                    int.TryParse(dr["piezas_recibidas"].ToString(), out entero);
                    o.Piezas_recibidas = entero;
                    entero = 0;
                }

                if (dr["bultos_recibidos"] != DBNull.Value)
                {
                    int.TryParse(dr["bultos_recibidos"].ToString(), out entero);
                    o.Bultos_recibidos = entero;
                    entero = 0;
                }


                if (dr["piezas_falt"] != DBNull.Value)
                {
                    int.TryParse(dr["piezas_falt"].ToString(), out entero);
                    o.Piezas_falt = entero;
                    entero = 0;
                }
                if (dr["piezas_sobr"] != DBNull.Value)
                {
                    int.TryParse(dr["piezas_sobr"].ToString(), out entero);
                    o.Piezas_sobr = entero;
                    entero = 0;
                }
                if (dr["bultos_falt"] != DBNull.Value)
                {
                    int.TryParse(dr["bultos_falt"].ToString(), out entero);
                    o.Bultos_falt = entero;
                    entero = 0;
                }
                if (dr["bultos_sobr"] != DBNull.Value)
                {
                    int.TryParse(dr["bultos_sobr"].ToString(), out entero);
                    o.Bultos_sobr = entero;
                    entero = 0;
                }
                o.Observaciones = dr["observaciones"].ToString();
                if (dr["fecha_maquila"] != DBNull.Value)
                {
                    DateTime.TryParse(dr["fecha_maquila"].ToString(), out fecha);
                    o.Fecha_maquila = fecha;
                    fecha = new DateTime(1, 1, 1);
                }
                
                if (dr["maquila_abierta"] != DBNull.Value)
                {
                    logica = string.Compare("1", dr["maquila_abierta"].ToString()) == 0 ? true : false; 
                    o.Maquila_abierta = logica;
                    logica = false;
                }
            }
            catch
            {
                throw;
            }
        }

        public override void fillLst()
        {
            try
            {
                this.comm = GenericDataAccess.CreateCommandSP("sp_Entrada_inventario");
                addParameters(0);
                this.dt = GenericDataAccess.ExecuteSelectCommand(comm);
                this._lst = new List<Entrada_inventario>();
                foreach (DataRow dr in dt.Rows)
                {
                    Entrada_inventario o = new Entrada_inventario();
                    BindByDataRow(dr, o);
                    this._lst.Add(o);
                }
            }
            catch
            {
                throw;
            }
        }

        public override void selById()
        {
            try
            {
                this.comm = GenericDataAccess.CreateCommandSP("sp_Entrada_inventario");
                addParameters(1);
                this.dt = GenericDataAccess.ExecuteSelectCommand(comm);
                if (dt.Rows.Count == 1)
                {
                    DataRow dr = dt.Rows[0];
                    BindByDataRow(dr, this._oEntrada_inventario);
                    //this._oEntrada_inventario.PzasPorBulto = 0;
                    //if(this.O_Entrada_inventario.Bultos != 0)
                    //    this._oEntrada_inventario.PzasPorBulto = this._oEntrada_inventario.Piezas / this._oEntrada_inventario.Bultos;
                }
                else if (dt.Rows.Count > 1)
                    throw new Exception("Error de integridad");
                else
                    throw new Exception("No existe información para el registro solicitado");
            }
            catch
            {
                throw;
            }
        }

        public override void add()
        {
            try
            {
                this.comm = GenericDataAccess.CreateCommandSP("sp_Entrada_inventario");
                addParameters(2);
                GenericDataAccess.ExecuteNonQuery(this.comm);
                this._oEntrada_inventario.Id = Convert.ToInt32(GenericDataAccess.getParameterValue(comm, "?P_id"));
            }
            catch
            {
                throw;
            }
        }

        public override void udt()
        {
            try
            {
                this.comm = GenericDataAccess.CreateCommandSP("sp_Entrada_inventario");
                addParameters(3);
                GenericDataAccess.ExecuteNonQuery(this.comm);
            }
            catch
            {
                throw;
            }
        }

        public override void dlt()
        {
            try
            {
                this.comm = GenericDataAccess.CreateCommandSP("sp_Entrada_inventario");
                addParameters(4);
                GenericDataAccess.ExecuteNonQuery(this.comm);
            }
            catch
            {
                throw;
            }
        }

        #endregion

        public void add(IDbTransaction trans)
        {
            try
            {
                this.comm = GenericDataAccess.CreateCommandSP("sp_Entrada_inventario");
                addParameters(2);
                GenericDataAccess.ExecuteNonQuery(this.comm, trans);
                this._oEntrada_inventario.Id = Convert.ToInt32(GenericDataAccess.getParameterValue(comm, "?P_id"));
            }
            catch
            {
                throw;
            }
        }

        public void udt(IDbTransaction trans)
        {
            try
            {
                this.comm = GenericDataAccess.CreateCommandSP("sp_Entrada_inventario");
                addParameters(3);
                GenericDataAccess.ExecuteNonQuery(this.comm, trans);
            }
            catch
            {
                throw;
            }
        }

        internal void getByIdEntrada(bool withDetail = true)
        {
            try
            {
                this.comm = GenericDataAccess.CreateCommandSP("sp_Entrada_inventario");
                addParameters(withDetail ? 5 : 6);
                this.dt = GenericDataAccess.ExecuteSelectCommand(comm);
                this._lst = new List<Entrada_inventario>();
                foreach (DataRow dr in dt.Rows)
                {
                    Entrada_inventario o = new Entrada_inventario();

                    if (withDetail)
                    {
                        BindByDataRow(dr, o);
                        //o.Ubicacion = dr["ubicacion"].ToString();
                        if (dr["consec"] != DBNull.Value)
                        {
                            int.TryParse(dr["consec"].ToString(), out entero);
                            o.Consec = entero;
                            entero = 0;
                        }
                        //o.Comprador = dr["comprador"].ToString();
                        o.Proveedor = dr["proveedor"].ToString();
                        o.Nom = dr["nom"].ToString();
                    }
                    else
                    {
                        int.TryParse(dr["id"].ToString(), out entero);
                        o.Id = entero;
                        entero = 0;
                        if (dr["id_entrada"] != DBNull.Value)
                        {
                            int.TryParse(dr["id_entrada"].ToString(), out entero);
                            o.Id_entrada = entero;
                            entero = 0;
                        }
                        o.Orden_compra = dr["orden_compra"].ToString();
                        o.Codigo = dr["codigo"].ToString();

                        o.Mercancia = dr["mercancia"].ToString();
                        o.Mercancia += Entrada_inventario_loteMng.getLotesByIdEntradaInventario(o.Id);

                        
                    }
                    this._lst.Add(o);
                }
            }
            catch
            {
                throw;
            }
        }

        internal void selByIdFechaMaquila()
        {
            try
            {
                this.comm = GenericDataAccess.CreateCommandSP("sp_Entrada_inventario");
                addParameters(7);
                this.dt = GenericDataAccess.ExecuteSelectCommand(comm);
                this._lst = new List<Entrada_inventario>();
                foreach (DataRow dr in dt.Rows)
                {
                    Entrada_inventario o = new Entrada_inventario();
                    BindByDataRow(dr, o);
                    this._lst.Add(o);
                }
            }
            catch
            {
                throw;
            }
        }

        internal void getByIdEstatus()
        {
            try
            {
                this.comm = GenericDataAccess.CreateCommandSP("sp_Entrada_inventario");
                addParameters(8);
                this.dt = GenericDataAccess.ExecuteSelectCommand(comm);
                this._lst = new List<Entrada_inventario>();
                foreach (DataRow dr in dt.Rows)
                {
                    Entrada_inventario o = new Entrada_inventario();
                    int.TryParse(dr["id"].ToString(), out entero);
                    o.Id = entero;
                    entero = 0;
                    if (dr["id_entrada"] != DBNull.Value)
                    {
                        int.TryParse(dr["id_entrada"].ToString(), out entero);
                        o.Id_entrada = entero;
                        entero = 0;
                    }
                    o.FolioEntrada = dr["folio"].ToString();
                    o.Referencia = dr["referencia"].ToString();
                    o.Orden_compra = dr["orden_compra"].ToString();
                    o.Codigo = dr["codigo"].ToString();
                    //Entrada_inventario_loteMng oEILMng = new Entrada_inventario_loteMng()
                    //{
                    //    O_Entrada_inventario_lote = new Entrada_inventario_lote() { Id_entrada_inventario = o.Id }
                    //};
                    //oEILMng.selDistinctLote();
                    //List<Entrada_inventario_lote> lstEIL = oEILMng.Lst;
                    o.Mercancia = dr["mercancia"].ToString();
                    
                    this._lst.Add(o);
                }
            }
            catch
            {
                
                throw;
            }
        }

        internal void getSinMaquila()
        {
            try
            {
                this.comm = GenericDataAccess.CreateCommandSP("sp_Entrada_inventario");
                addParameters(9);
                this.dt = GenericDataAccess.ExecuteSelectCommand(comm);
                this._lst = new List<Entrada_inventario>();
                foreach (DataRow dr in dt.Rows)
                {
                    Entrada_inventario o = new Entrada_inventario();
                    int.TryParse(dr["id"].ToString(), out entero);
                    o.Id = entero;
                    entero = 0;
                    if (dr["id_entrada"] != DBNull.Value)
                    {
                        int.TryParse(dr["id_entrada"].ToString(), out entero);
                        o.Id_entrada = entero;
                        entero = 0;
                    }
                    o.FolioEntrada = dr["folio"].ToString();
                    o.Referencia = dr["referencia"].ToString();
                    o.Orden_compra = dr["orden_compra"].ToString();
                    o.Codigo = dr["codigo"].ToString();
                    o.Mercancia = dr["mercancia"].ToString();
                    this._lst.Add(o);
                }
            }
            catch
            {

                throw;
            }
        }

        internal void updateMaquilaCerrada(IDbTransaction trans)
        {
            try
            {
                this.comm = GenericDataAccess.CreateCommandSP("sp_Entrada_inventario");
                addParameters(10);
                GenericDataAccess.ExecuteNonQuery(this.comm, trans);
            }
            catch
            {
                throw;
            }
        }

        internal void getMaquilado()
        {
            try
            {
                this.comm = GenericDataAccess.CreateCommandSP("sp_Entrada_inventario");
                addParameters(11);
                this.dt = GenericDataAccess.ExecuteSelectCommand(comm);
                this._lst = new List<Entrada_inventario>();
                foreach (DataRow dr in dt.Rows)
                {
                    Entrada_inventario o = new Entrada_inventario();

                    int.TryParse(dr["id"].ToString(), out entero);
                    o.Id = entero;
                    entero = 0;
                    if (dr["id_entrada"] != DBNull.Value)
                    {
                        int.TryParse(dr["id_entrada"].ToString(), out entero);
                        o.Id_entrada = entero;
                        entero = 0;
                    }
                    o.Orden_compra = dr["orden_compra"].ToString();
                    o.Codigo = dr["codigo"].ToString();

                    o.Mercancia = dr["mercancia"].ToString();
                    o.Lote = dr["lote"].ToString();
                    o.Maquilado = string.Compare("1", dr["maquilado"].ToString()) == 0 ? true : false;
                    this._lst.Add(o);
                }
            }
            catch
            {
                throw;
            }
        }

        internal void udtMercancia()
        {
            try
            {
                this.comm = GenericDataAccess.CreateCommandSP("sp_Entrada_inventario");
                addParameters(12);
                GenericDataAccess.ExecuteNonQuery(this.comm);
            }
            catch
            {
                throw;
            }
        }

        internal void udtMaqAbierta()
        {
            try
            {
                this.comm = GenericDataAccess.CreateCommandSP("sp_Entrada_inventario");
                addParameters(13);
                GenericDataAccess.ExecuteNonQuery(this.comm);
            }
            catch
            {
                throw;
            }
        }

        internal void getByReferencia()
        {
            try
            {
                this.comm = GenericDataAccess.CreateCommandSP("sp_Entrada_inventario");
                addParameters(14);
                this.dt = GenericDataAccess.ExecuteSelectCommand(comm);
                this._lst = new List<Entrada_inventario>();
                foreach (DataRow dr in dt.Rows)
                {
                    Entrada_inventario o = new Entrada_inventario();
                    BindByDataRow(dr, o);
                    this._lst.Add(o);
                }
            }
            catch
            {
                throw;
            }
        }

        internal void udtCodigo()
        {
            try
            {
                this.comm = GenericDataAccess.CreateCommandSP("sp_Entrada_inventario");
                addParameters(13);
                GenericDataAccess.ExecuteNonQuery(this.comm);
            }
            catch
            {
                throw;
            }
        }
    }
}
