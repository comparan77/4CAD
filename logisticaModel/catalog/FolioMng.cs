using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using Model;

namespace logisticaModel.catalog
{
    internal class FolioMng: Crud
    {
        #region Campos
        protected Folio _oFolio;
        protected List<Folio> _lst;
        #endregion

        #region Propiedades
        public Folio O_Folio { get { return _oFolio; } set { _oFolio = value; } }
        public List<Folio> Lst { get { return _lst; } set { _lst = value; } }
        #endregion

        #region Constructores
        public FolioMng()
        {
            this._oFolio = new Folio();
            this._lst = new List<Folio>();
        }
        #endregion

        #region Metodos
        protected override void addParameters(int opcion)
        {
            GenericDataAccess.AddInParameter(this.comm, "?P_opcion", DbType.Int32, opcion);
            GenericDataAccess.AddInOutParameter(this.comm, "?P_id", DbType.Int32, this._oFolio.Id);
            GenericDataAccess.AddInParameter(this.comm, "?P_tipo", DbType.String, this._oFolio.Tipo);
            GenericDataAccess.AddInParameter(this.comm, "?P_anio_actual", DbType.Int32, this._oFolio.Anio_actual);
            GenericDataAccess.AddInParameter(this.comm, "?P_actual", DbType.Int32, this._oFolio.Actual);
            GenericDataAccess.AddInParameter(this.comm, "?P_digitos", DbType.Int32, this._oFolio.Digitos);
            GenericDataAccess.AddInParameter(this.comm, "?P_folio_inicial", DbType.Int32, this._oFolio.Folio_inicial);
        }

        protected void BindByDataRow(DataRow dr, Folio o)
        {
            try
            {
                int.TryParse(dr["id"].ToString(), out entero);
                o.Id = entero;
                entero = 0;
                o.Tipo = dr["tipo"].ToString();
                if (dr["anio_actual"] != DBNull.Value)
                {
                    int.TryParse(dr["anio_actual"].ToString(), out entero);
                    o.Anio_actual = entero;
                    entero = 0;
                }
                if (dr["actual"] != DBNull.Value)
                {
                    int.TryParse(dr["actual"].ToString(), out entero);
                    o.Actual = entero;
                    entero = 0;
                }
                if (dr["digitos"] != DBNull.Value)
                {
                    int.TryParse(dr["digitos"].ToString(), out entero);
                    o.Digitos = entero;
                    entero = 0;
                }
                if (dr["folio_inicial"] != DBNull.Value)
                {
                    int.TryParse(dr["folio_inicial"].ToString(), out entero);
                    o.Folio_inicial = entero;
                    entero = 0;
                }
                //if (dr["IsActive"] != DBNull.Value)
                //{
                //    bool.TryParse(dr["IsActive"].ToString(), out logica);
                //    o.IsActive = logica;
                //    logica = false;
                //}
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
                this.comm = GenericDataAccess.CreateCommandSP("sp_Folio");
                addParameters(0);
                if (trans == null)
                    this.dt = GenericDataAccess.ExecuteSelectCommand(comm);
                else
                    this.dt = GenericDataAccess.ExecuteSelectCommand(comm, trans);
                this._lst = new List<Folio>();
                foreach (DataRow dr in dt.Rows)
                {
                    Folio o = new Folio();
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
                this.comm = GenericDataAccess.CreateCommandSP("sp_Folio");
                addParameters(1);
                if (trans == null)
                    this.dt = GenericDataAccess.ExecuteSelectCommand(comm);
                else
                    this.dt = GenericDataAccess.ExecuteSelectCommand(comm, trans);
                if (dt.Rows.Count == 1)
                {
                    DataRow dr = dt.Rows[0];
                    BindByDataRow(dr, this._oFolio);
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
                this.comm = GenericDataAccess.CreateCommandSP("sp_Folio");
                addParameters(2);
                if (trans == null)
                    GenericDataAccess.ExecuteNonQuery(this.comm);
                else
                    GenericDataAccess.ExecuteNonQuery(this.comm, trans);
                this._oFolio.Id = Convert.ToInt32(GenericDataAccess.getParameterValue(comm, "?P_id"));
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
                this.comm = GenericDataAccess.CreateCommandSP("sp_Folio");
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
                this.comm = GenericDataAccess.CreateCommandSP("sp_Folio");
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

        public void getFolio(IDbTransaction trans)
        {
            try
            {
                this.comm = GenericDataAccess.CreateCommandSP("sp_Folio");
                addParameters(5);
                if (trans == null)
                    this.dt = GenericDataAccess.ExecuteSelectCommand(comm);
                else
                    this.dt = GenericDataAccess.ExecuteSelectCommand(comm, trans);
                if (dt.Rows.Count == 1)
                {
                    DataRow dr = dt.Rows[0];
                    BindByDataRow(dr, this._oFolio);
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

        #endregion
    }
}
