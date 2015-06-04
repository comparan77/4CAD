using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Model;
using System.Data;

namespace ModelCasc.operation
{
    internal class Entrada_documentoMng : dbTable
    {
        #region Campos
        protected Entrada_documento _oEntrada_documento;
        protected List<Entrada_documento> _lst;
        #endregion

        #region Propiedades
        public Entrada_documento O_Entrada_documento { get { return _oEntrada_documento; } set { _oEntrada_documento = value; } }
        public List<Entrada_documento> Lst { get { return _lst; } set { _lst = value; } }
        #endregion

        #region Constructores
        public Entrada_documentoMng()
        {
            this._oEntrada_documento = new Entrada_documento();
        }
        #endregion

        #region Metodos
        protected override void addParameters(int opcion)
        {
            GenericDataAccess.AddInParameter(this.comm, "?P_opcion", DbType.Int32, opcion);
            GenericDataAccess.AddInOutParameter(this.comm, "?P_id", DbType.Int32, this._oEntrada_documento.Id);
            GenericDataAccess.AddInParameter(this.comm, "?P_id_entrada", DbType.Int32, this._oEntrada_documento.Id_entrada);
            GenericDataAccess.AddInParameter(this.comm, "?P_id_documento", DbType.Int32, this._oEntrada_documento.Id_documento);
            GenericDataAccess.AddInParameter(this.comm, "?P_referencia", DbType.String, this._oEntrada_documento.Referencia);
        }

        public override void fillLst()
        {
            try
            {
                this.comm = GenericDataAccess.CreateCommandSP("sp_Entrada_documento");
                addParameters(0);
                this.dt = GenericDataAccess.ExecuteSelectCommand(comm);
                this._lst = new List<Entrada_documento>();
                foreach (DataRow dr in dt.Rows)
                {
                    Entrada_documento o = new Entrada_documento();
                    int.TryParse(dr["id"].ToString(), out entero);
                    o.Id = entero;
                    entero = 0;
                    if (dr["id_entrada"] != DBNull.Value)
                    {
                        int.TryParse(dr["id_entrada"].ToString(), out entero);
                        o.Id_entrada = entero;
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
                this.comm = GenericDataAccess.CreateCommandSP("sp_Entrada_documento");
                addParameters(1);
                this.dt = GenericDataAccess.ExecuteSelectCommand(comm);
                if (dt.Rows.Count == 1)
                {
                    DataRow dr = dt.Rows[0];
                    if (dr["id_entrada"] != DBNull.Value)
                    {
                        int.TryParse(dr["id_entrada"].ToString(), out entero);
                        this._oEntrada_documento.Id_entrada = entero;
                        entero = 0;
                    }
                    if (dr["id_documento"] != DBNull.Value)
                    {
                        int.TryParse(dr["id_documento"].ToString(), out entero);
                        this._oEntrada_documento.Id_documento = entero;
                        entero = 0;
                    }
                    this._oEntrada_documento.Referencia = dr["referencia"].ToString();
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
                this.comm = GenericDataAccess.CreateCommandSP("sp_Entrada_documento");
                addParameters(2);
                GenericDataAccess.ExecuteNonQuery(this.comm, trans);
                this._oEntrada_documento.Id = Convert.ToInt32(GenericDataAccess.getParameterValue(comm, "?P_id"));
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
                this.comm = GenericDataAccess.CreateCommandSP("sp_Entrada_documento");
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
                this.comm = GenericDataAccess.CreateCommandSP("sp_Entrada_documento");
                addParameters(4);
                GenericDataAccess.ExecuteNonQuery(this.comm);
            }
            catch
            {
                throw;
            }
        }

        public void SelByIdEntrada()
        {
            try
            {
                this.comm = GenericDataAccess.CreateCommandSP("sp_Entrada_documento");
                addParameters(5);
                this.dt = GenericDataAccess.ExecuteSelectCommand(comm);
                this._lst = new List<Entrada_documento>();
                foreach (DataRow dr in dt.Rows)
                {
                    Entrada_documento o = new Entrada_documento();
                    int.TryParse(dr["id"].ToString(), out entero);
                    o.Id = entero;
                    entero = 0;
                    if (dr["id_entrada"] != DBNull.Value)
                    {
                        int.TryParse(dr["id_entrada"].ToString(), out entero);
                        o.Id_entrada = entero;
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
    }
}
