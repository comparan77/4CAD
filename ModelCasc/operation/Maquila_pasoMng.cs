using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using Model;

namespace ModelCasc.operation
{
    internal class Maquila_pasoMng: dbTable
    {
        #region Campos
        protected Maquila_paso _oMaquila_paso;
        protected List<Maquila_paso> _lst;
        #endregion

        #region Propiedades
        public Maquila_paso O_Maquila_paso { get { return _oMaquila_paso; } set { _oMaquila_paso = value; } }
        public List<Maquila_paso> Lst { get { return _lst; } set { _lst = value; } }
        #endregion

        #region Constructores
        public Maquila_pasoMng()
        {
            this._oMaquila_paso = new Maquila_paso();
            this._lst = new List<Maquila_paso>();
        }
        #endregion

        #region Metodos
        protected override void addParameters(int opcion)
        {
            GenericDataAccess.AddInParameter(this.comm, "?P_opcion", DbType.Int32, opcion);
            GenericDataAccess.AddInOutParameter(this.comm, "?P_id", DbType.Int32, this._oMaquila_paso.Id);
            GenericDataAccess.AddInParameter(this.comm, "?P_id_ord_tbj_srv", DbType.Int32, this._oMaquila_paso.Id_ord_tbj_srv);
            GenericDataAccess.AddInParameter(this.comm, "?P_foto64", DbType.String, this._oMaquila_paso.Foto64);
            GenericDataAccess.AddInParameter(this.comm, "?P_descripcion", DbType.String, this._oMaquila_paso.Descripcion);
        }

        protected void BindByDataRow(DataRow dr, Maquila_paso o)
        {
            try
            {
                int.TryParse(dr["id"].ToString(), out entero);
                o.Id = entero;
                entero = 0;
                if (dr["id_ord_tbj_srv"] != DBNull.Value)
                {
                    int.TryParse(dr["id_ord_tbj_srv"].ToString(), out entero);
                    o.Id_ord_tbj_srv = entero;
                    entero = 0;
                }
                o.Foto64 = dr["foto64"].ToString();
                o.Descripcion = dr["descripcion"].ToString();
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
                this.comm = GenericDataAccess.CreateCommandSP("sp_Maquila_paso");
                addParameters(0);
                this.dt = GenericDataAccess.ExecuteSelectCommand(comm);
                this._lst = new List<Maquila_paso>();
                foreach (DataRow dr in dt.Rows)
                {
                    Maquila_paso o = new Maquila_paso();
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
                this.comm = GenericDataAccess.CreateCommandSP("sp_Maquila_paso");
                addParameters(1);
                this.dt = GenericDataAccess.ExecuteSelectCommand(comm);
                if (dt.Rows.Count == 1)
                {
                    DataRow dr = dt.Rows[0];
                    BindByDataRow(dr, this._oMaquila_paso);
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
                this.comm = GenericDataAccess.CreateCommandSP("sp_Maquila_paso");
                addParameters(2);
                GenericDataAccess.ExecuteNonQuery(this.comm);
                this._oMaquila_paso.Id = Convert.ToInt32(GenericDataAccess.getParameterValue(comm, "?P_id"));
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
                this.comm = GenericDataAccess.CreateCommandSP("sp_Maquila_paso");
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
                this.comm = GenericDataAccess.CreateCommandSP("sp_Maquila_paso");
                addParameters(4);
                GenericDataAccess.ExecuteNonQuery(this.comm);
            }
            catch
            {
                throw;
            }
        }

        #endregion

        internal void add(IDbTransaction trans)
        {
            try
            {
                this.comm = GenericDataAccess.CreateCommandSP("sp_Maquila_paso");
                addParameters(2);
                GenericDataAccess.ExecuteNonQuery(this.comm, trans);
                this._oMaquila_paso.Id = Convert.ToInt32(GenericDataAccess.getParameterValue(comm, "?P_id"));
            }
            catch
            {
                throw;
            }
        }

        internal void fillByIdOTS()
        {
            try
            {
                this.comm = GenericDataAccess.CreateCommandSP("sp_Maquila_paso");
                addParameters(5);
                this.dt = GenericDataAccess.ExecuteSelectCommand(comm);
                this._lst = new List<Maquila_paso>();
                foreach (DataRow dr in dt.Rows)
                {
                    Maquila_paso o = new Maquila_paso();
                    BindByDataRow(dr, o);
                    this._lst.Add(o);
                }
            }
            catch
            {
                throw;
            }
        }
    }
}
