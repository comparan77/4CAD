using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using Model;

namespace ModelCasc.operation.liverpool
{
    internal class Entrada_liverpoolMng: dbTable
    {
        #region Campos
        protected Entrada_liverpool _oEntrada_liverpool;
        protected List<Entrada_liverpool> _lst;
        #endregion

        #region Propiedades
        public Entrada_liverpool O_Entrada_liverpool { get { return _oEntrada_liverpool; } set { _oEntrada_liverpool = value; } }
        public List<Entrada_liverpool> Lst { get { return _lst; } set { _lst = value; } }
        #endregion

        #region Constructores
        public Entrada_liverpoolMng()
        {
            this._oEntrada_liverpool = new Entrada_liverpool();
            this._lst = new List<Entrada_liverpool>();
        }
        #endregion

        #region Metodos
        protected override void addParameters(int opcion)
        {
            GenericDataAccess.AddInParameter(this.comm, "?P_opcion", DbType.Int32, opcion);
            GenericDataAccess.AddInOutParameter(this.comm, "?P_id", DbType.Int32, this._oEntrada_liverpool.Id);
            GenericDataAccess.AddInParameter(this.comm, "?P_id_entrada", DbType.Int32, this._oEntrada_liverpool.Id_entrada);
            GenericDataAccess.AddInParameter(this.comm, "?P_proveedor", DbType.String, this._oEntrada_liverpool.Proveedor);
            GenericDataAccess.AddInParameter(this.comm, "?P_trafico", DbType.String, this._oEntrada_liverpool.Trafico);
            GenericDataAccess.AddInParameter(this.comm, "?P_pedido", DbType.Int32, this._oEntrada_liverpool.Pedido);
            GenericDataAccess.AddInParameter(this.comm, "?P_piezas", DbType.Int32, this._oEntrada_liverpool.Piezas);
            GenericDataAccess.AddInParameter(this.comm, "?P_fecha_confirma", DbType.DateTime, this._oEntrada_liverpool.Fecha_confirma);
            GenericDataAccess.AddInParameter(this.comm, "?P_piezas_maq", DbType.Int32, this._oEntrada_liverpool.Piezas_maq);
            GenericDataAccess.AddInParameter(this.comm, "?P_fecha_maquila", DbType.DateTime, this._oEntrada_liverpool.Fecha_maquila);
            GenericDataAccess.AddInParameter(this.comm, "?P_num_pasos", DbType.Int32, this._oEntrada_liverpool.Num_pasos);
        }

        protected void BindByDataRow(DataRow dr, Entrada_liverpool o)
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
                o.Proveedor = dr["proveedor"].ToString();
                o.Trafico = dr["trafico"].ToString();
                if (dr["pedido"] != DBNull.Value)
                {
                    int.TryParse(dr["pedido"].ToString(), out entero);
                    o.Pedido = entero;
                    entero = 0;
                }
                if (dr["piezas"] != DBNull.Value)
                {
                    int.TryParse(dr["piezas"].ToString(), out entero);
                    o.Piezas = entero;
                    entero = 0;
                }
                if (dr["fecha_confirma"] != DBNull.Value)
                {
                    DateTime.TryParse(dr["fecha_confirma"].ToString(), out fecha);
                    o.Fecha_confirma = fecha;
                    fecha = default(DateTime);
                }
                if (dr["piezas_maq"] != DBNull.Value)
                {
                    int.TryParse(dr["piezas_maq"].ToString(), out entero);
                    o.Piezas_maq = entero;
                    entero = 0;
                }
                if (dr["fecha_maquila"] != DBNull.Value)
                {
                    DateTime.TryParse(dr["fecha_maquila"].ToString(), out fecha);
                    o.Fecha_maquila = fecha;
                    fecha = default(DateTime);
                }
                if (dr["num_pasos"] != DBNull.Value)
                {
                    int.TryParse(dr["num_pasos"].ToString(), out entero);
                    o.Num_pasos = entero;
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
                this.comm = GenericDataAccess.CreateCommandSP("sp_Entrada_liverpool");
                addParameters(0);
                this.dt = GenericDataAccess.ExecuteSelectCommand(comm);
                this._lst = new List<Entrada_liverpool>();
                foreach (DataRow dr in dt.Rows)
                {
                    Entrada_liverpool o = new Entrada_liverpool();
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
                this.comm = GenericDataAccess.CreateCommandSP("sp_Entrada_liverpool");
                addParameters(1);
                this.dt = GenericDataAccess.ExecuteSelectCommand(comm);
                if (dt.Rows.Count == 1)
                {
                    DataRow dr = dt.Rows[0];
                    BindByDataRow(dr, this._oEntrada_liverpool);
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
                this.comm = GenericDataAccess.CreateCommandSP("sp_Entrada_liverpool");
                addParameters(2);
                GenericDataAccess.ExecuteNonQuery(this.comm);
                this._oEntrada_liverpool.Id = Convert.ToInt32(GenericDataAccess.getParameterValue(comm, "?P_id"));
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
                this.comm = GenericDataAccess.CreateCommandSP("sp_Entrada_liverpool");
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
                this.comm = GenericDataAccess.CreateCommandSP("sp_Entrada_liverpool");
                addParameters(4);
                GenericDataAccess.ExecuteNonQuery(this.comm);
            }
            catch
            {
                throw;
            }
        }

        #endregion

        internal void add(IDbTransaction tran)
        {
            try
            {
                this.comm = GenericDataAccess.CreateCommandSP("sp_Entrada_liverpool");
                addParameters(2);
                GenericDataAccess.ExecuteNonQuery(this.comm, tran);
                this._oEntrada_liverpool.Id = Convert.ToInt32(GenericDataAccess.getParameterValue(comm, "?P_id"));
            }
            catch
            {
                throw;
            }
        }

        internal void fillLstCodPendientes()
        {
            try
            {
                this.comm = GenericDataAccess.CreateCommandSP("sp_Entrada_liverpool");
                addParameters(5);
                this.dt = GenericDataAccess.ExecuteSelectCommand(comm);
                this._lst = new List<Entrada_liverpool>();
                foreach (DataRow dr in dt.Rows)
                {
                    Entrada_liverpool o = new Entrada_liverpool();
                    BindByDataRow(dr, o);
                    this._lst.Add(o);
                }
            }
            catch
            {
                throw;
            }
        }

        internal void udtPiezasMaq(IDbTransaction trans)
        {
            try
            {
                this.comm = GenericDataAccess.CreateCommandSP("sp_Entrada_liverpool");
                addParameters(6);
                GenericDataAccess.ExecuteNonQuery(this.comm, trans);
            }
            catch
            {
                throw;
            }
        }

        internal void selByUniqueKey(Entrada_liverpool o, IDbTransaction trans = null)
        {
            try
            {
                this.comm = GenericDataAccess.CreateCommandSP("sp_Entrada_liverpool");
                addParameters(7);
                if(trans!=null)
                    this.dt = GenericDataAccess.ExecuteSelectCommand(comm, trans);
                else
                    this.dt = GenericDataAccess.ExecuteSelectCommand(comm);
                if (dt.Rows.Count == 1)
                {
                    DataRow dr = dt.Rows[0];
                    BindByDataRow(dr, this._oEntrada_liverpool);
                }
                else if (dt.Rows.Count > 1)
                    throw new Exception("Error de integridad");
            }
            catch
            {
                throw;
            }
        }

        internal void udtFechaConfirmacion(IDbTransaction trans)
        {
            try
            {
                this.comm = GenericDataAccess.CreateCommandSP("sp_Entrada_liverpool");
                addParameters(8);
                GenericDataAccess.ExecuteNonQuery(this.comm, trans);
            }
            catch
            {
                throw;
            }
        }
    }
}
