using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Model;
using System.Data;

namespace ModelCasc.operation
{
    internal class SalidaMng : dbTable
    {
        #region Campos
        protected Salida _oSalida;
        protected List<Salida> _lst;
        #endregion

        #region Propiedades
        public Salida O_Salida { get { return _oSalida; } set { _oSalida = value; } }
        public List<Salida> Lst { get { return _lst; } set { _lst = value; } }
        #endregion

        #region Constructores
        public SalidaMng()
        {
            this._oSalida = new Salida();
        }
        #endregion

        #region Metodos
        protected override void addParameters(int opcion)
        {
            GenericDataAccess.AddInParameter(this.comm, "?P_opcion", DbType.Int32, opcion);
            GenericDataAccess.AddInOutParameter(this.comm, "?P_id", DbType.Int32, this._oSalida.Id);
            GenericDataAccess.AddInParameter(this.comm, "?P_id_bodega", DbType.Int32, this._oSalida.Id_bodega);
            GenericDataAccess.AddInParameter(this.comm, "?P_folio", DbType.String, this._oSalida.Folio);
            GenericDataAccess.AddInParameter(this.comm, "?P_folio_indice", DbType.String, this._oSalida.Folio_indice);
            GenericDataAccess.AddInParameter(this.comm, "?P_fecha", DbType.DateTime, this._oSalida.Fecha);
            if(this._oSalida.Hora_salida.Length == 0)
                GenericDataAccess.AddInParameter(this.comm, "?P_hora_salida", DbType.String, DBNull.Value);
            else
                GenericDataAccess.AddInParameter(this.comm, "?P_hora_salida", DbType.String, this._oSalida.Hora_salida);
            GenericDataAccess.AddInParameter(this.comm, "?P_id_cortina", DbType.Int32, this._oSalida.Id_cortina);
            GenericDataAccess.AddInParameter(this.comm, "?P_id_cliente", DbType.Int32, this._oSalida.Id_cliente);
            GenericDataAccess.AddInParameter(this.comm, "?P_referencia", DbType.String, this._oSalida.Referencia);
            GenericDataAccess.AddInParameter(this.comm, "?P_destino", DbType.String, this._oSalida.Destino);
            GenericDataAccess.AddInParameter(this.comm, "?P_mercancia", DbType.String, this._oSalida.Mercancia);
            GenericDataAccess.AddInParameter(this.comm, "?P_id_transporte", DbType.Int32, this._oSalida.Id_transporte);
            GenericDataAccess.AddInParameter(this.comm, "?P_id_transporte_tipo", DbType.Int32, this._oSalida.Id_transporte_tipo);
            GenericDataAccess.AddInParameter(this.comm, "?P_placa", DbType.String, this._oSalida.Placa);
            GenericDataAccess.AddInParameter(this.comm, "?P_caja1", DbType.String, this._oSalida.Caja1);
            GenericDataAccess.AddInParameter(this.comm, "?P_caja2", DbType.String, this._oSalida.Caja2);
            GenericDataAccess.AddInParameter(this.comm, "?P_sello", DbType.String, this._oSalida.Sello);
            GenericDataAccess.AddInParameter(this.comm, "?P_talon", DbType.String, this._oSalida.Talon);
            GenericDataAccess.AddInParameter(this.comm, "?P_id_custodia", DbType.Int32, this._oSalida.Id_custodia);
            GenericDataAccess.AddInParameter(this.comm, "?P_operador", DbType.String, this._oSalida.Operador);
            GenericDataAccess.AddInParameter(this.comm, "?P_no_pallet", DbType.Int32, this._oSalida.No_pallet);
            GenericDataAccess.AddInParameter(this.comm, "?P_no_bulto", DbType.Int32, this._oSalida.No_bulto);
            GenericDataAccess.AddInParameter(this.comm, "?P_no_pieza", DbType.Int32, this._oSalida.No_pieza);
            GenericDataAccess.AddInParameter(this.comm, "?P_peso_unitario", DbType.Double, this._oSalida.Peso_unitario);
            GenericDataAccess.AddInParameter(this.comm, "?P_total_carga", DbType.Double, this._oSalida.Total_carga);
            GenericDataAccess.AddInParameter(this.comm, "?P_es_unica", DbType.Boolean, this._oSalida.Es_unica);
            if(this.O_Salida.Hora_carga.Length ==0)
                GenericDataAccess.AddInParameter(this.comm, "?P_hora_carga", DbType.String, DBNull.Value);
            else
                GenericDataAccess.AddInParameter(this.comm, "?P_hora_carga", DbType.String, this._oSalida.Hora_carga);
            GenericDataAccess.AddInParameter(this.comm, "?P_vigilante", DbType.String, this._oSalida.Vigilante);
            GenericDataAccess.AddInParameter(this.comm, "?P_observaciones", DbType.String, this._oSalida.Observaciones);
            GenericDataAccess.AddInParameter(this.comm, "?P_motivo_cancelacion", DbType.String, this._oSalida.Motivo_cancelacion);
        }

        public override void fillLst()
        {
            try
            {
                this.comm = GenericDataAccess.CreateCommandSP("sp_Salida");
                addParameters(0);
                this.dt = GenericDataAccess.ExecuteSelectCommand(comm);
                this._lst = new List<Salida>();
                foreach (DataRow dr in dt.Rows)
                {
                    Salida o = new Salida();
                    int.TryParse(dr["id"].ToString(), out entero);
                    o.Id = entero;
                    entero = 0;
                    if (dr["id_bodega"] != DBNull.Value)
                    {
                        int.TryParse(dr["id_bodega"].ToString(), out entero);
                        o.Id_bodega = entero;
                        entero = 0;
                    }
                    o.Folio = dr["folio"].ToString();
                    o.Folio_indice = dr["folio_indice"].ToString();
                    if (dr["fecha"] != DBNull.Value)
                    {
                        DateTime.TryParse(dr["fecha"].ToString(), out fecha);
                        o.Fecha = fecha;
                        fecha = new DateTime(1, 1, 1);
                    }
                    o.Hora_salida = dr["hora_salida"].ToString();
                    if (dr["id_cortina"] != DBNull.Value)
                    {
                        int.TryParse(dr["id_cortina"].ToString(), out entero);
                        o.Id_cortina = entero;
                        entero = 0;
                    }
                    if (dr["id_cliente"] != DBNull.Value)
                    {
                        int.TryParse(dr["id_cliente"].ToString(), out entero);
                        o.Id_cliente = entero;
                        entero = 0;
                    }
                    o.Referencia = dr["referencia"].ToString(); 
                    o.Destino = dr["destino"].ToString();
                    o.Mercancia = dr["mercancia"].ToString();
                    if (dr["id_transporte"] != DBNull.Value)
                    {
                        int.TryParse(dr["id_transporte"].ToString(), out entero);
                        o.Id_transporte = entero;
                        entero = 0;
                    }
                    if (dr["id_transporte_tipo"] != DBNull.Value)
                    {
                        int.TryParse(dr["id_transporte_tipo"].ToString(), out entero);
                        o.Id_transporte_tipo = entero;
                        entero = 0;
                    }
                    o.Placa = dr["placa"].ToString();
                    o.Caja1 = dr["caja1"].ToString();
                    o.Caja2 = dr["caja2"].ToString();
                    o.Sello = dr["sello"].ToString();
                    o.Talon = dr["talon"].ToString();
                    if (dr["id_custodia"] != DBNull.Value)
                    {
                        int.TryParse(dr["id_custodia"].ToString(), out entero);
                        o.Id_custodia = entero;
                        entero = 0;
                    }
                    o.Operador = dr["operador"].ToString();
                    if (dr["no_pallet"] != DBNull.Value)
                    {
                        int.TryParse(dr["no_pallet"].ToString(), out entero);
                        o.No_pallet = entero;
                        entero = 0;
                    }
                    if (dr["no_bulto"] != DBNull.Value)
                    {
                        int.TryParse(dr["no_bulto"].ToString(), out entero);
                        o.No_bulto = entero;
                        entero = 0;
                    }
                    if (dr["no_pieza"] != DBNull.Value)
                    {
                        int.TryParse(dr["no_pieza"].ToString(), out entero);
                        o.No_pieza = entero;
                        entero = 0;
                    }
                    if (dr["peso_unitario"] != DBNull.Value)
                    {
                        Double.TryParse(dr["peso_unitario"].ToString(), out doble);
                        o.Peso_unitario = doble;
                        doble = 0;
                    }
                    if (dr["total_carga"] != DBNull.Value)
                    {
                        Double.TryParse(dr["total_carga"].ToString(), out doble);
                        o.Total_carga = doble;
                        doble = 0;
                    }
                    if (dr["es_unica"] != DBNull.Value)
                    {
                        bool.TryParse(dr["es_unica"].ToString(), out logica);
                        o.Es_unica = logica;
                        logica = false;
                    }
                    o.Hora_carga = dr["hora_carga"].ToString();
                    o.Vigilante = dr["vigilante"].ToString();
                    o.Observaciones = dr["observaciones"].ToString();
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
                this.comm = GenericDataAccess.CreateCommandSP("sp_Salida");
                addParameters(1);
                this.dt = GenericDataAccess.ExecuteSelectCommand(comm);
                if (dt.Rows.Count == 1)
                {
                    DataRow dr = dt.Rows[0];
                    if (dr["id_bodega"] != DBNull.Value)
                    {
                        int.TryParse(dr["id_bodega"].ToString(), out entero);
                        this._oSalida.Id_bodega = entero;
                        entero = 0;
                    }
                    this._oSalida.Folio = dr["folio"].ToString();
                    this._oSalida.Folio_indice = dr["folio_indice"].ToString();
                    if (dr["fecha"] != DBNull.Value)
                    {
                        DateTime.TryParse(dr["fecha"].ToString(), out fecha);
                        this._oSalida.Fecha = fecha;
                        fecha = new DateTime(1, 1, 1);
                    }
                    this._oSalida.Hora_salida = dr["hora_salida"].ToString();
                    if (dr["id_cortina"] != DBNull.Value)
                    {
                        int.TryParse(dr["id_cortina"].ToString(), out entero);
                        this._oSalida.Id_cortina = entero;
                        entero = 0;
                    }
                    if (dr["id_cliente"] != DBNull.Value)
                    {
                        int.TryParse(dr["id_cliente"].ToString(), out entero);
                        this._oSalida.Id_cliente = entero;
                        entero = 0;
                    }
                    this._oSalida.Referencia = dr["referencia"].ToString(); 
                    this._oSalida.Destino = dr["destino"].ToString();
                    this._oSalida.Mercancia = dr["mercancia"].ToString();
                    if (dr["id_transporte"] != DBNull.Value)
                    {
                        int.TryParse(dr["id_transporte"].ToString(), out entero);
                        this._oSalida.Id_transporte = entero;
                        entero = 0;
                    }
                    if (dr["id_transporte_tipo"] != DBNull.Value)
                    {
                        int.TryParse(dr["id_transporte_tipo"].ToString(), out entero);
                        this._oSalida.Id_transporte_tipo = entero;
                        entero = 0;
                    }
                    this._oSalida.Placa = dr["placa"].ToString();
                    this._oSalida.Caja1 = dr["caja1"].ToString();
                    this._oSalida.Caja2 = dr["caja2"].ToString();
                    this._oSalida.Sello = dr["sello"].ToString();
                    this._oSalida.Talon = dr["talon"].ToString();
                    if (dr["id_custodia"] != DBNull.Value)
                    {
                        int.TryParse(dr["id_custodia"].ToString(), out entero);
                        this._oSalida.Id_custodia = entero;
                        entero = 0;
                    }
                    this._oSalida.Operador = dr["operador"].ToString();
                    if (dr["no_pallet"] != DBNull.Value)
                    {
                        int.TryParse(dr["no_pallet"].ToString(), out entero);
                        this._oSalida.No_pallet = entero;
                        entero = 0;
                    }
                    if (dr["no_bulto"] != DBNull.Value)
                    {
                        int.TryParse(dr["no_bulto"].ToString(), out entero);
                        this._oSalida.No_bulto = entero;
                        entero = 0;
                    }
                    if (dr["no_pieza"] != DBNull.Value)
                    {
                        int.TryParse(dr["no_pieza"].ToString(), out entero);
                        this._oSalida.No_pieza = entero;
                        entero = 0;
                    }
                    if (dr["peso_unitario"] != DBNull.Value)
                    {
                        Double.TryParse(dr["peso_unitario"].ToString(), out doble);
                        this._oSalida.Peso_unitario = doble;
                        doble = 0;
                    }
                    if (dr["total_carga"] != DBNull.Value)
                    {
                        Double.TryParse(dr["total_carga"].ToString(), out doble);
                        this._oSalida.Total_carga = doble;
                        doble = 0;
                    }
                    if (dr["es_unica"] != DBNull.Value)
                    {
                        bool.TryParse(dr["es_unica"].ToString(), out logica);
                        this._oSalida.Es_unica = logica;
                        logica = false;
                    }
                    this._oSalida.Hora_carga = dr["hora_carga"].ToString();
                    this._oSalida.Vigilante = dr["vigilante"].ToString();
                    this._oSalida.Observaciones = dr["observaciones"].ToString();
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
            throw new NotImplementedException();
        }

        public void add(IDbTransaction trans)
        {
            try
            {
                this.comm = GenericDataAccess.CreateCommandSP("sp_Salida");
                addParameters(2);
                GenericDataAccess.ExecuteNonQuery(this.comm, trans);
                this._oSalida.Id = Convert.ToInt32(GenericDataAccess.getParameterValue(comm, "?P_id"));
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
                this.comm = GenericDataAccess.CreateCommandSP("sp_Salida");
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
                this.comm = GenericDataAccess.CreateCommandSP("sp_Salida");
                addParameters(4);
                GenericDataAccess.ExecuteNonQuery(this.comm);
            }
            catch
            {
                throw;
            }
        }

        /// <summary>
        /// Cancelacion parcial
        /// Solo se cancela el folio al que se hace referencia.
        /// </summary>
        public void PartialCancel()
        {
            this.comm = GenericDataAccess.CreateCommandSP("sp_Salida");
            addParameters(4);
            GenericDataAccess.ExecuteNonQuery(this.comm);
        }

        /// <summary>
        /// Selecciona activa e inactiva
        /// </summary>
        public void selByIdEvenInactive()
        {
            try
            {
                this.comm = GenericDataAccess.CreateCommandSP("sp_Salida");
                addParameters(8);
                this.dt = GenericDataAccess.ExecuteSelectCommand(comm);
                if (dt.Rows.Count == 1)
                {
                    DataRow dr = dt.Rows[0];
                    if (dr["id_bodega"] != DBNull.Value)
                    {
                        int.TryParse(dr["id_bodega"].ToString(), out entero);
                        this._oSalida.Id_bodega = entero;
                        entero = 0;
                    }
                    this._oSalida.Folio = dr["folio"].ToString();
                    this._oSalida.Folio_indice = dr["folio_indice"].ToString();
                    if (dr["fecha"] != DBNull.Value)
                    {
                        DateTime.TryParse(dr["fecha"].ToString(), out fecha);
                        this._oSalida.Fecha = fecha;
                        fecha = new DateTime(1, 1, 1);
                    }
                    this._oSalida.Hora_salida = dr["hora_salida"].ToString();
                    if (dr["id_cortina"] != DBNull.Value)
                    {
                        int.TryParse(dr["id_cortina"].ToString(), out entero);
                        this._oSalida.Id_cortina = entero;
                        entero = 0;
                    }
                    if (dr["id_cliente"] != DBNull.Value)
                    {
                        int.TryParse(dr["id_cliente"].ToString(), out entero);
                        this._oSalida.Id_cliente = entero;
                        entero = 0;
                    }
                    this._oSalida.Referencia = dr["referencia"].ToString();
                    this._oSalida.Destino = dr["destino"].ToString();
                    this._oSalida.Mercancia = dr["mercancia"].ToString();
                    if (dr["id_transporte"] != DBNull.Value)
                    {
                        int.TryParse(dr["id_transporte"].ToString(), out entero);
                        this._oSalida.Id_transporte = entero;
                        entero = 0;
                    }
                    if (dr["id_transporte_tipo"] != DBNull.Value)
                    {
                        int.TryParse(dr["id_transporte_tipo"].ToString(), out entero);
                        this._oSalida.Id_transporte_tipo = entero;
                        entero = 0;
                    }
                    this._oSalida.Placa = dr["placa"].ToString();
                    this._oSalida.Caja1 = dr["caja1"].ToString();
                    this._oSalida.Caja2 = dr["caja2"].ToString();
                    this._oSalida.Sello = dr["sello"].ToString();
                    this._oSalida.Talon = dr["talon"].ToString();
                    if (dr["id_custodia"] != DBNull.Value)
                    {
                        int.TryParse(dr["id_custodia"].ToString(), out entero);
                        this._oSalida.Id_custodia = entero;
                        entero = 0;
                    }
                    this._oSalida.Operador = dr["operador"].ToString();
                    if (dr["no_pallet"] != DBNull.Value)
                    {
                        int.TryParse(dr["no_pallet"].ToString(), out entero);
                        this._oSalida.No_pallet = entero;
                        entero = 0;
                    }
                    if (dr["no_bulto"] != DBNull.Value)
                    {
                        int.TryParse(dr["no_bulto"].ToString(), out entero);
                        this._oSalida.No_bulto = entero;
                        entero = 0;
                    }
                    if (dr["no_pieza"] != DBNull.Value)
                    {
                        int.TryParse(dr["no_pieza"].ToString(), out entero);
                        this._oSalida.No_pieza = entero;
                        entero = 0;
                    }
                    if (dr["peso_unitario"] != DBNull.Value)
                    {
                        Double.TryParse(dr["peso_unitario"].ToString(), out doble);
                        this._oSalida.Peso_unitario = doble;
                        doble = 0;
                    }
                    if (dr["total_carga"] != DBNull.Value)
                    {
                        Double.TryParse(dr["total_carga"].ToString(), out doble);
                        this._oSalida.Total_carga = doble;
                        doble = 0;
                    }
                    if (dr["es_unica"] != DBNull.Value)
                    {
                        bool.TryParse(dr["es_unica"].ToString(), out logica);
                        this._oSalida.Es_unica = logica;
                        logica = false;
                    }
                    this._oSalida.Hora_carga = dr["hora_carga"].ToString();
                    this._oSalida.Vigilante = dr["vigilante"].ToString();
                    this._oSalida.Observaciones = dr["observaciones"].ToString();
                    this._oSalida.Observaciones = dr["observaciones"].ToString();
                    this._oSalida.IsActive = Convert.ToBoolean(dr["isactive"].ToString());
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

        internal bool IsPartial()
        {
            bool Exist = false;
            try
            {
                this.comm = GenericDataAccess.CreateCommandSP("sp_Salida");
                addParameters(7);
                this.dt = GenericDataAccess.ExecuteSelectCommand(comm);
                if (dt.Rows.Count >= 1)
                {
                    Exist = true;
                    DataRow dr = dt.Rows[0];

                    this.O_Salida.Folio = dr["folio"].ToString();
                    this.O_Salida.Folio_indice = dr["folio_indice"].ToString();
                    this.O_Salida.FolioSalida = this.O_Salida.Folio + this.O_Salida.Folio_indice;
                }

            }
            catch
            {
                throw;
            }
            return Exist;
        }

        #endregion

        internal bool ExistsAndIsUnique()
        {
            bool Exist = false;
            try
            {
                this.comm = GenericDataAccess.CreateCommandSP("sp_Salida");
                addParameters(9);
                this.dt = GenericDataAccess.ExecuteSelectCommand(comm);
                if (dt.Rows.Count >= 1)
                {
                    Exist = true;
                    DataRow dr = dt.Rows[0];

                    this.O_Salida.Folio = dr["folio"].ToString();
                    this.O_Salida.Folio_indice = dr["folio_indice"].ToString();
                    this.O_Salida.FolioSalida = this.O_Salida.Folio + this.O_Salida.Folio_indice;
                }

            }
            catch
            {
                throw;
            }
            return Exist;
        }

        internal void searchByFolioPedimento()
        {
            try
            {
                this.comm = GenericDataAccess.CreateCommandSP("sp_Salida");
                addParameters(10);
                this.dt = GenericDataAccess.ExecuteSelectCommand(comm);
                this._lst = new List<Salida>();
                foreach (DataRow dr in dt.Rows)
                {
                    Salida o = new Salida();
                    int.TryParse(dr["id"].ToString(), out entero);
                    o.Id = entero;
                    entero = 0;
                    o.Folio = dr["folio"].ToString();
                    o.Folio_indice = dr["folio_indice"].ToString();
                    o.Folio = o.Folio + o.Folio_indice;
                    o.Referencia = dr["referencia"].ToString();
                    this._lst.Add(o);
                }
            }
            catch
            {
                throw;
            }
        }

        internal void refValida()
        {
            try
            {
                this.comm = GenericDataAccess.CreateCommandSP("sp_Salida");
                addParameters(11);
                this.dt = GenericDataAccess.ExecuteSelectCommand(comm);
                if (dt.Rows.Count == 1)
                {
                    DataRow dr = dt.Rows[0];
                    this._oSalida.Referencia = dr["referencia"].ToString();
                    this._oSalida.Destino = dr["destino"].ToString();
                    this._oSalida.Mercancia = dr["mercancia"].ToString();

                    if (dr["id_salida_orden_carga"] != DBNull.Value)
                    {
                        int.TryParse(dr["id_salida_orden_carga"].ToString(), out entero);
                        this._oSalida.Id_salida_orden_carga = entero;
                        entero = 0;
                    }

                    if (dr["id_transporte"] != DBNull.Value)
                    {
                        int.TryParse(dr["id_transporte"].ToString(), out entero);
                        this._oSalida.Id_transporte = entero;
                        entero = 0;
                    }

                    if (dr["id_transporte_tipo"] != DBNull.Value)
                    {
                        int.TryParse(dr["id_transporte_tipo"].ToString(), out entero);
                        this._oSalida.Id_transporte_tipo = entero;
                        entero = 0;
                    }

                    if (dr["bulto"] != DBNull.Value)
                    {
                        int.TryParse(dr["bulto"].ToString(), out entero);
                        this._oSalida.No_bulto = entero;
                        entero = 0;
                    }

                    if (dr["piezas"] != DBNull.Value)
                    {
                        int.TryParse(dr["piezas"].ToString(), out entero);
                        this._oSalida.No_pieza = entero;
                        entero = 0;
                    }
                }
                else if (dt.Rows.Count > 1)
                    throw new Exception("Error de integridad");
                else
                    throw new Exception("No existe la orden de carga para la referencia proporcionada");
            }
            catch
            {
                throw;
            }
        }

        internal int piezasInventarioByReferencia()
        {
            int piezasInventario = 0;
            try
            {
                this.comm = GenericDataAccess.CreateCommandSP("sp_Salida");
                addParameters(12);
                if (!int.TryParse(GenericDataAccess.ExecuteScalar(this.comm).ToString(), out piezasInventario))
                    throw new Exception("La referencia proporcionada no existe en el inventario");
            }
            catch
            {
                throw;
            }

            return piezasInventario;
        }
    }
}
