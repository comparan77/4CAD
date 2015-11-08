using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using Model;

namespace ModelCasc.operation
{
    internal class Salida_remisionMng: dbTable
    {
        #region Campos
        protected Salida_remision _oSalida_remision;
        protected List<Salida_remision> _lst;
        #endregion

        #region Propiedades
        public Salida_remision O_Salida_remision { get { return _oSalida_remision; } set { _oSalida_remision = value; } }
        public List<Salida_remision> Lst { get { return _lst; } set { _lst = value; } }
        #endregion

        #region Constructores
        public Salida_remisionMng()
        {
            this._oSalida_remision = new Salida_remision();
            this._lst = new List<Salida_remision>();
        }
        #endregion

        #region Metodos
        protected override void addParameters(int opcion)
        {
            GenericDataAccess.AddInParameter(this.comm, "?P_opcion", DbType.Int32, opcion);
            GenericDataAccess.AddInOutParameter(this.comm, "?P_id", DbType.Int32, this._oSalida_remision.Id);
            GenericDataAccess.AddInParameter(this.comm, "?P_id_entrada", DbType.Int32, this._oSalida_remision.Id_entrada);
            GenericDataAccess.AddInParameter(this.comm, "?P_id_entrada_inventario", DbType.Int32, this._oSalida_remision.Id_entrada_inventario);
            GenericDataAccess.AddInParameter(this.comm, "?P_id_usuario_elaboro", DbType.Int32, this._oSalida_remision.Id_usuario_elaboro);
            GenericDataAccess.AddInParameter(this.comm, "?P_id_usuario_autorizo", DbType.Int32, this._oSalida_remision.Id_usuario_autorizo);
            GenericDataAccess.AddInParameter(this.comm, "?P_id_salida_trafico", DbType.Int32, this._oSalida_remision.Id_salida_trafico);
            GenericDataAccess.AddInParameter(this.comm, "?P_folio_remision", DbType.String, this._oSalida_remision.Folio_remision);
            GenericDataAccess.AddInParameter(this.comm, "?P_referencia", DbType.String, this._oSalida_remision.Referencia);
            GenericDataAccess.AddInParameter(this.comm, "?P_codigo_cliente", DbType.String, this._oSalida_remision.Codigo_cliente);
            GenericDataAccess.AddInParameter(this.comm, "?P_codigo", DbType.String, this._oSalida_remision.Codigo);
            GenericDataAccess.AddInParameter(this.comm, "?P_orden", DbType.String, this._oSalida_remision.Orden);
            GenericDataAccess.AddInParameter(this.comm, "?P_fecha_remision", DbType.DateTime, this._oSalida_remision.Fecha_remision);
            GenericDataAccess.AddInParameter(this.comm, "?P_etiqueta_rr", DbType.String, this._oSalida_remision.Etiqueta_rr);
            GenericDataAccess.AddInParameter(this.comm, "?P_fecha_recibido", DbType.DateTime, this._oSalida_remision.Fecha_recibido);
            GenericDataAccess.AddInParameter(this.comm, "?P_dano_especifico", DbType.String, this._oSalida_remision.Dano_especifico);
            GenericDataAccess.AddInParameter(this.comm, "?P_id_estatus", DbType.Int32, this._oSalida_remision.Id_estatus);
        }

        protected void BindByDataRow(DataRow dr, Salida_remision o)
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
                if (dr["id_entrada_inventario"] != DBNull.Value)
                {
                    int.TryParse(dr["id_entrada_inventario"].ToString(), out entero);
                    o.Id_entrada_inventario = entero;
                    entero = 0;
                }
                else
                {
                    o.Id_entrada_inventario = null;
                }
                if (dr["id_usuario_elaboro"] != DBNull.Value)
                {
                    int.TryParse(dr["id_usuario_elaboro"].ToString(), out entero);
                    o.Id_usuario_elaboro = entero;
                    entero = 0;
                }
                if (dr["id_salida_trafico"] != DBNull.Value)
                {
                    int.TryParse(dr["id_salida_trafico"].ToString(), out entero);
                    o.Id_usuario_elaboro = entero;
                    entero = 0;
                }
                if (dr["id_usuario_autorizo"] != DBNull.Value)
                {
                    int.TryParse(dr["id_usuario_autorizo"].ToString(), out entero);
                    o.Id_usuario_autorizo = entero;
                    entero = 0;
                }
                o.Folio_remision = dr["folio_remision"].ToString();
                o.Referencia = dr["referencia"].ToString();
                o.Codigo_cliente = dr["codigo_cliente"].ToString();
                o.Codigo = dr["codigo"].ToString();
                o.Orden = dr["orden"].ToString();
                if (dr["fecha_remision"] != DBNull.Value)
                {
                    DateTime.TryParse(dr["fecha_remision"].ToString(), out fecha);
                    o.Fecha_remision = fecha;
                    fecha = default(DateTime);
                }
                o.Etiqueta_rr = dr["etiqueta_rr"].ToString();
                if (dr["fecha_recibido"] != DBNull.Value)
                {
                    DateTime.TryParse(dr["fecha_recibido"].ToString(), out fecha);
                    o.Fecha_recibido = fecha;
                    fecha = default(DateTime);
                }
                else
                {
                    o.Fecha_recibido = null;
                }
                o.Dano_especifico = dr["dano_especifico"].ToString();
                if (dr["id_estatus"] != DBNull.Value)
                {
                    int.TryParse(dr["id_estatus"].ToString(), out entero);
                    o.Id_estatus = entero;
                    entero = 0;
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
                this.comm = GenericDataAccess.CreateCommandSP("sp_Salida_remision");
                addParameters(0);
                this.dt = GenericDataAccess.ExecuteSelectCommand(comm);
                this._lst = new List<Salida_remision>();
                foreach (DataRow dr in dt.Rows)
                {
                    Salida_remision o = new Salida_remision();
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
                this.comm = GenericDataAccess.CreateCommandSP("sp_Salida_remision");
                addParameters(1);
                DataSet ds = GenericDataAccess.ExecuteMultSelectCommand(comm);
                this.dt = ds.Tables[0];
                if (dt.Rows.Count == 1)
                {
                    DataRow dr = dt.Rows[0];
                    BindByDataRow(dr, this._oSalida_remision);

                    this._oSalida_remision.Mercancia = dr["mercancia"].ToString();
                    this._oSalida_remision.Vendor = dr["vendor"].ToString();
                    this._oSalida_remision.Proveedor = dr["proveedor"].ToString();
                    this._oSalida_remision.Proveedor_direccion = dr["Proveedor_direccion"].ToString();
                    this._oSalida_remision.Autorizo = dr["autorizo"].ToString();
                    this._oSalida_remision.Elaboro = dr["elaboro"].ToString();
                    //this.O_Salida_remision.Lote = dr["lote"].ToString();
                    
                    DataTable dtDetail = ds.Tables[1];
                    Salida_remision_detailMng oMngSRD = new Salida_remision_detailMng();
                    this._oSalida_remision.LstSRDetail = new List<Salida_remision_detail>();
                    foreach (DataRow drDet in dtDetail.Rows)
                    {
                        Salida_remision_detail oSRD = new Salida_remision_detail();
                        oMngSRD.BindByDataRow(drDet, oSRD);
                        this._oSalida_remision.LstSRDetail.Add(oSRD);
                        this._oSalida_remision.PiezaTotal += oSRD.Piezas;
                    }

                    DataTable dtTrafico = ds.Tables[2];
                    Salida_traficoMng oMngST = new Salida_traficoMng();
                    Salida_trafico oST = new Salida_trafico();
                    oMngST.BindByDataRow(dtTrafico.Rows[0], oST);
                    oST.Transporte = dtTrafico.Rows[0]["transporte"].ToString();
                    oST.Transporte_tipo = dtTrafico.Rows[0]["transporte_tipo"].ToString();
                    this._oSalida_remision.PTrafico = oST;

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
                this.comm = GenericDataAccess.CreateCommandSP("sp_Salida_remision");
                addParameters(2);
                GenericDataAccess.ExecuteNonQuery(this.comm);
                this._oSalida_remision.Id = Convert.ToInt32(GenericDataAccess.getParameterValue(comm, "?P_id"));
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
                this.comm = GenericDataAccess.CreateCommandSP("sp_Salida_remision");
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
                this.comm = GenericDataAccess.CreateCommandSP("sp_Salida_remision");
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
                this.comm = GenericDataAccess.CreateCommandSP("sp_Salida_remision");
                addParameters(2);
                GenericDataAccess.ExecuteNonQuery(this.comm, trans);
                this._oSalida_remision.Id = Convert.ToInt32(GenericDataAccess.getParameterValue(comm, "?P_id"));
            }
            catch
            {
                throw;
            }
        }

        internal void sumAvailableByEntradaInventario(IDbTransaction trans = null)
        {
            try
            {
                this.comm = GenericDataAccess.CreateCommandSP("sp_Salida_remision");
                addParameters(5);
                if (trans != null)
                    this.dt = GenericDataAccess.ExecuteSelectCommand(comm, trans);
                else
                    this.dt = GenericDataAccess.ExecuteSelectCommand(comm);
                if (dt.Rows.Count == 1)
                {
                    DataRow dr = dt.Rows[0];
                    if (dr["bulto"] != DBNull.Value)
                    {
                        int.TryParse(dr["bulto"].ToString(), out entero);
                        this._oSalida_remision.BultoTotal = entero;
                        entero = 0;
                    }
                    if (dr["pieza"] != DBNull.Value)
                    {
                        int.TryParse(dr["pieza"].ToString(), out entero);
                        this._oSalida_remision.PiezaTotal = entero;
                        entero = 0;
                    }
                }
                else if (dt.Rows.Count > 1)
                    throw new Exception("Error de integridad");
                //else
                //    throw new Exception("No existe información para el registro solicitado");
            }
            catch
            {
                throw;
            }
        }

        internal void selByIdInventario()
        {
            try
            {
                this.comm = GenericDataAccess.CreateCommandSP("sp_Salida_remision");
                addParameters(6);
                this.dt = GenericDataAccess.ExecuteSelectCommand(comm);
                this._lst = new List<Salida_remision>();
                foreach (DataRow dr in dt.Rows)
                {
                    Salida_remision o = new Salida_remision();
                    BindByDataRow(dr, o);
                    
                    o.Mercancia = dr["mercancia"].ToString();
                    o.Mercancia += Entrada_inventario_loteMng.getLotesByIdEntradaInventario(Convert.ToInt32(o.Id_entrada_inventario));

                    o.PTrafico = new Salida_trafico();
                    entero = 0;
                    if (dr["id_entrada"] != DBNull.Value)
                    {
                        int.TryParse(dr["id_entrada"].ToString(), out entero);
                        o.PTrafico.Id = entero;
                        entero = 0;
                    }

                    o.PTrafico.Folio_cita = dr["folio_cita"].ToString();

                    this._lst.Add(o);
                }
            }
            catch
            {
                throw;
            }
        }

        internal void selByFolio()
        {
            try
            {
                this.comm = GenericDataAccess.CreateCommandSP("sp_Salida_remision");
                addParameters(7);
                DataSet ds = GenericDataAccess.ExecuteMultSelectCommand(comm);
                this.dt = ds.Tables[0];
                if (dt.Rows.Count == 1)
                {
                    DataRow dr = dt.Rows[0];
                    BindByDataRow(dr, this._oSalida_remision);

                    DataTable dtDetail = ds.Tables[1];
                    Salida_remision_detailMng oMngSRD = new Salida_remision_detailMng();
                    this._oSalida_remision.LstSRDetail = new List<Salida_remision_detail>();
                    foreach (DataRow drDet in dtDetail.Rows)
                    {
                        Salida_remision_detail oSRD = new Salida_remision_detail();
                        oMngSRD.BindByDataRow(drDet, oSRD);
                        this._oSalida_remision.LstSRDetail.Add(oSRD);
                        this._oSalida_remision.PiezaTotal += oSRD.Piezas;
                    }
                }
            }
            catch
            {
                throw;
            }
        }

        internal void Udt_RR()
        {
            try
            {
                this.comm = GenericDataAccess.CreateCommandSP("sp_Salida_remision");
                addParameters(8);
                GenericDataAccess.ExecuteNonQuery(this.comm);
            }
            catch
            {
                throw;
            }
        }

        internal void selById(IDbTransaction trans)
        {
            try
            {
                this.comm = GenericDataAccess.CreateCommandSP("sp_Salida_remision");
                addParameters(1);
                DataSet ds = GenericDataAccess.ExecuteMultSelectCommand(comm);
                this.dt = ds.Tables[0];
                if (dt.Rows.Count == 1)
                {
                    DataRow dr = dt.Rows[0];
                    BindByDataRow(dr, this._oSalida_remision);
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

        internal void selByIdSalidaTrafico()
        {
            try
            {
                this.comm = GenericDataAccess.CreateCommandSP("sp_Salida_remision");
                addParameters(9);
                this.dt = GenericDataAccess.ExecuteSelectCommand(comm);
                this._lst = new List<Salida_remision>();
                foreach (DataRow dr in dt.Rows)
                {
                    Salida_remision o = new Salida_remision();
                    int.TryParse(dr["id"].ToString(), out entero);
                    o.Id = entero;
                    entero = 0;
                    
                    o.Folio_remision = dr["folio_remision"].ToString();
                    o.Referencia = dr["referencia"].ToString();
                    //o.Codigo_cliente = dr["codigo_cliente"].ToString();
                    o.Codigo = dr["codigo"].ToString();
                    o.Orden = dr["orden"].ToString();

                    if (dr["pieza"] != DBNull.Value)
                    {
                        int.TryParse(dr["pieza"].ToString(), out entero);
                        o.PiezaTotal = entero;
                        entero = 0;
                    }

                    if (dr["bulto"] != DBNull.Value)
                    {
                        int.TryParse(dr["bulto"].ToString(), out entero);
                        o.BultoTotal = entero;
                        entero = 0;
                    }

                    this._lst.Add(o);
                }
            }
            catch
            {
                throw;
            }            
        }

        internal void Udt_FolioCita()
        {
            try
            {
                this.comm = GenericDataAccess.CreateCommandSP("sp_Salida_remision");
                addParameters(10);
                GenericDataAccess.ExecuteNonQuery(this.comm);
            }
            catch
            {
                throw;
            }
        }
    }
}
