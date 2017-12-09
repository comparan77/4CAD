using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using Model;

namespace ModelCasc.operation.liverpool
{
    internal class Entrada_liverpool_pasoMng:dbTable
    {
        #region Campos
        protected Entrada_liverpool_paso _oEntrada_liverpool_paso;
        protected List<Entrada_liverpool_paso> _lst;
        #endregion

        #region Propiedades
        public Entrada_liverpool_paso O_Entrada_liverpool_paso { get { return _oEntrada_liverpool_paso; } set { _oEntrada_liverpool_paso = value; } }
        public List<Entrada_liverpool_paso> Lst { get { return _lst; } set { _lst = value; } }
        #endregion

        #region Constructores
        public Entrada_liverpool_pasoMng()
        {
            this._oEntrada_liverpool_paso = new Entrada_liverpool_paso();
            this._lst = new List<Entrada_liverpool_paso>();
        }
        #endregion

        #region Metodos
        protected override void addParameters(int opcion)
        {
            GenericDataAccess.AddInParameter(this.comm, "?P_opcion", DbType.Int32, opcion);
            GenericDataAccess.AddInOutParameter(this.comm, "?P_id", DbType.Int32, this._oEntrada_liverpool_paso.Id);
            GenericDataAccess.AddInParameter(this.comm, "?P_id_liverpool_maquila", DbType.Int32, this._oEntrada_liverpool_paso.Id_liverpool_maquila);
            GenericDataAccess.AddInParameter(this.comm, "?P_descripcion", DbType.Int32, this._oEntrada_liverpool_paso.Descripcion);
            GenericDataAccess.AddInParameter(this.comm, "?P_foto", DbType.String, this._oEntrada_liverpool_paso.Foto);
        }

        protected void BindByDataRow(DataRow dr, Entrada_liverpool_paso o)
        {
            try
            {
                int.TryParse(dr["id"].ToString(), out entero);
                o.Id = entero;
                entero = 0;
                if (dr["id_liverpool_maquila"] != DBNull.Value)
                {
                    int.TryParse(dr["id_liverpool_maquila"].ToString(), out entero);
                    o.Id_liverpool_maquila = entero;
                    entero = 0;
                }
                if (dr["descripcion"] != DBNull.Value)
                {
                    int.TryParse(dr["descripcion"].ToString(), out entero);
                    o.Descripcion = entero;
                    entero = 0;
                }
                o.Foto = dr["foto"].ToString();
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
                this.comm = GenericDataAccess.CreateCommandSP("sp_Entrada_liverpool_paso");
                addParameters(0);
                this.dt = GenericDataAccess.ExecuteSelectCommand(comm);
                this._lst = new List<Entrada_liverpool_paso>();
                foreach (DataRow dr in dt.Rows)
                {
                    Entrada_liverpool_paso o = new Entrada_liverpool_paso();
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
                this.comm = GenericDataAccess.CreateCommandSP("sp_Entrada_liverpool_paso");
                addParameters(1);
                this.dt = GenericDataAccess.ExecuteSelectCommand(comm);
                if (dt.Rows.Count == 1)
                {
                    DataRow dr = dt.Rows[0];
                    BindByDataRow(dr, this._oEntrada_liverpool_paso);
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
                this.comm = GenericDataAccess.CreateCommandSP("sp_Entrada_liverpool_paso");
                addParameters(2);
                GenericDataAccess.ExecuteNonQuery(this.comm);
                this._oEntrada_liverpool_paso.Id = Convert.ToInt32(GenericDataAccess.getParameterValue(comm, "?P_id"));
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
                this.comm = GenericDataAccess.CreateCommandSP("sp_Entrada_liverpool_paso");
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
                this.comm = GenericDataAccess.CreateCommandSP("sp_Entrada_liverpool_paso");
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
