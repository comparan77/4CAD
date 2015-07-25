using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using Model;

namespace ModelCasc.operation
{
    internal class Entrada_inventario_cambiosMng: dbTable
    {
        #region Campos
        protected Entrada_inventario_cambios _oEntrada_inventario_cambios;
        protected List<Entrada_inventario_cambios> _lst;
        #endregion

        #region Propiedades
        public Entrada_inventario_cambios O_Entrada_inventario_cambios { get { return _oEntrada_inventario_cambios; } set { _oEntrada_inventario_cambios = value; } }
        public List<Entrada_inventario_cambios> Lst { get { return _lst; } set { _lst = value; } }
        #endregion

        #region Constructores
        public Entrada_inventario_cambiosMng()
        {
            this._oEntrada_inventario_cambios = new Entrada_inventario_cambios();
            this._lst = new List<Entrada_inventario_cambios>();
        }
        #endregion

        #region Metodos
        protected override void addParameters(int opcion)
        {
            GenericDataAccess.AddInParameter(this.comm, "?P_opcion", DbType.Int32, opcion);
            GenericDataAccess.AddInOutParameter(this.comm, "?P_id", DbType.Int32, this._oEntrada_inventario_cambios.Id);
            GenericDataAccess.AddInParameter(this.comm, "?P_id_entrada_inventario", DbType.Int32, this._oEntrada_inventario_cambios.Id_entrada_inventario);
            GenericDataAccess.AddInParameter(this.comm, "?P_id_usuario", DbType.Int32, this._oEntrada_inventario_cambios.Id_usuario);
            GenericDataAccess.AddInParameter(this.comm, "?P_codigo", DbType.String, this._oEntrada_inventario_cambios.Codigo);
            GenericDataAccess.AddInParameter(this.comm, "?P_orden", DbType.String, this._oEntrada_inventario_cambios.Orden);
            GenericDataAccess.AddInParameter(this.comm, "?P_observaciones", DbType.String, this._oEntrada_inventario_cambios.Observaciones);
        }

        protected void BindByDataRow(DataRow dr, Entrada_inventario_cambios o)
        {
            try
            {
                int.TryParse(dr["id"].ToString(), out entero);
                o.Id = entero;
                entero = 0;
                if (dr["id_entrada_inventario"] != DBNull.Value)
                {
                    int.TryParse(dr["id_entrada_inventario"].ToString(), out entero);
                    o.Id_entrada_inventario = entero;
                    entero = 0;
                }
                if (dr["id_usuario"] != DBNull.Value)
                {
                    int.TryParse(dr["id_usuario"].ToString(), out entero);
                    o.Id_usuario = entero;
                    entero = 0;
                }
                o.Codigo = dr["codigo"].ToString();
                o.Orden = dr["orden"].ToString();
                o.Observaciones = dr["observaciones"].ToString();
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
                this.comm = GenericDataAccess.CreateCommandSP("sp_Entrada_inventario_cambios");
                addParameters(0);
                this.dt = GenericDataAccess.ExecuteSelectCommand(comm);
                this._lst = new List<Entrada_inventario_cambios>();
                foreach (DataRow dr in dt.Rows)
                {
                    Entrada_inventario_cambios o = new Entrada_inventario_cambios();
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
                this.comm = GenericDataAccess.CreateCommandSP("sp_Entrada_inventario_cambios");
                addParameters(1);
                this.dt = GenericDataAccess.ExecuteSelectCommand(comm);
                if (dt.Rows.Count == 1)
                {
                    DataRow dr = dt.Rows[0];
                    BindByDataRow(dr, this._oEntrada_inventario_cambios);
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
                this.comm = GenericDataAccess.CreateCommandSP("sp_Entrada_inventario_cambios");
                addParameters(2);
                GenericDataAccess.ExecuteNonQuery(this.comm);
                this._oEntrada_inventario_cambios.Id = Convert.ToInt32(GenericDataAccess.getParameterValue(comm, "?P_id"));
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
                this.comm = GenericDataAccess.CreateCommandSP("sp_Entrada_inventario_cambios");
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
                this.comm = GenericDataAccess.CreateCommandSP("sp_Entrada_inventario_cambios");
                addParameters(4);
                GenericDataAccess.ExecuteNonQuery(this.comm);
            }
            catch
            {
                throw;
            }
        }

        #endregion

        internal int udtCodigo()
        {
            int success = -1;
            try
            {
                this.comm = GenericDataAccess.CreateCommandSP("sp_Entrada_inventario_cambios");
                addParameters(5);
                success = GenericDataAccess.ExecuteNonQuery(this.comm);
            }
            catch
            {
                throw;
            }
            return success;
        }
    }
}
