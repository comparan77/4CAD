using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Model;
using System.Data;

namespace ModelCasc.catalog
{
    public class FolioMng: dbTable
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

        public override void fillLst()
        {
            try
            {
                this.comm = GenericDataAccess.CreateCommandSP("sp_Folio");
                addParameters(0);
                this.dt = GenericDataAccess.ExecuteSelectCommand(comm);
                this._lst = new List<Folio>();
                foreach (DataRow dr in dt.Rows)
                {
                    Folio o = new Folio();
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
                this.comm = GenericDataAccess.CreateCommandSP("sp_Folio");
                addParameters(1);
                this.dt = GenericDataAccess.ExecuteSelectCommand(comm);
                if (dt.Rows.Count == 1)
                {
                    DataRow dr = dt.Rows[0];
                    this._oFolio.Tipo = dr["tipo"].ToString();
                    if (dr["anio_actual"] != DBNull.Value)
                    {
                        int.TryParse(dr["anio_actual"].ToString(), out entero);
                        this._oFolio.Anio_actual = entero;
                        entero = 0;
                    }
                    if (dr["actual"] != DBNull.Value)
                    {
                        int.TryParse(dr["actual"].ToString(), out entero);
                        this._oFolio.Actual = entero;
                        entero = 0;
                    }
                    if (dr["digitos"] != DBNull.Value)
                    {
                        int.TryParse(dr["digitos"].ToString(), out entero);
                        this._oFolio.Digitos = entero;
                        entero = 0;
                    }
                    if (dr["folio_inicial"] != DBNull.Value)
                    {
                        int.TryParse(dr["folio_inicial"].ToString(), out entero);
                        this._oFolio.Folio_inicial = entero;
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
            try
            {
                this.comm = GenericDataAccess.CreateCommandSP("sp_Folio");
                addParameters(2);
                GenericDataAccess.ExecuteNonQuery(this.comm);
                this._oFolio.Id = Convert.ToInt32(GenericDataAccess.getParameterValue(comm, "?P_id"));
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
                this.comm = GenericDataAccess.CreateCommandSP("sp_Folio");
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
                this.comm = GenericDataAccess.CreateCommandSP("sp_Folio");
                addParameters(4);
                GenericDataAccess.ExecuteNonQuery(this.comm);
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
                this.dt = GenericDataAccess.ExecuteSelectCommand(comm, trans);
                if (dt.Rows.Count == 1)
                {
                    DataRow dr = dt.Rows[0];
                    this._oFolio.Tipo = dr["tipo"].ToString();
                    if (dr["anio_actual"] != DBNull.Value)
                    {
                        int.TryParse(dr["anio_actual"].ToString(), out entero);
                        this._oFolio.Anio_actual = entero;
                        entero = 0;
                    }
                    if (dr["actual"] != DBNull.Value)
                    {
                        int.TryParse(dr["actual"].ToString(), out entero);
                        this._oFolio.Actual = entero;
                        entero = 0;
                    }
                    if (dr["digitos"] != DBNull.Value)
                    {
                        int.TryParse(dr["digitos"].ToString(), out entero);
                        this._oFolio.Digitos = entero;
                        entero = 0;
                    }
                    if (dr["folio_inicial"] != DBNull.Value)
                    {
                        int.TryParse(dr["folio_inicial"].ToString(), out entero);
                        this._oFolio.Folio_inicial = entero;
                        entero = 0;
                    }
                }
                else if (dt.Rows.Count > 1)
                    throw new Exception("Error de integridad");
                else
                    throw new Exception("No existe folios asigndos");
            }
            catch
            {
                throw;
            }
        }

        #endregion
    }
}
