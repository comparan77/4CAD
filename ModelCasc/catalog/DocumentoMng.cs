using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Model;
using System.Data;

namespace ModelCasc.catalog
{
    public class DocumentoMng : dbTable
    {
        #region Campos
        protected Documento _oDocumento;
        protected List<Documento> _lst;
        #endregion

        #region Propiedades
        public Documento O_Documento { get { return _oDocumento; } set { _oDocumento = value; } }
        public List<Documento> Lst { get { return _lst; } set { _lst = value; } }
        #endregion

        #region Constructores
        public DocumentoMng()
        {
            this._oDocumento = new Documento();
        }
        #endregion

        #region Metodos
        protected override void addParameters(int opcion)
        {
            GenericDataAccess.AddInParameter(this.comm, "?P_opcion", DbType.Int32, opcion);
            GenericDataAccess.AddInOutParameter(this.comm, "?P_id", DbType.Int32, this._oDocumento.Id);
            GenericDataAccess.AddInParameter(this.comm, "?P_nombre", DbType.String, this._oDocumento.Nombre);
            GenericDataAccess.AddInParameter(this.comm, "?P_mascara", DbType.String, this._oDocumento.Mascara);
        }

        public void fillAllLst()
        {
            try
            {
                this.comm = GenericDataAccess.CreateCommandSP("sp_Documento");
                addParameters(-1);
                this.dt = GenericDataAccess.ExecuteSelectCommand(comm);
                this._lst = new List<Documento>();
                foreach (DataRow dr in dt.Rows)
                {
                    Documento o = new Documento();
                    int.TryParse(dr["id"].ToString(), out entero);
                    o.Id = entero;
                    entero = 0;
                    o.Nombre = dr["nombre"].ToString();
                    o.Mascara = dr["mascara"].ToString();
                    if (dr["IsActive"] != null)
                    {
                        bool.TryParse(dr["IsActive"].ToString(), out logica);
                        o.IsActive = logica;
                    }
                    this._lst.Add(o);
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
                this.comm = GenericDataAccess.CreateCommandSP("sp_Documento");
                addParameters(0);
                this.dt = GenericDataAccess.ExecuteSelectCommand(comm);
                this._lst = new List<Documento>();
                foreach (DataRow dr in dt.Rows)
                {
                    Documento o = new Documento();
                    int.TryParse(dr["id"].ToString(), out entero);
                    o.Id = entero;
                    entero = 0;
                    o.Nombre = dr["nombre"].ToString();
                    o.Mascara = dr["mascara"].ToString();
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
                this.comm = GenericDataAccess.CreateCommandSP("sp_Documento");
                addParameters(1);
                this.dt = GenericDataAccess.ExecuteSelectCommand(comm);
                if (dt.Rows.Count == 1)
                {
                    DataRow dr = dt.Rows[0];
                    this._oDocumento.Nombre = dr["nombre"].ToString();
                    this._oDocumento.Mascara = dr["mascara"].ToString();
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
                this.comm = GenericDataAccess.CreateCommandSP("sp_Documento");
                addParameters(2);
                GenericDataAccess.ExecuteNonQuery(this.comm);
                this._oDocumento.Id = Convert.ToInt32(GenericDataAccess.getParameterValue(comm, "?P_id"));
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
                this.comm = GenericDataAccess.CreateCommandSP("sp_Documento");
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
                this.comm = GenericDataAccess.CreateCommandSP("sp_Documento");
                addParameters(4);
                GenericDataAccess.ExecuteNonQuery(this.comm);
            }
            catch
            {
                throw;
            }
        }

        public void reactive()
        {
            try
            {
                this.comm = GenericDataAccess.CreateCommandSP("sp_Documento");
                addParameters(-2);
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
