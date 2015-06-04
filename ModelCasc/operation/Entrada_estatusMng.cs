using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using Model;

namespace ModelCasc.operation
{
    internal class Entrada_estatusMng: dbTable
    {
        #region Campos
        protected Entrada_estatus _oEntrada_estatus;
        protected List<Entrada_estatus> _lst;
        #endregion

        #region Propiedades
        public Entrada_estatus O_Entrada_estatus { get { return _oEntrada_estatus; } set { _oEntrada_estatus = value; } }
        public List<Entrada_estatus> Lst { get { return _lst; } set { _lst = value; } }
        #endregion

        #region Constructores
        public Entrada_estatusMng()
        {
            this._oEntrada_estatus = new Entrada_estatus();
            this._lst = new List<Entrada_estatus>();
        }
        #endregion

        #region Metodos
        protected override void addParameters(int opcion)
        {
            GenericDataAccess.AddInParameter(this.comm, "?P_opcion", DbType.Int32, opcion);
            GenericDataAccess.AddInOutParameter(this.comm, "?P_id", DbType.Int32, this._oEntrada_estatus.Id);
            GenericDataAccess.AddInParameter(this.comm, "?P_id_usuario", DbType.Int32, this._oEntrada_estatus.Id_usuario);
            GenericDataAccess.AddInParameter(this.comm, "?P_id_entrada_inventario", DbType.Int32, this._oEntrada_estatus.Id_entrada_inventario);
            if (this._oEntrada_estatus.Id_entrada_maquila == null)
                GenericDataAccess.AddInParameter(this.comm, "?P_id_entrada_maquila", DbType.Int32, DBNull.Value);
            else
                GenericDataAccess.AddInParameter(this.comm, "?P_id_entrada_maquila", DbType.Int32, this._oEntrada_estatus.Id_entrada_maquila);

            if (this._oEntrada_estatus.Id_salida_remision == null)
                GenericDataAccess.AddInParameter(this.comm, "?P_id_salida_remision", DbType.Int32, DBNull.Value);
            else
                GenericDataAccess.AddInParameter(this.comm, "?P_id_salida_remision", DbType.Int32, this._oEntrada_estatus.Id_salida_remision);

            GenericDataAccess.AddInParameter(this.comm, "?P_id_estatus_proceso", DbType.Int32, this._oEntrada_estatus.Id_estatus_proceso);
        }

        protected void BindByDataRow(DataRow dr, Entrada_estatus o)
        {
            try
            {
                int.TryParse(dr["id"].ToString(), out entero);
                o.Id = entero;
                entero = 0;
                if (dr["id_usuario"] != DBNull.Value)
                {
                    int.TryParse(dr["id_usuario"].ToString(), out entero);
                    o.Id_usuario = entero;
                    entero = 0;
                }
                if (dr["id_entrada_inventario"] != DBNull.Value)
                {
                    int.TryParse(dr["id_entrada_inventario"].ToString(), out entero);
                    o.Id_entrada_inventario = entero;
                    entero = 0;
                }
                if (dr["id_entrada_maquila"] != DBNull.Value)
                {
                    int.TryParse(dr["id_entrada_maquila"].ToString(), out entero);
                    o.Id_entrada_maquila = entero;
                    entero = 0;
                }
                if (dr["id_estatus_proceso"] != DBNull.Value)
                {
                    int.TryParse(dr["id_estatus_proceso"].ToString(), out entero);
                    o.Id_estatus_proceso = entero;
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
                this.comm = GenericDataAccess.CreateCommandSP("sp_Entrada_estatus");
                addParameters(0);
                this.dt = GenericDataAccess.ExecuteSelectCommand(comm);
                this._lst = new List<Entrada_estatus>();
                foreach (DataRow dr in dt.Rows)
                {
                    Entrada_estatus o = new Entrada_estatus();
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
                this.comm = GenericDataAccess.CreateCommandSP("sp_Entrada_estatus");
                addParameters(1);
                this.dt = GenericDataAccess.ExecuteSelectCommand(comm);
                if (dt.Rows.Count == 1)
                {
                    DataRow dr = dt.Rows[0];
                    BindByDataRow(dr, this._oEntrada_estatus);
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
                this.comm = GenericDataAccess.CreateCommandSP("sp_Entrada_estatus");
                addParameters(2);
                GenericDataAccess.ExecuteNonQuery(this.comm);
                this._oEntrada_estatus.Id = Convert.ToInt32(GenericDataAccess.getParameterValue(comm, "?P_id"));
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
                this.comm = GenericDataAccess.CreateCommandSP("sp_Entrada_estatus");
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
                this.comm = GenericDataAccess.CreateCommandSP("sp_Entrada_estatus");
                addParameters(4);
                GenericDataAccess.ExecuteNonQuery(this.comm);
            }
            catch
            {
                throw;
            }
        }

        #endregion

        internal void add(IDbTransaction trans = null)
        {
            try
            {
                this.comm = GenericDataAccess.CreateCommandSP("sp_Entrada_estatus");
                addParameters(2);
                if (trans != null)
                    GenericDataAccess.ExecuteNonQuery(this.comm, trans);
                else
                    GenericDataAccess.ExecuteNonQuery(this.comm);
                this._oEntrada_estatus.Id = Convert.ToInt32(GenericDataAccess.getParameterValue(comm, "?P_id"));
            }
            catch
            {
                throw;
            }
        }

        internal void closeMaquila(IDbTransaction trans = null)
        {
            try
            {
                this.comm = GenericDataAccess.CreateCommandSP("sp_Entrada_estatus");
                addParameters(5);
                if (trans != null)
                    GenericDataAccess.ExecuteNonQuery(this.comm, trans);
                else
                    GenericDataAccess.ExecuteNonQuery(this.comm);
            }
            catch
            {
                throw;
            }
        }
    }
}
