using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using Model;

namespace logisticaModel.process
{
    internal class AsnMng: Crud
    {
        #region Campos
        protected Asn _oAsn;
        protected List<Asn> _lst;
        #endregion

        #region Propiedades
        public Asn O_Asn { get { return _oAsn; } set { _oAsn = value; } }
        public List<Asn> Lst { get { return _lst; } set { _lst = value; } }
        #endregion

        #region Constructores
        public AsnMng()
        {
            this._oAsn = new Asn();
            this._lst = new List<Asn>();
        }
        #endregion

        #region Metodos
        protected override void addParameters(int opcion)
        {
            GenericDataAccess.AddInParameter(this.comm, "?P_opcion", DbType.Int32, opcion);
            GenericDataAccess.AddInOutParameter(this.comm, "?P_id", DbType.Int32, this._oAsn.Id);
            GenericDataAccess.AddInParameter(this.comm, "?P_id_cliente", DbType.Int32, this._oAsn.Id_cliente);
            GenericDataAccess.AddInParameter(this.comm, "?P_folio", DbType.String, this._oAsn.Folio);
            GenericDataAccess.AddInParameter(this.comm, "?P_referencia", DbType.String, this._oAsn.Referencia);
            GenericDataAccess.AddInParameter(this.comm, "?P_id_bodega", DbType.Int32, this._oAsn.Id_bodega);
            GenericDataAccess.AddInParameter(this.comm, "?P_fecha_hora", DbType.DateTime, this._oAsn.Fecha_hora);
            GenericDataAccess.AddInParameter(this.comm, "?P_id_transporte", DbType.Int32, this._oAsn.Id_transporte);
            GenericDataAccess.AddInParameter(this.comm, "?P_sello", DbType.String, this._oAsn.Sello);
            GenericDataAccess.AddInParameter(this.comm, "?P_operador", DbType.String, this._oAsn.Operador);
            GenericDataAccess.AddInParameter(this.comm, "?P_pallet", DbType.Int32, this._oAsn.Pallet);
            GenericDataAccess.AddInParameter(this.comm, "?P_caja", DbType.Int32, this._oAsn.Caja);
            GenericDataAccess.AddInParameter(this.comm, "?P_pieza", DbType.Int32, this._oAsn.Pieza);
            GenericDataAccess.AddInParameter(this.comm, "?P_descargada", DbType.Boolean, this._oAsn.Descargada);
            GenericDataAccess.AddInParameter(this.comm, "?P_fecha_hora_descarga", DbType.DateTime, this._oAsn.Fecha_hora_descarga);
        }

        protected void BindByDataRow(DataRow dr, Asn o)
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
                o.Folio = dr["folio"].ToString();
                o.Referencia = dr["referencia"].ToString();
                if (dr["id_bodega"] != DBNull.Value)
                {
                    int.TryParse(dr["id_bodega"].ToString(), out entero);
                    o.Id_bodega = entero;
                    entero = 0;
                }
                else
                {
                    o.Id_bodega = null;
                }
                if (dr["fecha_hora"] != DBNull.Value)
                {
                    DateTime.TryParse(dr["fecha_hora"].ToString(), out fecha);
                    o.Fecha_hora = fecha;
                    fecha = default(DateTime);
                }
                else
                {
                    o.Fecha_hora = null;
                }
                if (dr["id_transporte"] != DBNull.Value)
                {
                    int.TryParse(dr["id_transporte"].ToString(), out entero);
                    o.Id_transporte = entero;
                    entero = 0;
                }
                else
                {
                    o.Id_transporte = null;
                }
                o.Sello = dr["sello"].ToString();
                o.Operador = dr["operador"].ToString();
                if (dr["pallet"] != DBNull.Value)
                {
                    int.TryParse(dr["pallet"].ToString(), out entero);
                    o.Pallet = entero;
                    entero = 0;
                }
                else
                {
                    o.Pallet = null;
                }
                if (dr["caja"] != DBNull.Value)
                {
                    int.TryParse(dr["caja"].ToString(), out entero);
                    o.Caja = entero;
                    entero = 0;
                }
                else
                {
                    o.Caja = null;
                }
                if (dr["pieza"] != DBNull.Value)
                {
                    int.TryParse(dr["pieza"].ToString(), out entero);
                    o.Pieza = entero;
                    entero = 0;
                }
                else
                {
                    o.Pieza = null;
                }
                if (dr["descargada"] != DBNull.Value)
                {
                    bool.TryParse(dr["descargada"].ToString(), out logica);
                    o.Descargada = logica;
                    logica = false;
                }
                if (dr["fecha_hora_descarga"] != DBNull.Value)
                {
                    DateTime.TryParse(dr["fecha_hora_descarga"].ToString(), out fecha);
                    o.Fecha_hora_descarga = fecha;
                    fecha = default(DateTime);
                }
                else
                {
                    o.Fecha_hora_descarga = null;
                }
            }
            catch
            {
                throw;
            }
        }

        public override void fillLst(IDbTransaction trans = null)
        {
            try
            {
                this.comm = GenericDataAccess.CreateCommandSP("sp_Asn");
                addParameters(0);
                if (trans == null)
                    this.dt = GenericDataAccess.ExecuteSelectCommand(comm);
                else
                    this.dt = GenericDataAccess.ExecuteSelectCommand(comm, trans);
                this._lst = new List<Asn>();
                foreach (DataRow dr in dt.Rows)
                {
                    Asn o = new Asn();
                    BindByDataRow(dr, o);
                    this._lst.Add(o);
                }
            }
            catch
            {
                throw;
            }
        }

        public override void selById(IDbTransaction trans = null)
        {
            try
            {
                this.comm = GenericDataAccess.CreateCommandSP("sp_Asn");
                addParameters(1);
                if (trans == null)
                    this.dt = GenericDataAccess.ExecuteSelectCommand(comm);
                else
                    this.dt = GenericDataAccess.ExecuteSelectCommand(comm, trans);
                if (dt.Rows.Count == 1)
                {
                    DataRow dr = dt.Rows[0];
                    BindByDataRow(dr, this._oAsn);
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

        public override void add(IDbTransaction trans = null)
        {
            try
            {
                this.comm = GenericDataAccess.CreateCommandSP("sp_Asn");
                addParameters(2);
                if (trans == null)
                    GenericDataAccess.ExecuteNonQuery(this.comm);
                else
                    GenericDataAccess.ExecuteNonQuery(this.comm, trans);
                this._oAsn.Id = Convert.ToInt32(GenericDataAccess.getParameterValue(comm, "?P_id"));
            }
            catch
            {
                throw;
            }
        }

        public override void udt(IDbTransaction trans = null)
        {
            try
            {
                this.comm = GenericDataAccess.CreateCommandSP("sp_Asn");
                addParameters(3);
                if (trans == null)
                    GenericDataAccess.ExecuteNonQuery(this.comm);
                else
                    GenericDataAccess.ExecuteNonQuery(this.comm, trans);
            }
            catch
            {
                throw;
            }
        }

        public override void dlt(IDbTransaction trans = null)
        {
            try
            {
                this.comm = GenericDataAccess.CreateCommandSP("sp_Asn");
                addParameters(4);
                if (trans == null)
                    GenericDataAccess.ExecuteNonQuery(this.comm);
                else
                    GenericDataAccess.ExecuteNonQuery(this.comm, trans);
            }
            catch
            {
                throw;
            }
        }

        public void getLstConcentrado()
        {
            try
            {
                this.comm = GenericDataAccess.CreateCommand(
                    "select a.id, a.folio, a.fecha_hora, c.nombre cliente, a.referencia, a.pallet, a.caja, a.pieza, a.descargada from asn a " +
                    "left join cliente c on c.id = a.id_cliente");

                this.dt = GenericDataAccess.ExecuteSelectCommand(this.comm);
                var qry =
                    from result in this.dt.AsEnumerable()
                    select new
                    {
                        id = result.Field<Int32>("id"),
                        folio = result.Field<string>("folio"),
                        fecha = result.Field<DateTime>("fecha_hora"),
                        cliente = result.Field<string>("cliente"),
                        referencia = result.Field<string>("referencia"),
                        pallet = result.Field<Int32>("pallet"),
                        caja = result.Field<Int32>("caja"),
                        pieza = result.Field<Int32>("pieza"),
                        descargada = result.Field<Boolean>("descargada")
                    };

                foreach (var item in qry)
                {
                    Asn o = new Asn()
                    {
                        Id = item.id,
                        Folio = item.folio,
                        Fecha_hora = item.fecha,
                        ClienteNombre = item.cliente,
                        Referencia = item.referencia,
                        Pallet = item.pallet,
                        Caja = item.caja,
                        Pieza = item.pieza,
                        Descargada = item.descargada
                    };
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
