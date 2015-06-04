using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Model;
using System.Data;

namespace ModelCasc.operation
{
    internal class Entrada_maquilaMng: dbTable
    {
        #region Campos
        protected Entrada_maquila _oEntrada_maquila;
        protected List<Entrada_maquila> _lst;
        #endregion

        #region Propiedades
        public Entrada_maquila O_Entrada_maquila { get { return _oEntrada_maquila; } set { _oEntrada_maquila = value; } }
        public List<Entrada_maquila> Lst { get { return _lst; } set { _lst = value; } }
        #endregion

        #region Constructores
        public Entrada_maquilaMng()
        {
            this._oEntrada_maquila = new Entrada_maquila();
            this._lst = new List<Entrada_maquila>();
        }
        #endregion

        #region Metodos
        protected override void addParameters(int opcion)
        {
            GenericDataAccess.AddInParameter(this.comm, "?P_opcion", DbType.Int32, opcion);
            GenericDataAccess.AddInOutParameter(this.comm, "?P_id", DbType.Int32, this._oEntrada_maquila.Id);
            GenericDataAccess.AddInParameter(this.comm, "?P_id_cliente", DbType.Int32, this._oEntrada_maquila.Id_cliente);
            GenericDataAccess.AddInParameter(this.comm, "?P_id_entrada", DbType.Int32, this._oEntrada_maquila.Id_entrada);
            GenericDataAccess.AddInParameter(this.comm, "?P_id_usuario", DbType.Int32, this._oEntrada_maquila.Id_usuario);
            GenericDataAccess.AddInParameter(this.comm, "?P_id_entrada_inventario", DbType.Int32, this._oEntrada_maquila.Id_entrada_inventario);
            GenericDataAccess.AddInParameter(this.comm, "?P_fecha_trabajo", DbType.DateTime, this._oEntrada_maquila.Fecha_trabajo);
            GenericDataAccess.AddInParameter(this.comm, "?P_pallet", DbType.Int32, this._oEntrada_maquila.Pallet);
            GenericDataAccess.AddInParameter(this.comm, "?P_bulto", DbType.Int32, this._oEntrada_maquila.Bulto);
            GenericDataAccess.AddInParameter(this.comm, "?P_pieza", DbType.Int32, this._oEntrada_maquila.Pieza);
            GenericDataAccess.AddInParameter(this.comm, "?P_pieza_danada", DbType.Int32, this._oEntrada_maquila.Pieza_danada);
            GenericDataAccess.AddInParameter(this.comm, "?P_bulto_faltante", DbType.Int32, this._oEntrada_maquila.Bulto_faltante);
            GenericDataAccess.AddInParameter(this.comm, "?P_bulto_sobrante", DbType.Int32, this._oEntrada_maquila.Bulto_sobrante);
            GenericDataAccess.AddInParameter(this.comm, "?P_pieza_faltante", DbType.Int32, this._oEntrada_maquila.Pieza_faltante);
            GenericDataAccess.AddInParameter(this.comm, "?P_pieza_sobrante", DbType.Int32, this._oEntrada_maquila.Pieza_sobrante);
            GenericDataAccess.AddInParameter(this.comm, "?P_id_estatus", DbType.Int32, this._oEntrada_maquila.Id_estatus);
        }

        protected void BindByDataRow(DataRow dr, Entrada_maquila o)
        {
            try
            {
                int.TryParse(dr["id"].ToString(), out entero);
                o.Id = entero;
                entero = 0;
                if (dr["id_cliente"] != DBNull.Value)
                {
                    int.TryParse(dr["id_cliente"].ToString(), out entero);
                    o.Id_cliente = entero;
                    entero = 0;
                }
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
                if (dr["id_entrada_inventario"] != DBNull.Value)
                {
                    int.TryParse(dr["id_entrada_inventario"].ToString(), out entero);
                    o.Id_entrada_inventario = entero;
                    entero = 0;
                }
                if (dr["fecha_trabajo"] != DBNull.Value)
                {
                    DateTime.TryParse(dr["fecha_trabajo"].ToString(), out fecha);
                    o.Fecha_trabajo = fecha;
                    fecha = default(DateTime);
                }
                else
                {
                    o.Fecha_trabajo = null;
                }
                if (dr["pallet"] != DBNull.Value)
                {
                    int.TryParse(dr["pallet"].ToString(), out entero);
                    o.Pallet = entero;
                    entero = 0;
                }
                if (dr["bulto"] != DBNull.Value)
                {
                    int.TryParse(dr["bulto"].ToString(), out entero);
                    o.Bulto = entero;
                    entero = 0;
                }
                if (dr["pieza"] != DBNull.Value)
                {
                    int.TryParse(dr["pieza"].ToString(), out entero);
                    o.Pieza = entero;
                    entero = 0;
                }
                if (dr["pieza_danada"] != DBNull.Value)
                {
                    int.TryParse(dr["pieza_danada"].ToString(), out entero);
                    o.Pieza_danada = entero;
                    entero = 0;
                }
                if (dr["bulto_faltante"] != DBNull.Value)
                {
                    int.TryParse(dr["bulto_faltante"].ToString(), out entero);
                    o.Bulto_faltante = entero;
                    entero = 0;
                }
                if (dr["bulto_sobrante"] != DBNull.Value)
                {
                    int.TryParse(dr["bulto_sobrante"].ToString(), out entero);
                    o.Bulto_sobrante = entero;
                    entero = 0;
                }
                if (dr["pieza_faltante"] != DBNull.Value)
                {
                    int.TryParse(dr["pieza_faltante"].ToString(), out entero);
                    o.Pieza_faltante = entero;
                    entero = 0;
                }
                if (dr["pieza_sobrante"] != DBNull.Value)
                {
                    int.TryParse(dr["pieza_sobrante"].ToString(), out entero);
                    o.Pieza_sobrante = entero;
                    entero = 0;
                }
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
                this.comm = GenericDataAccess.CreateCommandSP("sp_Entrada_maquila");
                addParameters(0);
                this.dt = GenericDataAccess.ExecuteSelectCommand(comm);
                this._lst = new List<Entrada_maquila>();
                foreach (DataRow dr in dt.Rows)
                {
                    Entrada_maquila o = new Entrada_maquila();
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
                this.comm = GenericDataAccess.CreateCommandSP("sp_Entrada_maquila");
                addParameters(1);
                this.dt = GenericDataAccess.ExecuteSelectCommand(comm);
                if (dt.Rows.Count == 1)
                {
                    DataRow dr = dt.Rows[0];
                    BindByDataRow(dr, this._oEntrada_maquila);
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
                this.comm = GenericDataAccess.CreateCommandSP("sp_Entrada_maquila");
                addParameters(2);
                GenericDataAccess.ExecuteNonQuery(this.comm);
                this._oEntrada_maquila.Id = Convert.ToInt32(GenericDataAccess.getParameterValue(comm, "?P_id"));
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
                this.comm = GenericDataAccess.CreateCommandSP("sp_Entrada_maquila");
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
                this.comm = GenericDataAccess.CreateCommandSP("sp_Entrada_maquila");
                addParameters(4);
                GenericDataAccess.ExecuteNonQuery(this.comm);
            }
            catch
            {
                throw;
            }
        }

        #endregion

        internal void selBy()
        {
            try
            {
                this.comm = GenericDataAccess.CreateCommandSP("sp_Entrada_maquila");
                addParameters(5);
                DataSet ds = GenericDataAccess.ExecuteMultSelectCommand(comm);
                this.dt = ds.Tables[0];
                if (dt.Rows.Count == 1)
                {
                    DataRow dr = dt.Rows[0];
                    BindByDataRow(dr, this._oEntrada_maquila);

                    DataTable dtDetMaq = ds.Tables[1];
                    this._oEntrada_maquila.LstEntMaqDet = new List<Entrada_maquila_detail>();
                    Entrada_maquila_detailMng oEMDMng = new Entrada_maquila_detailMng();
                    foreach (DataRow drDetMaq in dtDetMaq.Rows)
                    {
                        Entrada_maquila_detail oEMD = new Entrada_maquila_detail();
                        oEMDMng.BindByDataRow(drDetMaq, oEMD);
                        if (dr["id_estatus"] != DBNull.Value)
                        {
                            int.TryParse(dr["id_estatus"].ToString(), out entero);
                            oEMD.IdEstatus = entero;
                            entero = 0;
                        }
                        this._oEntrada_maquila.LstEntMaqDet.Add(oEMD);
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

        internal void sumByEntradaInventario()
        {
            try
            {
                this.comm = GenericDataAccess.CreateCommandSP("sp_Entrada_maquila");
                addParameters(6);
                this.dt = GenericDataAccess.ExecuteSelectCommand(comm);
                if (dt.Rows.Count == 1)
                {
                    DataRow dr = dt.Rows[0];
                    if (dr["pallet"] != DBNull.Value)
                    {
                        int.TryParse(dr["pallet"].ToString(), out entero);
                        this._oEntrada_maquila.Pallet = entero;
                        entero = 0;
                    }
                    if (dr["bulto"] != DBNull.Value)
                    {
                        int.TryParse(dr["bulto"].ToString(), out entero);
                        this._oEntrada_maquila.Bulto = Math.Abs(entero);
                        entero = 0;
                    }
                    if (dr["pieza"] != DBNull.Value)
                    {
                        int.TryParse(dr["pieza"].ToString(), out entero);
                        this._oEntrada_maquila.Pieza = Math.Abs(entero);
                        entero = 0;
                    }
                    if (dr["pieza_danada"] != DBNull.Value)
                    {
                        int.TryParse(dr["pieza_danada"].ToString(), out entero);
                        this._oEntrada_maquila.Pieza_danada = entero;
                        entero = 0;
                    }
                    if (dr["bulto_faltante"] != DBNull.Value)
                    {
                        int.TryParse(dr["bulto_faltante"].ToString(), out entero);
                        this._oEntrada_maquila.Bulto_faltante = entero;
                        entero = 0;
                    }
                    if (dr["bulto_sobrante"] != DBNull.Value)
                    {
                        int.TryParse(dr["bulto_sobrante"].ToString(), out entero);
                        this._oEntrada_maquila.Bulto_sobrante = Math.Abs(entero);
                        entero = 0;
                    }
                    if (dr["pieza_faltante"] != DBNull.Value)
                    {
                        int.TryParse(dr["pieza_faltante"].ToString(), out entero);
                        this._oEntrada_maquila.Pieza_faltante = entero;
                        entero = 0;
                    }
                    if (dr["pieza_sobrante"] != DBNull.Value)
                    {
                        int.TryParse(dr["pieza_sobrante"].ToString(), out entero);
                        this._oEntrada_maquila.Pieza_sobrante = Math.Abs(entero);
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

        internal void selByInventario()
        {
            try
            {
                this.comm = GenericDataAccess.CreateCommandSP("sp_Entrada_maquila");
                addParameters(7);
                this.dt = GenericDataAccess.ExecuteSelectCommand(comm);
                this._lst = new List<Entrada_maquila>();
                foreach (DataRow dr in dt.Rows)
                {
                    Entrada_maquila o = new Entrada_maquila();
                    BindByDataRow(dr, o);
                    this._lst.Add(o);
                }
            }
            catch
            {
                throw;
            }
        }

        ///// <summary> Nota: El estatus se lleva en la tabla entrada_estatus
        ///// Pone en estatus pendiente por abprobar y cierra la maquila con o sin incidencias
        ///// </summary>
        //internal void close()
        //{
        //    try
        //    {
        //        this.comm = GenericDataAccess.CreateCommandSP("sp_Entrada_maquila");
        //        addParameters(8);
        //        GenericDataAccess.ExecuteNonQuery(this.comm);
        //    }
        //    catch
        //    {
        //        throw;
        //    }
        //}

        internal void add(IDbTransaction trans)
        {
            try
            {
                this.comm = GenericDataAccess.CreateCommandSP("sp_Entrada_maquila");
                addParameters(2);
                GenericDataAccess.ExecuteNonQuery(this.comm, trans);
                this._oEntrada_maquila.Id = Convert.ToInt32(GenericDataAccess.getParameterValue(comm, "?P_id"));
            }
            catch
            {
                throw;
            }
        }

        internal void udt(IDbTransaction trans)
        {
            try
            {
                this.comm = GenericDataAccess.CreateCommandSP("sp_Entrada_maquila");
                addParameters(3);
                GenericDataAccess.ExecuteNonQuery(this.comm, trans);
            }
            catch
            {
                throw;
            }
        }

        internal void selDetail()
        {
            try
            {
                this.comm = GenericDataAccess.CreateCommandSP("sp_Entrada_maquila");
                addParameters(9);
                this.dt = GenericDataAccess.ExecuteSelectCommand(comm);
                this.O_Entrada_maquila.LstEntMaqDet = new List<Entrada_maquila_detail>();

                Entrada_maquila_detailMng oEMDMng = new Entrada_maquila_detailMng();
                foreach (DataRow drDetMaq in dt.Rows)
                {
                    Entrada_maquila_detail oEMD = new Entrada_maquila_detail();
                    oEMDMng.BindByDataRow(drDetMaq, oEMD);

                    if (drDetMaq["piezastotales"] != DBNull.Value)
                    {
                        int.TryParse(drDetMaq["piezastotales"].ToString(), out entero);
                        oEMD.PiezasTotales = entero;
                        entero = 0;
                    }

                    if (drDetMaq["bultoSR"] != DBNull.Value)
                    {
                        int.TryParse(drDetMaq["bultoSR"].ToString(), out entero);
                        oEMD.BultoSR = entero;
                        entero = 0;
                    }

                    if (drDetMaq["bultoD"] != DBNull.Value)
                    {
                        int.TryParse(drDetMaq["bultoD"].ToString(), out entero);
                        oEMD.BultoD = entero;
                        entero = 0;
                    }

                    if (drDetMaq["id_estatus_proceso"] != DBNull.Value)
                    {
                        int.TryParse(drDetMaq["id_estatus_proceso"].ToString(), out entero);
                        oEMD.IdEstatus = entero;
                        entero = 0;
                    }

                    oEMD.cssLocked = oEMD.IdEstatus == Globals.EST_MAQ_PAR_SIN_CERRAR ? "un" : "";

                    this._oEntrada_maquila.LstEntMaqDet.Add(oEMD);
                }
                
            }
            catch
            {
                throw;
            }
        }
    }
}
