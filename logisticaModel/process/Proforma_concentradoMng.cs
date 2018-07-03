using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using Model;

namespace logisticaModel.process
{
    internal class Proforma_concentradoMng: Crud
    {
        #region Campos
        protected Proforma_concentrado _oProforma_concentrado;
        protected List<Proforma_concentrado> _lst;
        #endregion

        #region Propiedades
        public Proforma_concentrado O_Proforma_concentrado { get { return _oProforma_concentrado; } set { _oProforma_concentrado = value; } }
        public List<Proforma_concentrado> Lst { get { return _lst; } set { _lst = value; } }
        #endregion

        #region Constructores
        public Proforma_concentradoMng()
        {
            this._oProforma_concentrado = new Proforma_concentrado();
            this._lst = new List<Proforma_concentrado>();
        }
        #endregion

        #region Metodos
        protected override void addParameters(int opcion)
        {
            GenericDataAccess.AddInParameter(this.comm, "?P_opcion", DbType.Int32, opcion);
            GenericDataAccess.AddInParameter(this.comm, "?P_cliente", DbType.String, this._oProforma_concentrado.Cliente);
            GenericDataAccess.AddInParameter(this.comm, "?P_id_cliente", DbType.Int32, this._oProforma_concentrado.Id_cliente);
            GenericDataAccess.AddInParameter(this.comm, "?P_id_servicio", DbType.Int32, this._oProforma_concentrado.Id_servicio);
            GenericDataAccess.AddInParameter(this.comm, "?P_fecha_servicio", DbType.DateTime, this._oProforma_concentrado.Fecha_servicio);
            GenericDataAccess.AddInParameter(this.comm, "?P_nombre_servicio", DbType.String, this._oProforma_concentrado.Nombre_servicio);
            GenericDataAccess.AddInParameter(this.comm, "?P_cantidad", DbType.Int32, this._oProforma_concentrado.Cantidad);
            GenericDataAccess.AddInParameter(this.comm, "?P_tarifa", DbType.Decimal, this._oProforma_concentrado.Tarifa);
            GenericDataAccess.AddInParameter(this.comm, "?P_total", DbType.Decimal, this._oProforma_concentrado.Total);
            GenericDataAccess.AddInParameter(this.comm, "?P_folio_aplicada", DbType.String, this._oProforma_concentrado.Folio_aplicada);
        }

        protected void BindByDataRow(DataRow dr, Proforma_concentrado o)
        {
            try
            {
                o.Cliente = dr["cliente"].ToString();
                if (dr["id_cliente"] != DBNull.Value)
                {
                    int.TryParse(dr["id_cliente"].ToString(), out entero);
                    o.Id_cliente = entero;
                    entero = 0;
                }
                if (dr["id_servicio"] != DBNull.Value)
                {
                    int.TryParse(dr["id_servicio"].ToString(), out entero);
                    o.Id_servicio = entero;
                    entero = 0;
                }
                if (dr["fecha_servicio"] != DBNull.Value)
                {
                    DateTime.TryParse(dr["fecha_servicio"].ToString(), out fecha);
                    o.Fecha_servicio = fecha;
                    fecha = default(DateTime);
                }
                o.Nombre_servicio = dr["nombre_servicio"].ToString();
                if (dr["cantidad"] != DBNull.Value)
                {
                    int.TryParse(dr["cantidad"].ToString(), out entero);
                    o.Cantidad = entero;
                    entero = 0;
                }
                if (dr["tarifa"] != DBNull.Value)
                {
                    float.TryParse(dr["tarifa"].ToString(), out flotante);
                    o.Tarifa = flotante;
                    flotante = 0;
                }
                if (dr["total"] != DBNull.Value)
                {
                    float.TryParse(dr["total"].ToString(), out flotante);
                    o.Total = flotante;
                    flotante = 0;
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
                this.comm = GenericDataAccess.CreateCommandSP("sp_Proforma_concentrado");
                addParameters(0);
                if (trans == null)
                    this.dt = GenericDataAccess.ExecuteSelectCommand(this.comm);
                else
                    this.dt = GenericDataAccess.ExecuteSelectCommand(this.comm, trans);
                this._lst = new List<Proforma_concentrado>();
                foreach (DataRow dr in dt.Rows)
                {
                    Proforma_concentrado o = new Proforma_concentrado();
                    BindByDataRow(dr, o);
                    if (dr["min_fecha"] != DBNull.Value)
                    {
                        DateTime.TryParse(dr["min_fecha"].ToString(), out fecha);
                        o.Fecha_serv_min = fecha;
                        fecha = default(DateTime);
                    }
                    if (dr["max_fecha"] != DBNull.Value)
                    {
                        DateTime.TryParse(dr["max_fecha"].ToString(), out fecha);
                        o.Fecha_serv_max = fecha;
                        fecha = default(DateTime);
                    }
                    this._lst.Add(o);
                }
            }
            catch
            {
                throw;
            }
        }

        public void fillLstCte(IDbTransaction trans = null)
        {
            try
            {
                this.comm = GenericDataAccess.CreateCommandSP("sp_Proforma_concentrado");
                addParameters(1);
                if (trans == null)
                    this.dt = GenericDataAccess.ExecuteSelectCommand(this.comm);
                else
                    this.dt = GenericDataAccess.ExecuteSelectCommand(this.comm, trans);
                this._lst = new List<Proforma_concentrado>();
                foreach (DataRow dr in dt.Rows)
                {
                    Proforma_concentrado o = new Proforma_concentrado();
                    BindByDataRow(dr, o);
                    if (dr["min_fecha"] != DBNull.Value)
                    {
                        DateTime.TryParse(dr["min_fecha"].ToString(), out fecha);
                        o.Fecha_serv_min = fecha;
                        fecha = default(DateTime);
                    }
                    if (dr["max_fecha"] != DBNull.Value)
                    {
                        DateTime.TryParse(dr["max_fecha"].ToString(), out fecha);
                        o.Fecha_serv_max = fecha;
                        fecha = default(DateTime);
                    }
                    this._lst.Add(o);
                }
            }
            catch
            {
                throw;
            }
        }

        public void fillAllCteLst(IDbTransaction trans = null)
        {
            try
            {
                this.comm = GenericDataAccess.CreateCommandSP("sp_Proforma_concentrado");
                addParameters(0);
                if (trans == null)
                    this.dt = GenericDataAccess.ExecuteSelectCommand(this.comm);
                else
                    this.dt = GenericDataAccess.ExecuteSelectCommand(this.comm, trans);
                this._lst = new List<Proforma_concentrado>();
                foreach (DataRow dr in dt.Rows)
                {
                    Proforma_concentrado o = new Proforma_concentrado();
                    BindByDataRow(dr, o);
                    this._lst.Add(o);
                }
            }
            catch
            {
                throw;
            }
        }

        public void udtActiva(IDbTransaction trans = null)
        {
            try
            {
                this.comm = GenericDataAccess.CreateCommandSP("sp_Proforma_concentrado");
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

        internal void fillActByFolio(IDbTransaction trans = null)
        {
            try
            {
                this.comm = GenericDataAccess.CreateCommandSP("sp_Proforma_concentrado");
                addParameters(4);
                if (trans == null)
                    this.dt = GenericDataAccess.ExecuteSelectCommand(this.comm);
                else
                    this.dt = GenericDataAccess.ExecuteSelectCommand(this.comm, trans);
                this._lst = new List<Proforma_concentrado>();
                foreach (DataRow dr in dt.Rows)
                {
                    Proforma_concentrado o = new Proforma_concentrado();
                    BindByDataRow(dr, o);
                    if (dr["min_fecha"] != DBNull.Value)
                    {
                        DateTime.TryParse(dr["min_fecha"].ToString(), out fecha);
                        o.Fecha_serv_min = fecha;
                        fecha = default(DateTime);
                    }
                    if (dr["max_fecha"] != DBNull.Value)
                    {
                        DateTime.TryParse(dr["max_fecha"].ToString(), out fecha);
                        o.Fecha_serv_max = fecha;
                        fecha = default(DateTime);
                    }
                    this._lst.Add(o);
                }
            }
            catch
            {
                throw;
            }
        }

        public void fillLstAplicada(IDbTransaction trans = null)
        {
            try
            {
                this.comm = GenericDataAccess.CreateCommandSP("sp_Proforma_concentrado");
                addParameters(5);
                if (trans == null)
                    this.dt = GenericDataAccess.ExecuteSelectCommand(this.comm);
                else
                    this.dt = GenericDataAccess.ExecuteSelectCommand(this.comm, trans);
                this._lst = new List<Proforma_concentrado>();
                foreach (DataRow dr in dt.Rows)
                {
                    Proforma_concentrado o = new Proforma_concentrado();
                    BindByDataRow(dr, o);
                    if (dr["min_fecha"] != DBNull.Value)
                    {
                        DateTime.TryParse(dr["min_fecha"].ToString(), out fecha);
                        o.Fecha_serv_min = fecha;
                        fecha = default(DateTime);
                    }
                    if (dr["max_fecha"] != DBNull.Value)
                    {
                        DateTime.TryParse(dr["max_fecha"].ToString(), out fecha);
                        o.Fecha_serv_max = fecha;
                        fecha = default(DateTime);
                    }
                    this._lst.Add(o);
                }
            }
            catch
            {
                throw;
            }
        }

        internal void fillLstCteApp(IDbTransaction trans = null)
        {
            try
            {
                this.comm = GenericDataAccess.CreateCommandSP("sp_Proforma_concentrado");
                addParameters(6);
                if (trans == null)
                    this.dt = GenericDataAccess.ExecuteSelectCommand(this.comm);
                else
                    this.dt = GenericDataAccess.ExecuteSelectCommand(this.comm, trans);
                this._lst = new List<Proforma_concentrado>();
                foreach (DataRow dr in dt.Rows)
                {
                    Proforma_concentrado o = new Proforma_concentrado();
                    BindByDataRow(dr, o);
                    if (dr["min_fecha"] != DBNull.Value)
                    {
                        DateTime.TryParse(dr["min_fecha"].ToString(), out fecha);
                        o.Fecha_serv_min = fecha;
                        fecha = default(DateTime);
                    }
                    if (dr["max_fecha"] != DBNull.Value)
                    {
                        DateTime.TryParse(dr["max_fecha"].ToString(), out fecha);
                        o.Fecha_serv_max = fecha;
                        fecha = default(DateTime);
                    }
                    this._lst.Add(o);
                }
            }
            catch
            {
                throw;
            }
        }

        #endregion

        public override void add(IDbTransaction trans)
        {
            throw new NotImplementedException();
        }

        public override void selById(IDbTransaction trans)
        {
            throw new NotImplementedException();
        }

        public override void udt(IDbTransaction trans)
        {
            throw new NotImplementedException();
        }

        public override void dlt(IDbTransaction trans)
        {
            throw new NotImplementedException();
        }
    }
}
