using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Model;
using System.Data;

namespace ModelCasc.operation
{
    internal class Salida_documentoMng : dbTable
    {
        #region Campos
        protected Salida_documento _oSalida_documento;
        protected List<Salida_documento> _lst;
        #endregion

        #region Propiedades
        public Salida_documento O_Salida_documento { get { return _oSalida_documento; } set { _oSalida_documento = value; } }
        public List<Salida_documento> Lst { get { return _lst; } set { _lst = value; } }
        #endregion

        #region Constructores
        public Salida_documentoMng()
        {
            this._oSalida_documento = new Salida_documento();
        }
        #endregion

        #region Metodos
        protected override void addParameters(int opcion)
        {
            GenericDataAccess.AddInParameter(this.comm, "?P_opcion", DbType.Int32, opcion);
            GenericDataAccess.AddInOutParameter(this.comm, "?P_id", DbType.Int32, this._oSalida_documento.Id);
            GenericDataAccess.AddInParameter(this.comm, "?P_id_salida", DbType.Int32, this._oSalida_documento.Id_salida);
            GenericDataAccess.AddInParameter(this.comm, "?P_id_documento", DbType.Int32, this._oSalida_documento.Id_documento);
            GenericDataAccess.AddInParameter(this.comm, "?P_referencia", DbType.String, this._oSalida_documento.Referencia);
        }

        public override void fillLst()
        {
            try
            {
                this.comm = GenericDataAccess.CreateCommandSP("sp_Salida_documento");
                addParameters(0);
                this.dt = GenericDataAccess.ExecuteSelectCommand(comm);
                this._lst = new List<Salida_documento>();
                foreach (DataRow dr in dt.Rows)
                {
                    Salida_documento o = new Salida_documento();
                    int.TryParse(dr["id"].ToString(), out entero);
                    o.Id = entero;
                    entero = 0;
                    if (dr["id_salida"] != DBNull.Value)
                    {
                        int.TryParse(dr["id_salida"].ToString(), out entero);
                        o.Id_salida = entero;
                        entero = 0;
                    }
                    if (dr["id_documento"] != DBNull.Value)
                    {
                        int.TryParse(dr["id_documento"].ToString(), out entero);
                        o.Id_documento = entero;
                        entero = 0;
                    }
                    o.Referencia = dr["referencia"].ToString();
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
                this.comm = GenericDataAccess.CreateCommandSP("sp_Salida_documento");
                addParameters(1);
                this.dt = GenericDataAccess.ExecuteSelectCommand(comm);
                if (dt.Rows.Count == 1)
                {
                    DataRow dr = dt.Rows[0];
                    if (dr["id_salida"] != DBNull.Value)
                    {
                        int.TryParse(dr["id_salida"].ToString(), out entero);
                        this._oSalida_documento.Id_salida = entero;
                        entero = 0;
                    }
                    if (dr["id_documento"] != DBNull.Value)
                    {
                        int.TryParse(dr["id_documento"].ToString(), out entero);
                        this._oSalida_documento.Id_documento = entero;
                        entero = 0;
                    }
                    this._oSalida_documento.Referencia = dr["referencia"].ToString();
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
                this.comm = GenericDataAccess.CreateCommandSP("sp_Salida_documento");
                addParameters(2);
                GenericDataAccess.ExecuteNonQuery(this.comm, trans);
                this._oSalida_documento.Id = Convert.ToInt32(GenericDataAccess.getParameterValue(comm, "?P_id"));
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
                this.comm = GenericDataAccess.CreateCommandSP("sp_Salida_documento");
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
                this.comm = GenericDataAccess.CreateCommandSP("sp_Salida_documento");
                addParameters(4);
                GenericDataAccess.ExecuteNonQuery(this.comm);
            }
            catch
            {
                throw;
            }
        }

        public void SelByIdSalida()
        {
            try
            {
                this.comm = GenericDataAccess.CreateCommandSP("sp_Salida_documento");
                addParameters(5);
                this.dt = GenericDataAccess.ExecuteSelectCommand(comm);
                this._lst = new List<Salida_documento>();
                foreach (DataRow dr in dt.Rows)
                {
                    Salida_documento o = new Salida_documento();
                    int.TryParse(dr["id"].ToString(), out entero);
                    o.Id = entero;
                    entero = 0;
                    if (dr["id_salida"] != DBNull.Value)
                    {
                        int.TryParse(dr["id_salida"].ToString(), out entero);
                        o.Id_salida = entero;
                        entero = 0;
                    }
                    if (dr["id_documento"] != DBNull.Value)
                    {
                        int.TryParse(dr["id_documento"].ToString(), out entero);
                        o.Id_documento = entero;
                        entero = 0;
                    }
                    o.Referencia = dr["referencia"].ToString();
                    this._lst.Add(o);
                }
            }
            catch
            {
                throw;
            }
        }
        #endregion

        internal void dltByIdEntrada(IDbTransaction trans)
        {
            try
            {
                this.comm = GenericDataAccess.CreateCommandSP("sp_Salida_documento");
                addParameters(6);
                GenericDataAccess.ExecuteNonQuery(this.comm, trans);
            }
            catch
            {
                throw;
            }
        }
    }
}
