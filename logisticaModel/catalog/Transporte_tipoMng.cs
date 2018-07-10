using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using Model;

namespace logisticaModel.catalog
{
    internal class Transporte_tipoMng: Crud
    {
        #region Campos
        protected Transporte_tipo _oTransporte_tipo;
        protected List<Transporte_tipo> _lst;
        #endregion

        #region Propiedades
        public Transporte_tipo O_Transporte_tipo { get { return _oTransporte_tipo; } set { _oTransporte_tipo = value; } }
        public List<Transporte_tipo> Lst { get { return _lst; } set { _lst = value; } }
        #endregion

        #region Constructores
        public Transporte_tipoMng()
        {
            this._oTransporte_tipo = new Transporte_tipo();
            this._lst = new List<Transporte_tipo>();
        }
        #endregion

        #region Metodos
        protected override void addParameters(int opcion)
        {
            GenericDataAccess.AddInParameter(this.comm, "?P_opcion", DbType.Int32, opcion);
            GenericDataAccess.AddInOutParameter(this.comm, "?P_id", DbType.Int32, this._oTransporte_tipo.Id);
            GenericDataAccess.AddInParameter(this.comm, "?P_nombre", DbType.String, this._oTransporte_tipo.Nombre);
            GenericDataAccess.AddInParameter(this.comm, "?P_peso_maximo", DbType.Int32, this._oTransporte_tipo.Peso_maximo);
            GenericDataAccess.AddInParameter(this.comm, "?P_requiere_placa", DbType.Boolean, this._oTransporte_tipo.Requiere_placa);
            GenericDataAccess.AddInParameter(this.comm, "?P_requiere_caja1", DbType.Boolean, this._oTransporte_tipo.Requiere_caja1);
            GenericDataAccess.AddInParameter(this.comm, "?P_requiere_caja2", DbType.Boolean, this._oTransporte_tipo.Requiere_caja2);
            GenericDataAccess.AddInParameter(this.comm, "?P_requiere_caja3", DbType.Boolean, this._oTransporte_tipo.Requiere_caja3);
        }

        protected void BindByDataRow(DataRow dr, Transporte_tipo o)
        {
            try
            {
                int.TryParse(dr["id"].ToString(), out entero);
                o.Id = entero;
                entero = 0;
                int.TryParse(dr["id"].ToString(), out entero);
                o.Id = entero;
                entero = 0;
                o.Nombre = dr["nombre"].ToString();
                if (dr["peso_maximo"] != DBNull.Value)
                {
                    int.TryParse(dr["peso_maximo"].ToString(), out entero);
                    o.Peso_maximo = entero;
                    entero = 0;
                }
                if (dr["requiere_placa"] != DBNull.Value)
                {
                    bool.TryParse(dr["requiere_placa"].ToString(), out logica);
                    o.Requiere_placa = logica;
                    logica = false;
                }
                if (dr["requiere_caja1"] != DBNull.Value)
                {
                    bool.TryParse(dr["requiere_caja1"].ToString(), out logica);
                    o.Requiere_caja1 = logica;
                    logica = false;
                }
                if (dr["requiere_caja2"] != DBNull.Value)
                {
                    bool.TryParse(dr["requiere_caja2"].ToString(), out logica);
                    o.Requiere_caja2 = logica;
                    logica = false;
                }
                if (dr["requiere_caja3"] != DBNull.Value)
                {
                    bool.TryParse(dr["requiere_caja3"].ToString(), out logica);
                    o.Requiere_caja3 = logica;
                    logica = false;
                }
                if (dr["IsActive"] != DBNull.Value)
                {
                    bool.TryParse(dr["IsActive"].ToString(), out logica);
                    o.IsActive = logica;
                    logica = false;
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
                this.comm = GenericDataAccess.CreateCommandSP("sp_Transporte_tipo");
                addParameters(0);
                if (trans == null)
                    this.dt = GenericDataAccess.ExecuteSelectCommand(comm);
                else
                    this.dt = GenericDataAccess.ExecuteSelectCommand(comm, trans);
                this._lst = new List<Transporte_tipo>();
                foreach (DataRow dr in dt.Rows)
                {
                    Transporte_tipo o = new Transporte_tipo();
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
                this.comm = GenericDataAccess.CreateCommandSP("sp_Transporte_tipo");
                addParameters(1);
                if (trans == null)
                    this.dt = GenericDataAccess.ExecuteSelectCommand(comm);
                else
                    this.dt = GenericDataAccess.ExecuteSelectCommand(comm, trans);
                if (dt.Rows.Count == 1)
                {
                    DataRow dr = dt.Rows[0];
                    BindByDataRow(dr, this._oTransporte_tipo);
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
                this.comm = GenericDataAccess.CreateCommandSP("sp_Transporte_tipo");
                addParameters(2);
                if (trans == null)
                    GenericDataAccess.ExecuteNonQuery(this.comm);
                else
                    GenericDataAccess.ExecuteNonQuery(this.comm, trans);
                this._oTransporte_tipo.Id = Convert.ToInt32(GenericDataAccess.getParameterValue(comm, "?P_id"));
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
                this.comm = GenericDataAccess.CreateCommandSP("sp_Transporte_tipo");
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
                this.comm = GenericDataAccess.CreateCommandSP("sp_Transporte_tipo");
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

        public void active(IDbTransaction trans = null)
        {
            try
            {
                this.comm = GenericDataAccess.CreateCommandSP("sp_Transporte_tipo");
                addParameters(5);
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


        public void fillAllLst(IDbTransaction trans = null)
        {
            try
            {
                this.comm = GenericDataAccess.CreateCommandSP("sp_Transporte_tipo");
                addParameters(6);
                if (trans == null)
                    this.dt = GenericDataAccess.ExecuteSelectCommand(comm);
                else
                    this.dt = GenericDataAccess.ExecuteSelectCommand(comm, trans);
                this._lst = new List<Transporte_tipo>();
                foreach (DataRow dr in dt.Rows)
                {
                    Transporte_tipo o = new Transporte_tipo();
                    BindByDataRow(dr, o);
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
