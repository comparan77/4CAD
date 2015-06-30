using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using Model;

namespace ModelCasc.operation
{
    internal class Salida_remision_detailMng: dbTable
    {
        #region Campos
        protected Salida_remision_detail _oSalida_remision_detail;
        protected List<Salida_remision_detail> _lst;
        #endregion

        #region Propiedades
        public Salida_remision_detail O_Salida_remision_detail { get { return _oSalida_remision_detail; } set { _oSalida_remision_detail = value; } }
        public List<Salida_remision_detail> Lst { get { return _lst; } set { _lst = value; } }
        #endregion

        #region Constructores
        public Salida_remision_detailMng()
        {
            this._oSalida_remision_detail = new Salida_remision_detail();
            this._lst = new List<Salida_remision_detail>();
        }
        #endregion

        #region Metodos
        protected override void addParameters(int opcion)
        {
            GenericDataAccess.AddInParameter(this.comm, "?P_opcion", DbType.Int32, opcion);
            GenericDataAccess.AddInOutParameter(this.comm, "?P_id", DbType.Int32, this._oSalida_remision_detail.Id);
            GenericDataAccess.AddInParameter(this.comm, "?P_id_salida_remision", DbType.Int32, this._oSalida_remision_detail.Id_salida_remision);
            GenericDataAccess.AddInParameter(this.comm, "?P_id_entrada_maquila_detail", DbType.Int32, this._oSalida_remision_detail.Id_entrada_maquila_detail);
            GenericDataAccess.AddInParameter(this.comm, "?P_bulto", DbType.Int32, this._oSalida_remision_detail.Bulto);
            GenericDataAccess.AddInParameter(this.comm, "?P_piezaxbulto", DbType.Int32, this._oSalida_remision_detail.Piezaxbulto);
            GenericDataAccess.AddInParameter(this.comm, "?P_piezas", DbType.Int32, this._oSalida_remision_detail.Piezas);
            GenericDataAccess.AddInParameter(this.comm, "?P_danado", DbType.Boolean, this._oSalida_remision_detail.Danado);
            if(this._oSalida_remision_detail.Lote == null)
                GenericDataAccess.AddInParameter(this.comm, "?P_lote", DbType.String, DBNull.Value);
            else
                GenericDataAccess.AddInParameter(this.comm, "?P_lote", DbType.String, this._oSalida_remision_detail.Lote);
        }

        public void BindByDataRow(DataRow dr, Salida_remision_detail o)
        {
            try
            {
                int.TryParse(dr["id"].ToString(), out entero);
                o.Id = entero;
                entero = 0;
                if (dr["id_salida_remision"] != DBNull.Value)
                {
                    int.TryParse(dr["id_salida_remision"].ToString(), out entero);
                    o.Id_salida_remision = entero;
                    entero = 0;
                }
                if (dr["id_entrada_maquila_detail"] != DBNull.Value)
                {
                    int.TryParse(dr["id_entrada_maquila_detail"].ToString(), out entero);
                    o.Id_salida_remision = entero;
                    entero = 0;
                }
                if (dr["bulto"] != DBNull.Value)
                {
                    int.TryParse(dr["bulto"].ToString(), out entero);
                    o.Bulto = entero;
                    entero = 0;
                }
                if (dr["piezaxbulto"] != DBNull.Value)
                {
                    int.TryParse(dr["piezaxbulto"].ToString(), out entero);
                    o.Piezaxbulto = entero;
                    entero = 0;
                }
                if (dr["piezas"] != DBNull.Value)
                {
                    int.TryParse(dr["piezas"].ToString(), out entero);
                    o.Piezas = entero;
                    entero = 0;
                }
                if (dr["danado"] != DBNull.Value)
                {
                    bool.TryParse(dr["danado"].ToString(), out logica);
                    o.Danado = logica;
                    logica = false;
                }
                o.Lote = dr["lote"].ToString();
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
                this.comm = GenericDataAccess.CreateCommandSP("sp_Salida_remision_detail");
                addParameters(0);
                this.dt = GenericDataAccess.ExecuteSelectCommand(comm);
                this._lst = new List<Salida_remision_detail>();
                foreach (DataRow dr in dt.Rows)
                {
                    Salida_remision_detail o = new Salida_remision_detail();
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
                this.comm = GenericDataAccess.CreateCommandSP("sp_Salida_remision_detail");
                addParameters(1);
                this.dt = GenericDataAccess.ExecuteSelectCommand(comm);
                if (dt.Rows.Count == 1)
                {
                    DataRow dr = dt.Rows[0];
                    BindByDataRow(dr, this._oSalida_remision_detail);
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
                this.comm = GenericDataAccess.CreateCommandSP("sp_Salida_remision_detail");
                addParameters(2);
                GenericDataAccess.ExecuteNonQuery(this.comm);
                this._oSalida_remision_detail.Id = Convert.ToInt32(GenericDataAccess.getParameterValue(comm, "?P_id"));
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
                this.comm = GenericDataAccess.CreateCommandSP("sp_Salida_remision_detail");
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
                this.comm = GenericDataAccess.CreateCommandSP("sp_Salida_remision_detail");
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
                this.comm = GenericDataAccess.CreateCommandSP("sp_Salida_remision_detail");
                addParameters(2);
                GenericDataAccess.ExecuteNonQuery(this.comm, trans);
                this._oSalida_remision_detail.Id = Convert.ToInt32(GenericDataAccess.getParameterValue(comm, "?P_id"));
            }
            catch
            {
                throw;
            }
        }

        internal void selByIdRemision()
        {
            try
            {
                this.comm = GenericDataAccess.CreateCommandSP("sp_Salida_remision_detail");
                addParameters(5);
                this.dt = GenericDataAccess.ExecuteSelectCommand(comm);
                this._lst = new List<Salida_remision_detail>();
                foreach (DataRow dr in dt.Rows)
                {
                    Salida_remision_detail o = new Salida_remision_detail();
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
