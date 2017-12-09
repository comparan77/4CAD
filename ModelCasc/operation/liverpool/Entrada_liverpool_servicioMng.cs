using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using Model;

namespace ModelCasc.operation.liverpool
{
    internal class Entrada_liverpool_servicioMng:dbTable
    {
        #region Campos
        protected Entrada_liverpool_servicio _oEntrada_liverpool_servicio;
        protected List<Entrada_liverpool_servicio> _lst;
        #endregion

        #region Propiedades
        public Entrada_liverpool_servicio O_Entrada_liverpool_servicio { get { return _oEntrada_liverpool_servicio; } set { _oEntrada_liverpool_servicio = value; } }
        public List<Entrada_liverpool_servicio> Lst { get { return _lst; } set { _lst = value; } }
        #endregion

        #region Constructores
        public Entrada_liverpool_servicioMng()
        {
            this._oEntrada_liverpool_servicio = new Entrada_liverpool_servicio();
            this._lst = new List<Entrada_liverpool_servicio>();
        }
        #endregion

        #region Metodos
        protected override void addParameters(int opcion)
        {
            GenericDataAccess.AddInParameter(this.comm, "?P_opcion", DbType.Int32, opcion);
            GenericDataAccess.AddInOutParameter(this.comm, "?P_id", DbType.Int32, this._oEntrada_liverpool_servicio.Id);
            GenericDataAccess.AddInParameter(this.comm, "?P_id_liverpool_maquila", DbType.Int32, this._oEntrada_liverpool_servicio.Id_liverpool_maquila);
            GenericDataAccess.AddInParameter(this.comm, "?P_id_servicio", DbType.Int32, this._oEntrada_liverpool_servicio.Id_servicio);
        }

        protected void BindByDataRow(DataRow dr, Entrada_liverpool_servicio o)
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
                if (dr["id_servicio"] != DBNull.Value)
                {
                    int.TryParse(dr["id_servicio"].ToString(), out entero);
                    o.Id_servicio = entero;
                    entero = 0;
                }
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
                this.comm = GenericDataAccess.CreateCommandSP("sp_Entrada_liverpool_servicio");
                addParameters(0);
                this.dt = GenericDataAccess.ExecuteSelectCommand(comm);
                this._lst = new List<Entrada_liverpool_servicio>();
                foreach (DataRow dr in dt.Rows)
                {
                    Entrada_liverpool_servicio o = new Entrada_liverpool_servicio();
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
                this.comm = GenericDataAccess.CreateCommandSP("sp_Entrada_liverpool_servicio");
                addParameters(1);
                this.dt = GenericDataAccess.ExecuteSelectCommand(comm);
                if (dt.Rows.Count == 1)
                {
                    DataRow dr = dt.Rows[0];
                    BindByDataRow(dr, this._oEntrada_liverpool_servicio);
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
                this.comm = GenericDataAccess.CreateCommandSP("sp_Entrada_liverpool_servicio");
                addParameters(2);
                GenericDataAccess.ExecuteNonQuery(this.comm);
                this._oEntrada_liverpool_servicio.Id = Convert.ToInt32(GenericDataAccess.getParameterValue(comm, "?P_id"));
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
                this.comm = GenericDataAccess.CreateCommandSP("sp_Entrada_liverpool_servicio");
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
                this.comm = GenericDataAccess.CreateCommandSP("sp_Entrada_liverpool_servicio");
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
