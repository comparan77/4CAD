using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Model;
using System.Data;

namespace ModelCasc.catalog
{
    public class Cliente_grupoMng: dbTable
    {
        #region Campos
        protected Cliente_grupo _oCliente_grupo;
        protected List<Cliente_grupo> _lst;
        #endregion

        #region Propiedades
        public Cliente_grupo O_Cliente_grupo { get { return _oCliente_grupo; } set { _oCliente_grupo = value; } }
        public List<Cliente_grupo> Lst { get { return _lst; } set { _lst = value; } }
        #endregion

        #region Constructores
        public Cliente_grupoMng()
        {
            this._oCliente_grupo = new Cliente_grupo();
        }
        #endregion

        #region Metodos
        protected override void addParameters(int opcion)
        {
            GenericDataAccess.AddInParameter(this.comm, "?P_opcion", DbType.Int32, opcion);
            GenericDataAccess.AddInOutParameter(this.comm, "?P_id", DbType.Int32, this._oCliente_grupo.Id);
            GenericDataAccess.AddInParameter(this.comm, "?P_nombre", DbType.String, this._oCliente_grupo.Nombre);
        }

        protected void BindByDataRow(DataRow dr, Cliente_grupo o)
        {
            try
            {
                int.TryParse(dr["id"].ToString(), out entero);
                o.Id = entero;
                entero = 0;
                o.Nombre = dr["nombre"].ToString();
                if (dr["isactive"] != DBNull.Value)
                {
                    bool.TryParse(dr["isactive"].ToString(), out logica);
                    o.Isactive = logica;
                    logica = false;
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
                this.comm = GenericDataAccess.CreateCommandSP("sp_Cliente_grupo");
                addParameters(0);
                this.dt = GenericDataAccess.ExecuteSelectCommand(comm);
                this._lst = new List<Cliente_grupo>();
                foreach (DataRow dr in dt.Rows)
                {
                    Cliente_grupo o = new Cliente_grupo();
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
                this.comm = GenericDataAccess.CreateCommandSP("sp_Cliente_grupo");
                addParameters(1);
                this.dt = GenericDataAccess.ExecuteSelectCommand(comm);
                if (dt.Rows.Count == 1)
                {
                    DataRow dr = dt.Rows[0];
                    BindByDataRow(dr, this._oCliente_grupo);
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
                this.comm = GenericDataAccess.CreateCommandSP("sp_Cliente_grupo");
                addParameters(2);
                GenericDataAccess.ExecuteNonQuery(this.comm);
                this._oCliente_grupo.Id = Convert.ToInt32(GenericDataAccess.getParameterValue(comm, "?P_id"));
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
                this.comm = GenericDataAccess.CreateCommandSP("sp_Cliente_grupo");
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
                this.comm = GenericDataAccess.CreateCommandSP("sp_Cliente_grupo");
                addParameters(4);
                GenericDataAccess.ExecuteNonQuery(this.comm);
            }
            catch
            {
                throw;
            }
        }

        #endregion

        public void fillAllLst()
        {
            try
            {
                this.comm = GenericDataAccess.CreateCommandSP("sp_Cliente_grupo");
                addParameters(-1);
                this.dt = GenericDataAccess.ExecuteSelectCommand(comm);
                this._lst = new List<Cliente_grupo>();
                foreach (DataRow dr in dt.Rows)
                {
                    Cliente_grupo o = new Cliente_grupo();
                    BindByDataRow(dr, o);
                    this._lst.Add(o);
                }
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
                this.comm = GenericDataAccess.CreateCommandSP("sp_Cliente_grupo");
                addParameters(-2);
                GenericDataAccess.ExecuteNonQuery(this.comm);
            }
            catch
            {
                throw;
            }
        }

        public void countCatalog()
        {
            try
            {
                this.comm = GenericDataAccess.CreateCommandSP("sp_Cliente_grupo");
                addParameters(5);
                this.dt = GenericDataAccess.ExecuteSelectCommand(comm);
                if (dt.Rows.Count == 1)
                {
                    DataRow dr = dt.Rows[0];
                    if (dr["comprador"] != DBNull.Value)
                    {
                        int.TryParse(dr["comprador"].ToString(), out entero);
                        this._oCliente_grupo.cantComprador = entero;
                        entero = 0;
                    }
                    if (dr["vendor"] != DBNull.Value)
                    {
                        int.TryParse(dr["vendor"].ToString(), out entero);
                        this._oCliente_grupo.cantVendor = entero;
                        entero = 0;
                    }
                    if (dr["mercancia"] != DBNull.Value)
                    {
                        int.TryParse(dr["mercancia"].ToString(), out entero);
                        this._oCliente_grupo.cantMercancia = entero;
                        entero = 0;
                    }
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
    }
}
