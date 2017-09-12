using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Model;
using System.Data;

namespace ModelCasc.operation
{
    internal class Entrada_transporteMng: dbTable
    {
        #region Campos
        protected Entrada_transporte _oEntrada_transporte;
        protected List<Entrada_transporte> _lst;
        #endregion

        #region Propiedades
        public Entrada_transporte O_Entrada_transporte { get { return _oEntrada_transporte; } set { _oEntrada_transporte = value; } }
        public List<Entrada_transporte> Lst { get { return _lst; } set { _lst = value; } }
        #endregion

        #region Constructores
        public Entrada_transporteMng()
        {
            this._oEntrada_transporte = new Entrada_transporte();
        }
        #endregion

        #region Metodos
        protected override void addParameters(int opcion)
        {
            GenericDataAccess.AddInParameter(this.comm, "?P_opcion", DbType.Int32, opcion);
            GenericDataAccess.AddInOutParameter(this.comm, "?P_id", DbType.Int32, this._oEntrada_transporte.Id);
            GenericDataAccess.AddInParameter(this.comm, "?P_id_entrada", DbType.Int32, this._oEntrada_transporte.Id_entrada);
            GenericDataAccess.AddInParameter(this.comm, "?P_transporte_linea", DbType.String, this._oEntrada_transporte.Transporte_linea);
            GenericDataAccess.AddInParameter(this.comm, "?P_id_transporte_tipo", DbType.Int32, this._oEntrada_transporte.Id_transporte_tipo);
            GenericDataAccess.AddInParameter(this.comm, "?P_placa", DbType.String, this._oEntrada_transporte.Placa);
            GenericDataAccess.AddInParameter(this.comm, "?P_caja", DbType.String, this._oEntrada_transporte.Caja);
            GenericDataAccess.AddInParameter(this.comm, "?P_caja1", DbType.String, this._oEntrada_transporte.Caja1);
            GenericDataAccess.AddInParameter(this.comm, "?P_caja2", DbType.String, this._oEntrada_transporte.Caja2);
        }

        public override void fillLst()
        {
            try
            {
                this.comm = GenericDataAccess.CreateCommandSP("sp_Entrada_transporte");
                addParameters(0);
                this.dt = GenericDataAccess.ExecuteSelectCommand(comm);
                this._lst = new List<Entrada_transporte>();
                foreach (DataRow dr in dt.Rows)
                {
                    Entrada_transporte o = new Entrada_transporte();
                    int.TryParse(dr["id"].ToString(), out entero);
                    o.Id = entero;
                    entero = 0;
                    if (dr["id_entrada"] != DBNull.Value)
                    {
                        int.TryParse(dr["id_entrada"].ToString(), out entero);
                        o.Id_entrada = entero;
                        entero = 0;
                    }
                    o.Transporte_linea = dr["transporte_linea"].ToString();
                    if (dr["id_transporte_tipo"] != DBNull.Value)
                    {
                        int.TryParse(dr["id_transporte_tipo"].ToString(), out entero);
                        o.Id_transporte_tipo = entero;
                        entero = 0;
                    }
                    o.Placa = dr["placa"].ToString();
                    o.Caja = dr["caja"].ToString();
                    o.Caja1 = dr["caja1"].ToString();
                    o.Caja2 = dr["caja2"].ToString();
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
                this.comm = GenericDataAccess.CreateCommandSP("sp_Entrada_transporte");
                addParameters(1);
                this.dt = GenericDataAccess.ExecuteSelectCommand(comm);
                if (dt.Rows.Count == 1)
                {
                    DataRow dr = dt.Rows[0];
                    if (dr["id_entrada"] != DBNull.Value)
                    {
                        int.TryParse(dr["id_entrada"].ToString(), out entero);
                        this._oEntrada_transporte.Id_entrada = entero;
                        entero = 0;
                    }
                    this._oEntrada_transporte.Transporte_linea = dr["transporte_linea"].ToString();
                    if (dr["id_transporte_tipo"] != DBNull.Value)
                    {
                        int.TryParse(dr["id_transporte_tipo"].ToString(), out entero);
                        this._oEntrada_transporte.Id_transporte_tipo = entero;
                        entero = 0;
                    }
                    this._oEntrada_transporte.Placa = dr["placa"].ToString();
                    this._oEntrada_transporte.Caja = dr["caja"].ToString();
                    this._oEntrada_transporte.Caja1 = dr["caja1"].ToString();
                    this._oEntrada_transporte.Caja2 = dr["caja2"].ToString();
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
            throw new NotImplementedException();
        }

        public void add(IDbTransaction trans)
        {
            try
            {
                this.comm = GenericDataAccess.CreateCommandSP("sp_Entrada_transporte");
                addParameters(2);
                GenericDataAccess.ExecuteNonQuery(this.comm, trans);
                this._oEntrada_transporte.Id = Convert.ToInt32(GenericDataAccess.getParameterValue(comm, "?P_id"));
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
                this.comm = GenericDataAccess.CreateCommandSP("sp_Entrada_transporte");
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
                this.comm = GenericDataAccess.CreateCommandSP("sp_Entrada_transporte");
                addParameters(4);
                GenericDataAccess.ExecuteNonQuery(this.comm);
            }
            catch
            {
                throw;
            }
        }

        public void selByIdEntrada()
        {
            try
            {
                this.comm = GenericDataAccess.CreateCommandSP("sp_Entrada_transporte");
                addParameters(5);
                this.dt = GenericDataAccess.ExecuteSelectCommand(comm);
                this._lst = new List<Entrada_transporte>();
                foreach (DataRow dr in dt.Rows)
                {
                    Entrada_transporte o = new Entrada_transporte();
                    int.TryParse(dr["id"].ToString(), out entero);
                    o.Id = entero;
                    entero = 0;
                    if (dr["id_entrada"] != DBNull.Value)
                    {
                        int.TryParse(dr["id_entrada"].ToString(), out entero);
                        o.Id_entrada = entero;
                        entero = 0;
                    }
                    o.Transporte_linea = dr["transporte_linea"].ToString();
                    if (dr["id_transporte_tipo"] != DBNull.Value)
                    {
                        int.TryParse(dr["id_transporte_tipo"].ToString(), out entero);
                        o.Id_transporte_tipo = entero;
                        entero = 0;
                    }
                    o.Placa = dr["placa"].ToString();
                    o.Caja = dr["caja"].ToString();
                    o.Caja1 = dr["caja1"].ToString();
                    o.Caja2 = dr["caja2"].ToString();
                    o.Transporte_tipo = dr["nombre"].ToString();
                    this._lst.Add(o);
                }
            }
            catch
            {
                throw;
            }
        }

        internal void dltByIdEntrada(IDbTransaction trans)
        {
            try
            {
                this.comm = GenericDataAccess.CreateCommandSP("sp_Entrada_transporte");
                addParameters(6);
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
