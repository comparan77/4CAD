using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using Model;

namespace logisticaModel.operation.warehouse
{
    internal class Cortina_disponibleMng: Crud
    {
        #region Campos
        protected Cortina_disponible _oCortina_disponible;
        protected List<Cortina_disponible> _lst;
        #endregion

        #region Propiedades
        public Cortina_disponible O_Cortina_disponible { get { return _oCortina_disponible; } set { _oCortina_disponible = value; } }
        public List<Cortina_disponible> Lst { get { return _lst; } set { _lst = value; } }
        #endregion

        #region Constructores
        public Cortina_disponibleMng()
        {
            this._oCortina_disponible = new Cortina_disponible();
            this._lst = new List<Cortina_disponible>();
        }
        #endregion

        #region Metodos
        protected override void addParameters(int opcion)
        {
            GenericDataAccess.AddInParameter(this.comm, "?P_opcion", DbType.Int32, opcion);
            GenericDataAccess.AddInOutParameter(this.comm, "?P_id", DbType.Int32, this._oCortina_disponible.Id);
            GenericDataAccess.AddInParameter(this.comm, "?P_id_usuario", DbType.Int32, this._oCortina_disponible.Id_usuario);
            GenericDataAccess.AddInParameter(this.comm, "?P_id_cliente", DbType.Int32, this._oCortina_disponible.Id_cliente);
            GenericDataAccess.AddInParameter(this.comm, "?P_cliente", DbType.String, this._oCortina_disponible.Cliente);
            GenericDataAccess.AddInParameter(this.comm, "?P_id_bodega", DbType.Int32, this._oCortina_disponible.Id_bodega);
            GenericDataAccess.AddInParameter(this.comm, "?P_bodega", DbType.String, this._oCortina_disponible.Bodega);
            GenericDataAccess.AddInParameter(this.comm, "?P_id_cortina", DbType.Int32, this._oCortina_disponible.Id_cortina);
            GenericDataAccess.AddInParameter(this.comm, "?P_cortina", DbType.String, this._oCortina_disponible.Cortina);
            GenericDataAccess.AddInParameter(this.comm, "?P_por_recibir", DbType.Int32, this._oCortina_disponible.Por_recibir);
            GenericDataAccess.AddInParameter(this.comm, "?P_tarimas", DbType.Int32, this._oCortina_disponible.Tarimas);
        }

        protected void BindByDataRow(DataRow dr, Cortina_disponible o)
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
                if (dr["id_cliente"] != DBNull.Value)
                {
                    int.TryParse(dr["id_cliente"].ToString(), out entero);
                    o.Id_cliente = entero;
                    entero = 0;
                }
                o.Cliente = dr["cliente"].ToString();
                if (dr["id_bodega"] != DBNull.Value)
                {
                    int.TryParse(dr["id_bodega"].ToString(), out entero);
                    o.Id_bodega = entero;
                    entero = 0;
                }
                o.Bodega = dr["bodega"].ToString();
                if (dr["id_cortina"] != DBNull.Value)
                {
                    int.TryParse(dr["id_cortina"].ToString(), out entero);
                    o.Id_cortina = entero;
                    entero = 0;
                }
                o.Cortina = dr["cortina"].ToString();
                if (dr["inicio"] != DBNull.Value)
                {
                    DateTime.TryParse(dr["inicio"].ToString(), out fecha);
                    o.Inicio = fecha;
                    fecha = default(DateTime);
                }
                if (dr["fin"] != DBNull.Value)
                {
                    DateTime.TryParse(dr["fin"].ToString(), out fecha);
                    o.Fin = fecha;
                    fecha = default(DateTime);
                }
                if (dr["tarimas"] != DBNull.Value)
                {
                    int.TryParse(dr["tarimas"].ToString(), out entero);
                    o.Tarimas = entero;
                    entero = 0;
                }
                if (dr["por_recibir"] != DBNull.Value)
                {
                    int.TryParse(dr["por_recibir"].ToString(), out entero);
                    o.Por_recibir = entero;
                    entero = 0;
                }
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
                this.comm = GenericDataAccess.CreateCommandSP("sp_Cortina_disponible");
                addParameters(0);
                if (trans == null)
                    this.dt = GenericDataAccess.ExecuteSelectCommand(comm);
                else
                    this.dt = GenericDataAccess.ExecuteSelectCommand(comm, trans);
                this._lst = new List<Cortina_disponible>();
                foreach (DataRow dr in dt.Rows)
                {
                    Cortina_disponible o = new Cortina_disponible();
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
                this.comm = GenericDataAccess.CreateCommandSP("sp_Cortina_disponible");
                addParameters(1);
                if (trans == null)
                    this.dt = GenericDataAccess.ExecuteSelectCommand(comm);
                else
                    this.dt = GenericDataAccess.ExecuteSelectCommand(comm, trans);
                if (dt.Rows.Count == 1)
                {
                    DataRow dr = dt.Rows[0];
                    BindByDataRow(dr, this._oCortina_disponible);
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
                this.comm = GenericDataAccess.CreateCommandSP("sp_Cortina_disponible");
                addParameters(2);
                if (trans == null)
                    GenericDataAccess.ExecuteNonQuery(this.comm);
                else
                    GenericDataAccess.ExecuteNonQuery(this.comm, trans);
                this._oCortina_disponible.Id = Convert.ToInt32(GenericDataAccess.getParameterValue(comm, "?P_id"));
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
                this.comm = GenericDataAccess.CreateCommandSP("sp_Cortina_disponible");
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
                this.comm = GenericDataAccess.CreateCommandSP("sp_Cortina_disponible");
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

        internal void liberar(IDbTransaction trans = null)
        {
            try
            {
                this.comm = GenericDataAccess.CreateCommandSP("sp_Cortina_disponible");
                addParameters(5);
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

        #endregion
    }
}
