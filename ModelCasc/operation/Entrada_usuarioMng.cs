using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Model;
using System.Data;

namespace ModelCasc.operation
{
    internal class Entrada_usuarioMng: dbTable
    {
        #region Campos
        protected Entrada_usuario _oEntrada_usuario;
        protected List<Entrada_usuario> _lst;
        #endregion

        #region Propiedades
        public Entrada_usuario O_Entrada_usuario { get { return _oEntrada_usuario; } set { _oEntrada_usuario = value; } }
        public List<Entrada_usuario> Lst { get { return _lst; } set { _lst = value; } }
        #endregion

        #region Constructores
        public Entrada_usuarioMng()
        {
            this._oEntrada_usuario = new Entrada_usuario();
        }
        #endregion

        #region Metodos
        protected override void addParameters(int opcion)
        {
            GenericDataAccess.AddInParameter(this.comm, "?P_opcion", DbType.Int32, opcion);
            GenericDataAccess.AddInOutParameter(this.comm, "?P_id", DbType.Int32, this._oEntrada_usuario.Id);
            GenericDataAccess.AddInParameter(this.comm, "?P_id_entrada", DbType.Int32, this._oEntrada_usuario.Id_entrada);
            GenericDataAccess.AddInParameter(this.comm, "?P_id_usuario", DbType.Int32, this._oEntrada_usuario.Id_usuario);
            GenericDataAccess.AddInParameter(this.comm, "?P_folio", DbType.String, this._oEntrada_usuario.Folio);
        }

        public override void fillLst()
        {
            try
            {
                this.comm = GenericDataAccess.CreateCommandSP("sp_Entrada_usuario");
                addParameters(0);
                this.dt = GenericDataAccess.ExecuteSelectCommand(comm);
                this._lst = new List<Entrada_usuario>();
                foreach (DataRow dr in dt.Rows)
                {
                    Entrada_usuario o = new Entrada_usuario();
                    int.TryParse(dr["id"].ToString(), out entero);
                    o.Id = entero;
                    entero = 0;
                    if (dr["id_entrada"] != DBNull.Value)
                    {
                        int.TryParse(dr["id_entrada"].ToString(), out entero);
                        o.Id_entrada = entero;
                        entero = 0;
                    }
                    if (dr["id_usuario"] != DBNull.Value)
                    {
                        int.TryParse(dr["id_usuario"].ToString(), out entero);
                        o.Id_usuario = entero;
                        entero = 0;
                    }
                    o.Folio = dr["folio"].ToString();
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
                this.comm = GenericDataAccess.CreateCommandSP("sp_Entrada_usuario");
                addParameters(1);
                this.dt = GenericDataAccess.ExecuteSelectCommand(comm);
                if (dt.Rows.Count == 1)
                {
                    DataRow dr = dt.Rows[0];
                    if (dr["id_entrada"] != DBNull.Value)
                    {
                        int.TryParse(dr["id_entrada"].ToString(), out entero);
                        this._oEntrada_usuario.Id_entrada = entero;
                        entero = 0;
                    }
                    if (dr["id_usuario"] != DBNull.Value)
                    {
                        int.TryParse(dr["id_usuario"].ToString(), out entero);
                        this._oEntrada_usuario.Id_usuario = entero;
                        entero = 0;
                    }
                    this._oEntrada_usuario.Folio= dr["folio"].ToString();
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

        public  void add(IDbTransaction trans)
        {
            try
            {
                this.comm = GenericDataAccess.CreateCommandSP("sp_Entrada_usuario");
                addParameters(2);
                GenericDataAccess.ExecuteNonQuery(this.comm, trans);
                this._oEntrada_usuario.Id = Convert.ToInt32(GenericDataAccess.getParameterValue(comm, "?P_id"));
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
                this.comm = GenericDataAccess.CreateCommandSP("sp_Entrada_usuario");
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
                this.comm = GenericDataAccess.CreateCommandSP("sp_Entrada_usuario");
                addParameters(4);
                GenericDataAccess.ExecuteNonQuery(this.comm);
            }
            catch
            {
                throw;
            }
        }

        public void fillLstEntradasHoy()
        {
            try
            {
                this.comm = GenericDataAccess.CreateCommandSP("sp_Entrada_usuario");
                addParameters(5);
                this.dt = GenericDataAccess.ExecuteSelectCommand(comm);
                this._lst = new List<Entrada_usuario>();
                foreach (DataRow dr in dt.Rows)
                {
                    Entrada_usuario o = new Entrada_usuario();
                    int.TryParse(dr["id"].ToString(), out entero);
                    o.Id = entero;
                    entero = 0;
                    if (dr["id_entrada"] != DBNull.Value)
                    {
                        int.TryParse(dr["id_entrada"].ToString(), out entero);
                        o.Id_entrada = entero;
                        entero = 0;
                    }
                    if (dr["id_usuario"] != DBNull.Value)
                    {
                        int.TryParse(dr["id_usuario"].ToString(), out entero);
                        o.Id_usuario = entero;
                        entero = 0;
                    }
                    o.Folio = dr["folio"].ToString();
                    this._lst.Add(o);
                }
            }
            catch
            {
                throw;
            }
        }

        public void selByIdEnt()
        {
            try
            {
                this.comm = GenericDataAccess.CreateCommandSP("sp_Entrada_usuario");
                addParameters(6);
                this.dt = GenericDataAccess.ExecuteSelectCommand(comm);
                if (dt.Rows.Count == 1)
                {
                    DataRow dr = dt.Rows[0];
                    if (dr["id_entrada"] != DBNull.Value)
                    {
                        int.TryParse(dr["id_entrada"].ToString(), out entero);
                        this._oEntrada_usuario.Id_entrada = entero;
                        entero = 0;
                    }
                    if (dr["id_usuario"] != DBNull.Value)
                    {
                        int.TryParse(dr["id_usuario"].ToString(), out entero);
                        this._oEntrada_usuario.Id_usuario = entero;
                        entero = 0;
                    }
                    this._oEntrada_usuario.Folio = dr["folio"].ToString();
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
