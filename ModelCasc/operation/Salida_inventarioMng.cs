using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using Model;

namespace ModelCasc.operation
{
    internal class Salida_inventarioMng: dbTable
    {
        #region Campos
        protected Salida_inventario _oSalida_inventario;
        protected List<Salida_inventario> _lst;
        #endregion

        #region Propiedades
        public Salida_inventario O_Salida_inventario { get { return _oSalida_inventario; } set { _oSalida_inventario = value; } }
        public List<Salida_inventario> Lst { get { return _lst; } set { _lst = value; } }
        #endregion

        #region Constructores
        public Salida_inventarioMng()
        {
            this._oSalida_inventario = new Salida_inventario();
            this._lst = new List<Salida_inventario>();
        }
        #endregion

        #region Metodos
        protected override void addParameters(int opcion)
        {
            GenericDataAccess.AddInParameter(this.comm, "?P_opcion", DbType.Int32, opcion);
            GenericDataAccess.AddInParameter(this.comm, "?P_id", DbType.Int32, this._oSalida_inventario.Id);
            GenericDataAccess.AddInParameter(this.comm, "?P_id_entrada", DbType.Int32, this._oSalida_inventario.Id_entrada);
            GenericDataAccess.AddInParameter(this.comm, "?P_id_entrada_inventario", DbType.Int32, this._oSalida_inventario.Id_entrada_inventario);
            GenericDataAccess.AddInParameter(this.comm, "?P_id_usuario", DbType.Int32, this._oSalida_inventario.Id_usuario);
            GenericDataAccess.AddInParameter(this.comm, "?P_referencia", DbType.String, this._oSalida_inventario.Referencia);
            GenericDataAccess.AddInParameter(this.comm, "?P_pedimento", DbType.String, this._oSalida_inventario.Pedimento);
            GenericDataAccess.AddInParameter(this.comm, "?P_codigo", DbType.String, this._oSalida_inventario.Codigo);
            GenericDataAccess.AddInParameter(this.comm, "?P_orden", DbType.String, this._oSalida_inventario.Orden);
            GenericDataAccess.AddInParameter(this.comm, "?P_pallet", DbType.Int32, this._oSalida_inventario.Pallet);
            GenericDataAccess.AddInParameter(this.comm, "?P_bulto", DbType.Int32, this._oSalida_inventario.Bulto);
            GenericDataAccess.AddInParameter(this.comm, "?P_pieza", DbType.Int32, this._oSalida_inventario.Pieza);
            GenericDataAccess.AddInParameter(this.comm, "?P_fecha", DbType.DateTime, this._oSalida_inventario.Fecha);
        }

        protected void BindByDataRow(DataRow dr, Salida_inventario o)
        {
            try
            {
                int.TryParse(dr["id"].ToString(), out entero);
                o.Id = entero;
                entero = 0;
                if (dr["id_entrada"] != DBNull.Value)
                {
                    int.TryParse(dr["id_entrada"].ToString(), out entero);
                    o.Id_entrada = entero;
                    entero = 0;
                }
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
                o.Referencia = dr["referencia"].ToString();
                o.Pedimento = dr["pedimento"].ToString();
                o.Codigo = dr["codigo"].ToString();
                o.Orden = dr["orden"].ToString();
                if (dr["pallet"] != DBNull.Value)
                {
                    int.TryParse(dr["pallet"].ToString(), out entero);
                    o.Pallet = entero;
                    entero = 0;
                }
                if (dr["bulto"] != DBNull.Value)
                {
                    int.TryParse(dr["bulto"].ToString(), out entero);
                    o.Bulto = entero;
                    entero = 0;
                }
                if (dr["pieza"] != DBNull.Value)
                {
                    int.TryParse(dr["pieza"].ToString(), out entero);
                    o.Pieza = entero;
                    entero = 0;
                }
                if (dr["fecha"] != DBNull.Value)
                {
                    DateTime.TryParse(dr["fecha"].ToString(), out fecha);
                    o.Fecha = fecha;
                    fecha = default(DateTime);
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
                this.comm = GenericDataAccess.CreateCommandSP("sp_Salida_inventario");
                addParameters(0);
                this.dt = GenericDataAccess.ExecuteSelectCommand(comm);
                this._lst = new List<Salida_inventario>();
                foreach (DataRow dr in dt.Rows)
                {
                    Salida_inventario o = new Salida_inventario();
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
                this.comm = GenericDataAccess.CreateCommandSP("sp_Salida_inventario");
                addParameters(1);
                this.dt = GenericDataAccess.ExecuteSelectCommand(comm);
                if (dt.Rows.Count == 1)
                {
                    DataRow dr = dt.Rows[0];
                    BindByDataRow(dr, this._oSalida_inventario);
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
                this.comm = GenericDataAccess.CreateCommandSP("sp_Salida_inventario");
                addParameters(2);
                GenericDataAccess.ExecuteNonQuery(this.comm);
                this._oSalida_inventario.Id = Convert.ToInt32(GenericDataAccess.getParameterValue(comm, "?P_id"));
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
                this.comm = GenericDataAccess.CreateCommandSP("sp_Salida_inventario");
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
                this.comm = GenericDataAccess.CreateCommandSP("sp_Salida_inventario");
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
