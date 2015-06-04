using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Model;
using System.Data;

namespace ModelCasc.operation
{
    internal class EntradaMng: dbTable
    {
        #region Campos
        protected Entrada _oEntrada;
        protected List<Entrada> _lst;
        #endregion

        #region Propiedades
        public Entrada O_Entrada { get { return _oEntrada; } set { _oEntrada = value; } }
        public List<Entrada> Lst { get { return _lst; } set { _lst = value; } }
        #endregion

        #region Constructores
        public EntradaMng()
        {
            this._oEntrada = new Entrada();
        }
        #endregion

        #region Metodos
        protected override void addParameters(int opcion)
        {
            GenericDataAccess.AddInParameter(this.comm, "?P_opcion", DbType.Int32, opcion);
            GenericDataAccess.AddInOutParameter(this.comm, "?P_id", DbType.Int32, this._oEntrada.Id);
            GenericDataAccess.AddInParameter(this.comm, "?P_id_bodega", DbType.Int32, this._oEntrada.Id_bodega);
            GenericDataAccess.AddInParameter(this.comm, "?P_folio", DbType.String, this._oEntrada.Folio);
            GenericDataAccess.AddInParameter(this.comm, "?P_folio_indice", DbType.String, this._oEntrada.Folio_indice);
            GenericDataAccess.AddInParameter(this.comm, "?P_fecha", DbType.DateTime, this._oEntrada.Fecha);
            if(this._oEntrada.Hora.Length == 0)
                GenericDataAccess.AddInParameter(this.comm, "?P_hora", DbType.String, DBNull.Value);
            else
                GenericDataAccess.AddInParameter(this.comm, "?P_hora", DbType.String, this._oEntrada.Hora);
            GenericDataAccess.AddInParameter(this.comm, "?P_id_cortina", DbType.Int32, this._oEntrada.Id_cortina);
            GenericDataAccess.AddInParameter(this.comm, "?P_id_cliente", DbType.Int32, this._oEntrada.Id_cliente);
            GenericDataAccess.AddInParameter(this.comm, "?P_referencia", DbType.String, this._oEntrada.Referencia);
            GenericDataAccess.AddInParameter(this.comm, "?P_origen", DbType.String, this._oEntrada.Origen);
            GenericDataAccess.AddInParameter(this.comm, "?P_mercancia", DbType.String, this._oEntrada.Mercancia);
            GenericDataAccess.AddInParameter(this.comm, "?P_sello", DbType.String, this._oEntrada.Sello);
            GenericDataAccess.AddInParameter(this.comm, "?P_talon", DbType.String, this._oEntrada.Talon);
            GenericDataAccess.AddInParameter(this.comm, "?P_id_custodia", DbType.Int32, this._oEntrada.Id_custodia);
            GenericDataAccess.AddInParameter(this.comm, "?P_operador", DbType.String, this._oEntrada.Operador);
            GenericDataAccess.AddInParameter(this.comm, "?P_no_caja_cinta_aduanal", DbType.Int32, this._oEntrada.No_caja_cinta_aduanal);
            GenericDataAccess.AddInParameter(this.comm, "?P_no_pallet", DbType.Int32, this._oEntrada.No_pallet);
            GenericDataAccess.AddInParameter(this.comm, "?P_no_bulto_danado", DbType.Int32, this._oEntrada.No_bulto_danado);
            GenericDataAccess.AddInParameter(this.comm, "?P_no_bulto_abierto", DbType.Int32, this._oEntrada.No_bulto_abierto);
            GenericDataAccess.AddInParameter(this.comm, "?P_no_bulto_declarado", DbType.Int32, this._oEntrada.No_bulto_declarado);
            GenericDataAccess.AddInParameter(this.comm, "?P_no_pieza_declarada", DbType.Int32, this._oEntrada.No_pieza_declarada);
            GenericDataAccess.AddInParameter(this.comm, "?P_no_bulto_recibido", DbType.Int32, this._oEntrada.No_bulto_recibido);
            GenericDataAccess.AddInParameter(this.comm, "?P_no_pieza_recibida", DbType.Int32, this._oEntrada.No_pieza_recibida);
            GenericDataAccess.AddInParameter(this.comm, "?P_es_unica", DbType.Boolean, this._oEntrada.Es_unica);
            if(this._oEntrada.Hora_descarga.Length == 0)
                GenericDataAccess.AddInParameter(this.comm, "?P_hora_descarga", DbType.String, DBNull.Value);
            else
                GenericDataAccess.AddInParameter(this.comm, "?P_hora_descarga", DbType.String, this._oEntrada.Hora_descarga);
            GenericDataAccess.AddInParameter(this.comm, "?P_vigilante", DbType.String, this._oEntrada.Vigilante);
            GenericDataAccess.AddInParameter(this.comm, "?P_observaciones", DbType.String, this._oEntrada.Observaciones);
            GenericDataAccess.AddInParameter(this.comm, "?P_motivo_cancelacion", DbType.String, this._oEntrada.Motivo_cancelacion);
            GenericDataAccess.AddInParameter(this.comm, "?P_codigo", DbType.String, this._oEntrada.Codigo);
            GenericDataAccess.AddInParameter(this.comm, "?P_id_tipo_carga", DbType.Int32, this._oEntrada.Id_tipo_carga);
        }

        public override void fillLst()
        {
            try
            {
                this.comm = GenericDataAccess.CreateCommandSP("sp_Entrada");
                addParameters(0);
                this.dt = GenericDataAccess.ExecuteSelectCommand(comm);
                this._lst = new List<Entrada>();
                foreach (DataRow dr in dt.Rows)
                {
                    Entrada o = new Entrada();
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
                    o.Hora = dr["hora"].ToString();
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
                    o.Origen = dr["origen"].ToString();
                    o.Mercancia = dr["mercancia"].ToString();
                    o.Sello = dr["sello"].ToString();
                    o.Talon = dr["talon"].ToString();
                    if (dr["id_custodia"] != DBNull.Value)
                    {
                        int.TryParse(dr["id_custodia"].ToString(), out entero);
                        o.Id_custodia = entero;
                        entero = 0;
                    }
                    o.Operador = dr["operador"].ToString();
                    if (dr["no_caja_cinta_aduanal"] != DBNull.Value)
                    {
                        int.TryParse(dr["no_caja_cinta_aduanal"].ToString(), out entero);
                        o.No_caja_cinta_aduanal = entero;
                        entero = 0;
                    }
                    if (dr["no_pallet"] != DBNull.Value)
                    {
                        int.TryParse(dr["no_pallet"].ToString(), out entero);
                        o.No_pallet = entero;
                        entero = 0;
                    }
                    if (dr["no_bulto_danado"] != DBNull.Value)
                    {
                        int.TryParse(dr["no_bulto_danado"].ToString(), out entero);
                        o.No_bulto_danado = entero;
                        entero = 0;
                    }
                    if (dr["no_bulto_abierto"] != DBNull.Value)
                    {
                        int.TryParse(dr["no_bulto_abierto"].ToString(), out entero);
                        o.No_bulto_abierto = entero;
                        entero = 0;
                    }
                    if (dr["no_bulto_declarado"] != DBNull.Value)
                    {
                        int.TryParse(dr["no_bulto_declarado"].ToString(), out entero);
                        o.No_bulto_declarado = entero;
                        entero = 0;
                    }
                    if (dr["no_pieza_declarada"] != DBNull.Value)
                    {
                        int.TryParse(dr["no_pieza_declarada"].ToString(), out entero);
                        o.No_pieza_declarada = entero;
                        entero = 0;
                    }
                    if (dr["no_bulto_recibido"] != DBNull.Value)
                    {
                        int.TryParse(dr["no_bulto_recibido"].ToString(), out entero);
                        o.No_bulto_recibido = entero;
                        entero = 0;
                    }
                    if (dr["no_pieza_recibida"] != DBNull.Value)
                    {
                        int.TryParse(dr["no_pieza_recibida"].ToString(), out entero);
                        o.No_pieza_recibida = entero;
                        entero = 0;
                    }
                    o.Hora_descarga = dr["hora_descarga"].ToString();
                    o.Vigilante = dr["vigilante"].ToString();
                    o.Observaciones = dr["observaciones"].ToString();
                    o.Codigo = dr["codigo"].ToString();
                    if (dr["id_tipo_carga"] != DBNull.Value)
                    {
                        int.TryParse(dr["id_tipo_carga"].ToString(), out entero);
                        o.Id_tipo_carga = entero;
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

        public override void selById()
        {
            try
            {
                this.comm = GenericDataAccess.CreateCommandSP("sp_Entrada");
                addParameters(1);
                this.dt = GenericDataAccess.ExecuteSelectCommand(comm);
                if (dt.Rows.Count == 1)
                {
                    DataRow dr = dt.Rows[0];
                    if (dr["id_bodega"] != DBNull.Value)
                    {
                        int.TryParse(dr["id_bodega"].ToString(), out entero);
                        this._oEntrada.Id_bodega = entero;
                        entero = 0;
                    }
                    this._oEntrada.Folio = dr["folio"].ToString();
                    this._oEntrada.Folio_indice = dr["folio_indice"].ToString();
                    if (dr["fecha"] != DBNull.Value)
                    {
                        DateTime.TryParse(dr["fecha"].ToString(), out fecha);
                        this._oEntrada.Fecha = fecha;
                        fecha = new DateTime(1, 1, 1);
                    }
                    this._oEntrada.Hora = dr["hora"].ToString();
                    if (dr["id_cortina"] != DBNull.Value)
                    {
                        int.TryParse(dr["id_cortina"].ToString(), out entero);
                        this._oEntrada.Id_cortina = entero;
                        entero = 0;
                    }
                    if (dr["id_cliente"] != DBNull.Value)
                    {
                        int.TryParse(dr["id_cliente"].ToString(), out entero);
                        this._oEntrada.Id_cliente = entero;
                        entero = 0;
                    }
                    this._oEntrada.Referencia = dr["referencia"].ToString(); 
                    this._oEntrada.Origen = dr["origen"].ToString();
                    this._oEntrada.Mercancia = dr["mercancia"].ToString();
                    this._oEntrada.Sello = dr["sello"].ToString();
                    this._oEntrada.Talon = dr["talon"].ToString();
                    if (dr["id_custodia"] != DBNull.Value)
                    {
                        int.TryParse(dr["id_custodia"].ToString(), out entero);
                        this._oEntrada.Id_custodia = entero;
                        entero = 0;
                    }
                    this._oEntrada.Operador = dr["operador"].ToString();
                    if (dr["no_caja_cinta_aduanal"] != DBNull.Value)
                    {
                        int.TryParse(dr["no_caja_cinta_aduanal"].ToString(), out entero);
                        this._oEntrada.No_caja_cinta_aduanal = entero;
                        entero = 0;
                    }
                    if (dr["no_pallet"] != DBNull.Value)
                    {
                        int.TryParse(dr["no_pallet"].ToString(), out entero);
                        this._oEntrada.No_pallet = entero;
                        entero = 0;
                    }
                    if (dr["no_bulto_danado"] != DBNull.Value)
                    {
                        int.TryParse(dr["no_bulto_danado"].ToString(), out entero);
                        this._oEntrada.No_bulto_danado = entero;
                        entero = 0;
                    }
                    if (dr["no_bulto_abierto"] != DBNull.Value)
                    {
                        int.TryParse(dr["no_bulto_abierto"].ToString(), out entero);
                        this._oEntrada.No_bulto_abierto = entero;
                        entero = 0;
                    }
                    if (dr["no_bulto_declarado"] != DBNull.Value)
                    {
                        int.TryParse(dr["no_bulto_declarado"].ToString(), out entero);
                        this._oEntrada.No_bulto_declarado = entero;
                        entero = 0;
                    }
                    if (dr["no_pieza_declarada"] != DBNull.Value)
                    {
                        int.TryParse(dr["no_pieza_declarada"].ToString(), out entero);
                        this._oEntrada.No_pieza_declarada = entero;
                        entero = 0;
                    }
                    if (dr["no_bulto_recibido"] != DBNull.Value)
                    {
                        int.TryParse(dr["no_bulto_recibido"].ToString(), out entero);
                        this._oEntrada.No_bulto_recibido = entero;
                        entero = 0;
                    }
                    if (dr["no_pieza_recibida"] != DBNull.Value)
                    {
                        int.TryParse(dr["no_pieza_recibida"].ToString(), out entero);
                        this._oEntrada.No_pieza_recibida = entero;
                        entero = 0;
                    }
                    this._oEntrada.Hora_descarga = dr["hora_descarga"].ToString();
                    this._oEntrada.Vigilante = dr["vigilante"].ToString();
                    this._oEntrada.Observaciones = dr["observaciones"].ToString();
                    this._oEntrada.Codigo = dr["codigo"].ToString();
                    if (dr["id_tipo_carga"] != DBNull.Value)
                    {
                        int.TryParse(dr["id_tipo_carga"].ToString(), out entero);
                        this._oEntrada.Id_tipo_carga = entero;
                        entero = 0;
                    }
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
                this.comm = GenericDataAccess.CreateCommandSP("sp_Entrada");
                addParameters(2);
                GenericDataAccess.ExecuteNonQuery(this.comm, trans);
                this._oEntrada.Id = Convert.ToInt32(GenericDataAccess.getParameterValue(comm, "?P_id"));
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
                this.comm = GenericDataAccess.CreateCommandSP("sp_Entrada");
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
            
        }

        /// <summary>
        /// Cancelacion parcial
        /// Solo se cancela el folio al que se hace referencia.
        /// </summary>
        public void PartialCancel()
        {
            try
            {
                this.comm = GenericDataAccess.CreateCommandSP("sp_Entrada");
                addParameters(4);
                GenericDataAccess.ExecuteNonQuery(this.comm);
            }
            catch
            {
                throw;
            }
        }

        public List<IDocumentoEncontrado> fillLstByFolio()
        {
            List<IDocumentoEncontrado> lstDocEnc = new List<IDocumentoEncontrado>();
            try
            {
                this.comm = GenericDataAccess.CreateCommandSP("sp_Entrada");
                addParameters(5);
                this.dt = GenericDataAccess.ExecuteSelectCommand(comm);
                foreach (DataRow dr in dt.Rows)
                {
                    IDocumentoEncontrado o = new Entrada();
                    int.TryParse(dr["id"].ToString(), out entero);
                    o.IdEntrada = entero;
                    entero = 0;

                    string folio = dr["folio"].ToString();
                    string indice = dr["folio_indice"].ToString();

                    o.FolioEntrada = folio + indice;
                    lstDocEnc.Add(o);
                }
            }
            catch
            {
                throw;
            }
            return lstDocEnc;
        }

        /// <summary>
        /// Devuelve verdadero si una entrada ya ha sido capturada con base en su referencia
        /// </summary>
        /// <returns></returns>
        public bool Exists()
        {
            bool Exist = false;
            try
            {
                this.comm = GenericDataAccess.CreateCommandSP("sp_Entrada");
                addParameters(6);
                this.dt = GenericDataAccess.ExecuteSelectCommand(comm);
                if (dt.Rows.Count >= 1)
                {
                    Exist = true;
                    DataRow dr = dt.Rows[0];
                    
                    this._oEntrada.Folio = dr["folio"].ToString();
                    this._oEntrada.Folio_indice = dr["folio_indice"].ToString();
                    this._oEntrada.FolioEntrada = this._oEntrada.Folio + this._oEntrada.Folio_indice;
                }

            }
            catch
            {
                throw;
            }
            return Exist;
        }

        /// <summary>
        /// devuelve verdadero si una entrada es parcial con base en su referencia.
        /// </summary>
        /// <returns></returns>
        public bool IsPartial()
        {
            bool Exist = false;
            try
            {
                this.comm = GenericDataAccess.CreateCommandSP("sp_Entrada");
                addParameters(7);
                this.dt = GenericDataAccess.ExecuteSelectCommand(comm);
                if (dt.Rows.Count >= 1)
                {
                    Exist = true;
                    DataRow dr = dt.Rows[0];

                    this._oEntrada.Folio = dr["folio"].ToString();
                    this._oEntrada.Folio_indice = dr["folio_indice"].ToString();
                    this._oEntrada.FolioEntrada = this._oEntrada.Folio + this._oEntrada.Folio_indice;
                }

            }
            catch
            {
                throw;
            }
            return Exist;
        }

        /// <summary>
        /// Selecciona activa e inactiva
        /// </summary>
        public void selByIdEvenInactive()
        {
            try
            {
                this.comm = GenericDataAccess.CreateCommandSP("sp_Entrada");
                addParameters(8);
                this.dt = GenericDataAccess.ExecuteSelectCommand(comm);
                if (dt.Rows.Count == 1)
                {
                    DataRow dr = dt.Rows[0];
                    if (dr["id_bodega"] != DBNull.Value)
                    {
                        int.TryParse(dr["id_bodega"].ToString(), out entero);
                        this._oEntrada.Id_bodega = entero;
                        entero = 0;
                    }
                    this._oEntrada.Folio = dr["folio"].ToString();
                    this._oEntrada.Folio_indice = dr["folio_indice"].ToString();
                    if (dr["fecha"] != DBNull.Value)
                    {
                        DateTime.TryParse(dr["fecha"].ToString(), out fecha);
                        this._oEntrada.Fecha = fecha;
                        fecha = new DateTime(1, 1, 1);
                    }
                    this._oEntrada.Hora = dr["hora"].ToString();
                    if (dr["id_cortina"] != DBNull.Value)
                    {
                        int.TryParse(dr["id_cortina"].ToString(), out entero);
                        this._oEntrada.Id_cortina = entero;
                        entero = 0;
                    }
                    if (dr["id_cliente"] != DBNull.Value)
                    {
                        int.TryParse(dr["id_cliente"].ToString(), out entero);
                        this._oEntrada.Id_cliente = entero;
                        entero = 0;
                    }
                    this._oEntrada.Referencia = dr["referencia"].ToString();
                    this._oEntrada.Origen = dr["origen"].ToString();
                    this._oEntrada.Mercancia = dr["mercancia"].ToString();
                    this._oEntrada.Sello = dr["sello"].ToString();
                    this._oEntrada.Talon = dr["talon"].ToString();
                    if (dr["id_custodia"] != DBNull.Value)
                    {
                        int.TryParse(dr["id_custodia"].ToString(), out entero);
                        this._oEntrada.Id_custodia = entero;
                        entero = 0;
                    }
                    this._oEntrada.Operador = dr["operador"].ToString();
                    if (dr["no_caja_cinta_aduanal"] != DBNull.Value)
                    {
                        int.TryParse(dr["no_caja_cinta_aduanal"].ToString(), out entero);
                        this._oEntrada.No_caja_cinta_aduanal = entero;
                        entero = 0;
                    }
                    if (dr["no_pallet"] != DBNull.Value)
                    {
                        int.TryParse(dr["no_pallet"].ToString(), out entero);
                        this._oEntrada.No_pallet = entero;
                        entero = 0;
                    }
                    if (dr["no_bulto_danado"] != DBNull.Value)
                    {
                        int.TryParse(dr["no_bulto_danado"].ToString(), out entero);
                        this._oEntrada.No_bulto_danado = entero;
                        entero = 0;
                    }
                    if (dr["no_bulto_abierto"] != DBNull.Value)
                    {
                        int.TryParse(dr["no_bulto_abierto"].ToString(), out entero);
                        this._oEntrada.No_bulto_abierto = entero;
                        entero = 0;
                    }
                    if (dr["no_bulto_declarado"] != DBNull.Value)
                    {
                        int.TryParse(dr["no_bulto_declarado"].ToString(), out entero);
                        this._oEntrada.No_bulto_declarado = entero;
                        entero = 0;
                    }
                    if (dr["no_pieza_declarada"] != DBNull.Value)
                    {
                        int.TryParse(dr["no_pieza_declarada"].ToString(), out entero);
                        this._oEntrada.No_pieza_declarada = entero;
                        entero = 0;
                    }
                    if (dr["no_bulto_recibido"] != DBNull.Value)
                    {
                        int.TryParse(dr["no_bulto_recibido"].ToString(), out entero);
                        this._oEntrada.No_bulto_recibido = entero;
                        entero = 0;
                    }
                    this._oEntrada.Es_unica = Convert.ToBoolean(dr["es_unica"].ToString());
                    if (dr["no_pieza_recibida"] != DBNull.Value)
                    {
                        int.TryParse(dr["no_pieza_recibida"].ToString(), out entero);
                        this._oEntrada.No_pieza_recibida = entero;
                        entero = 0;
                    }
                    this._oEntrada.Hora_descarga = dr["hora_descarga"].ToString();
                    this._oEntrada.Vigilante = dr["vigilante"].ToString();
                    this._oEntrada.Observaciones = dr["observaciones"].ToString();
                    this._oEntrada.Codigo = dr["codigo"].ToString();
                    if (dr["id_tipo_carga"] != DBNull.Value)
                    {
                        int.TryParse(dr["id_tipo_carga"].ToString(), out entero);
                        this._oEntrada.Id_tipo_carga = entero;
                        entero = 0;
                    }
                    if (dr["IsActive"] != DBNull.Value)
                    {
                        bool.TryParse(dr["IsActive"].ToString(), out logica);
                        this._oEntrada.IsActive = logica;
                        logica = false;
                    }
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

        internal void searchByFolioPedimento()
        {
            try
            {
                this.comm = GenericDataAccess.CreateCommandSP("sp_Entrada");
                addParameters(9);
                this.dt = GenericDataAccess.ExecuteSelectCommand(comm);
                this._lst = new List<Entrada>();
                foreach (DataRow dr in dt.Rows)
                {
                    Entrada o = new Entrada();
                    int.TryParse(dr["id"].ToString(), out entero);
                    o.Id = entero;
                    entero = 0;
                    o.Folio = dr["folio"].ToString();
                    o.Folio_indice = dr["folio_indice"].ToString();
                    o.Folio = o.Folio + o.Folio_indice;
                    o.Referencia = dr["referencia"].ToString();

                    int.TryParse(dr["confondeo"].ToString(), out entero);
                    o.ConFondeo = entero > 0;
                    entero = 0;

                    this._lst.Add(o);
                }
            }
            catch
            {
                throw;
            }
        }

        #endregion
    }
}
