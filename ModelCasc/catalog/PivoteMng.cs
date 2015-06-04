using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using Model;

namespace ModelCasc.catalog
{
    internal class PivoteMng: dbTable
    {
        #region Campos
        protected Pivote _oPivote;
        protected List<Pivote> _lst;
        #endregion

        #region Propiedades
        public Pivote O_Pivote { get { return _oPivote; } set { _oPivote = value; } }
        public List<Pivote> Lst { get { return _lst; } set { _lst = value; } }
        #endregion

        #region Constructores
        public PivoteMng()
        {
            this._oPivote = new Pivote();
            this._lst = new List<Pivote>();
        }
        #endregion

        #region Metodos
        protected override void addParameters(int opcion)
        {
            GenericDataAccess.AddInParameter(this.comm, "?P_opcion", DbType.Int32, opcion);
            GenericDataAccess.AddInOutParameter(this.comm, "?P_id", DbType.Int32, this._oPivote.Id);
            GenericDataAccess.AddInParameter(this.comm, "?P_campo", DbType.String, this._oPivote.Campo);
            GenericDataAccess.AddInParameter(this.comm, "?P_tipo", DbType.String, this._oPivote.Tipo);
            GenericDataAccess.AddInParameter(this.comm, "?P_campoxls", DbType.String, this._oPivote.Campoxls);
            GenericDataAccess.AddInParameter(this.comm, "?P_requerido", DbType.Boolean, this._oPivote.Requerido);
            GenericDataAccess.AddInParameter(this.comm, "?P_campotbl", DbType.String, this._oPivote.Campotbl);
        }

        protected void BindByDataRow(DataRow dr, Pivote o)
        {
            try
            {
                int.TryParse(dr["id"].ToString(), out entero);
                o.Id = entero;
                entero = 0;
                o.Campo = dr["campo"].ToString();
                o.Tipo = dr["tipo"].ToString();
                o.Campoxls = dr["campoxls"].ToString();
                if (dr["requerido"] != DBNull.Value)
                {
                    bool.TryParse(dr["requerido"].ToString(), out logica);
                    o.Requerido = logica;
                    logica = false;
                }
                o.Campotbl = dr["campotbl"].ToString();
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
                this.comm = GenericDataAccess.CreateCommandSP("sp_Pivote");
                addParameters(0);
                this.dt = GenericDataAccess.ExecuteSelectCommand(comm);
                this._lst = new List<Pivote>();
                foreach (DataRow dr in dt.Rows)
                {
                    Pivote o = new Pivote();
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
                this.comm = GenericDataAccess.CreateCommandSP("sp_Pivote");
                addParameters(1);
                this.dt = GenericDataAccess.ExecuteSelectCommand(comm);
                if (dt.Rows.Count == 1)
                {
                    DataRow dr = dt.Rows[0];
                    BindByDataRow(dr, this._oPivote);
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
                this.comm = GenericDataAccess.CreateCommandSP("sp_Pivote");
                addParameters(2);
                GenericDataAccess.ExecuteNonQuery(this.comm);
                this._oPivote.Id = Convert.ToInt32(GenericDataAccess.getParameterValue(comm, "?P_id"));
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
                this.comm = GenericDataAccess.CreateCommandSP("sp_Pivote");
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
                this.comm = GenericDataAccess.CreateCommandSP("sp_Pivote");
                addParameters(4);
                GenericDataAccess.ExecuteNonQuery(this.comm);
            }
            catch
            {
                throw;
            }
        }

        #endregion
    }
}
